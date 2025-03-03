import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Chart } from 'chart.js';
import { Observable, Subscription, combineLatest } from 'rxjs';
import { depositsCountSelector, withdrawalsCountSelector } from '../../../store/dashboard.selectors';
import { SharedState } from '../../../store/shared.reducers';

@Component({
  selector: 'app-deposit-withraw-count-graph',
  imports: [],
  templateUrl: './deposit-withraw-count-graph.component.html',
  styleUrl: './deposit-withraw-count-graph.component.css'
})
export class DepositWithrawCountGraphComponent implements OnInit, AfterViewInit, OnDestroy {
  @ViewChild('DepositWithdrawCount') myCanvas: ElementRef;
  depositCount$: Observable<number>;
  depositCount: number;

  withdrawCount$: Observable<number>;
  withdrawCount: number;

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
    this.depositCount$ = this.sharedStore.pipe(
      select(depositsCountSelector)
    );
    this.withdrawCount$ = this.sharedStore.pipe(
      select(withdrawalsCountSelector)
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
    this.combinedSub = combineLatest([this.withdrawCount$, this.depositCount$]).subscribe(([withdrawCount, depositCount]) => {
      const gradientStroke = this.context.createLinearGradient(0, 230, 0, 50);

      gradientStroke.addColorStop(1, 'rgba(53,136,89,.6)');

      const gradientStroke2 = this.context.createLinearGradient(0, 230, 0, 50);

      gradientStroke2.addColorStop(1, 'rgba(82,179,194,.6)');

      if (this.myChart) {
        this.myChart.destroy();
      }

      this.myChart = new Chart(this.context, {
        type: 'pie',
        data: {
          labels: ['Deposit:' + depositCount, 'Withdrawals ' + withdrawCount],
          datasets: [{
            label: 'Deposit and Withdrawals Count',
            backgroundColor: [gradientStroke, gradientStroke2],
            borderColor: '#27293D',
            borderWidth: 2,
            data: [withdrawCount, depositCount],
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
