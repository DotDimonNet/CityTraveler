import { Component, OnChanges, OnInit } from '@angular/core';
import { IReviewModel } from 'src/app/models/review.model';
import { SocialMediaService } from 'src/app/services/socialMediaService';

@Component({
  selector: 'add-review-trip',
  templateUrl: './addReviewTripPage.component.html'
})
export class AddReviewTripPageComponent implements OnInit {
  public reviewInfo: IReviewModel = {
      userId: "",
      description: "",
      title: "",
      enterteinmentId: "",
      tripId: "",
      ratingId: "",
      comments: [],
      images: []
  } as IReviewModel;

  constructor(private service: SocialMediaService) {}

    ngOnInit() {
        this.service.addReviewTrip(this.reviewInfo, "")
        .subscribe((res: IReviewModel) => {
            this.reviewInfo = res;
        });
    }
}
