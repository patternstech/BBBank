import { createAction, props } from "@ngrx/store";
import { AppUser } from "../models/app-user";

export const loginSuccessAction = createAction(
    '[App Component] Login Success',
    props<{ loggedInUser: AppUser }>());

    export const logoutAction = createAction(
        '[Top Menu] Logout',
    );
    export const appLoadAction = createAction(
        '[App Component] App Load',
        props<{ loggedInUser: AppUser }>(),
    );