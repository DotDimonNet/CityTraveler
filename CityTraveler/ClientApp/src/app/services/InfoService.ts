import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IEntertainment } from "../models/entertainment.model";
import { InfoDataService } from "./InfoService.data";

@Injectable()
export class InfoService {

    constructor(private dataService: InfoDataService) {}

    getPopularEntertainment(userId: string) : Observable<IEntertainment> {
        return this.dataService.getPopularEntertainment(userId);
    }
}
