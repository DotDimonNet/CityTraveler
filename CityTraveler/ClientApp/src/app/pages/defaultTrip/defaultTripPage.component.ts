import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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
        id: "",
        title: "",
        description: "",
        tagString: "",
        price: "",
        averageRating: "",
        optimalSpent: new Date()
    } as IDefaultTrip;

    constructor(private service: TripService, private activeRoute: ActivatedRoute){
        this.defaultTrip.id = activeRoute.snapshot.params['id']
    }
    

    ngOnInit(){
        this.service.getDefaultTripById(`${this.defaultTrip.id}`).subscribe((res: IDefaultTrip) => {
            this.defaultTrip = res;
        })
    }
    
}
