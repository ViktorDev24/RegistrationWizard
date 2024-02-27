// app.component.ts
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { RegistrationComponent } from '../registration/registration.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: true,
  imports: [RouterOutlet, RegistrationComponent, HttpClientModule] // Import the RegistrationComponent here
})
export class AppComponent {
  title = 'WizardFrontend';
}
