import { Component } from '@angular/core';

import { Router } from '@angular/router';

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

