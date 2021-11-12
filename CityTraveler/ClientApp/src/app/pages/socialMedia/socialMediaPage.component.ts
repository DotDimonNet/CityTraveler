import { Component, OnInit } from '@angular/core';
import { IReviewModel } from 'src/app/models/review.model';
import { SocialMediaService } from 'src/app/services/socialMediaService';
@Component({
  selector: 'review-model',
  templateUrl: './socialMediaPage.component.html'
})
export class SocialMediaPageComponent implements OnInit {
  public reviewInfo: IReviewModel = {
      userId: "",
      description: "",
      title: "",
      enterteinmentId: "",
      tripId: "",
      ratingId: "",
      comments: new Array(),
      images: new Array()
  } as IReviewModel;
  public reviewToAdd: IReviewModel = {
    userId: "",
    description: "",
    title: "",
    enterteinmentId: "",
    tripId: "",
    ratingId: "",
    comments: new Array(),
    images: new Array()
} as IReviewModel;

  constructor(private service: SocialMediaService) {}

    ngOnInit() {
        this.service.getReview('12560264-8E8C-429F-FBFC-08D9A457DA34')
        .subscribe((res: IReviewModel) => {
            this.reviewInfo = res;
        });
    }
    submit()
    {
      this.service.addReviewEnetrtainment(this.reviewToAdd, this.reviewToAdd.enterteinmentId)
        .subscribe((res: IReviewModel) => {
            this.reviewInfo = res;
        });
    }
}
