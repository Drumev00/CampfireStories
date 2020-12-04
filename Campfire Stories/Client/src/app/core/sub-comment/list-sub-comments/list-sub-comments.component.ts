import { Component, OnInit, Input } from '@angular/core';
import { IComment } from 'src/app/models/IComment';
import { ISubComment } from 'src/app/models/ISubComment';

@Component({
  selector: 'app-list-sub-comments',
  templateUrl: './list-sub-comments.component.html',
  styleUrls: ['./list-sub-comments.component.css']
})
export class ListSubCommentsComponent implements OnInit {
  @Input() subComments: ISubComment[];

  constructor() { }

  ngOnInit(): void {
    console.log(this.subComments)
  }

  get userId() {
    return localStorage.getItem('userId');
  }
}
