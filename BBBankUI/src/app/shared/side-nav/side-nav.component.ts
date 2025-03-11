import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Observable } from 'rxjs';
import { AppState } from '../../store/appstate.reducers';
import { select, Store } from '@ngrx/store';
import { isLoggedInUserAccountHolderSelector, isLoggedInUserManagerSelector } from '../../store/auth.selectors';

@Component({
  selector: 'app-side-nav',
  imports: [RouterModule, CommonModule],
  templateUrl: './side-nav.component.html',
  styleUrl: './side-nav.component.css'
})
export class SideNavComponent implements OnInit {
  constructor(private store: Store<AppState>) { }
  isLoggedInUserManager$: Observable<boolean>;
  isLoggedInUserAccountHolder$: Observable<boolean>;
  loggedInUserRole: string;
  ngOnInit(): void {
    this.isLoggedInUserManager$ = this.store.pipe(
      select(isLoggedInUserManagerSelector)
    );
    this.isLoggedInUserAccountHolder$ = this.store.pipe(
      select(isLoggedInUserAccountHolderSelector)
    );
  }
}
