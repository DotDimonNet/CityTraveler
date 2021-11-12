import { Component } from "@angular/core";
import { DefaultTrip, IDefaultTrip } from "src/app/models/defaultTrip.model";
import { IFilterTrips } from "src/app/models/filters/filterTrips";
import { SearchService } from "src/app/services/searchService";

@Component({
    selector: 'search-trips',
    templateUrl: './searchTrips.component.html',
    styleUrls: ['./searchService.component.css']
  })
  export class SearchTripsComponent {
    public propsTrips = propsTrip;
    public trips: IDefaultTrip [] = [];
  
    constructor(private service: SearchService) 
    {
    }
    submitTrips()
    {
      this.service.getTrips(this.propsTrips).subscribe((res: IDefaultTrip[]) => {
        this.trips = res;
       });
    }
  }
  
  const propsTrip: IFilterTrips = {
    tripStart: new Date(),
    tripEnd: new Date(),
    entertainmentName: '',
    user: '',
    priceMore: 0,
    priceLess: 10000,
    averageRatingMore: 0,
    averageRatingLess: 5,
    title: '',
    description: '',
    optimalSpent: null,
    realSpent: null,
    tripStatus: -1
  };