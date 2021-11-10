import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IReviewModel } from "../models/review.model";
import { SocialMediaDataService } from "./socialMediaService.data";

@Injectable()
export class SocialMediaService {

    constructor(private dataService: SocialMediaDataService) {
        
    }

    getReview(reviewId: string) : Observable<IReviewModel> {
        return this.dataService.getReviewById(reviewId);
    }
    getReviewByDescription(description: string) : Observable<IReviewModel> {
        return this.dataService.getReviewByDescription(description);
    }
    getReviewByTitle(title: string) : Observable<IReviewModel> {
        return this.dataService.getReviewByTitle(title);
    }
    addReviewTrip(review:IReviewModel, trip:string) : Observable<IReviewModel>
    {
        return this.dataService.addReviewTrip(review,trip);   
    }
    deleteReview(review:string):Observable<boolean>
    {
        return this.dataService.deleteReview(review);
    }
    deleteComment(commentId:string):Observable<boolean>
    {
        return this.dataService.deleteReview(commentId);
    }
}