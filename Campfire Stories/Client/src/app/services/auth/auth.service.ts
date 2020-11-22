import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';


import { environment } from '../../../environments/environment'

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private registerPath = environment.apiURL + 'identity/register';
  private loginPath = environment.apiURL + 'identity/login';

  constructor(private http: HttpClient) {  }

  register(data): Observable<any> {
    return this.http.post(this.registerPath, data);
  }

  login(data): Observable<any> {
    return this.http.post(this.loginPath, data);
  }

  getToken(): string {
    return localStorage.getItem('token');
  }

  getUsername(): string {
    return localStorage.getItem('username');
  }

  setUserInfo(token: string, id: string, username: string, isAdmin: boolean): void {
    localStorage.setItem('token', token);
    localStorage.setItem('userId', id);
    localStorage.setItem('username', username);
    localStorage.setItem('isAdmin', isAdmin.toString())
  }

  getIsAdmin(){
    return localStorage.getItem('isAdmin');
  }

  adminCheck(): boolean {
    var isAdmin = this.getIsAdmin();
    if (isAdmin == 'true') {
      return true;
    }

    return false;
  }

  isAuthenticated(): boolean {
    if (this.getToken()) {
      return true;
    }
    return false;
  }

  logout(){
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    localStorage.removeItem('username');
    localStorage.removeItem('isAdmin');
  }
}
