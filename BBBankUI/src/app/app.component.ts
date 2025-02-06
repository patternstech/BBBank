import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ToolbarComponent } from "./shared/toolbar/toolbar.component";
import {MatSidenavModule} from '@angular/material/sidenav';
import { SideNavComponent } from "./shared/side-nav/side-nav.component";
import { SharedModule } from './shared/shared.module';


@Component({
  selector: 'app-root',
  imports: [SharedModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'BBBankUI';
}
