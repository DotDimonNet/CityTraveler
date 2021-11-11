import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IEntertainment } from "../models/entertainment.model";
import { IUserProfile } from "../models/user.model";
import { EntertainmentDataService } from "./entertainmentService.data";

@Injectable()
export class EntertainmentService {

    constructor(private dataService: EntertainmentDataService) {

    }

    getEntertainment(Id: string) : Observable<IEntertainment> {
        return this.dataService.getEntertainment(Id);
    }
}