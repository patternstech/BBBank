import { createAction, props } from "@ngrx/store";
import { LineGraphData } from "../../models/line-graph-data";
import { Transaction } from "../../models/transaction";

export const loadLast12MonthsBalancesAction = createAction(
    '[DashboardComponent] Load Last 12 Month Balances',
    props<{ userId: string }>()
  );

  export const last12MonthsBalancesLoadedAction = createAction(
    '[DashBoard Effect] Last 12 Month Balances Loaded',
    props<{ lineGraphData: LineGraphData }>(),
  );

  export const last12MonthsBalancesLoadErrorAction = createAction(
    '[DashBoard Effect] Last 12 Month Balances Load Error'
  );


  export const loadAllTransactionsAction = createAction(
    '[DashBoardComponent] Load All Transactions',
    props<{ userId: string }>()
  );

  export const allTransactionLoadedAction = createAction(
    '[DashBoard Effect] All Transactions Loaded',
    props<{ transactions: Transaction[] }>(),
  );

  export const loadAllTransactionsErrorAction = createAction(
    '[DashBoard Effect] Error Loading Transactions'
  );