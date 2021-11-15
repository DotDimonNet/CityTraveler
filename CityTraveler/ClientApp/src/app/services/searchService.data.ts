import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { first, map, take } from "rxjs/operators";
import { IDefaultTrip } from "../models/defaultTrip.model";
import { IEntertainmentShow } from "../models/entertainment.show.model";
import { IFilterEntertainments } from "../models/filters/filtertEntertainments";
import { IFilterTrips } from "../models/filters/filterTrips";
import { IFiltertUsers } from "../models/filters/filterUsers";
import { IUserProfile } from "../models/user.model";

@Injectable()
export class SearchDataService {

    constructor(private client: HttpClient) {}

    getUsers(props: IFiltertUsers) : Observable<IUserProfile[]> {
        return this.client.get(`/api/user/users-search?UserName=${props.name}&EntertainmentName=${props.entertainmentName}&Gender=${props.gender}`)
        .pipe(first(), map((res: IUserProfile[]) => res));
    }

    getTrips(props: IFilterTrips) : Observable<IDefaultTrip[]> {
        return this.client.get(`/api/search/trips?TripStart=${props.tripStart}&TripEnd=${props.tripEnd}&EntertainmentName=${props.entertainmentName}&User=${props.user}&PriceMore=${props.priceMore}&PriceLess=${props.priceLess}&AverageRatingMore=${props.priceMore}&AverageRatingLess=${props.averageRatingLess}&Title=${props.title}&Description=${props.description}&OptimalSpent=${props.optimalSpent}&RealSpent=${props.realSpent}&TripStatus=${props.tripStatus}`)
        .pipe(first(), map((res: IDefaultTrip[]) => res));
    }
    getEntertainments(props: IFilterEntertainments) : Observable<IEntertainmentShow[]> {
        return this.client.get(`/api/search/entertainments?StreetName=${props.streetName}&Type=${props.type}&HouseNumber=${props.houseNumber}&TripName=${props.tripName}&Title=${props.title}&PriceMore=${props.priceMore}&PriceLess=${props.priceLess}&RatingMore=${props.ratingMore}&RatingLess=${props.ratingLess}`)
        .pipe(first(), map((res: IEntertainmentShow[]) => res));
    }
}