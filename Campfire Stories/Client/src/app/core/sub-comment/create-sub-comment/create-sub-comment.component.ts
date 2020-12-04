import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { SubCommentService } from 'src/app/services/sub-comment/sub-comment.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-create-sub-comment',
  templateUrl: './create-sub-comment.component.html',
  styleUrls: ['./create-sub-comment.component.css']
})
export class CreateSubCommentComponent implements OnInit {
  formGroup: FormGroup;
  @Input() isReplying: boolean;
  @Input() rootCommentId: string;
  @Output() toggleForm = new EventEmitter<boolean>();

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

    this.subCommentService.postSubComment(dataToSend).subscribe(res => {
      console.log(res);
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
