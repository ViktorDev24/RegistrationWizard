import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment'; 
import { RegistrationData } from '../../models/registration.model';
import { Country } from '../../models/country.model';
import { Province } from '../../models/province.model';

@Injectable({
  providedIn: 'root'
})
export class LocationService {
  private countriesUrl = `${environment.apiUrl}/countries`;
  private provincesUrl = (countryId: string) => `${environment.apiUrl}/provinces/${countryId}/`;
  private submitRegistrationDataUrl = `${environment.apiUrl}/registration`;
  constructor(private http: HttpClient) { }

  getCountries(): Observable<Country[]> {
    return this.http.get<Country[]>(this.countriesUrl);
  }

  getProvinces(countryId: string): Observable<Province[]> {
    return this.http.get<Province[]>(this.provincesUrl(countryId));
  }
  submitRegistrationData(data: RegistrationData) {
    return this.http.post(this.submitRegistrationDataUrl, data);
  }

}
