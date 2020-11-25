import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { CreateCategoryComponent } from './create-category/create-category.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ListCategoriesComponent } from './list-categories/list-categories.component';
import { EditCategoryComponent } from './edit-category/edit-category.component';



@NgModule({
  declarations: [
    CreateCategoryComponent,
    ListCategoriesComponent,
    EditCategoryComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    ReactiveFormsModule
  ]
})
export class AdminModule { }
