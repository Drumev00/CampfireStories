import { Component, OnInit, Input } from '@angular/core';
import { IComment } from 'src/app/models/IComment';
import { ISubComment } from 'src/app/models/ISubComment';
import { SubCommentService } from 'src/app/services/sub-comment/sub-comment.service';

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

  constructor(private subCommentService: SubCommentService) { }

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


  get userId() {
    return localStorage.getItem('userId');
  }
}
