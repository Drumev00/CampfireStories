import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { IStory } from 'src/app/models/IStory';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StoryService {
  storyRoute: string = environment.apiURL + "story";

  constructor(private http: HttpClient) { }

  createStory(data: IStory) {
    return this.http.post(this.storyRoute, data);
  }

  getAll(): Observable<IStory[]> {
    return this.http.get<IStory[]>(this.storyRoute);
  }
}
