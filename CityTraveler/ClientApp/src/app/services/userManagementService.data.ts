import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { first, map } from "rxjs/operators";
import { IUserProfile, IUserSearchProperties } from "../models/user.model";

@Injectable()
export class UserManagementDataService {

    constructor(private client: HttpClient) {}

    /*getUserProfile(userId: string) : Observable<IUserProfile> {
        return this.client.get(`/api/user/id?userId=${userId}`)
        .pipe(first(), map((res: any) => {
            return res as IUserProfile;
        }));
    }*/

    getUsers(props: IUserSearchProperties) : Observable<IUserProfile[]> {
        return this.client.get(`/api/user/users-search?name=${props.name}&email=${props.email}&gender=${props.gender}`)
        .pipe(first(), map((res: IUserProfile[]) => res));
    }
    deleteUser(username: string) : Observable<boolean>
     {
        return this.client.delete(`/api/user/user-delete?username=${username}`)
        .pipe(first(), map((res: any) => {
            return res as boolean;
        }));
    }
}