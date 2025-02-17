import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { EntraIdUser } from '../../models/entra-id-user';
import { environment } from '../../../environments/environment.development';


@Injectable({
  providedIn: 'root'
})
export class EntraIdService {

  constructor(private httpClient: HttpClient) { }

  getAzureAdUsersList(): Observable<EntraIdUser[]> {
    return this.httpClient.post<EntraIdUser[]>(environment.entraIdUsersUrl, {});
  }
}
