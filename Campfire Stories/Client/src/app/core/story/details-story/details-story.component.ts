import { Component, OnInit } from '@angular/core';
import { StoryService } from 'src/app/services/story/story.service';
import { ActivatedRoute, Router } from '@angular/router';
import { map, mergeMap } from 'rxjs/operators';
import { IStory } from 'src/app/models/IStory';
import { DatePipe } from '@angular/common';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-details-story',
  templateUrl: './details-story.component.html',
  styleUrls: ['./details-story.component.css']
})
export class DetailsStoryComponent implements OnInit {
  storyId: string;
  story: IStory;
  categories: string;

  constructor(
    private storyService: StoryService,
    private route: ActivatedRoute,
    private datePipe: DatePipe,
    private router: Router,) { }

  ngOnInit(): void {
    this.route.params.pipe(map(params => {
      this.storyId = params['id'];
      return this.storyId;
    }),
    mergeMap(id => this.storyService.getById(id))).subscribe(res => {
      this.story = res;
      this.categories = res['result'].categories.join(' / ');
      this.story.createdOn = this.datePipe.transform(res['result'].createdOn);
      console.log(this.story)
    })
  }

  get userId() {
    return localStorage.getItem('userId');
  }
}
