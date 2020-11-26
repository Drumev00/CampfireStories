import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth/auth.service';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  cloudUrl: string;
  constructor(
    private auth: AuthService,
    private router: Router) {
      this.cloudUrl = environment.getBaseUrl;
     }

  ngOnInit(): void {
  }

  get displayName() {
    return localStorage.getItem('displayName')
  }

  get profilePic() {
    return localStorage.getItem('profilePic')
  }

  get userId(): string {
    return localStorage.getItem('userId');
  }

  logout(): void {
    this.auth.logout();
    this.router.navigate(['/login']);
  }

  isLoggedIn(): boolean {
    return this.auth.isAuthenticated();
  }

  isAdmin(): boolean {
    return this.auth.adminCheck();
  }
}
