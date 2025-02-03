import { Injectable } from '@angular/core';
import {environment} from '../../../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {CookieService} from 'ngx-cookie-service';
import {Observable} from 'rxjs';
import {Recenzija} from '../models/recenzije';
import {User} from '../../auth/models/user';
import {Klijent} from '../models/klijent';

@Injectable({
  providedIn: 'root'
})
export class RecenzijeService {

  private userApiUrl = `${environment.apiBaseUrl}api/auth/user`;
  private apiUrl = `${environment.apiBaseUrl}api/recenzija`;

  constructor(private http: HttpClient, private cookieService: CookieService) {}




  getRecenzijeZaSalon(salonId: number): Observable<Recenzija[]> {
    const token = this.cookieService.get('Authorization');
    const headers = new HttpHeaders().set('Authorization', `${token}`);

    return this.http.get<Recenzija[]>(`${this.apiUrl}/${salonId}`, {
      headers,
      withCredentials: true
    });
  }

  dodajRecenziju(recenzija: any, slike: File[]): Observable<any> {
    const token = this.cookieService.get('Authorization');
    const headers = new HttpHeaders().set('Authorization', `${token}`);

    const formData = new FormData();
    formData.append('rezervacijaId', recenzija.rezervacijaId.toString());
    formData.append('ocjena', recenzija.ocjena.toString());
    formData.append('tekst', recenzija.tekst);

    if (slike && slike.length > 0) {
      for (let i = 0; i < slike.length; i++) {
        formData.append('slike', slike[i], slike[i].name); // Slanje File objekata
      }
    }

    return this.http.post<any>(`${this.apiUrl}`, formData, {
      headers, // Ne postavljamo Content-Type ručno
      withCredentials: true
    });
  }




  getUserByUsername(username: string): Observable<Klijent> {
    return this.http.get<Klijent>(`${this.userApiUrl}/${username}`, { withCredentials: true });
  }

}
