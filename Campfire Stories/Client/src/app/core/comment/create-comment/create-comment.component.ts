import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { IComment } from 'src/app/models/IComment';

@Component({
  selector: 'app-create-comment',
  templateUrl: './create-comment.component.html',
  styleUrls: ['./create-comment.component.css']
})
export class CreateCommentComponent implements OnInit {
  formGroup: FormGroup;
  @Output() contentValue = new EventEmitter<string>();
  @Input() storyId: string;
  @Input() comments: IComment[];

  constructor(
    private fb: FormBuilder,
    private toastrService: ToastrService,) {
      this.formGroup = this.fb.group({
        'content': ['', [Validators.required, Validators.maxLength(500)]]
      })
     }

  ngOnInit(): void {
  }

  getValue() {
    if (this.formGroup.invalid) {
      this.toastrService.error('You don\'t populate the fileds correctly.');
      return;
    }
    this.contentValue.emit(this.formGroup.value.content);
  }


  get content() {
    return this.formGroup.get('content');
  }
}
