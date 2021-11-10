import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IDefaultTrip } from "../models/defaultTrip.model";
import { TripDataService } from "./tripService.data";

@Injectable()
export class TripService{
    constructor(private dataService: TripDataService){}

    getDefaultTripById(tripId: string):Observable<IDefaultTrip>{
        return this.dataService.getDefaultTripById(tripId);
    }

    
}