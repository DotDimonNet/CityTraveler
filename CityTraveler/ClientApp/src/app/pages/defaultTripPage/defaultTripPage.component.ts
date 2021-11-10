import { Component, OnInit } from '@angular/core';
import {Observable} from 'rxjs';
import { IDefaultTrip } from "src/app/models/defaultTrip.model";
import { TripService } from "src/app/services/tripService";

@Component({
  selector: "default-trip",
  templateUrl: "./defaultTripPage.component.html",
  styleUrls: ["./defaultTripPage.component.css"]
})

export class DefaultTripPageComponent implements OnInit{
    public defaultTrip: IDefaultTrip = {
        Id: "",
        title: "",
        description: "",
        tagString: "",
        price: "",
        averageRating: "",
        optimalSpent: new Date()
    } as IDefaultTrip;

    constructor(private service: TripService){}

    ngOnInit(){
        this.service.getDefaultTripById("9A5A3FE9-8F52-4F64-FB22-08D9A4576885").subscribe((res: IDefaultTrip) => {
            this.defaultTrip = res;
        })
    }
    
}
