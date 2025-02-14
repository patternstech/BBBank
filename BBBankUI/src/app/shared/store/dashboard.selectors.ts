import { createFeatureSelector, createSelector } from "@ngrx/store";
import { SharedState } from "./shared.reducers";

export const selectSharedState = createFeatureSelector<SharedState>('shared');

export const last12MonthsBalancesSelector = createSelector(
    selectSharedState,
      (sharedState) => sharedState.last12MonthsBalances);
