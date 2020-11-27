import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateStoryComponent } from './story/create-story/create-story.component';
import { EditorComponent, EditorModule } from '@tinymce/tinymce-angular';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    CreateStoryComponent
  ],
  imports: [
    CommonModule,
    EditorModule,
    ReactiveFormsModule,
    RouterModule
  ]
})
export class CoreModule { }
