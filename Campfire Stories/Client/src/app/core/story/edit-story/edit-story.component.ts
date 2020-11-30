import { Component, OnInit, SecurityContext } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray, FormControl } from '@angular/forms';
import { ICategory } from 'src/app/models/ICategory';
import { CategoryService } from 'src/app/services/admin/category.service';
import { ActivatedRoute, Router } from '@angular/router';
import { map, mergeMap } from 'rxjs/operators';
import { StoryService } from 'src/app/services/story/story.service';
import { IStory } from 'src/app/models/IStory';
import { ToastrService } from 'ngx-toastr';
import { DomSanitizer } from '@angular/platform-browser';
import { UploadService } from 'src/app/services/upload/upload.service';

@Component({
  selector: 'app-edit-story',
  templateUrl: './edit-story.component.html',
  styleUrls: ['./edit-story.component.css']
})
export class EditStoryComponent implements OnInit {
  formGroup: FormGroup
  categoriesFromApi: ICategory[];
  selectedIds: string[];
  storyId: string;
  story: IStory;
  selectedFile: File;
  selectedFileUrl: string;
  upload: boolean;

  constructor(
    private fb: FormBuilder,
    private categoryService: CategoryService,
    private route: ActivatedRoute,
    private storyService: StoryService,
    private toastrService: ToastrService,
    private sanitizer: DomSanitizer,
    private uploadService: UploadService,
    private router: Router) {
    this.formGroup = this.fb.group({
      'title': [''],
      'content': [''],
    })

  }

  ngOnInit(): void {
    this.route.params.pipe(map(params => {
      this.storyId = params['id'];
      return this.storyId;
    }),
      mergeMap(id => this.storyService.getById(id))).subscribe(res => {
        this.story = res['result'];

        this.formGroup = this.fb.group({
          'title': [this.story.title, [Validators.required, Validators.minLength(1), Validators.maxLength(80)]],
          'content': [this.story.content, [Validators.required, Validators.minLength(500)]],
          'selectedIds': new FormArray([]),

        })
        this.getCategories();

        console.log(this.story)
      })

  }

  getCategories() {
    this.categoryService.getAll().subscribe(res => {
      this.categoriesFromApi = res;
      this.selectedIds = res.map((value) => value.categoryId);
      this.addCheckboxes();
      console.log(res);
    })
  }

  editStory() {
    if (this.formGroup.invalid) {
      this.toastrService.error('You don\'t populate the fields correctly.');
      return;
    }
    this.sanitizer.sanitize(SecurityContext.HTML, this.content);
    const selectedCategoryIds = this.formGroup.value.selectedIds
      .map((checked, i) => checked ? this.selectedIds[i] : null)
      .filter(v => v !== null);

    if (!this.upload) {
      this.selectedFileUrl = this.story.pictureUrl;
    }

    const dataToSend = {
      title: this.formGroup.value.title,
      content: this.formGroup.value.content,
      pictureUrl: this.selectedFileUrl,
      categories: selectedCategoryIds,
    };

    this.storyService.edit(this.storyId, dataToSend).subscribe(res => {
      console.log(res);
      this.router.navigate(['myStories', this.story.userId]);
      this.toastrService.success('You edited your story successfully!');
    });
  }

  uploadPhoto($event) {
    this.selectedFile = $event.target.files[0];
    const fd = new FormData();
    fd.append('file', this.selectedFile, this.selectedFile.name);
    fd.append('upload_preset', 'kfrkwxy7');
    fd.append('cloud_name', 'dn2ouybbf');

    this.uploadService.uploadImage(fd).subscribe(res => {
      this.selectedFileUrl = res['secure_url'];
      this.upload = true;
    })
  }

  private addCheckboxes() {
    this.categoriesFromApi.forEach(() => this.idsFormArray.push(new FormControl(false)));
  }

  get idsFormArray() {
    return this.formGroup.controls.selectedIds as FormArray;
  }

  get title() {
    return this.formGroup.get('title');
  }

  get content() {
    return this.formGroup.get('content');
  }
}
