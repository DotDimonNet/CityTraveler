import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EntertaimentfilterComponent } from './entertaimentfilter.component';

describe('EntertaimentfilterComponent', () => {
  let component: EntertaimentfilterComponent;
  let fixture: ComponentFixture<EntertaimentfilterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EntertaimentfilterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EntertaimentfilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
