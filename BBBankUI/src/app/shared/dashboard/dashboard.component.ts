import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Last12MonthGraphComponent } from "./graphs/last12-month-graph/last12-month-graph.component";
import { SharedState } from '../store/shared.reducers';
import { Store } from '@ngrx/store';
import { loadAllTransactionsAction, loadLast12MonthsBalancesAction } from '../store/dashboard.actions';
import { AppState } from '../../store/appstate.reducers';
import { Subscription } from 'rxjs';
import { loggedInUserIdSelector } from '../../store/auth.selectors';
import { Last6MonthsGraphComponent } from './graphs/last6-months-graph/last6-months-graph.component';
import { DepositWithrawCountGraphComponent } from './graphs/deposit-withraw-count-graph/deposit-withraw-count-graph.component';
import { DepositWithrawTotalsGraphComponent } from './graphs/deposit-withraw-totals-graph/deposit-withraw-totals-graph.component';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, Last12MonthGraphComponent, Last6MonthsGraphComponent, DepositWithrawCountGraphComponent, DepositWithrawTotalsGraphComponent],
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
        this.sharedStore.dispatch(loadAllTransactionsAction({ userId: userId }));
      });
  }
  ngOnDestroy(): void {
    this.loggedInUserSub.unsubscribe();
  }
}
