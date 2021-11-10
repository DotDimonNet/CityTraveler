import { Component } from '@angular/core';
import { SocialMediaService } from 'src/app/services/socialMediaService';
@Component({
  selector: 'delete-comment',
  templateUrl: './deleteCommentPage.component.html'
})
export class DeleteCommentPageComponent {
  public result : boolean
  public commentId : ""

  constructor(private service: SocialMediaService)
  {
    
  }
    submit() {
        this.service.deleteComment(this.commentId)
        .subscribe((res: boolean) => {
            this.result = res;
        });
    }
}
