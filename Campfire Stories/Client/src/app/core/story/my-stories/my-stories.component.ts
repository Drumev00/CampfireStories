import { Component, OnInit } from '@angular/core';
import { StoryService } from 'src/app/services/story/story.service';
import { ActivatedRoute } from '@angular/router';
import { map, mergeMap } from 'rxjs/operators';
import { IStory } from 'src/app/models/IStory';

@Component({
  selector: 'app-my-stories',
  templateUrl: './my-stories.component.html',
  styleUrls: ['./my-stories.component.css']
})
export class MyStoriesComponent implements OnInit {
  userId: string;
  stories: IStory[];

  constructor(
    private storyService: StoryService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.pipe(map(params => {
      this.userId = params['id'];
      return this.userId;
    }),
      mergeMap(id => this.storyService.getAllById(id))).subscribe(res => {
        this.stories = res
        console.log(this.stories);
      })
  }

}
