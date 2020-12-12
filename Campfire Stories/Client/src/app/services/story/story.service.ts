import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { IStory } from 'src/app/models/IStory';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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

  getAll(params): Observable<IStory[]> {
    return this.http.get<IStory[]>(this.storyRoute, { params });
  }

  getById(id: string): Observable<IStory> {
    return this.http.get<IStory>(this.storyRoute + `/${id}`);
  }

  rate(data): Observable<any> {
    return this.http.post(this.storyRoute + '/rate', data);
  }

  alreadyRated(storyId: string) {
    return this.http.get(this.storyRoute + `/rated/${storyId}`);
  }

  getAllById(id: string): Observable<IStory[]> {
    return this.http.get<IStory[]>(this.storyRoute + `/myStories/${id}`);
  }

  edit(id: string, data) {
    return this.http.put(this.storyRoute + `/${id}`, data);
  }

  delete(id: string): Observable<any> {
    return this.http.delete(this.storyRoute + `/${id}`);
  }

  getByUsername(username: string): Observable<IStory[]> {
    return this.http.get<IStory[]>(this.storyRoute + `/foreign/${username}`);
  }
}
