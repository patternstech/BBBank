import { createReducer, on } from "@ngrx/store";
import { LineGraphData } from "../../models/line-graph-data";
import { allTransactionLoadedAction, last12MonthsBalancesLoadedAction, loadAllTransactionsAction, loadLast12MonthsBalancesAction } from "./dashboard.actions";
import { Transaction } from "../../models/transaction";

export const sharedFeatureKey = 'shared';
export interface SharedState {
  last12MonthsBalances: LineGraphData;
  transactions: Transaction[];
}
export const initialSharedState: SharedState = {
  last12MonthsBalances: null,
  transactions: null
};

export const sharedReducer = createReducer(
  initialSharedState,
  on(last12MonthsBalancesLoadedAction, (state, action) => {
    return {
      ...state,
      last12MonthsBalances: action.lineGraphData,
    };
  }),
  on(allTransactionLoadedAction, (state, action) => {
    return {
      ...state,
      transactions: action.transactions,
    };
  })
);