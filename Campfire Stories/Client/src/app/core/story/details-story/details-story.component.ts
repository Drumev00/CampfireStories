import { Component, OnInit } from '@angular/core';
import { StoryService } from 'src/app/services/story/story.service';
import { ActivatedRoute } from '@angular/router';
import { map, mergeMap } from 'rxjs/operators';
import { IStory } from 'src/app/models/IStory';
import { DatePipe } from '@angular/common';
import { CommentService } from 'src/app/services/comment/comment.service';
import { ToastrService } from 'ngx-toastr';
import { IComment } from 'src/app/models/IComment';

@Component({
  selector: 'app-details-story',
  templateUrl: './details-story.component.html',
  styleUrls: ['./details-story.component.css']
})
export class DetailsStoryComponent implements OnInit {
  storyId: string;
  story: IStory;
  categories: string;
  isRated: boolean;
  

  constructor(
    private storyService: StoryService,
    private route: ActivatedRoute,
    private datePipe: DatePipe,
    private commentService: CommentService,
    private toastrService: ToastrService) { }

  ngOnInit(): void {
    this.fetch();
  }

  fetch() {
    this.route.params.pipe(map(params => {
      this.storyId = params['id'];
      return this.storyId;
    }),
      mergeMap(id => this.storyService.getById(id))).subscribe(res => {
        this.story = res;
        this.categories = res['result'].categories.join(' / ');
        this.story.createdOn = this.datePipe.transform(res['result'].createdOn);

        for (let i = 0; i < this.story['result'].comments.length; i++) {
          const date = this.story['result'].comments[i].createdOn;
          this.story['result'].comments[i].createdOn = this.datePipe.transform(date);
        }
        
        console.log(this.story)
      })
  }

  rate(rating: number) {
    const dataToSend = {
      storyId: this.storyId,
      rating: rating,
    };
    this.storyService.rate(dataToSend).subscribe(res => {
      console.log(res);
      if (this.storyId === res.storyId && this.userId === res.userId) {
        this.isRated = true;
      }
      this.story['result'].rating = res.rating;
      this.story['result'].votes = res.votes;
      this.toastrService.success('You succesfully rated a story!');
    })
  }

  comment(content: string) {
    const commentToSend = {
      storyId: this.storyId,
      content: content,
    };

    this.commentService.postComment(commentToSend).subscribe(res => {
      this.toastrService.success('You commented successfully!');
      this.fetch();
      console.log(res)
    })
  }

  get userId() {
    return localStorage.getItem('userId');
  }

  get rating() {
    return this.story['result'].rating;
  }

  get votes() {
    return this.story['result'].votes;
  }
}
