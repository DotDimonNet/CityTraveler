import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IEntertainmentPreview } from "../models/entertainment.preview.model";
import { IEntertainmentShow } from "../models/entertainment.show.model";
import { IUserProfile } from "../models/user.model";
import { EntertainmentDataService } from "./entertainmentService.data";

@Injectable()
export class EntertainmentService {

    constructor(private dataService: EntertainmentDataService) {

    }

    getEntertainment(Id: string) : Observable<IEntertainmentShow> {
        return this.dataService.getEntertainment(Id);
    }

    getAllEntertainment(type: number) : Observable<Array<IEntertainmentPreview>> {
        return this.dataService.getAllEntertainment(type);
    }
}