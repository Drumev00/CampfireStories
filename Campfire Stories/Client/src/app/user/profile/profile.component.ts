import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UsersService } from 'src/app/services/users/users.service';
import { IUser } from 'src/app/models/IUser';
import { map, switchMap, mergeMap } from 'rxjs/operators';
import { DatePipe } from '@angular/common';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth/auth.service';


@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  profileForm: FormGroup;
  userId: string;
  user: IUser;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private usersService: UsersService,
    private datePipe: DatePipe,
    private fb: FormBuilder,
    private auth: AuthService) {
      this.profileForm = this.fb.group({
        'userName': [''],
        'email': [''],
        'biography': [''],
        'createdOn': [''],
        'displayName': [''],
        'gender': [''],
        'profilePictureUrl': [''],
      })
     }

  ngOnInit(): void {
    this.fetch();
  }

  fetch() {
    this.route.params.pipe(map(params => {
      this.userId = params['id']
      return this.userId
    }),
    mergeMap(id => this.usersService.getUser(id))).subscribe(res => {
      this.user = res;
      this.user.createdOn = this.datePipe.transform(res.createdOn, 'dd/MM/yyyy')
      this.profileForm = this.fb.group({
        'userName': [this.user.userName, [Validators.required]],
        'email': [this.user.email, Validators.required],
        'biography': [this.user.biography, [Validators.minLength(20)]],
        'createdOn': [this.user.createdOn, [Validators.required]],
        'displayName': [this.user.displayName, [Validators.minLength(2), Validators.maxLength(50)]],
        'gender': [this.user.gender, [Validators.required]],
        'profilePictureUrl': [this.user.profilePictureUrl],
      })
    })
    this.profileForm.get('userName').disable();
    this.profileForm.get('createdOn').disable();
    this.profileForm.get('gender').disable();
  }

  edit(userId: string): void {
    this.usersService.editUser(userId, this.profileForm.value).subscribe(data => {
      this.fetch();
      localStorage.setItem('displayName', this.profileForm.value.displayName);
    });
  }

  delete(userId: string): void {
    this.usersService.deleteUser(userId).subscribe();
    this.auth.logout();
    this.router.navigate(['register']);
  }

  get biography() {
    return this.profileForm.get('biography');
  }

  get displayName() {
    return this.profileForm.get('displayName');
  }

  get email() {
    return this.profileForm.get('email');
  }
}
