import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { first, map, take } from "rxjs/operators";
import { IEntertainmentShow } from "../models/entertainment.show.model";
import { IEntertainmentPreview } from "../models/entertainment.preview.model";
import { IUserProfile } from "../models/user.model";

@Injectable()
export class EntertainmentDataService {

    constructor(private client: HttpClient) {}

    getEntertainment(Id: string) : Observable<IEntertainmentShow> {
        return this.client.get(`/api/entertainment/id?Id=${Id}`)
        .pipe(first(), map((res: any) => {
            return res as IEntertainmentShow;
        }));
    }

    getAllEntertainment(type: number) : Observable<Array<IEntertainmentPreview>> {
        return this.client.get(`/api/entertainment/all?type=${type}`)
        .pipe(first(), map((res: any) => {
            return res as Array<IEntertainmentPreview>;
        }));
    }
}