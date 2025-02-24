import { Injectable } from '@angular/core';
import { ApiResponse } from '../../models/api-response';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment.development';
import { DepositRequest } from '../../models/deposit-request';

@Injectable({
  providedIn: 'root'
})
export class AccountsService {

  constructor(private httpClient: HttpClient) { }

  getAccountByUserInfo(userId: string): Observable<ApiResponse> {
    return this.httpClient.get<ApiResponse>(`${environment.apiBaseUrl}Accounts/GetAccountInfoByUser/${userId}`);
  }
  getAccountByAccountNumber(accountNumber: string): Observable<ApiResponse> {
    return this.httpClient.get<ApiResponse>(`${environment.apiBaseUrl}Accounts/GetAccountInfoByAccountNumber/${accountNumber}`);
  }
  deposit(depositRequest: DepositRequest): Observable<ApiResponse> {
    const headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
    return this.httpClient.post<ApiResponse>(`${environment.apiBaseUrl}Accounts/Deposit`, JSON.stringify(depositRequest), headers);
  }
}
