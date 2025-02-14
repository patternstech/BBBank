import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSidenavModule } from '@angular/material/sidenav';
import { RouterOutlet } from '@angular/router';
import { SideNavComponent } from './side-nav/side-nav.component';
import { ToolbarComponent } from './toolbar/toolbar.component';
import { provideState, StoreModule } from '@ngrx/store';
import { sharedFeatureKey, sharedReducer } from './store/shared.reducers';
import { EffectsModule, provideEffects } from '@ngrx/effects';
import { DashBoardEffects } from './store/dashboard.effects';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterOutlet, ToolbarComponent, MatSidenavModule, SideNavComponent 
  ],
  exports: [
    RouterOutlet, ToolbarComponent, MatSidenavModule, SideNavComponent
  ],
  providers: [
    provideState({ name: sharedFeatureKey, reducer: sharedReducer }),
    provideEffects([DashBoardEffects])
  ],
})
export class SharedModule { }
