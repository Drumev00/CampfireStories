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

  like(id: string): Observable<number> {
    return this.http.get<number>(this.commentRoute + `/like/${id}`);
  }

  dislike(id: string): Observable<number> {
    return this.http.get<number>(this.commentRoute + `/dislike/${id}`);
  }

  getById(commentId: string): Observable<IComment> {
    return this.http.get<IComment>(this.commentRoute + `/getOne/${commentId}`);
  }

  edit(id: string, content) {
    return this.http.put(this.commentRoute + `/${id}`, content, { responseType: 'text'});
  }

  delete(id: string) {
    return this.http.delete(this.commentRoute + `/${id}`);
  }
}
