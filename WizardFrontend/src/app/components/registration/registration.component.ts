import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule,FormGroup, FormBuilder, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field'; 
import { MatInputModule } from '@angular/material/input'; 
import { MatButtonModule } from '@angular/material/button'; 
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatCardModule } from '@angular/material/card'; 
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { LocationService } from '../../services/location/location.service'; 
import { RegistrationData } from '../../models/registration.model';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
  standalone: true,
  imports: [CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule, 
    MatInputModule, 
    MatButtonModule, 
    MatCheckboxModule,
    MatCardModule,
    MatSelectModule,
    MatSnackBarModule] //
})
export class RegistrationComponent implements OnInit {
  step1Form: FormGroup;
  step2Form: FormGroup;
  countries: any[] = [];
  provinces: any[] = [];
  currentStep = 1;
  showError: boolean = false;

  constructor(private fb: FormBuilder,
    private locationService: LocationService,
    private snackBar: MatSnackBar) {

    const checkPasswords = (control: AbstractControl): ValidationErrors | null => {
      const pass = control.get('password')?.value;
      const confirmPass = control.get('confirmPassword')?.value;
      this.showError = pass !== confirmPass;

      return pass === confirmPass ? null : { notSame: true };
    };
    this.step1Form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.pattern('^(?=.*[0-9])(?=.*[a-zA-Z]).+$')]],
      confirmPassword: ['', Validators.required],
      agreeToTerms: [false, Validators.requiredTrue]
    }, { validators: checkPasswords });

    this.step2Form = this.fb.group({
      country: ['', Validators.required],
      province: ['', Validators.required] 
    });
  }

  ngOnInit(): void {
    this.showError = false;
    this.loadCountries();
    
  }



  loadCountries() {
    this.locationService.getCountries().subscribe({
      next: (countries) => this.countries = countries,
      error: (error) => console.error('Error fetching countries', error)
    });
  }

  onCountryChange(countryId: string) {
    this.locationService.getProvinces(countryId).subscribe({
      next: (provinces) => this.provinces = provinces,
      error: (error) => console.error('Error fetching provinces', error)
    });
  }

  nextStep(): void {
    if (this.step1Form.valid) {
      if (this.currentStep < 2) {
        this.currentStep++;
      }
      console.log('Form Value', this.step1Form.value);
    }
  }
  submitRegistration(): void {
    if (this.step1Form.valid && this.step2Form.valid) {
      const registrationData: RegistrationData = {
        email: this.step1Form.value.email,
        password: this.step1Form.value.password,
        agreeToTerms: this.step1Form.value.agreeToTerms,
        countryId: this.step2Form.value.country,
        provinceId: this.step2Form.value.province
      };

      this.locationService.submitRegistrationData(registrationData).subscribe({
        next: (response) => {
          this.snackBar.open('Registration successful', 'Close', {
            duration: 3000,
            panelClass: ['snackbar-success']
          });
          console.log('Registration Success', response);

        },
        error: (error) => {
          let errorMessage = 'Error submitting registration'; 
          if (error.error && Array.isArray(error.error) && error.error.length > 0) {
            errorMessage = error.error[0]; 
          }
          this.snackBar.open(errorMessage, 'Close', {
            duration: 3000,
            panelClass: ['snackbar-error']
          });
          console.log('Registration Error', error);
        }
      });
    } else {
      this.snackBar.open('Forms are not valid', 'Close', {
        duration: 3000,
        panelClass: ['snackbar-error'] 
      });
      this.step1Form.markAllAsTouched();
      this.step2Form.markAllAsTouched();
    }
  }
}
