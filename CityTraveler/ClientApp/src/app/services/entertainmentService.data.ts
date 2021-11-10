import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { first, map, take } from "rxjs/operators";
import { IEntertainment } from "../models/entertainment.model";
import { IUserProfile } from "../models/user.model";

@Injectable()
export class EntertainmentDataService {

    constructor(private client: HttpClient) {}

    getEntertainment(Id: string) : Observable<IEntertainment> {
        return this.client.get(`/api/entertainment/get-by-id?Id=${Id}`)
        .pipe(first(), map((res: any) => {
            return res as IEntertainment;
        }));
    }
}