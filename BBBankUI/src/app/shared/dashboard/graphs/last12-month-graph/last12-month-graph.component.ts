import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { CategoryScale, Chart, LinearScale, registerables } from 'chart.js';
import { AppUser } from '../../../../models/app-user';
import { LineGraphData } from '../../../../models/line-graph-data';
import { TransactionService } from '../../../../services/transaction.service';
import { Observable, Subscription } from 'rxjs';
import { AppState } from '../../../../store/appstate.reducers';
import { select, Store } from '@ngrx/store';
import { last12MonthsBalancesSelector } from '../../../store/dashboard.selectors';
Chart.register(LinearScale, CategoryScale);
Chart.register(...registerables);
@Component({
  selector: 'app-last12-month-graph',
  imports: [],
  templateUrl: './last12-month-graph.component.html',
  styleUrl: './last12-month-graph.component.css'
})
export class Last12MonthGraphComponent implements OnInit, AfterViewInit {
  @ViewChild('chartBig1') myCanvas: ElementRef;
  lineGraphData$: Observable<LineGraphData>;
  last12MonthBalancesSub: Subscription;
  lineGraphData: LineGraphData;
  gradientChartOptionsConfigurationWithTooltipRed: any;
  loggedInUser?: AppUser;
  public context: CanvasRenderingContext2D;
  myChart: any;
  constructor(private store: Store<AppState>) {
    this.gradientChartOptionsConfigurationWithTooltipRed = {
      responsive: true,
      maintainAspectRatio: false,
      legend: {
        display: false,
      },
      tooltips: {
        backgroundColor: 'f5f5f5',
        titleFontColor: '333',
        bodyFontColor: '666',
      },
      scales: {
        yAxes: {
          ticks: {
            beginAtZero: true,
            suggestedMin: 60,
            suggestedMax: 125,
          },
        },
      }
    }
  }
  ngOnInit(): void {
    this.lineGraphData$ = this.store.pipe(
      select(last12MonthsBalancesSelector)
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

    //this.transactionService.getLast12MonthBalances(this.loggedInUser?.id)
    this.last12MonthBalancesSub = this.lineGraphData$
      .subscribe({
        next: (data: LineGraphData) => {
          if (data != null) {
            this.lineGraphData = data;
            const gradientStroke = this.context.createLinearGradient(0, 230, 0, 50);
            gradientStroke.addColorStop(1, 'rgba(233,32,16,0.2)');
            gradientStroke.addColorStop(0.4, 'rgba(233,32,16,0.0)');
            gradientStroke.addColorStop(0, 'rgba(233,32,16,0)'); // red colors

            this.myChart = new Chart(this.context, {
              type: 'line',
              data: {
                labels: this.lineGraphData?.labels,
                datasets: [
                  {
                    label: 'Balance',
                    fill: true,
                    backgroundColor: gradientStroke,
                    borderColor: '#ec250d',
                    borderWidth: 2,
                    pointBackgroundColor: '#ec250d',
                    pointBorderColor: 'rgba(255,255,255,0)',
                    pointHoverBackgroundColor: '#ec250d',
                    pointBorderWidth: 20,
                    pointHoverRadius: 4,
                    pointHoverBorderWidth: 15,
                    pointRadius: 4,
                    data: this.lineGraphData?.figures,
                  },
                ],
              },
              options: this.gradientChartOptionsConfigurationWithTooltipRed
            });
          }
        },
        error: (error: any) => {
          console.log(error);
        },
      });
  }
}
