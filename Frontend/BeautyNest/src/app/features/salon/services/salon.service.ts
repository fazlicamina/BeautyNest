import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {Salon} from '../models/salon';
import {environment} from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SalonService {

  constructor(private http:HttpClient) { }

  getAllSaloni():Observable<Salon[]>{
    return this.http.get<Salon[]>(`${environment.apiBaseUrl}api/salon`);
  }

  getSalonById(id:number):Observable<Salon>{
    return this.http.get<Salon>(`${environment.apiBaseUrl}api/salon/${id}`);
  }

}
