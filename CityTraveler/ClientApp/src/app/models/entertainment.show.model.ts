export interface IEntertainmentShow {
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
    description: string
}

export class Entertainment { }