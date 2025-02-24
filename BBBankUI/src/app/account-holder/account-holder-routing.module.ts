import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from '../shared/dashboard/dashboard.component';
import { DepositFundsComponent } from './deposit-funds/deposit-funds.component';
import { TransferFundsComponent } from './transfer-funds/transfer-funds.component';
import { accountHolderGuard } from '../guards/account-holder.guard';
import { transferResolver } from './transfer.resolver';

const routes: Routes = [
  { path: '', component: DashboardComponent },  // Default view
  { path: 'deposit-funds', component: DepositFundsComponent, canActivate: [accountHolderGuard] },
  { path: 'transfer-funds', component: TransferFundsComponent, canActivate: [accountHolderGuard], resolve: { loggedInUserAccountInfo: transferResolver } }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountHolderRoutingModule { }
