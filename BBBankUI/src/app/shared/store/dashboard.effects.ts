import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { TransactionService } from "../../services/transaction.service";
import { last12MonthsBalancesLoadedAction, last12MonthsBalancesLoadErrorAction, loadLast12MonthsBalancesAction } from "./dashboard.actions";
import { catchError, concatMap, map, of } from "rxjs";

@Injectable()
export class DashBoardEffects {
    loadLast12MonthsBalances$
    constructor(
        private actions$: Actions,
        private transactionService: TransactionService) {
        this.loadLast12MonthsBalances$ = createEffect(() =>
            this.actions$.pipe(
                ofType(loadLast12MonthsBalancesAction),
                concatMap((action) =>
                    this.transactionService.getLast12MonthBalances(action.userId).pipe(
                        map((lineGraphData) =>
                            last12MonthsBalancesLoadedAction({ lineGraphData })
                        ),
                        catchError((err) => {
                            return of(last12MonthsBalancesLoadErrorAction());
                        })
                    )
                )
            )
        );
    }
}