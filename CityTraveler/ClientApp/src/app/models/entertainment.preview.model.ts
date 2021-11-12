import * as internal from "assert";

export interface IEntertainmentPreview {
    id: string,
    title: string,
    type: string,
    address: {
        street: {
            title: string,
        },
        houseNumber: string,
        apartmentNumber: string,
    },
    description: string,
    tripsCount: number,
    reviewsCount: number,
    averageRating: number,
    imageDTO: {
        sourse: string,
        title: string,
    }
}
