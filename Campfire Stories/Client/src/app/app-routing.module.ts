import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { RegisterComponent } from './user/register/register.component';
import { LoginComponent } from './user/login/login.component';
import { CreateCategoryComponent } from './admin/create-category/create-category.component';
import { AdminGuardService } from './services/auth/admin-guard.service';
import { ListCategoriesComponent } from './admin/list-categories/list-categories.component';
import { ProfileComponent } from './user/profile/profile.component';
import { AuthGuardService } from './services/auth/auth-guard.service';
import { EditCategoryComponent } from './admin/edit-category/edit-category.component';
import { CreateStoryComponent } from './core/story/create-story/create-story.component';

const routes: Routes = [
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'user/profile/:id', component: ProfileComponent, canActivate: [AuthGuardService]},
  { path: 'create/category', component: CreateCategoryComponent, canActivate: [AdminGuardService] },
  { path: 'list/category', component: ListCategoriesComponent, canActivate: [AdminGuardService] },
  { path: 'edit/category/:id', component: EditCategoryComponent, canActivate: [AdminGuardService]},
  { path: 'create/story', component: CreateStoryComponent, canActivate: [AuthGuardService]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
