import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/services/admin/category.service';
import { ICategory } from 'src/app/models/ICategory';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-list-categories',
  templateUrl: './list-categories.component.html',
  styleUrls: ['./list-categories.component.css']
})
export class ListCategoriesComponent implements OnInit {
  categories: ICategory[];
  selectedCategoryId: string;
  constructor(
    private categoryService: CategoryService,
     private router: Router,
     private toastrService: ToastrService) { }

  ngOnInit(): void {
    this.getAll();
  }

  getAll(): void {
    this.categoryService.getAll().subscribe(data => {
      this.categories = data;
    })
  }

  deleteCategory(categoryId): void {
    this.categoryService.delete(categoryId).subscribe(data => {
      this.getAll()
      this.toastrService.warning("A category has been deleted.");
    })
  }

  editCategory(categoryId): void {
    this.router.navigate(['edit', 'category', categoryId]);
  }
  
}
