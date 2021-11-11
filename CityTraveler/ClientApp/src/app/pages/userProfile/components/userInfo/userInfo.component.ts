import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IUserProfile, IUserSearchProperties } from 'src/app/models/user.model';
import { UserManagementService } from 'src/app/services/userManagementService';

@Component({
    selector: 'user-info',
    templateUrl: './userInfo.component.html'
})
export class UserInfoComponent {
    public users: IUserProfile[] = [];
    public props: IUserSearchProperties = {
        name: "",
        email: "",
        gender: ""
    };

    constructor(private service: UserManagementService) {}
    
    submit() {
        this.service.getUsers(this.props).subscribe((res: IUserProfile[]) => {
            this.users = res;
        })
    }

}





