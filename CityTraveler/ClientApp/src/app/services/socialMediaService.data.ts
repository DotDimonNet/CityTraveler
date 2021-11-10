import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { first, map, take } from "rxjs/operators";
import { IReviewModel } from "../models/review.model";

@Injectable()
export class SocialMediaDataService {

    constructor(private client: HttpClient) {}

    getReviewById(reviewId: string) : Observable<IReviewModel> {
        return this.client.get(`/api/socialmedia/by-id?reviewId=${reviewId}`)
        .pipe(first(), map((res: any) => {
            return res as IReviewModel;
        }));
    }
    getReviewByDescription(reviewDescription: string) : Observable<IReviewModel> {
        return this.client.get(`/api/socialmedia/by-description?description=${reviewDescription}`)
        .pipe(first(), map((res: any) => {
            return res as IReviewModel;
        }));
    }
    getReviewByTitle(reviewTitle: string) : Observable<IReviewModel> {
        return this.client.get(`/api/socialmedia/by-description?title=${reviewTitle}`)
        .pipe(first(), map((res: any) => {
            return res as IReviewModel;
        }));
    }
    addReviewTrip(review:IReviewModel, tripId: string):Observable<IReviewModel>
    {
        return this.client.get(`/api/socialmedia/review-trip?review=${review}`)
        .pipe(first(), map((res: any) => {
            return res as IReviewModel;
        }));
    }
    deleteReview(reviewId:string):Observable<boolean>
    {
        return this.client.delete(`/api/socialmedia/review?reviewId=${reviewId}`)
        .pipe(first(), map((res: any) => {
            return res as boolean;
        }));
    }
    deleteComment(commentId:string):Observable<boolean>
    {
        return this.client.delete(`/api/socialmedia/review?reviewId=${commentId}`)
        .pipe(first(), map((res: any) => {
            return res as boolean;
        }));
    }
}