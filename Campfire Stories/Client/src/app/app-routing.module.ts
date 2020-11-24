import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { RegisterComponent } from './user/register/register.component';
import { LoginComponent } from './user/login/login.component';
import { CreateCategoryComponent } from './admin/create-category/create-category.component';
import { AdminGuardService } from './services/auth/admin-guard.service';
import { ListCategoriesComponent } from './admin/list-categories/list-categories.component';
import { ProfileComponent } from './user/profile/profile.component';
import { AuthGuardService } from './services/auth/auth-guard.service';

const routes: Routes = [
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'create/category', component: CreateCategoryComponent, canActivate: [AdminGuardService] },
  { path: 'list/category', component: ListCategoriesComponent, canActivate: [AdminGuardService] },
  { path: 'user/profile/:id', component: ProfileComponent, canActivate: [AuthGuardService]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
