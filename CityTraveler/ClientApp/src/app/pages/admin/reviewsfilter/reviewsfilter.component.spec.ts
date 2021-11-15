import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewsfilterComponent } from './reviewsfilter.component';

describe('ReviewsfilterComponent', () => {
  let component: ReviewsfilterComponent;
  let fixture: ComponentFixture<ReviewsfilterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReviewsfilterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewsfilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
