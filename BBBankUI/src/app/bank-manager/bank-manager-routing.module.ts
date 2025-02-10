import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from '../shared/dashboard/dashboard.component';
import { CreateAccountComponent } from './create-account/create-account.component';
import { ManageAccountsComponent } from './manage-accounts/manage-accounts.component';
import { bankManagerGuard } from '../guards/bank-manager.guard';

const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'create-account', component: CreateAccountComponent, canActivate: [bankManagerGuard] },
  { path: 'manage-accounts', component: ManageAccountsComponent, canActivate: [bankManagerGuard] },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BankManagerRoutingModule { }
