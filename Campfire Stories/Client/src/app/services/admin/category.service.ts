import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { environment } from '../../../environments/environment';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  createRoute: string = environment.apiURL + 'category'
  constructor(private http : HttpClient, private auth: AuthService) { }

 createCategory(data): Observable<any> {
   return this.http.post(this.createRoute, data, {responseType: 'text'});
 }

}
