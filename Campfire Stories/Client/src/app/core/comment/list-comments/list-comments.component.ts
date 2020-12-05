import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IComment } from 'src/app/models/IComment';
import { CommentService } from 'src/app/services/comment/comment.service';
import { ToastrService } from 'ngx-toastr';
import { ISubComment } from 'src/app/models/ISubComment';

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

  replying: boolean;
  rootCommentId: string;


  constructor(private commentService: CommentService, private toastrService: ToastrService) { }

  ngOnInit(): void {
  }

  like(id: string) {
    this.commentService.like(id).subscribe(res => this.toastrService.success("You successfully liked a comment!"))
  }

  dislike(id: string) {
    this.commentService.dislike(id).subscribe(res => this.toastrService.success("You successfully disliked a comment!"))
  }

  getComment(id: string) {
    this.isEditing = true;

    this.commentService.getById(id).subscribe(res => {
      this.commentForEdit = res;
      this.selectedId = res.id;
    })
  }

  editedContent($event) {
    this.newContent = $event;
    this.refreshEditedComments();
  }

  receiveEditing($event) {
    this.isEditing = $event;
  }

  refreshEditedComments() {
    const commentsTemp = this.comments.slice();
    const comment = commentsTemp.filter((val) => val.id === this.selectedId)[0];
    comment.content = this.newContent;
    this.comments = commentsTemp;
  }

  deleteComment(id: string) {
    this.commentService.delete(id).subscribe(res => {
      const commentsTemp = this.comments.slice();
      const remainedComments = commentsTemp.filter((val) => val.id !== id);
      this.comments = remainedComments;

      this.toastrService.success("You successfully deleted a comment.");
    });
  }



  reply(id: string) {
    this.replying = true;

    this.commentService.getById(id).subscribe(res => {
      this.rootCommentId = res.id;
    })
  }

  receiveReplying($event) {
    this.replying = $event;
  }

  receiveRefreshedSubComments($event) {
    this.comments = $event;
    this.toastrService.success('You successfully replied to a comment!');
  }

  get userId() {
    return localStorage.getItem('userId');
  }

  get isAdmin() {
    return localStorage.getItem('isAdmin');
  }
}
