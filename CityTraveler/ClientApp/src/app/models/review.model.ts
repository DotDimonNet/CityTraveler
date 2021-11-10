import { ICommentModel } from "./comment.model";
import { IImageModel } from "./image.model";

export interface IReviewModel {
    ratingId: string,
    userId: string,
    title: string,
    description: string,
    enterteinmentId: string,
    tripId: string
    images: IImageModel[]
    comments: ICommentModel[]
}
