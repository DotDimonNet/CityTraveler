import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { first, map, take } from "rxjs/operators";

@Injectable()
export class StatisticDataService {

    
    constructor(private client: HttpClient) {}

    GetAverageAgeUser() : Observable<number> {
        return this.client.get('/api/statistic/get-users-average-age')
        .pipe(first(), map((res: number) => {
            return res as number;
        }));
    }
}