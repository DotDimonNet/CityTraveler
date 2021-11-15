import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { first, map } from "rxjs/operators";
import {IEntertainmentShow } from "../models/entertainment.show.model";

@Injectable()
export class InfoDataService {

    constructor(private client: HttpClient) {}

  getPopularEntertainment(userId: string): Observable<IEntertainmentShow> {
        return this.client.get(`/api/info/user/popular-entertaiment?userId=${userId}`)
        .pipe(first(), map((res: any) => {
          return res as IEntertainmentShow;
        }));
    }
}
