import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Account } from '../../models/account';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import { ApiResponse } from '../../models/api-response';
import { AccountsListResponse } from '../models/accounts-list-response';

@Injectable({
  providedIn: 'root'
})
export class AccountsService {

  constructor(private httpClient: HttpClient) { }
  openAccount(account: Account): Observable<ApiResponse> {
    const headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
    return this.httpClient.post<ApiResponse>(`${environment.apiBaseUrl}Accounts/OpenAccount`, JSON.stringify(account), headers);
  }
  getAllAccountsPaginated(pageIndex: number, pageSize: number): Observable<ApiResponse<AccountsListResponse>> {
    return this.httpClient.get<ApiResponse<AccountsListResponse>>
      (`${environment.apiBaseUrl}Accounts/GetAllAccountsPaginated?pageIndex=${pageIndex}&pageSize=${pageSize}`);
  }
  updateAccount(account: Account): Observable<ApiResponse> {
    const headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
    return this.httpClient.put<ApiResponse>(`${environment.apiBaseUrl}Accounts/UpdateAccount`, JSON.stringify(account), headers);
  }
  deleteAccount(accountId: string): Observable<ApiResponse> {
    return this.httpClient.delete<ApiResponse>(`${environment.apiBaseUrl}Accounts/DeleteAccount/${accountId}`);
  }
}
