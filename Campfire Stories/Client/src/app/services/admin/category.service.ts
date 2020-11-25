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
  categoryRoute: string = environment.apiURL + 'category'
  constructor(private http : HttpClient, private auth: AuthService) { }

 createCategory(data): Observable<any> {
   return this.http.post(this.categoryRoute, data, {responseType: 'text'});
 }

 getAll(): Observable<ICategory[]> {
   return this.http.get<ICategory[]>(this.categoryRoute);
 }

 delete(id): Observable<any> {
   return this.http.delete(this.categoryRoute + `/${id}`,);
 }

 getDetails(id): Observable<ICategory> {
   return this.http.get<ICategory>(this.categoryRoute + `/${id}`)
 }

 editCategory(id: string, data: ICategory): Observable<any> {
   return this.http.put(this.categoryRoute + `/${id}`, data);
 }

}
