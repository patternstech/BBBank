import { ResolveFn } from '@angular/router';
import { Store } from '@ngrx/store';
import { AppState } from '../store/appstate.reducers';
import { inject } from '@angular/core';
import { loggedInUserIdSelector } from '../store/auth.selectors';
import { AccountsService } from './services/accounts.service';
import { AccountByUserInfo } from './models/account-by-userInfo';
import { ApiResponse } from '../models/api-response';
import { ToastrService } from 'ngx-toastr';
import { catchError, map, switchMap } from 'rxjs';

export const transferResolver: ResolveFn<AccountByUserInfo | undefined> = () => {
  const accountsService = inject(AccountsService);
  const store = inject(Store<AppState>);
  const toastrService = inject(ToastrService);
  return store.select(loggedInUserIdSelector).pipe(
    switchMap((loggedInUserId: string | null) => {
      return accountsService.getAccountByUserInfo(loggedInUserId).pipe(
        map((response: ApiResponse<AccountByUserInfo>) => {
          if (response?.result?.data) {
            return response.result.data;
          } else {
            toastrService.error('Account info not found.');
            return null;
          }
        }),
        catchError(() => {
          toastrService.error('Error fetching logged-in user account info.');
          throw new Error('Failed to fetch data. Navigation canceled.');
        })
      );
    })
  );
}
