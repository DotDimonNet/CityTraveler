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
  } as IUserProfile;

  constructor(private service: UserManagementService) {}

    ngOnInit() {
        this.service.getUserProfile('AFEB2BF5-552C-44D4-3531-08D9A38628E7')
        .subscribe((res: IUserProfile) => {
            this.userInfo = res;
        });
    }
}
