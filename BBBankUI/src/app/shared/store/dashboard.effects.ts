import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { TransactionService } from "../../services/transaction.service";
import { allTransactionLoadedAction, last12MonthsBalancesLoadedAction, last12MonthsBalancesLoadErrorAction, loadAllTransactionsAction, loadAllTransactionsErrorAction, loadLast12MonthsBalancesAction } from "./dashboard.actions";
import { catchError, concatMap, map, of } from "rxjs";

@Injectable()
export class DashBoardEffects {
    loadLast12MonthsBalances$;
    loadAllTransactions$;
    constructor(
        private actions$: Actions,
        private transactionService: TransactionService) {
        this.loadLast12MonthsBalances$ = createEffect(() =>
            this.actions$.pipe(
                ofType(loadLast12MonthsBalancesAction),
                concatMap((action) =>
                    this.transactionService.getLast12MonthBalances(action.userId).pipe(
                        map((response) =>
                            last12MonthsBalancesLoadedAction({ lineGraphData: response.result.data })
                        ),
                        catchError(() => {
                            return of(last12MonthsBalancesLoadErrorAction());
                        })
                    )
                )
            )
        );


        this.loadAllTransactions$ = createEffect(() =>
            this.actions$.pipe(
                ofType(loadAllTransactionsAction),
                concatMap((action) =>
                    this.transactionService.loadAllTransactions(action.userId).pipe(
                        map((response) =>
                            allTransactionLoadedAction({ transactions: response.result.data })
                        ),
                        catchError(() => {
                            return of(loadAllTransactionsErrorAction());
                        })
                    )
                )
            )
        );
    }
}