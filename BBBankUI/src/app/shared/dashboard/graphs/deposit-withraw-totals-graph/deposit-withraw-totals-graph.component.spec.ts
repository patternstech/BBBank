import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepositWithrawTotalsGraphComponent } from './deposit-withraw-totals-graph.component';

describe('DepositWithrawTotalsGraphComponent', () => {
  let component: DepositWithrawTotalsGraphComponent;
  let fixture: ComponentFixture<DepositWithrawTotalsGraphComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DepositWithrawTotalsGraphComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DepositWithrawTotalsGraphComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
