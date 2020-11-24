import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/services/admin/category.service';
import { ICategory } from 'src/app/models/ICategory';

@Component({
  selector: 'app-list-categories',
  templateUrl: './list-categories.component.html',
  styleUrls: ['./list-categories.component.css']
})
export class ListCategoriesComponent implements OnInit {
  categories: ICategory[];
  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.categoryService.getAll().subscribe(data => {
      this.categories = data;
    })
  }

}
