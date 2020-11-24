import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UsersService } from 'src/app/services/users/users.service';
import { IUserProfile } from 'src/app/models/IUserProfile';
import { map, switchMap, mergeMap } from 'rxjs/operators';
import { DatePipe } from '@angular/common';


@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  id: string;
  user: IUserProfile;
  constructor(
    private route: ActivatedRoute,
     private usersService: UsersService,
     private datePipe: DatePipe) { }

  ngOnInit(): void {
    this.route.params.pipe(map(params => {
      this.id = params['id']
      return this.id
    }),
    mergeMap(id => this.usersService.getUser(id))).subscribe(res => {
      console.log(res)
      this.user = res;
      this.user.createdOn = this.datePipe.transform(res.createdOn, 'dd/MM/yyyy')
    })
  }
}
