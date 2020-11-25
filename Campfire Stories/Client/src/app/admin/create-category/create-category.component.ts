import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/services/admin/category.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html',
  styleUrls: ['./create-category.component.css']
})
export class CreateCategoryComponent implements OnInit {
  createCategoryForm: FormGroup;
  constructor(
    private categoryService: CategoryService,
    private fb: FormBuilder,
    private router: Router,
    private auth: AuthService) {
      this.createCategoryForm = this.fb.group({
        'name': ['', [Validators.required, Validators.minLength(3), Validators.maxLength(40)]]
      });
     }

  ngOnInit(): void {
  }

  get name() {
    return this.createCategoryForm.get('name');
  }
  
  create() {
    this.categoryService.createCategory(this.createCategoryForm.value).subscribe(data => {
      this.router.navigate(['/list/category'])
    })
  }
}
