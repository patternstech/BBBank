import { createReducer, on } from "@ngrx/store";
import { LineGraphData } from "../../models/line-graph-data";
import { last12MonthsBalancesLoadedAction, loadLast12MonthsBalancesAction } from "./dashboard.actions";

export const sharedFeatureKey = 'shared';
export interface SharedState {
    last12MonthsBalances: LineGraphData;
  }
export const initialSharedState: SharedState = {
    last12MonthsBalances: null
  };

  export const sharedReducer = createReducer(
    initialSharedState,
    on(last12MonthsBalancesLoadedAction, (state, action) => {
        return {
          last12MonthsBalances: action.lineGraphData
        };
      })
);