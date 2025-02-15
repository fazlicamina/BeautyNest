import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Salon} from '../models/salon';
import {environment} from '../../../../environments/environment';
import {CookieService} from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class SalonService {

  constructor(private http:HttpClient, private cookieService: CookieService) { }

  getAllSaloni():Observable<Salon[]>{
    return this.http.get<Salon[]>(`${environment.apiBaseUrl}api/salon`);
  }

  getSalonById(id:number):Observable<Salon>{
    return this.http.get<Salon>(`${environment.apiBaseUrl}api/salon/${id}`);
  }

  toggleOmiljeniSalon(salonId: number): Observable<any> {
    const token = this.cookieService.get('Authorization');
    console.log('Token:', token);

    const headers = new HttpHeaders().set('Authorization', `${token}`);

    return this.http.post<any>(`${environment.apiBaseUrl}api/OmiljeniSaloni/toggle?salonId=${salonId}`, {}, {
      headers,
      withCredentials: true
    });
  }




  getOmiljeniSaloni() {
    const token = this.cookieService.get('Authorization');
    const headers = new HttpHeaders().set('Authorization', `${token}`);

    return this.http.get<any[]>(`${environment.apiBaseUrl}api/OmiljeniSaloni/list`, {
      headers,
      withCredentials: true
    });
  }



}
