import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { first, map, take } from "rxjs/operators";
import { IUserProfile } from "../models/user.model";

@Injectable()
export class SearchDataService {

    constructor(private client: HttpClient) {}

    getUserProfile(userId: string) : Observable<IUserProfile> {
        return this.client.get(`/api/user/id?userId=${userId}`)
        .pipe(first(), map((res: any) => {
            return res as IUserProfile;
        }));
    }
}