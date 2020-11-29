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
import { ListStoryComponent } from './core/story/list-story/list-story.component';
import { ViewProfileComponent } from './user/view-profile/view-profile.component';
import { DetailsStoryComponent } from './core/story/details-story/details-story.component';

const routes: Routes = [
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'user/profile/:id', component: ProfileComponent, canActivate: [AuthGuardService] },
  { path: 'create/category', component: CreateCategoryComponent, canActivate: [AdminGuardService] },
  { path: 'list/category', component: ListCategoriesComponent, canActivate: [AdminGuardService] },
  { path: 'edit/category/:id', component: EditCategoryComponent, canActivate: [AdminGuardService] },
  { path: 'create/story', component: CreateStoryComponent, canActivate: [AuthGuardService] },
  { path: '', component: ListStoryComponent},
  { path: 'user/viewProfile/:id', component: ViewProfileComponent, canActivate: [AuthGuardService] },
  { path: 'story/details/:id', component: DetailsStoryComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
