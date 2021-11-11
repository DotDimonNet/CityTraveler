import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IUserProfile } from 'src/app/models/user.model';
import { UserManagementService } from 'src/app/services/userManagementService';

@Component({
  selector: 'user-profile',
  templateUrl: './userProfilePage.component.html',
  styleUrls: ['./userProfilePage.component.css']
})
export class UserProfilePageComponent {
  public userInfo: IUserProfile = {
    userId: "",
    email: "",
    userName: ""
  } as unknown as IUserProfile;

  public userId: string;

  constructor(private service: UserManagementService) {}

submit() {
    this.service.getUserProfile(this.userId)
    .subscribe((res: IUserProfile) => {
      this.userInfo = res;
    });
  }
}
