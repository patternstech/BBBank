import { Component, Input, OnInit } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { AppUser } from '../../models/app-user';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';

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
  constructor(private authService: AuthService) {

   }
   ngOnInit(): void {
    if (typeof localStorage !== 'undefined') {
      const userData = localStorage.getItem('loggedInUser');
      this.loggedInUser = userData ? JSON.parse(userData) : undefined;
      this.isUserLoggedIn = this.loggedInUser != undefined;
    }
  }
  logout(): void {
    this.authService.logout();
  }
}
