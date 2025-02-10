import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSidenavModule } from '@angular/material/sidenav';
import { RouterOutlet } from '@angular/router';
import { SideNavComponent } from './side-nav/side-nav.component';
import { ToolbarComponent } from './toolbar/toolbar.component';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterOutlet, ToolbarComponent, MatSidenavModule, SideNavComponent 
  ],
  exports: [
    RouterOutlet, ToolbarComponent, MatSidenavModule, SideNavComponent
  ]
})
export class SharedModule { }
