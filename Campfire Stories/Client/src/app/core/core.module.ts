import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateStoryComponent } from './story/create-story/create-story.component';
import { EditorModule } from '@tinymce/tinymce-angular';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ListStoryComponent } from './story/list-story/list-story.component';
import { DetailsStoryComponent } from './story/details-story/details-story.component';
import { RatingComponent } from './story/rating/rating.component';
import { NgbRatingModule } from '@ng-bootstrap/ng-bootstrap';
import { EditStoryComponent } from './story/edit-story/edit-story.component';
import { MyStoriesComponent } from './story/my-stories/my-stories.component';
import { ForeignStoriesComponent } from './story/foreign-stories/foreign-stories.component';
import { CreateCommentComponent } from './comment/create-comment/create-comment.component';
import { ListCommentsComponent } from './comment/list-comments/list-comments.component';
import { EditCommentComponent } from './comment/edit-comment/edit-comment.component';
import { CreateSubCommentComponent } from './sub-comment/create-sub-comment/create-sub-comment.component';
import { ListSubCommentsComponent } from './sub-comment/list-sub-comments/list-sub-comments.component';



@NgModule({
  declarations: [
    CreateStoryComponent,
    ListStoryComponent,
    DetailsStoryComponent,
    RatingComponent,
    EditStoryComponent,
    MyStoriesComponent,
    ForeignStoriesComponent,
    CreateCommentComponent,
    ListCommentsComponent,
    EditCommentComponent,
    CreateSubCommentComponent,
    ListSubCommentsComponent,
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
