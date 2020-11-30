import { Component, OnInit } from '@angular/core';
import { StoryService } from 'src/app/services/story/story.service';
import { ActivatedRoute } from '@angular/router';
import { map, mergeMap } from 'rxjs/operators';
import { IStory } from 'src/app/models/IStory';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-my-stories',
  templateUrl: './my-stories.component.html',
  styleUrls: ['./my-stories.component.css']
})
export class MyStoriesComponent implements OnInit {
  userId: string;
  stories: IStory[];
  storyId: string;

  constructor(
    private storyService: StoryService,
    private route: ActivatedRoute,
    private toastrService: ToastrService) { }

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

  getStoryId(id: string) {
    this.storyId = id
    
    return this.storyId;
  }

  deleteStory(id: string) {
    this.storyService.delete(this.storyId).subscribe(res => {
      console.log(res);
      this.toastrService.success('You deleted a story successfully.');
    });
  }
}
