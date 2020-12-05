import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { SubCommentService } from 'src/app/services/sub-comment/sub-comment.service';
import { ToastrService } from 'ngx-toastr';
import { mergeMap } from 'rxjs/operators';
import { ISubComment } from 'src/app/models/ISubComment';

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
  @Output() toggleForm = new EventEmitter<boolean>();
  @Output() subCommentsEmitter = new EventEmitter<ISubComment[]>();

  constructor(
    private fb: FormBuilder,
    private subCommentService: SubCommentService,
    private toastrService: ToastrService) {
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

    this.subCommentService.postSubComment(dataToSend).pipe(
      mergeMap(params => this.subCommentService.getAllByRootCommentId(this.rootCommentId)))
      .subscribe(res => {
        this.subComments = res;
        this.subCommentsEmitter.emit(this.subComments);

        this.isReplying = false;
        this.toggleForm.emit(this.isReplying);

        this.toastrService.success('You successfully replied to a comment!');
      })
  }

  closeForm() {
    this.isReplying = false;
    this.toggleForm.emit(this.isReplying);
  }

  get content() {
    return this.formGroup.get('content');
  }
}
