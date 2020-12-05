import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ISubComment } from 'src/app/models/ISubComment';
import { SubCommentService } from 'src/app/services/sub-comment/sub-comment.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit-sub-comment',
  templateUrl: './edit-sub-comment.component.html',
  styleUrls: ['./edit-sub-comment.component.css']
})
export class EditSubCommentComponent implements OnInit {
  editForm: FormGroup;
  @Input() subCommentForEdit: ISubComment;
  @Input() editing: boolean;
  @Output() editingEmitter = new EventEmitter<boolean>();
  @Output() editedContentEmitter = new EventEmitter<string>();

  constructor(
    private fb: FormBuilder,
    private subCommentService: SubCommentService,
    private toastrService: ToastrService) {
    this.editForm = this.fb.group({
      'content': [''],
    })
   }

  ngOnInit(): void {
    this.editForm = this.fb.group({
      'content': [this.subCommentForEdit.content, [Validators.required, Validators.maxLength(500)]],
    })
  }

  edit(id: string) {
    if (this.editForm.invalid) {
      this.toastrService.error('Content field is populated incorrectly.');
      return;
    }
    this.subCommentService.editSubComment(id, this.editForm.value).subscribe(res => {
      this.editing = false;
      this.editingEmitter.emit(this.editing);
      this.editedContentEmitter.emit(res)
      
      this.toastrService.success('You successfully edited a subcomment!');

    })
  }

  closeForm() {
    this.editing = false;
    this.editingEmitter.emit(this.editing);
  }

  get content() {
    return this.editForm.get('content');
  }
}
