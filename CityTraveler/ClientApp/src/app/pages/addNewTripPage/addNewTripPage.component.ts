import { Component, OnInit } from '@angular/core';
import {Observable} from 'rxjs';
import { INewTrip } from 'src/app/models/newTripModel';
import { TripService } from "src/app/services/tripService";

@Component({
    selector: "new-trip",
    templateUrl: "./addNewTripPage.component.html",
    styleUrls: ["./addNewTripPage.componenet.css"]
})

export class NewTripComponentPage implements OnInit {
    public newTrip: INewTrip = {
        title: "",
        description:"",
        tripStart: new Date()

    }

    constructor(private service: TripService){}

    ngOnInit(){

    }
}