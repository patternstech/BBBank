import { Routes } from '@angular/router';
import { DashboardComponent } from './shared/dashboard/dashboard.component';
import { LoginComponent } from './login/login.component';

import {  MsalGuard } from '@azure/msal-angular';

export const routes: Routes = [
    { path: '', component: DashboardComponent, canActivate: [MsalGuard] },
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
