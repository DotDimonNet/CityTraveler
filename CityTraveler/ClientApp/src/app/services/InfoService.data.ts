import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { first, map } from "rxjs/operators";
import {IEntertainment } from "../models/entertainment.model";

@Injectable()
export class InfoDataService {

    constructor(private client: HttpClient) {}

    getPopularEntertainment(userId: string) : Observable<IEntertainment> {
        return this.client.get(`/api/info/user/popular-entertaiment?userId=${userId}`)
        .pipe(first(), map((res: any) => {
            return res as IEntertainment;
        }));
    }
}