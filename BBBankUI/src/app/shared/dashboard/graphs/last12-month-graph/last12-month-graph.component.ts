import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { CategoryScale, Chart, LinearScale, registerables } from 'chart.js';
import { AppUser } from '../../../../models/app-user';
import { LineGraphData } from '../../../../models/line-graph-data';
import { TransactionService } from '../../../../services/transaction.service';
Chart.register(LinearScale, CategoryScale);
Chart.register(...registerables);
@Component({
  selector: 'app-last12-month-graph',
  imports: [],
  templateUrl: './last12-month-graph.component.html',
  styleUrl: './last12-month-graph.component.css'
})
export class Last12MonthGraphComponent implements OnInit {
  @ViewChild('chartBig1') myCanvas: ElementRef;
  lineGraphData: LineGraphData;
  gradientChartOptionsConfigurationWithTooltipRed: any;
  loggedInUser?: AppUser;
  public context: CanvasRenderingContext2D;
  myChart: any;
  constructor(private transactionService: TransactionService) {
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
    this.loggedInUser = JSON.parse(localStorage.getItem('loggedInUser'));
  }
  ngAfterViewInit(): void {
    this.context = (this.myCanvas.nativeElement as HTMLCanvasElement).getContext('2d');
    this.transactionService.getLast12MonthBalances(this.loggedInUser?.id)
      .subscribe({
        next: (data: LineGraphData) => {
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
                  // borderDash: [],
                  borderDashOffset: 0.0,
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
            }
          });
        },
        error: (error: any) => {
          console.log(error);
        },
      })
  }
}
