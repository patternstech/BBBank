import { Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CreateAccountComponent } from './create-account/create-account.component';
import { ManageAccountsComponent } from './manage-accounts/manage-accounts.component';
import { DepositFundsComponent } from './deposit-funds/deposit-funds.component';
import { TransferFundsComponent } from './transfer-funds/transfer-funds.component';

export const routes: Routes = [
    { path: '', component: DashboardComponent },
    { path: 'create-account', component: CreateAccountComponent },
    { path: 'manage-accounts', component: ManageAccountsComponent },
    { path: 'deposit-funds', component: DepositFundsComponent },
    { path: 'transfer-funds', component: TransferFundsComponent },
];
