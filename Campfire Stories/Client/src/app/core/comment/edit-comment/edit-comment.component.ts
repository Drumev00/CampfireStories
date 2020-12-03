import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { IComment } from 'src/app/models/IComment';
import { CommentService } from 'src/app/services/comment/comment.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit-comment',
  templateUrl: './edit-comment.component.html',
  styleUrls: ['./edit-comment.component.css']
})
export class EditCommentComponent implements OnInit {
  editForm: FormGroup;
  @Input() commentForEdit: IComment;
  @Input() editing: boolean;
  @Output() editingEmitter = new EventEmitter<boolean>();
  @Output() editedContentEmitter = new EventEmitter<string>();

  constructor(
    private fb: FormBuilder,
    private commentService: CommentService,
    private toastrService: ToastrService) {
    this.editForm = this.fb.group({
      'content': ['']
    })
   }

  ngOnInit(): void {
    console.log(this.commentForEdit)
    this.editForm = this.fb.group({
      'content': [this.commentForEdit.content, [Validators.required, Validators.maxLength(500)]]
    })
  }

  closeForm() {
    this.editing = false;
    this.editingEmitter.emit(this.editing);
  }

  edit(id: string) {
    this.commentService.edit(id, this.editForm.value).subscribe(res => {
      this.editing = false;
      this.editingEmitter.emit(this.editing);
      this.editedContentEmitter.emit(res);
      this.toastrService.success("You successfully edited a comment!");
      console.log(res);
    })
  }

  get content() {
    return this.editForm.get('content');
  }
}
