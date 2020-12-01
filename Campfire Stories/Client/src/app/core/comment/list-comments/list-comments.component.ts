import { Component, OnInit, Input } from '@angular/core';
import { IComment } from 'src/app/models/IComment';

@Component({
  selector: 'app-list-comments',
  templateUrl: './list-comments.component.html',
  styleUrls: ['./list-comments.component.css']
})
export class ListCommentsComponent implements OnInit {
  @Input() storyId: string;
  @Input() comments: IComment[];

  constructor() { }

  ngOnInit(): void {
  }

}
