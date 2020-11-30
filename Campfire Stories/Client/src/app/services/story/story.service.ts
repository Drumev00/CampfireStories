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

  getById(id: string): Observable<IStory> {
    return this.http.get<IStory>(this.storyRoute + `/${id}`);
  }

  rate(data): Observable<any> {
    return this.http.post(this.storyRoute + '/rate', data);
  }

  getAllById(id: string): Observable<IStory[]> {
    return this.http.get<IStory[]>(this.storyRoute + `/myStories/${id}`);
  }
}
