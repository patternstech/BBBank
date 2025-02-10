import { Injectable } from '@angular/core';
import { AppUser } from '../models/app-user';
import { delay, Observable, of } from 'rxjs';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
   loggedInUser = {
    id: 'aa45e3c9-261d-41fe-a1b0-5b4dcf79cfd3',
    firstName: 'Muhammad',
    lastName: 'Ali',
    username: 'mali',
    roles: ['account-holder']
  } as AppUser; 
   /* loggedInUser = {
    firstName: 'Imran',
    lastName: 'Khan',
    username: 'ikhan',
    roles: ['bank-manager']
  } as AppUser;  */
  constructor(private router: Router) { }
  login(): Observable<AppUser> {
    localStorage.setItem('loggedInUser', JSON.stringify(this.loggedInUser));
    return of(this.loggedInUser).pipe(
      delay(1000)
    );
  }
  logout(): void {
    localStorage.removeItem('loggedInUser');
    this.router.navigate(['/login'])
    .then(() => {
      window.location.reload();
    });
  }

}
