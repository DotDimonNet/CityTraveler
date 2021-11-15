import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import {Observable} from 'rxjs';
import { IDefaultTripPreview } from 'src/app/models/defaultTripPreviewModel';
import { TripService } from "src/app/services/tripService";
import { IImageModel } from 'src/app/models/image.model';



@Component({
    selector: "default-trips-preview",
    templateUrl: "./defaultTripsPreviewPage.component.html",
    styleUrls: ["./defaultTripsPreviewPage.component.css"]
})



export class DefaultTripsPagePreviewComponent implements OnInit{

    trips: Array<IDefaultTripPreview>
    
    constructor(private service: TripService){
        this.trips = new Array<IDefaultTripPreview>();
    }

    ngOnInit(){
        this.service.getDefaultTrips(0,10).subscribe((res: Array<IDefaultTripPreview>)=>{
            this.trips = res;
        } )
    }
}