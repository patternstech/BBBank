import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { TransferFundsComponent } from './transfer-funds.component';
import { ActivatedRoute } from '@angular/router';
import { of, throwError } from 'rxjs';
import { AccountsService } from '../services/accounts.service';
import { TransactionService } from '../services/transaction.service';
import { ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
import { CurrencyPipe, CommonModule } from '@angular/common';

import { AccountInfo } from '../models/account-by-userInfo';
import { ApiResponse } from '../../models/api-response';
import { provideHttpClient } from '@angular/common/http';

fdescribe('TransferFundsComponent', () => {
  let component: TransferFundsComponent;
  let fixture: ComponentFixture<TransferFundsComponent>;
  let accountsServiceSpy: jasmine.SpyObj<AccountsService>;
  let transactionServiceSpy: jasmine.SpyObj<TransactionService>;
  let toastrServiceSpy: jasmine.SpyObj<ToastrService>;

  beforeEach(async () => {
    accountsServiceSpy = jasmine.createSpyObj('AccountsService', ['getAccountByAccountNumber']);
    transactionServiceSpy = jasmine.createSpyObj('TransactionService', ['transferFunds']);
    toastrServiceSpy = jasmine.createSpyObj('ToastrService', ['success', 'error']);

    await TestBed.configureTestingModule({
      imports: [TransferFundsComponent, FormsModule, CommonModule],  // ✅ Use imports instead of declarations
      providers: [
        { provide: AccountsService, useValue: accountsServiceSpy },
        { provide: TransactionService, useValue: transactionServiceSpy },
        { provide: ToastrService, useValue: toastrServiceSpy },
        { provide: ActivatedRoute, useValue: { data: of({ loggedInUserAccountInfo: { accountNumber: '12345' } }) } },
        provideHttpClient(), // ✅ Required for HttpClient in Angular 16+
      ],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TransferFundsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should initialize with logged-in user account info', () => {
    expect(component.loggedInUserAccountInfo.accountNumber).toBe('12345');
  });

  it('should fetch receiver account details on input', fakeAsync(() => {
    const mockResponse: ApiResponse<any> = {
      statusCode: 200,
      message: 'Success',
      result: {
        message: 'Account found',
        data: {
          accountNumber: '67890',
          accountTitle: 'John Doe',
          currentBalance: 5000,
          accountStatus: 'Active',
          userImageUrl: 'https://example.com/user.jpg',
        },
      },
      isError: false,
    };

accountsServiceSpy.getAccountByAccountNumber.and.returnValue(of(mockResponse));


    const inputElement = fixture.nativeElement.querySelector('#accountInput');
    inputElement.value = '67890';
    inputElement.dispatchEvent(new Event('input'));

    tick(2000); // Simulate debounceTime

    expect(accountsServiceSpy.getAccountByAccountNumber).toHaveBeenCalledWith('67890');
    expect(component.receiverAccountInfo.accountNumber).toBe('67890');
  }));

  it('should handle API errors when fetching receiver account', fakeAsync(() => {
    const errorResponse: ApiResponse = {
      statusCode: 404,
      message: 'Account not found',
      isError: true,
      responseException: {
        exceptionMessage: 'No account found with this number',
      },
    };

    accountsServiceSpy.getAccountByAccountNumber.and.returnValue(throwError(() => errorResponse));

    const inputElement = fixture.nativeElement.querySelector('#accountInput');
    inputElement.value = '99999';
    inputElement.dispatchEvent(new Event('input'));

    tick(2000);

    expect(accountsServiceSpy.getAccountByAccountNumber).toHaveBeenCalledWith('99999');
    expect(component.receiverAccountInfo).toBeNull();
  }));

  it('should call transferFunds() and show success message on successful transfer', () => {
    component.loggedInUserAccountInfo = {
      accountNumber: '12345',
      accountTitle: 'John Doe',
      currentBalance: 5000,
      accountStatus: 'Active',
      userImageUrl: 'https://example.com/user.jpg',
    };
    component.receiverAccountInfo = {
      accountNumber: '67890',
      accountTitle: 'John Doe',
      currentBalance: 5000,
      accountStatus: 'Active',
      userImageUrl: 'https://example.com/user.jpg',
    };

    const transferResponse: ApiResponse = {
      statusCode: 200,
      message: 'Success',
      result: {
        message: 'Transfer Successful',
        data: null,
      },
      isError: false,
    };

    transactionServiceSpy.transferFunds.and.returnValue(of(transferResponse));

    component.transfer();

    expect(transactionServiceSpy.transferFunds).toHaveBeenCalledWith({
      transferAmount: null,
      senderAccountNumber: '12345',
      receiverAccountNumber: '67890',
    });
    expect(toastrServiceSpy.success).toHaveBeenCalledWith('Transfer Successful', 'BBBank');
  });

  it('should show error message on transfer failure', () => {
    component.loggedInUserAccountInfo = {
      accountNumber: '12345',
      accountTitle: 'Jane Doe',
      currentBalance: 5000,
      accountStatus: 'Active',
      userImageUrl: 'https://example.com/user.jpg',
    };
    component.receiverAccountInfo = {
      accountNumber: '67890',
      accountTitle: 'John Doe',
      currentBalance: 5000,
      accountStatus: 'Active',
      userImageUrl: 'https://example.com/user.jpg',
    };

    const errorResponse: ApiResponse = {
      statusCode: 400,
      message: 'Insufficient funds',
      isError: true,
      errorMessage: 'Balance is too low to complete the transfer',
    };

    transactionServiceSpy.transferFunds.and.returnValue(throwError(() => errorResponse));

    component.transfer();

    expect(transactionServiceSpy.transferFunds).toHaveBeenCalled();
    expect(toastrServiceSpy.error).toHaveBeenCalledWith('Insufficient funds');
  });
});
