import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Chart, LinearScale, CategoryScale, registerables } from 'chart.js';
import { LineGraphData } from '../../../../models/line-graph-data';
import { AppState } from '../../../../store/appstate.reducers';
import { Observable, Subscription } from 'rxjs';
import { last6MonthsBalancesSelector } from '../../../store/dashboard.selectors';
Chart.register(LinearScale, CategoryScale);
Chart.register(...registerables);
@Component({
  selector: 'app-last6-months-graph',
  imports: [],
  templateUrl: './last6-months-graph.component.html',
  styleUrl: './last6-months-graph.component.css'
})
export class Last6MonthsGraphComponent implements OnInit, AfterViewInit, OnDestroy {
  @ViewChild('SixMonthChart') myCanvas: ElementRef;
  gradientChartOptionsConfigurationWithTooltipRed: any;
  public context: CanvasRenderingContext2D;
  lineGraphData: LineGraphData;
  last6MonthBalancesSub: Subscription;
  lineGraphData$: Observable<LineGraphData>;
  myChart: any;
  constructor(private store: Store<AppState>) {
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
      scales: {
        y:
        {
          gridLines: {
            drawBorder: false,
            color: 'rgba(29,140,248,0.1)',
            zeroLineColor: 'transparent',
          },
          ticks: {
            suggestedMin: 60,
            suggestedMax: 120,
            padding: 20,
            fontColor: '#9e9e9e',
          },
        },

        x:
        {
          gridLines: {
            drawBorder: false,
            color: 'rgba(29,140,248,0.1)',
            zeroLineColor: 'transparent',
          },
          ticks: {
            padding: 20,
            fontColor: '#9e9e9e',
          },
        },
      },
    };
  }
  ngOnInit(): void {
    this.lineGraphData$ = this.store.pipe(
      select(last6MonthsBalancesSelector)
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
    this.last6MonthBalancesSub = this.lineGraphData$
      .subscribe({
        next: (data: LineGraphData) => {
          if (data != null) {
            this.lineGraphData = data;
            const gradientStroke = this.context.createLinearGradient(0, 230, 0, 50);
            gradientStroke.addColorStop(1, 'rgba(29,140,248,0.2)');
            gradientStroke.addColorStop(0.4, 'rgba(29,140,248,0.0)');
            gradientStroke.addColorStop(0, 'rgba(29,140,248,0)'); // blue colors
            if (this.myChart) {
              this.myChart.destroy();
            }
            this.myChart = new Chart(this.context, {
              type: 'bar',
              data: {
                labels: this.lineGraphData?.labels,
                datasets: [{
                  label: 'Last 6 Month Balances',
                  backgroundColor: gradientStroke,
                  hoverBackgroundColor: gradientStroke,
                  borderColor: '#1f8ef1',
                  borderWidth: 2,
                  data: this.lineGraphData?.figures,
                }],
              },
              options: this.gradientChartOptionsConfigurationWithTooltipRed,
            });
          }
        },
        error: (error: any) => {
          console.log(error);
        },
      });

  }
  ngOnDestroy(): void {

    if (this.myChart) {
      this.myChart.destroy();
    }
  }
}    
