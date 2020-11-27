import { Component, OnInit } from '@angular/core';

import { AuthService } from 'src/app/services/auth/auth.service';
import {  FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  constructor(
    private auth: AuthService,
    private fb: FormBuilder,
    private router: Router,
    private toastrService: ToastrService) {
      this.registerForm = this.fb.group({
        'username': ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
        'email': ['', [Validators.required]],
        'password': ['', [Validators.required, Validators.minLength(6)]],
        'displayName': ['', [Validators.minLength(2), Validators.maxLength(50)]],
        'gender': ['Male', [Validators.required]]
      })
     }

  ngOnInit(): void {
  }

  register() {
    this.auth.register(this.registerForm.value).subscribe(data => {
      this.router.navigate(['/login'])
      this.toastrService.success("You registered successfully!");
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
