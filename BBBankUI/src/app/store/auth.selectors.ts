import { createFeatureSelector, createSelector } from "@ngrx/store";
import { AppState } from "./appstate.reducers";

export const selectGlobalState = createFeatureSelector<AppState>('globalState');

export const isLoggedInSelector = createSelector(
    selectGlobalState,
    (globalState) => !!globalState.loggedInUser);

    export const loggedInUserSelector = createSelector(
        selectGlobalState,
        (globalState) => globalState.loggedInUser
    );

    export const loggedInUserRoleSelector = createSelector(
        selectGlobalState,
        (globalState) => globalState.loggedInUser?.roles[0]
    );

    export const isLoggedInUserManagerSelector = createSelector(
        selectGlobalState,
        (globalState) => globalState.loggedInUser?.roles.includes('bank-manager')
    );

    export const isLoggedInUserAccountHolderSelector = createSelector(
        selectGlobalState,
        (globalState) => globalState.loggedInUser?.roles.includes('account-holder')
    );

    export const loggedInUserNameSelector = createSelector(
        selectGlobalState,
        (globalState) => globalState.loggedInUser?.firstName + ' ' + globalState.loggedInUser?.lastName
    );

    export const loggedInUserIdSelector = createSelector(
        selectGlobalState,
        (globalState) => globalState.loggedInUser?.roles.includes('bank-manager') ? null : globalState.loggedInUser?.id
    );