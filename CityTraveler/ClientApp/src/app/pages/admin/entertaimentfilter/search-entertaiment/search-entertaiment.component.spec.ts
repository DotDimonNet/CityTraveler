import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchEntertaimentComponent } from './search-entertaiment.component';

describe('SearchEntertaimentComponent', () => {
  let component: SearchEntertaimentComponent;
  let fixture: ComponentFixture<SearchEntertaimentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SearchEntertaimentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchEntertaimentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
