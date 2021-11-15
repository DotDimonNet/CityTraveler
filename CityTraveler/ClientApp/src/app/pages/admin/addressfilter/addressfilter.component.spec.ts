import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddressfilterComponent } from './addressfilter.component';

describe('AddressfilterComponent', () => {
  let component: AddressfilterComponent;
  let fixture: ComponentFixture<AddressfilterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddressfilterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddressfilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
