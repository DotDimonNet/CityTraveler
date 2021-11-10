import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IUserProfile } from "../models/user.model";
import { SearchDataService } from "./searchService.data";

@Injectable()
export class SearchService {

    constructor(private dataService: SearchDataService) {

    }

    getUsers(userId: string) : Observable<IUserProfile> {
        return this.dataService.getUserProfile(userId);
    }
}