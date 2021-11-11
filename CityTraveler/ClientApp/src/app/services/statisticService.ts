import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { StatisticDataService } from "./StatisticService.data";

@Injectable()
export class StatisticService {

    constructor(private dataService: StatisticDataService) {}

    GetAverageAgeUser() {
        return this.dataService.GetAverageAgeUser();
    }
}