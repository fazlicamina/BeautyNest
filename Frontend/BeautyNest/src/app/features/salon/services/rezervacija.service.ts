import { Injectable } from '@angular/core';
import {environment} from '../../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {RezervacijaRequest} from '../models/rezervacija-request';

@Injectable({
  providedIn: 'root'
})
export class RezervacijaService {

  private baseUrl = environment.apiBaseUrl + '/api/rezervacija';
  constructor(private http: HttpClient) { }

  getDostupniSlotovi(salonId: number, date: Date | null): Observable<any> {
    return this.http.get(`${this.baseUrl}/DostupniSlotovi?salonId=${salonId}&datum=${date}`);
  }

  kreirajRezervaciju(dto: RezervacijaRequest): Observable<any> {
    return this.http.post(this.baseUrl, dto);
  }

}
