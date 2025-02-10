import { Routes } from '@angular/router';
import { DashboardComponent } from './shared/dashboard/dashboard.component';
import { CreateAccountComponent } from './bank-manager/create-account/create-account.component';
import { ManageAccountsComponent } from './bank-manager/manage-accounts/manage-accounts.component';
import { DepositFundsComponent } from './account-holder/deposit-funds/deposit-funds.component';
import { TransferFundsComponent } from './account-holder/transfer-funds/transfer-funds.component';
import { LoginComponent } from './login/login.component';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
    { path: '', component: DashboardComponent, canActivate: [authGuard] },
    {
        path: 'bank-manager',
        loadChildren: () => import('./bank-manager/bank-manager.module').then(m => m.BankManagerModule)
    },
    {
        path: 'account-holder',
        loadChildren: () => import('./account-holder/account-holder.module').then(m => m.AccountHolderModule)
    },
    { path: 'login', component: LoginComponent },
    
     
];
