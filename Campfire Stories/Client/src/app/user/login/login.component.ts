import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth/auth.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  constructor(
    private auth: AuthService,
    private fb: FormBuilder) {
    this.loginForm = this.fb.group({
      'username': ['', [Validators.required]],
      'password': ['', [Validators.required]]
    })
  }

  ngOnInit(): void {
  }

  login() {
    this.auth.login(this.loginForm.value).subscribe(data => {
      console.log(data)
      this.auth.setUserInfo(data.result.token,
                            data.result.userId,
                            data.result.displayName,
                            data.result.isAdmin,
                            data.result.profilePictureUrl)
    })
  }

  get username() {
    return this.loginForm.get('username')
  }

  get password() {
    return this.loginForm.get('password')
  }
}
