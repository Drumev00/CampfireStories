import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  createRoute: string = environment.apiURL + 'category'
  constructor(private http : HttpClient) { }

 createCategory(data): Observable<any> {
   return this.http.post(this.createRoute, data, {responseType: 'text'});
 }

}
