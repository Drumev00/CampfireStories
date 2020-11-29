import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateStoryComponent } from './story/create-story/create-story.component';
import { EditorModule } from '@tinymce/tinymce-angular';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ListStoryComponent } from './story/list-story/list-story.component';
import { DetailsStoryComponent } from './story/details-story/details-story.component';
import { RatingComponent } from './story/rating/rating.component';
import { NgbRatingModule, NgbRating } from '@ng-bootstrap/ng-bootstrap';



@NgModule({
  declarations: [
    CreateStoryComponent,
    ListStoryComponent,
    DetailsStoryComponent,
    RatingComponent,
  ],
  imports: [
    CommonModule,
    EditorModule,
    ReactiveFormsModule,
    RouterModule,
    NgbRatingModule

  ]
})
export class CoreModule { }
