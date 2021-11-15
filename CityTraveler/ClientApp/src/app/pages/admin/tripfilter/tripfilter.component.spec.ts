import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TripfilterComponent } from './tripfilter.component';

describe('TripfilterComponent', () => {
  let component: TripfilterComponent;
  let fixture: ComponentFixture<TripfilterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TripfilterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TripfilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
