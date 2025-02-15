import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EntraIdUser } from '../models/entra-id-user';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class EntraIdService {

  constructor(private httpClient: HttpClient) { }

  getAzureAdUsersList(): Observable<EntraIdUser[]> {
    return this.httpClient.post<EntraIdUser[]>(environment.entraIdUsersUrl, {});
  }
}
