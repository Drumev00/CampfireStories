import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ISubComment } from 'src/app/models/ISubComment';
import { CommentService } from 'src/app/services/comment/comment.service';
import { IComment } from 'src/app/models/IComment';
import { SubCommentService } from 'src/app/services/sub-comment/sub-comment.service';
import { mergeMap } from 'rxjs/operators';

@Component({
  selector: 'app-create-sub-comment',
  templateUrl: './create-sub-comment.component.html',
  styleUrls: ['./create-sub-comment.component.css']
})
export class CreateSubCommentComponent implements OnInit {
  formGroup: FormGroup;
  @Input() isReplying: boolean;
  @Input() rootCommentId: string;
  @Input() subComments: ISubComment[];

  // Refreshing comments after creation
  @Input() storyId: string;
  @Input() comments: IComment[];
  @Output() commentsEmitter = new EventEmitter<object>();

  // Hide the replying form after submission or 'close' button is clicked
  @Output() toggleForm = new EventEmitter<boolean>();

  constructor(
    private fb: FormBuilder,
    private subCommentService: SubCommentService,
    private commentService: CommentService,) {
    this.formGroup = this.fb.group({
      'content': ['', [Validators.required, Validators.maxLength(500)]]
    })
  }

  ngOnInit(): void {
  }

  createSubComment() {
    const dataToSend = {
      rootCommentId: this.rootCommentId,
      content: this.formGroup.value.content,
    };

    this.subCommentService.postSubComment(dataToSend).pipe(mergeMap(params => 
      this.commentService.getAllByStoryId(this.storyId)
    )).subscribe(res => {
      this.isReplying = false;
      this.toggleForm.emit(this.isReplying);
      this.commentsEmitter.emit(res);
    })


    /*this.subCommentService.postSubComment(dataToSend).pipe(
      mergeMap(params => this.subCommentService.getAllByRootCommentId(this.rootCommentId)))
      .subscribe(res => {
        this.subComments = res;
        this.commentsEmitter.emit({
          rootCommentId: this.rootCommentId,
          subComments: this.subComments,
        });

        this.isReplying = false;
        this.toggleForm.emit(this.isReplying);
        this.formGroup = this.fb.group({
          'content': ['', [Validators.required, Validators.maxLength(500)]]
        })
        this.toastrService.success('You successfully replied to a comment!');
      })*/
  }

  closeForm() {
    this.isReplying = false;
    this.toggleForm.emit(this.isReplying);
  }

  get content() {
    return this.formGroup.get('content');
  }
}
