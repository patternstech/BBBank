import { Component, OnDestroy, OnInit } from '@angular/core';
import { LineGraphData } from '../../models/line-graph-data';
import { CommonModule } from '@angular/common';
import { TransactionService } from '../../services/transaction.service';
import { Last12MonthGraphComponent } from "./graphs/last12-month-graph/last12-month-graph.component";
import { SharedState } from '../store/shared.reducers';
import { Store } from '@ngrx/store';
import { loadLast12MonthsBalancesAction } from '../store/dashboard.actions';
import { AppState } from '../../store/appstate.reducers';
import { Subscription } from 'rxjs';
import { loggedInUserIdSelector } from '../../store/auth.selectors';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, Last12MonthGraphComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit, OnDestroy {
  loggedInUserSub: Subscription;
  constructor(private sharedStore: Store<SharedState>, private globalStore: Store<AppState>) { }
  ngOnInit(): void {
    this.loggedInUserSub = this.globalStore.select(loggedInUserIdSelector)
    .subscribe((userId: string) => {
      this.sharedStore.dispatch(loadLast12MonthsBalancesAction({ userId: userId }));

    });
}
  ngOnDestroy(): void {

  }
}
