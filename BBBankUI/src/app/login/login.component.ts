import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { userInfo } from 'os';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(private authService: AuthService, private router: Router) { }
  login() {
    this.authService.login()
      .subscribe(user => {
        console.log("Logged In User: " + user);
        if (user) {
          this.router.navigate(['/'])
            .then(() => {
              window.location.reload();
            });
        }

      });
  }
}
