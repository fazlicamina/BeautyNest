import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GradService {

  constructor(private http:HttpClient) { }

  getGradovi(): Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiBaseUrl}api/grad`);
  }

}
