import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import {Observable} from 'rxjs';
import { IDefaultTrip } from "src/app/models/defaultTrip.model";
import { TripService } from "src/app/services/tripService";
import {Subscription} from 'rxjs';

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

    private subscription: Subscription;
    constructor(private service: TripService, private activeRoute: ActivatedRoute){
        this.subscription = activeRoute.params.subscribe(params=>this.defaultTrip.id=params['id']);
    }
    
    trip:any;
    ngOnInit(){
        this.activeRoute.paramMap.subscribe(params =>{
            this.service.getDefaultTripById(params.get('id')).subscribe(c=>{
                this.trip = c;
            })
        })
    }
    
}
