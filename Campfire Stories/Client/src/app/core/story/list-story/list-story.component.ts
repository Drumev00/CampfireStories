import { Component, OnInit } from '@angular/core';
import { StoryService } from 'src/app/services/story/story.service';
import { IStory } from 'src/app/models/IStory';

@Component({
  selector: 'app-list-story',
  templateUrl: './list-story.component.html',
  styleUrls: ['./list-story.component.css']
})
export class ListStoryComponent implements OnInit {
  stories: IStory[];

  constructor(
    private storyService: StoryService,
    ) { }

  ngOnInit(): void {
    this.storyService.getAll().subscribe(res => {
      this.stories = res;
      console.log(this.stories);
    })
  }

  get userId() {
    return localStorage.getItem('userId');
  }
}
