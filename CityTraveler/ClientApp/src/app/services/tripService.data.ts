import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { first, map, take } from "rxjs/operators";
import { DefaultTrip, IDefaultTrip } from "../models/defaultTrip.model";
import { INewTrip } from "../models/newTripModel";
import { IDefaultTripPreview } from "../models/defaultTripPreviewModel";


@Injectable()
export class TripDataService{
    constructor(private client: HttpClient){
       
    }

    getDefaultTripById(tripId: string): Observable<IDefaultTrip>{
        return this.client.get(`/api/trips/default-trip?defaultTripId=${tripId}`)
        .pipe(first(), map((res: any)=>{
            return res as IDefaultTrip;
        }))
    }

    getDefaultTrips(skip: number, take: number):Observable<IDefaultTripPreview[]>{
        return this.client.get(`/api/trips/default-trips?skip=${skip}&take=${take}`)
        .pipe(first(), map((res: any)=>{
            return res as Array<IDefaultTripPreview>;
        }))
    }


    // addNewTrip(title: string, description: string, dateTime: Date):Observable<INewTrip>{
    //     return this.client.post(`/api/trips/trip?`)
    // }
}

