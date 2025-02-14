import { ActionReducerMap, createReducer, on } from "@ngrx/store";
import { AppUser } from "../models/app-user";
import { appLoadAction, loginSuccessAction, logoutAction } from "./auth.actions";

export interface AppState {
    loggedInUser: AppUser | undefined
}

export const initialGlobalState: AppState = {
    loggedInUser: undefined,
};

export const authReducer = createReducer(
    initialGlobalState,
    on(loginSuccessAction, (state, action) => {
        return {
            loggedInUser: action.loggedInUser,
        };
    }),
    on(logoutAction, (state, action) => {
        return {
            loggedInUser: undefined as AppUser | undefined,
        };
    }),
    on(appLoadAction, (state, action) => {
        return {
            loggedInUser: action.loggedInUser,
        };
    }),
);
export const reducers: ActionReducerMap<{ globalState: AppState }> = {
    globalState: authReducer,
};