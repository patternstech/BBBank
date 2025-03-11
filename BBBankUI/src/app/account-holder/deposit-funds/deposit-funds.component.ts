import { Component, OnInit } from '@angular/core';
import { AccountByUserInfo } from '../models/account-by-userInfo';
import { AppState } from '../../store/appstate.reducers';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { loggedInUserIdSelector } from '../../store/auth.selectors';
import { AccountsService } from '../services/accounts.service';
import { ToastrService } from 'ngx-toastr';
import { ApiResponse } from '../../models/api-response';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { DepositRequest } from '../models/deposit-request';
import { FormsModule } from '@angular/forms';
import { TransactionService } from '../services/transaction.service';

@Component({
  selector: 'app-deposit-funds',
  imports: [CommonModule, FormsModule],
  providers: [CurrencyPipe],
  templateUrl: './deposit-funds.component.html',
  styleUrl: './deposit-funds.component.css'
})
export class DepositFundsComponent implements OnInit {
  loggedInUserSub: Subscription;
  accountByUserInfo: AccountByUserInfo;
  loggedInUserId: string;
  depositRequest: DepositRequest;
  constructor(private globalStore: Store<AppState>, private accountsService: AccountsService, private toastrService: ToastrService, private transactionService: TransactionService) {
    this.depositRequest = new DepositRequest();
   }
  ngOnInit(): void {
    this.loggedInUserSub = this.globalStore.select(loggedInUserIdSelector)
      .subscribe((userId: string) => {
        this.loggedInUserId = userId;
      });

    this.accountsService.getAccountByUserInfo(this.loggedInUserId)
      .subscribe({
        next: (response: ApiResponse<AccountByUserInfo>) => {
          this.accountByUserInfo = response.result?.data;
          this.toastrService.success(response.result.message, "Account");
        },
        error: (error) => {
          console.log(error);

        },
      });
  }

  deposit(){
    this.depositRequest.accountNumber = this.accountByUserInfo.accountNumber;
    this.transactionService
    .deposit(this.depositRequest)
    .subscribe({
      next: (data) => {
        this.toastrService.success(data.result.message, 'BBBank');
      },
      error: (errorResponse) => {
        const ValidationErrors = errorResponse?.error?.responseException?.exceptionMessage?.errors;
        if (ValidationErrors) {
          Object.keys(ValidationErrors).forEach((key) => {
            ValidationErrors[key].forEach((msg: string) => {
              this.toastrService.error(`${key}: ${msg}`, 'Validation Error');
            });
          });
        } else {
          this.toastrService.error('An unexpected error occurred.', 'Error');
        }
      },
    });
  }
}
