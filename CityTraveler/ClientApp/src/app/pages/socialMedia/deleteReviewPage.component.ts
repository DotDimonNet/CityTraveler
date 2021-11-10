import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { SocialMediaService } from 'src/app/services/socialMediaService';
@Component({
  selector: 'delete-review',
  templateUrl: './deleteReviewPage.component.html'
})
export class DeleteReviewPageComponent {
  public result : boolean
  public reviewId : ""

  constructor(private service: SocialMediaService)
  {
    
  }
    submit() {
        this.service.deleteReview(this.reviewId)
        .subscribe((res: boolean) => {
            this.result = res;
        });
    }
}
