import { Component, Input, OnInit } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { AppUser } from '../../models/app-user';
import { CommonModule } from '@angular/common';
import { MsalService } from '@azure/msal-angular';

@Component({
  selector: 'app-toolbar',
  imports: [CommonModule],
  templateUrl: './toolbar.component.html',
  styleUrl: './toolbar.component.css'
})
export class ToolbarComponent implements OnInit {
  @Input() inputSideNav:MatSidenav | undefined
  loggedInUser?: AppUser;
  isUserLoggedIn: boolean;
  constructor(private authService: MsalService) {

   }
   ngOnInit(): void {
    if (typeof localStorage !== 'undefined') {
      const userData = localStorage.getItem('loggedInUser');
      this.loggedInUser = userData ? JSON.parse(userData) : undefined;
      this.isUserLoggedIn = this.loggedInUser != undefined;
    }
  }
  logout(): void {
    if (typeof localStorage !== 'undefined' ) {
      localStorage.removeItem('loggedInUser');
    }
    this.authService.logoutRedirect({
      postLogoutRedirectUri: '/login' // ✅ Redirect to a specific component
    });
  }
}
