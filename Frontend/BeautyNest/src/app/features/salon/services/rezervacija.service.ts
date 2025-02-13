import { Injectable } from '@angular/core';
import {environment} from '../../../../environments/environment';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {RezervacijaRequest} from '../models/rezervacija-request';
import {SlobodniTermin} from '../models/slobodni-termin';
import {CookieService} from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class RezervacijaService {

  constructor(private http: HttpClient, private cookieService: CookieService) { }

  getDostupniSlotovi(salonId: number, datum: Date, uslugaIds: number[]): Observable<SlobodniTermin[]> {
    const token = this.cookieService.get('Authorization');
    const headers = new HttpHeaders().set('Authorization', `${token}`);

    const adjustedDate = new Date(datum);
    adjustedDate.setDate(adjustedDate.getDate() + 1);

    let params = new HttpParams()
      .set('salonId', salonId.toString())
      .set('datum', adjustedDate.toISOString());

    uslugaIds.forEach(id => {
      params = params.append('uslugaIds', id.toString());
    });

    return this.http.get<SlobodniTermin[]>(`${environment.apiBaseUrl}api/rezervacija/slobodni-termini`, {
      headers,
      params,
      withCredentials: true
    });
  }

  createReservation(rezervacija: any): Observable<any> {
    const token = this.cookieService.get('Authorization');
    const headers = new HttpHeaders().set('Authorization', `${token}`);

    console.log('Slanje zahtjeva za kreiranje rezervacije:', rezervacija);


    return this.http.post(`${environment.apiBaseUrl}api/rezervacija`, rezervacija, {
      headers,
      withCredentials: true,
    });
  }


  getMojeRezervacije(page: number, pageSize: number, isZavrsena?: boolean | null): Observable<any> {
    const token = this.cookieService.get('Authorization');
    const headers = new HttpHeaders().set('Authorization', `${token}`);

    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    if (isZavrsena !== null && isZavrsena !== undefined) {
      params = params.set('isZavrsena', isZavrsena.toString());
    }

    return this.http.get<any>(`${environment.apiBaseUrl}api/rezervacija/moje-rezervacije`, { headers, params, withCredentials: true });
  }




  otkaziRezervaciju(id: number): Observable<any> {
    const token = this.cookieService.get('Authorization');
    const headers = new HttpHeaders().set('Authorization', `${token}`);

    return this.http.delete(`${environment.apiBaseUrl}api/rezervacija/${id}`, {
      headers,
      withCredentials: true
    });
  }




}
