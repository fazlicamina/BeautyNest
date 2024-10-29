import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Salon} from '../../salon/models/salon';
import {environment} from '../../../../environments/environment';
import {Kategorija} from '../models/kategorija';

@Injectable({
  providedIn: 'root'
})
export class KategorijaService {
  constructor(private http:HttpClient) { }

  getAllKategorije():Observable<Kategorija[]>{
    return this.http.get<Kategorija[]>(`${environment.apiBaseUrl}api/kategorija`);
  }

}
