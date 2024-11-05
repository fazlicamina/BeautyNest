import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {KategorijaUsluge} from '../models/kategorija-usluge';
import {environment} from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class KategorijaUslugeService {

  constructor(private http :HttpClient) { }

  getKategorijeUslugaBySalonId(salonId: number): Observable<KategorijaUsluge[]> {
    return this.http.get<KategorijaUsluge[]>(`${environment.apiBaseUrl}api/KategorijaUsluge/${salonId}/kategorije-sa-uslugama`);
  }





}
