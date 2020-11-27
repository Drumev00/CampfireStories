import { Component, OnInit, SecurityContext } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray, FormControl } from '@angular/forms';
import { CategoryService } from 'src/app/services/admin/category.service';
import { ICategory } from 'src/app/models/ICategory';
import { DomSanitizer } from '@angular/platform-browser';
import { UploadService } from 'src/app/services/upload/upload.service';
import { StoryService } from 'src/app/services/story/story.service';
import { IStory } from 'src/app/models/IStory';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-create-story',
  templateUrl: './create-story.component.html',
  styleUrls: ['./create-story.component.css']
})
export class CreateStoryComponent implements OnInit {
  formGroup: FormGroup;
  categoriesFromApi: ICategory[];
  selectedIds: string[];
  selectedFile: File;
  pictureUrl: string;
  upload: boolean = false;

  constructor(
    private fb: FormBuilder,
    private categoryService: CategoryService,
    private sanitizer: DomSanitizer,
    private uploadService: UploadService,
    private storyService: StoryService,
    private router: Router,
    private toastrService: ToastrService) {

    this.getCategories();
    
   }

  ngOnInit(): void {
    this.formGroup = this.fb.group({
      'title': ['', [Validators.required, Validators.minLength(1), Validators.maxLength(80)]],
      'content': ['Your story here...', [Validators.required, Validators.minLength(500)]],
      'selectedIds': new FormArray([])
    })
  }



  getCategories() {
    this.categoryService.getAll().subscribe(data => {
      this.categoriesFromApi = data;
      this.selectedIds = data.map((v) => v.categoryId);
      this.addCheckboxes();
    })

  }

  uploadPhoto($event) {
    this.selectedFile = $event.target.files[0];
    const fd = new FormData();
    fd.append('file', this.selectedFile, this.selectedFile.name);
    fd.append('upload_preset', 'kfrkwxy7');
    fd.append('cloud_name', 'dn2ouybbf');

    this.uploadService.uploadImage(fd).subscribe(res =>{
      this.pictureUrl = res['secure_url'];
      this.upload = true;
  } )};

  postStory() {
    this.sanitizer.sanitize(SecurityContext.HTML, this.content);
    const selectedCategoryIds = this.formGroup.value.selectedIds
    .map((checked, i) => checked ? this.selectedIds[i] : null)
    .filter(v => v !== null);

    if (!this.upload) {
      this.pictureUrl = 'https://res.cloudinary.com/dn2ouybbf/image/upload/v1606504431/k1kbqsaqeiycbm67cts8.webp';
    }    
    const storyToSend: IStory = {
      title: this.formGroup.value.title,
      content: this.formGroup.value.content,
      categories: selectedCategoryIds,
      pictureUrl: this.pictureUrl,
    }

    this.storyService.createStory(storyToSend).subscribe(res =>{
      console.log(res);
      this.router.navigate(['/']);
      this.toastrService.success("You send us a story successfully!");
    })
  }

  private addCheckboxes() {
    const ra = this.categoriesFromApi;
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

  get category() {
    return this.formGroup.get('category');
  }
}
