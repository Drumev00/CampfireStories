import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { environment } from '../../../environments/environment';
import { AuthService } from '../auth/auth.service';
import { ICategory } from 'src/app/models/ICategory';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  createRoute: string = environment.apiURL + 'category'
  listRoute: string = environment.apiURL + 'category';
  constructor(private http : HttpClient, private auth: AuthService) { }

 createCategory(data): Observable<any> {
   return this.http.post(this.createRoute, data, {responseType: 'text'});
 }

 getAll(): Observable<ICategory[]> {
   return this.http.get<ICategory[]>(this.listRoute);
 }

}
