import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ToolbarComponent } from "./toolbar/toolbar.component";
import {MatSidenavModule} from '@angular/material/sidenav';
import { SideNavComponent } from "./side-nav/side-nav.component";


@Component({
  selector: 'app-root',
  imports: [RouterOutlet, ToolbarComponent, MatSidenavModule, SideNavComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'BBBankUI';
}
