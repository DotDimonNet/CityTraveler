import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { first, map, take } from "rxjs/operators";
import { DefaultTrip, IDefaultTrip } from "../models/defaultTrip.model";
import { INewTrip } from "../models/newTripModel";


@Injectable()
export class TripDataService{
    constructor(private client: HttpClient){
       
    }

    getDefaultTripById(tripId: string): Observable<IDefaultTrip>{
        return this.client.get(`/api/trips/default-trip-by-id?defaultTripId=${tripId}`)
        .pipe(first(), map((res: any)=>{
            return res as IDefaultTrip;
        }))
    }

    // addNewTrip(title: string, description: string, dateTime: Date):Observable<INewTrip>{
    //     return this.client.post(`/api/trips/trip?`)
    // }
}

