import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Account } from '../../models/account';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import { ApiResponse } from '../../models/api-response';

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
}
