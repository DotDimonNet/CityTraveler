import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IDefaultTrip } from "../models/defaultTrip.model";
import { IEntertainmentShow } from "../models/entertainment.show.model";
import { IFilterEntertainments } from "../models/filters/filtertEntertainments";
import { IFilterTrips } from "../models/filters/filterTrips";
import { IFiltertUsers } from "../models/filters/filterUsers";
import { IUserProfile } from "../models/user.model";
import { SearchDataService } from "./searchService.data";

@Injectable()
export class SearchService {

    constructor(private dataService: SearchDataService) { }
    getUsers(props: IFiltertUsers) : Observable<IUserProfile[]> {
        return this.dataService.getUsers(props);
    }
    getTrips(props: IFilterTrips) : Observable<IDefaultTrip[]> {
        return this.dataService.getTrips(props);
    }
    getEntertainments(props: IFilterEntertainments) : Observable<IEntertainmentShow[]> {
        return this.dataService.getEntertainments(props);
    }
}