import { Time } from "@angular/common";
import { Timestamp } from "rxjs/internal/operators/timestamp";
import { Identifier } from "typescript";

export interface INewTrip{
    title: string,
    description: string,
    tripStart: Date,
}