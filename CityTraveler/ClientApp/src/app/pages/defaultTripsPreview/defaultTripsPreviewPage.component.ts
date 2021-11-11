import { Component, OnInit } from '@angular/core';
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
    public defaultTripsPewview: IDefaultTripPreview={
        Id: "",
        title: "",
        description: "",
        tagString: "",
        optimalSpent: new Date(),
    } as IDefaultTripPreview;

    constructor (private service: TripService){}

    ngOnInit(){
        
    }
}