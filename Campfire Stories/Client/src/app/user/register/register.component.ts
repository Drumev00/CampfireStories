import { Component, OnInit } from '@angular/core';

import { AuthService } from 'src/app/services/auth/auth.service';
import {  FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  constructor(
    private auth: AuthService,
    private fb: FormBuilder) {
      this.registerForm = this.fb.group({
        'username': ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
        'email': ['', [Validators.required]],
        'password': ['', [Validators.required, Validators.minLength(6)]],
        'displayName': ['', [Validators.minLength(2), Validators.maxLength(50)]],
        'gender': ['', [Validators.required]]
      })
     }

  ngOnInit(): void {
  }

  register() {
    this.auth.register(this.registerForm.value).subscribe(data => {
      console.log(data)
    })
  }

  get username() {
    return this.registerForm.get('username');
  }

  get password() {
    return this.registerForm.get('password');
  }

  get email() {
    return this.registerForm.get('email');
  }

  get displayName() {
    return this.registerForm.get('displayName');
  }
}
