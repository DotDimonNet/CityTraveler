import { Time } from "@angular/common";
import { Timestamp } from "rxjs";

export interface IFilterTrips
{
    tripStart: Date,
    tripEnd: Date,
    entertainmentName: string,
    user: string,
    priceMore: number,
    priceLess: number,
    averageRatingMore: number,
    averageRatingLess: number,
    title: string,
    description: string,
    optimalSpent: Timestamp<Time>,
    realSpent: Timestamp<Time>,
    tripStatus: number
}