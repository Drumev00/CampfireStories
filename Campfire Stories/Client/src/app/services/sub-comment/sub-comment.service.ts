import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SubCommentService {
  subCommentRoute : string = environment.apiURL + 'subComment';

  constructor(private http: HttpClient) { }

  postSubComment(data) {
    return this.http.post(this.subCommentRoute, data, { responseType: 'text' });
  }
}
