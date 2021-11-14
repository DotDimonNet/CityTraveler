import { Component } from '@angular/core';
import { IEntertainment } from 'src/app/models/entertainment.model';
import { IFilterEntertainments } from 'src/app/models/filters/filtertEntertainments';
import { SearchService } from 'src/app/services/searchService';

@Component({
  selector: 'search-entertainments',
  templateUrl: './searchServicePage.component.html',
  styleUrls: ['./searchService.component.css']
})
export class SearchServiceComponent {
  public propsEntertainments = propsEntertainment;
  public entertainments: IEntertainment []  = [];

  constructor(private service: SearchService) {
  }

  submitEntertainments() {
    this.service.getEntertainments(this.propsEntertainments).subscribe((res: IEntertainment[]) => {
      this.entertainments = res;
     });
  }
}

const propsEntertainment: IFilterEntertainments = {
  streetName: '',
  type: -1,
  houseNumber: '',
  tripName: '',
  title: '',
  priceMore: 0,
  priceLess: 10000,
  ratingMore: 0,
  ratingLess: 5
};
