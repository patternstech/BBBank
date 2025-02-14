import { Injectable } from "@angular/core";
import { loginSuccessAction, logoutAction } from "./auth.actions";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { tap } from "rxjs";
import { MsalService } from "@azure/msal-angular";

@Injectable()
export class AuthEffects {
    login$;
    logout$;
    constructor(private actions$: Actions, private authService: MsalService) {
        this.login$ = createEffect(() => this.actions$.pipe(
            ofType(loginSuccessAction),
            tap(action => {
                localStorage.setItem('loggedInUser', JSON.stringify(action.loggedInUser));
            })
        ), { dispatch: false });

        this.logout$ = createEffect(() => this.actions$.pipe(
            ofType(logoutAction),
            tap(() => {
                localStorage.removeItem('loggedInUser');
                this.authService.logout();
            })
        ), { dispatch: false });
    }
}
