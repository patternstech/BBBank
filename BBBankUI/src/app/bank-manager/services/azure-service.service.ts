import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AzureAccessService {

  constructor(private httpClient: HttpClient) { }

  uploadImageToBlob(imageData: FormData, fileName: string): Observable<any> {
    return this.httpClient.post(`${environment.profilePicUploadEndpoint}${fileName}`, imageData);    
  }
}
