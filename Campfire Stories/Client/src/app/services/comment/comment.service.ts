import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { IComment } from 'src/app/models/IComment';


@Injectable({
  providedIn: 'root'
})
export class CommentService {
  commentRoute: string = environment.apiURL + 'comment';

  constructor(private http: HttpClient) { }

  postComment(data) {
    return this.http.post(this.commentRoute, data, { responseType: 'text' });
  }

  getAllByStoryId(id: string): Observable<IComment[]> {
    return this.http.get<IComment[]>(this.commentRoute + `/${id}`);
  }
}
