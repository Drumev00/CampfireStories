import { Component, OnInit } from '@angular/core';
import { StoryService } from 'src/app/services/story/story.service';
import { IStory } from 'src/app/models/IStory';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-list-story',
  templateUrl: './list-story.component.html',
  styleUrls: ['./list-story.component.css']
})
export class ListStoryComponent implements OnInit {
  stories: IStory[];
  currentIndex: number = -1;
  title: string = '';
  searchForm: FormGroup;

  page: number = 1;
  count: number = 0;
  pageSize: number = 12;

  constructor(
    private storyService: StoryService,
    private fb: FormBuilder) {
      this.searchForm = this.fb.group({
        'search': ['']
      })
     }

  ngOnInit(): void {
    this.retrieveStories();
  }

  logg() {
    this.title = this.searchForm.value.search
  }

  getRequestParams(searchTitle, page): any {
    // tslint:disable-next-line:prefer-const
    let params = {};

    if (searchTitle) {
      params[`title`] = searchTitle;
    }

    if (page) {
      params[`page`] = page;
    }

    return params;
  }

  retrieveStories(): void {
    const params = this.getRequestParams(this.title, this.page);

    this.storyService.getAll(params)
      .subscribe(
        response => {
          const totalItems = response['totalItems'];
          this.stories = response['stories'];
          this.count = totalItems;
          this.page = response['currentPage'];
          console.log(response);
        });
  }

  get userId() {
    return localStorage.getItem('userId');
  }
}
