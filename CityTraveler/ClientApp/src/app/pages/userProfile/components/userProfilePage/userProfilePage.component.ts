import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IUserProfile } from 'src/app/models/user.model';
import { UserManagementService } from 'src/app/services/userManagementService';

@Component({
  selector: 'user-profile',
  templateUrl: './userProfilePage.component.html',
  styleUrls: ['./userProfilePage.component.css']
})
export class UserProfilePageComponent implements OnInit {
  public userInfo: IUserProfile = {
    userId: "",
    email: "",
    userName: ""
  } as unknown as IUserProfile;

  constructor(private service: UserManagementService) {}

    ngOnInit() {
        this.service.getUserProfile('345B9A15-0886-462C-0CC7-08D9A436A380')
        .subscribe((res: IUserProfile) => {
            this.userInfo = res;
        });
    }
}
