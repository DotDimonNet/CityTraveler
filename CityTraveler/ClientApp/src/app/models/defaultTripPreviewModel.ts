import { Time } from "@angular/common";
import { Timestamp } from "rxjs/internal/operators/timestamp";
import { Identifier } from "typescript";
import { IImageModel } from "./image.model";

export interface IDefaultTripPreview{
    Id: string,
    title: string,
    description: string,
    tagString: string
    //mainImage: IImageModel,
    optimalSpent: Date
}