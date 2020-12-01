import { Component, OnInit } from '@angular/core';
import { StoryService } from 'src/app/services/story/story.service';
import { ActivatedRoute } from '@angular/router';
import { IStory } from 'src/app/models/IStory';
import { map, mergeMap } from 'rxjs/operators';

@Component({
  selector: 'app-foreign-stories',
  templateUrl: './foreign-stories.component.html',
  styleUrls: ['./foreign-stories.component.css']
})
export class ForeignStoriesComponent implements OnInit {
  username: string;
  stories: IStory[];

  constructor(
    private storyService: StoryService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.pipe(map(params => {
      this.username = params['name'];
      return this.username
    }),
      mergeMap(username => this.storyService.getByUsername(username))).subscribe(res => {
        this.stories = res;
        console.log(res);
      })
  }

}
