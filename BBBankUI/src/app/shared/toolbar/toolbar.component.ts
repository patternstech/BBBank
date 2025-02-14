import { Component, Input, OnInit } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { AppUser } from '../../models/app-user';
import { CommonModule } from '@angular/common';
import { MsalService } from '@azure/msal-angular';
import { AppState } from '../../store/appstate.reducers';
import { select, Store } from '@ngrx/store';
import { Observable, Subscription } from 'rxjs';
import { isLoggedInSelector, loggedInUserNameSelector } from '../../store/auth.selectors';
import { logoutAction } from '../../store/auth.actions';

@Component({
  selector: 'app-toolbar',
  imports: [CommonModule],
  templateUrl: './toolbar.component.html',
  styleUrl: './toolbar.component.css'
})
export class ToolbarComponent implements OnInit {
  @Input() inputSideNav: MatSidenav | undefined
  loggedInUser?: AppUser;
  isUserLoggedIn: boolean;
  isUserLoggedIn$: Observable<boolean>;
  sub: Subscription;
  fullName$: Observable<string>;
  //fullName: string;
  constructor(private authService: MsalService, private store: Store<AppState>) {

  }
  ngOnInit(): void {
    this.fullName$ = this.store.pipe(select(loggedInUserNameSelector));
    this.isUserLoggedIn$ = this.store.pipe(select(isLoggedInSelector));
/*     this.sub = this.store.select(loggedInUserName)
      .subscribe((loggedInUserName: any) => {
        if (loggedInUserName != null) {
          this.fullName = loggedInUserName;
        }
      }); */
/*     this.sub = this.store.select(isLoggedIn)
      .subscribe((isLoggedIn: any) => {
        if (isLoggedIn != null) {
          this.isUserLoggedIn = isLoggedIn;
        }
      }); */
  }
  logout(): void {
    this.store.dispatch(logoutAction());
/*     if (typeof localStorage !== 'undefined') {
      localStorage.removeItem('loggedInUser');
    }
    this.authService.logoutRedirect({
      postLogoutRedirectUri: '/login'
    }); */
  }
/*   ngOnDestroy(): void {
    this.sub.unsubscribe();
  } */
}
