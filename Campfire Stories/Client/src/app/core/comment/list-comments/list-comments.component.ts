import { Component, OnInit, Input } from '@angular/core';
import { IComment } from 'src/app/models/IComment';
import { CommentService } from 'src/app/services/comment/comment.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-list-comments',
  templateUrl: './list-comments.component.html',
  styleUrls: ['./list-comments.component.css']
})
export class ListCommentsComponent implements OnInit {
  @Input() storyId: string;
  @Input() comments: IComment[];
  commentForEdit: IComment;
  isEditing: boolean;
  selectedId: string;
  newContent: string;

  constructor(private commentService: CommentService, private toastrService: ToastrService) { }

  ngOnInit(): void {
  }

  like(id: string) {
    this.commentService.like(id).subscribe(res => this.toastrService.success("You successfully liked a comment"))
  }

  dislike(id: string) {
    this.commentService.dislike(id).subscribe(res => this.toastrService.success("You successfully disliked a comment"))
  }

  emitComment(id: string) {
    this.isEditing = true;

    this.commentService.getById(id).subscribe(res => {
      this.commentForEdit = res;
      this.selectedId = res.id;
    })
  }

  editedContent($event) {
    this.newContent = $event;
    this.selectEditedComment();
  }

  receiveEditing($event) {
    this.isEditing = $event;
  }

  selectEditedComment() {
    const commentsTemp = this.comments.slice();
    const comment = commentsTemp.filter((val) => val.id === this.selectedId)[0];
    comment.content = this.newContent;
    this.comments = commentsTemp;
    console.log(commentsTemp);
  }

  get userId() {
    return localStorage.getItem('userId');
  }
}
