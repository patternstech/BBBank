import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-side-nav',
  imports: [RouterModule, CommonModule],
  templateUrl: './side-nav.component.html',
  styleUrl: './side-nav.component.css'
})
export class SideNavComponent {
  loggedInUserRole: string;
  ngOnInit(): void {
    if (typeof localStorage !== 'undefined') {
    this.loggedInUserRole = JSON.parse(localStorage.getItem('loggedInUser') || '{}').roles[0];
    }
  }
}
