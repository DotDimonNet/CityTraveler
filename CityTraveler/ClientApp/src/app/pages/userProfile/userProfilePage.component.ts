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
        this.service.getUserProfile('b96da308-e012-4696-3933-08d9a422b524')
        .subscribe((res: IUserProfile) => {
            this.userInfo = res;
        });
    }
}
