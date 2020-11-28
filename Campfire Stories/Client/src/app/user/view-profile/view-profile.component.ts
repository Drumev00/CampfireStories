import { Component, OnInit } from '@angular/core';
import { UsersService } from 'src/app/services/users/users.service';
import { ActivatedRoute } from '@angular/router';
import { map, mergeMap } from 'rxjs/operators';
import { IUser } from 'src/app/models/IUser';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-view-profile',
  templateUrl: './view-profile.component.html',
  styleUrls: ['./view-profile.component.css']
})
export class ViewProfileComponent implements OnInit {
  userId: string;
  viewUser: IUser;

  constructor(
    private usersServce: UsersService,
    private route: ActivatedRoute,
    private datePipe: DatePipe) { }

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.route.params.pipe(map(params => {
      this.userId = params['id'];
      return this.userId;
    }),
    mergeMap(id => this.usersServce.getUser(id))).subscribe(res => {
      this.viewUser = res;
      this.viewUser.createdOn = this.datePipe.transform(res.createdOn, 'dd/MM/yyyy');
      console.log(res);
    })
  }
}
