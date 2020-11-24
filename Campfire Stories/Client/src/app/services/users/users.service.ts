import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { IUserProfile } from 'src/app/models/IUserProfile';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  userRoute: string = environment.apiURL + 'user';
  constructor(private http: HttpClient) { }

  getUser(id): Observable<IUserProfile> {
    return this.http.get<IUserProfile>(this.userRoute + `/${id}`);
  }
}
