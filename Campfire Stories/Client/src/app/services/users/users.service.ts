import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { IUser } from 'src/app/models/IUser';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  userRoute: string = environment.apiURL + 'user';
  constructor(private http: HttpClient) { }

  getUser(id: string): Observable<IUser> {
    return this.http.get<IUser>(this.userRoute + `/${id}`);
  }

  editUser(id: string, data: IUser): Observable<IUser> {
    return this.http.put<IUser>(this.userRoute + `/${id}`, data);
  }

  deleteUser(id: string): Observable<any> {
    return this.http.delete(this.userRoute + `/${id}`);
  }

  resetPhoto(id: string): Observable<any> {
    return this.http.get(this.userRoute + '/reset');
  }
}
