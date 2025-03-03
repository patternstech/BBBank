import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { AppState } from '../store/appstate.reducers';
import { loggedInUserSelector } from '../store/auth.selectors';
import { HttpTransportType, HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { environment } from '../../environments/environment.development';
import { ToastrService } from 'ngx-toastr';
import { SharedState } from '../shared/store/shared.reducers';
import { loadLast12MonthsBalancesAction, loadAllTransactionsAction } from '../shared/store/dashboard.actions';
import { AppUser } from '../models/app-user';


@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  loggedInUser: AppUser;
  loggedInUserSub: Subscription;
  private hubConnection: HubConnection;
  constructor(private globalStore: Store<AppState>, private toastrService: ToastrService, private sharedStore: Store<SharedState>) {

    this.loggedInUserSub = this.globalStore.select(loggedInUserSelector)
      .subscribe((user: AppUser) => {
        this.loggedInUser = user;
      });
  }

  public connectToUpdateHub = () => {
    this.hubConnection = new HubConnectionBuilder()
  .withUrl(environment.apiBaseUrl + 'graphUpdates', {
    accessTokenFactory: () => localStorage.getItem('access_token') ?? ''
  })
  .withAutomaticReconnect()
  .build();
  
    this.hubConnection
      .start()
      .then(() => {
        this.toastrService.info('SignalR Connection Established', "BBBank");
        this.listenForGraphUpdates(); 
    })
      .catch(err => this.toastrService.error('Error while starting connection: ' + err, "BBBank"));
  }

  private listenForGraphUpdates() {
    this.hubConnection.on('updateGraphsData', () => {
      if (this.loggedInUser?.roles.includes('bank-manager')) {
        this.sharedStore.dispatch(loadLast12MonthsBalancesAction({ userId: null }));
        this.sharedStore.dispatch(loadAllTransactionsAction({ userId: null }));
      }
      if (this.loggedInUser?.roles.includes('account-holder')) {
        this.sharedStore.dispatch(loadLast12MonthsBalancesAction({ userId: this.loggedInUser.id }));
        this.sharedStore.dispatch(loadAllTransactionsAction({ userId: this.loggedInUser.id }));
      }
    });
  }
}
