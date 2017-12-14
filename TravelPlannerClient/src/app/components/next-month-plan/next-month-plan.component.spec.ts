import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextMonthPlanComponent } from './next-month-plan.component';

describe('NextMonthPlanComponent', () => {
  let component: NextMonthPlanComponent;
  let fixture: ComponentFixture<NextMonthPlanComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NextMonthPlanComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextMonthPlanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
