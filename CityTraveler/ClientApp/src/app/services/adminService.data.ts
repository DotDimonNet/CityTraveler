import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { first, map, take } from "rxjs/operators";

@Injectable()
export class StatisticDataService {

    constructor(private client: HttpClient) {}

    GetAverageAgeUser() {
        return this.client.get('/api/statistic/get-average-entertaiment-in-trip')
        .pipe(first(), map((res: any) => {
            return res as number;
        }));
    }
}