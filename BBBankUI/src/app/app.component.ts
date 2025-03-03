import { Component, OnDestroy, OnInit } from '@angular/core';
import { SharedModule } from './shared/shared.module';
import { MsalBroadcastService, MsalService } from '@azure/msal-angular';
import { filter, Subject, takeUntil } from 'rxjs';
import { AuthenticationResult, EventMessage, EventType } from '@azure/msal-browser';
import { AppUser } from './models/app-user';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { AppState } from './store/appstate.reducers';
import { Store } from '@ngrx/store';
import { appLoadAction, loginSuccessAction } from './store/auth.actions';
import { SignalrService } from './services/signalr.service';


@Component({
  selector: 'app-root',
  imports: [SharedModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'BBBankUI';
  loggedInUser: AppUser;
  private readonly _destroying$ = new Subject<void>();
  constructor(private authService: MsalService, private msalBroadcastService: MsalBroadcastService, private router: Router, private store: Store<AppState>, private signalRService: SignalrService) { }
  ngOnInit(): void {

    this.signalRService.connectToUpdateHub();
    const loggedInUser = localStorage.getItem('loggedInUser');

    if (loggedInUser) {

      this.store.dispatch(
        appLoadAction({ loggedInUser: JSON.parse(loggedInUser) })
      );
    }
    this.msalBroadcastService.msalSubject$
      .pipe(
        filter((msg: EventMessage) => msg.eventType === EventType.LOGIN_SUCCESS),
        takeUntil(this._destroying$),
      )
      .subscribe({
        next: (result: EventMessage) => {
          const payload = result.payload as AuthenticationResult;
          this.authService.instance.setActiveAccount(payload.account);
          if (payload.account.homeAccountId) {
            this.setLoggedInUser(payload.accessToken);
          }
        },
        error: (error) => {
          console.log(error);
        },
        complete: () => {
          this.store.dispatch(loginSuccessAction({ loggedInUser: this.loggedInUser }));
        }
      });
  }
  setLoggedInUser(accessToken: any) {
    //decoding the token and setting up values of logged in user.
    const tokenInfo = this.getDecodedAccessToken(accessToken);
    this.loggedInUser = {
      id: tokenInfo.oid,
      firstName: tokenInfo.given_name,
      lastName: tokenInfo.family_name,
      username: tokenInfo.unique_name,
      email: tokenInfo.email,
      roles: tokenInfo.roles,
    } as AppUser;
    if (this.loggedInUser.roles.includes('bank-manager')) {
      this.loggedInUser.id = null;
    }
    if (typeof localStorage !== 'undefined') {
      localStorage.setItem('access_token', accessToken);
      localStorage.setItem('loggedInUser', JSON.stringify(this.loggedInUser));
    }
    this.router.navigate(['/'])
      .then(() => {
        window.location.reload();
      });
  }
  getDecodedAccessToken(token: string): any {
    try {
      return jwtDecode(token);
    } catch (Error) {
      return null;
    }
  }
  ngOnDestroy(): void {
    this._destroying$.next(undefined);
    this._destroying$.complete();
  }
}
