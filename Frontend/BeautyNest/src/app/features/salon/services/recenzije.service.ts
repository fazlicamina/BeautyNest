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

  getUserByUsername(username: string): Observable<Klijent> {
    return this.http.get<Klijent>(`${this.userApiUrl}/${username}`, { withCredentials: true });
  }

}
