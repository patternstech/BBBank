import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccountInfo } from '../../models/account-by-userInfo';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { catchError, debounceTime, distinctUntilChanged, fromEvent, map, Observable, of, switchMap } from 'rxjs';
import { TransferRequest } from '../../models/transfer-request';
import { AccountsService } from '../services/accounts.service';
import { FormsModule } from '@angular/forms';
import { TransactionService } from '../services/transaction.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-transfer-funds',
  imports: [CurrencyPipe, CommonModule, FormsModule],
  templateUrl: './transfer-funds.component.html',
  styleUrl: './transfer-funds.component.css',
})
export class TransferFundsComponent implements OnInit {
  transferRequest: TransferRequest = {
    transferAmount: null,
    senderAccountNumber: '',
    receiverAccountNumber: ''
  };
  receiverAccountInfo: AccountInfo
  @ViewChild('accountInput', { static: true }) accountInput: ElementRef;
  accountDetails$: Observable<any> = of(null);
  loggedInUserAccountInfo : AccountInfo;
  constructor(private route: ActivatedRoute, private accountsService: AccountsService, private transactionService: TransactionService, private toastrService: ToastrService) {}
  ngOnInit(): void {
    
    this.route.data.subscribe((data) => {
      this.loggedInUserAccountInfo = data['loggedInUserAccountInfo'];
    });

    const inputElement = this.accountInput.nativeElement;
    this.accountDetails$ = fromEvent(inputElement, 'input').pipe(
      map((event: any) => event.target.value),
      debounceTime(2000),
      distinctUntilChanged(),
      switchMap((accountNumber) => {
        if (!accountNumber) {
          return of(null);
        }
        return this.accountsService.getAccountByAccountNumber(accountNumber).pipe(
          catchError(() => {
            this.receiverAccountInfo = null;
            return of(null); 
          })
        );
      })
    );

    this.accountDetails$.subscribe({
      next: (res) => {
        if (res) {
         this.receiverAccountInfo = res.result?.data;
        }
      },
      error: () => {
        this.receiverAccountInfo = null;
      },
    });
  }
  transfer() {

    this.transferRequest.senderAccountNumber = this.loggedInUserAccountInfo.accountNumber;
    this.transferRequest.receiverAccountNumber = this.receiverAccountInfo.accountNumber;
    this.transactionService
    .transferFunds(this.transferRequest)
    .subscribe({
      next: (data) => {
        this.toastrService.success(data.result.message, 'BBBank');
      },
      error: (errorResponse) => {
        this.toastrService.error(errorResponse.message);
      },
    });
  }
}
