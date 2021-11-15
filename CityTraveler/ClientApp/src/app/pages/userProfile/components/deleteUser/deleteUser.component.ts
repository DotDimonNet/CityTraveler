import { Component, OnInit } from '@angular/core';
import { IUserProfile } from 'src/app/models/user.model';
import { UserManagementService } from 'src/app/services/userManagementService';

@Component({
    selector: 'delete-user',
    templateUrl: './deleteUser.component.html',
    styleUrls: ['./deleteUser.component.css']
})
export class DeleteUserComponent {
    public user: IUserProfile;
    public result: boolean;
    public username: "";

    constructor(private service: UserManagementService) {}

    submit() {
        this.service.deleteUser(this.username).subscribe((res: boolean) => {
            this.result = res;
        })
    }
}


