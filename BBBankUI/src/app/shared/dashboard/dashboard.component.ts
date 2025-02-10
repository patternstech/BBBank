import { Component } from '@angular/core';
import { LineGraphData } from '../../models/line-graph-data';
import { CommonModule } from '@angular/common';
import { TransactionService } from '../../services/transaction.service';
import { Last12MonthGraphComponent } from "./graphs/last12-month-graph/last12-month-graph.component";

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, Last12MonthGraphComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  lineGraphData: LineGraphData | undefined;
  constructor(private transactionService: TransactionService) {

  }
  ngOnInit(): void {
    this.transactionService.getLast12MonthBalances('')
    .subscribe({
      next: (data) => {
        this.lineGraphData = data;
      },
      error: (error) => {
        console.log(error);
      }
    });
  }
}
