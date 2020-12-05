import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ISubComment } from 'src/app/models/ISubComment';
import { SubCommentService } from 'src/app/services/sub-comment/sub-comment.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-list-sub-comments',
  templateUrl: './list-sub-comments.component.html',
  styleUrls: ['./list-sub-comments.component.css']
})
export class ListSubCommentsComponent implements OnInit {
  @Input() subComments: ISubComment[];
  isEditing: boolean;
  subCommentForEdit: ISubComment;
  selectedSubCommentId: string;
  newContent: string;

  constructor(
    private subCommentService: SubCommentService,
    private toastrService: ToastrService) { }

  ngOnInit(): void {
    console.log(this.subComments)
  }

  getSubComment(id: string) {
    this.isEditing = true;

    this.subCommentService.getById(id).subscribe(res => {
      this.subCommentForEdit = res;
      this.selectedSubCommentId = res.id;
    })
  }

  receiveEditing($event) {
    this.isEditing = $event;
  }
  
  editedContent($event) {
    this.newContent = $event;
    this.refreshEditedComments();
  }

  refreshEditedComments() {
    const subCommentsTemp = this.subComments.slice();
    const subComment = subCommentsTemp.filter((val) => val.id === this.selectedSubCommentId)[0];
    subComment.content = this.newContent;
    this.subComments = subCommentsTemp;
  }

  like(id: string) {
    this.subCommentService.like(id).subscribe(res => this.toastrService.success('You successfully liked a subcomment!'));
  }

  dislike(id: string) {
    this.subCommentService.dislike(id).subscribe(res => this.toastrService.success('You successfully disliked a subcomment.'));
  }

  deleteSubComment(id: string) {
    this.subCommentService.delete(id).subscribe(res => {
      const commentsTemp = this.subComments.slice();
      const remainedComments = commentsTemp.filter((val) => val.id !== id);
      this.subComments = remainedComments;

      this.toastrService.success('You deleted a subcomment successfully.')
    });
  }

  get userId() {
    return localStorage.getItem('userId');
  }

  get isAdmin() {
    return localStorage.getItem('isAdmin');
  }
}
