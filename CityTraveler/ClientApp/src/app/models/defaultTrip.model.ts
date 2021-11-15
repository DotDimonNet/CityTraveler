import { Time } from "@angular/common";
import { Timestamp } from "rxjs/internal/operators/timestamp";
import { Identifier } from "typescript";

export interface IDefaultTrip{
    id:string
    title: string,
    description: string,
    tagString: string,
    price: string,
    averageRating: string,
    optimalSpent: Date,
    images: [],
    reviews: [],
    entertainments: [],
    users: []
}

export class DefaultTrip{
    static doSomething(){
        return "njqdj";
    }

    q(){
        DefaultTrip.doSomething();
    }
}
