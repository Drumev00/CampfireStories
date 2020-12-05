import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ISubComment } from 'src/app/models/ISubComment';

@Injectable({
  providedIn: 'root'
})
export class SubCommentService {
  subCommentRoute : string = environment.apiURL + 'subComment';

  constructor(private http: HttpClient) { }

  postSubComment(data) {
    return this.http.post(this.subCommentRoute, data, { responseType: 'text' });
  }

  getAllByRootCommentId(rootCommentId: string): Observable<ISubComment[]>{
    return this.http.get<ISubComment[]>(this.subCommentRoute + `/${rootCommentId}`);
  }

  getById(id: string): Observable<ISubComment>{
    return this.http.get<ISubComment>(this.subCommentRoute + `/getOne/${id}`);
  }

  editSubComment(id: string, data) {
    return this.http.put(this.subCommentRoute + `/${id}`, data, { responseType: 'text' });
  }
}
