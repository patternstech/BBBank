import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { LineGraphData } from '../models/line-graph-data';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  constructor(private httpClient: HttpClient) { }
  getLast12MonthBalances(userId: string): Observable<LineGraphData> {
    if (userId)
      return this.httpClient.get<LineGraphData>(`${environment.apiBaseUrl}Transaction/GetLast12MonthBalances/${userId}`);
    else
      return this.httpClient.get<LineGraphData>(`${environment.apiBaseUrl}Transaction/GetLast12MonthBalances`);
  }
}
