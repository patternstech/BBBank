import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepositWithrawCountGraphComponent } from './deposit-withraw-count-graph.component';

describe('DepositWithrawCountGraphComponent', () => {
  let component: DepositWithrawCountGraphComponent;
  let fixture: ComponentFixture<DepositWithrawCountGraphComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DepositWithrawCountGraphComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DepositWithrawCountGraphComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
