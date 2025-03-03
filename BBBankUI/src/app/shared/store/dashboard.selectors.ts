import { createFeatureSelector, createSelector } from "@ngrx/store";
import { SharedState } from "./shared.reducers";
import { TransactionType } from "../../models/transaction";

export const selectSharedState = createFeatureSelector<SharedState>('shared');

export const last12MonthsBalancesSelector = createSelector(
  selectSharedState,
  (sharedState) => sharedState.last12MonthsBalances);

export const last6MonthsBalancesSelector = createSelector(
  selectSharedState,
  ({ last12MonthsBalances }) => {
    if (!last12MonthsBalances) return null;

    const { figures, labels, ...rest } = last12MonthsBalances;

    return {
      ...rest,
      figures: figures.slice(-6),
      labels: labels.slice(-6),
    };
  }
);


export const createTransactionCountSelector = (transactionType: TransactionType) =>
  createSelector(selectSharedState, (sharedState) => {
    if (sharedState.transactions != null) {
      return sharedState.transactions.filter(
        (transaction) => transaction.transactionType === transactionType
      ).length;
    }
    return null;
  });

export const depositsCountSelector = createTransactionCountSelector(
  TransactionType.Deposit
);

export const withdrawalsCountSelector = createTransactionCountSelector(
  TransactionType.Withdraw
);


export const createTransactionTotalSelector = (transactionType: TransactionType) =>
  createSelector(selectSharedState, (sharedState) => {
    if (sharedState.transactions != null) {
      const filteredTransactions = sharedState.transactions.filter(
        (transaction) => transaction.transactionType === transactionType
      );
      return filteredTransactions.reduce(
        (acc, curr) => acc + curr.transactionAmount,
        0
      );
    }
    return null;
  });

export const withdrawalsTotalSelector = createTransactionTotalSelector(
  TransactionType.Withdraw
);

export const depositsTotalSelector = createTransactionTotalSelector(
  TransactionType.Deposit
);