import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UsersService } from 'src/app/services/users/users.service';
import { IUser } from 'src/app/models/IUser';
import { map, mergeMap } from 'rxjs/operators';
import { DatePipe } from '@angular/common';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth/auth.service';
import { UploadService } from 'src/app/services/upload/upload.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  profileForm: FormGroup;
  userId: string;
  user: IUser;
  selectedFile: File;
  selectedFileUrl: string;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private usersService: UsersService,
    private datePipe: DatePipe,
    private fb: FormBuilder,
    private auth: AuthService,
    private uploadService: UploadService,
    private toastrService: ToastrService) {
    this.profileForm = this.fb.group({
      'userName': [''],
      'email': [''],
      'biography': [''],
      'createdOn': [''],
      'displayName': [''],
      'gender': [''],
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
        })
        this.profileForm.controls['userName'].disable();
        this.profileForm.controls['createdOn'].disable();
        this.profileForm.controls['gender'].disable();
      })

  }

  edit(userId: string): void {
    if (this.profileForm.invalid) {
      this.toastrService.warning("Submitted form is invalid!");
      return;
    }
    const userToSend: IUser = {
      biography: this.profileForm.value.biography,
      displayName: this.profileForm.value.displayName,
      email: this.profileForm.value.email,
    }
    if (this.selectedFileUrl) {
      userToSend.profilePictureUrl = this.selectedFileUrl;
    }
    
    this.usersService.editUser(userId, userToSend).subscribe(data => {
      this.fetch();
      console.log(this.profileForm.value)
      if (this.displayName.value.trim()) {
        localStorage.setItem('displayName', this.profileForm.value.displayName);
      }
      else {
        localStorage.setItem('displayName', this.user.userName);
      }

      localStorage.setItem('profilePic', this.selectedFileUrl);
      this.toastrService.success("You successfully modified your profile!")

    });
  }

  delete(userId: string): void {
    this.usersService.deleteUser(userId).subscribe();
    this.auth.logout();
    this.router.navigate(['register']);
    this.toastrService.success("You deleted your account succesfully.")
  }

  resetPhoto() {
    this.usersService.resetPhoto(this.userId).pipe(
      mergeMap(params => this.usersService.getUser(this.userId)))
      .subscribe(res => {
        console.log(res);
        this.user = res;
        localStorage.setItem('profilePic', res.profilePictureUrl);
      })

    this.toastrService.success("You managed to change your photo.");
  }

  onFileSelected($event) {
    this.selectedFile = $event.target.files[0];
    const fd = new FormData();
    fd.append('file', this.selectedFile, this.selectedFile.name);
    fd.append('upload_preset', 'kfrkwxy7');
    fd.append('cloud_name', 'dn2ouybbf');

    this.uploadService.uploadImage(fd).subscribe(res => {
      this.selectedFileUrl = res['secure_url'];
      console.log(res);
    });
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
