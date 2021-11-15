import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IEntertainmentShow } from "../models/entertainment.show.model";
import { InfoDataService } from "./InfoService.data";

@Injectable()
export class InfoService {

    constructor(private dataService: InfoDataService) {}

    getPopularEntertainment(userId: string) : Observable<IEntertainmentShow> {
        return this.dataService.getPopularEntertainment(userId);
    }
}
