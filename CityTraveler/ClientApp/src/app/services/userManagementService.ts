import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IUserProfile } from "../models/user.model";
import { UserManagementDataService } from "./userManagementService.data";

@Injectable()
export class UserManagementService {

    constructor(private dataService: UserManagementDataService) {

    }

    getUserProfile(userId: string) : Observable<IUserProfile> {
        return this.dataService.getUserProfile(userId);
    }
}
