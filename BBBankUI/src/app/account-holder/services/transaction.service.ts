import { Injectable } from '@angular/core';
import { TransferRequest } from '../../models/transfer-request';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import { ApiResponse } from '../../models/api-response';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  constructor(private httpClient: HttpClient) { }

    transferFunds(transferRequest: TransferRequest): Observable<ApiResponse> {
      const headers = {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
        })
      }
      return this.httpClient.post<ApiResponse>(`${environment.apiBaseUrl}Transaction/TransferFunds`, JSON.stringify(transferRequest), headers);
    }
}
