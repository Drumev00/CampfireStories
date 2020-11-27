import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/services/admin/category.service';
import { ICategory } from 'src/app/models/ICategory';
import { ActivatedRoute, Router } from '@angular/router';
import { map, mergeMap } from 'rxjs/operators';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit {
  editForm: FormGroup;
  categoryId: string;
  category: ICategory;

  constructor(
    private categoryService: CategoryService,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private router: Router,
    private toastrService: ToastrService) {
      this.editForm = this.fb.group({
        'newName': [''],
      });
     }

  ngOnInit(): void {
    this.route.params.pipe(map(params => {
      this.categoryId = params['id'];
      return this.categoryId;
    }),
    mergeMap(id => this.categoryService.getDetails(id))).subscribe(data => {
      this.category = data;
      this.editForm = this.fb.group({
        'newName': [this.category.name, [Validators.required, Validators.minLength(3), Validators.maxLength(40)]]
      })
    })
  }

  edit(categoryId: string) {
    this.categoryService.editCategory(categoryId, this.editForm.value).subscribe(data => {
      console.log(data)
      this.router.navigate(['list', 'category']);
      this.toastrService.success("You modified your category successfully!");
    });
  }

  get newName() {
    return this.editForm.get('newName');
  }
}
