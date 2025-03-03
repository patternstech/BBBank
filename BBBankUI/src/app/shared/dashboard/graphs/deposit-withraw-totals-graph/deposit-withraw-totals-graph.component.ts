import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Chart, LinearScale, CategoryScale, registerables } from 'chart.js';
import { combineLatest, Observable, Subscription } from 'rxjs';
import { depositsTotalSelector, withdrawalsTotalSelector } from '../../../store/dashboard.selectors';
import { SharedState } from '../../../store/shared.reducers';
Chart.register(LinearScale, CategoryScale);
Chart.register(...registerables);
@Component({
  selector: 'app-deposit-withraw-totals-graph',
  imports: [],
  templateUrl: './deposit-withraw-totals-graph.component.html',
  styleUrl: './deposit-withraw-totals-graph.component.css'
})
export class DepositWithrawTotalsGraphComponent implements OnInit, AfterViewInit, OnDestroy {
  @ViewChild('DepositWithdrawAmounts') myCanvas: ElementRef;

  depositTotal$: Observable<number>;
  depositTotal: number;

  withdrawTotal$: Observable<number>;
  withdrawTotal: number;

  combinedSub: Subscription;

  gradientChartOptionsConfigurationWithTooltipRed: any;
  public context: CanvasRenderingContext2D;
  myChart: any;
  constructor(private sharedStore: Store<SharedState>) {
    this.gradientChartOptionsConfigurationWithTooltipRed = {
      maintainAspectRatio: false,
      legend: {
        display: false,
      },

      tooltips: {
        backgroundColor: '#f5f5f5',
        titleFontColor: '#333',
        bodyFontColor: '#666',
        bodySpacing: 4,
        xPadding: 12,
        mode: 'nearest',
        intersect: 0,
        position: 'nearest',
      },
      responsive: true,
    };
  }
  ngOnInit(): void {
    this.depositTotal$ = this.sharedStore.pipe(
      select(depositsTotalSelector)
    );
    this.withdrawTotal$ = this.sharedStore.pipe(
      select(withdrawalsTotalSelector)
    );

  }
  ngAfterViewInit(): void {
    const canvas = this.myCanvas.nativeElement as HTMLCanvasElement;
    if (!canvas) {
      console.error('Canvas element not found');
      return;
    }

    this.context = canvas.getContext('2d');
    if (!this.context) {
      console.error('Could not get 2D context');
      return;
    }
    this.combinedSub = combineLatest([this.withdrawTotal$, this.depositTotal$]).subscribe(([withdrawTotal, depositTotal]) => {
      const gradientStroke = this.context.createLinearGradient(0, 230, 0, 50);

      gradientStroke.addColorStop(1, 'rgba(255,149,16,.6)');

      const gradientStroke2 = this.context.createLinearGradient(0, 230, 0, 50);

      gradientStroke2.addColorStop(1, 'rgba(255,237,82,.6)');


      if (this.myChart) {
        this.myChart.destroy();
      }
      this.myChart = new Chart(this.context, {
        type: 'pie',
        data: {
          labels: ['Deposits Total: $' + depositTotal, 'Withdrawals Total: $' + withdrawTotal],
          datasets: [{
            label: 'Deposit and Withdrawals Totals',
            backgroundColor: [gradientStroke, gradientStroke2],
            borderColor: '#27293D',
            borderWidth: 2,
            data: [withdrawTotal, depositTotal],
          }],
        },
        options: this.gradientChartOptionsConfigurationWithTooltipRed,
      });
    });
  }
  ngOnDestroy(): void {
     this.combinedSub.unsubscribe();
    if (this.myChart) {
      this.myChart.destroy();
    }
  }
}
