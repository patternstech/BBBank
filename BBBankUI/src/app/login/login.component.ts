import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { userInfo } from 'os';
import { Router } from '@angular/router';
import { MsalBroadcastService, MsalService } from '@azure/msal-angular';
import { AuthenticationResult, EventMessage, EventType } from '@azure/msal-browser';
import { filter, Subject, takeUntil } from 'rxjs';
import { AppUser } from '../models/app-user';
import { jwtDecode } from "jwt-decode";
import { loginRequest } from '../auth-config';

@Component({
  selector: 'app-login',
  imports: [],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(private router: Router) { }

  private isLoginInProgress = false;

   login() {
    this.router.navigate(['/'])
    .then(() => {
      window.location.reload();
    });
  }
}

