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

  register(data: any): Observable<any> {
    return this.http.post(this.registerPath, data);
  }

  login(data: any): Observable<any> {
    return this.http.post(this.loginPath, data);
  }
}
