import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Last6MonthsGraphComponent } from './last6-months-graph.component';

describe('Last6MonthsGraphComponent', () => {
  let component: Last6MonthsGraphComponent;
  let fixture: ComponentFixture<Last6MonthsGraphComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Last6MonthsGraphComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Last6MonthsGraphComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
