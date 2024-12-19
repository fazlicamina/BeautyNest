import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {UserProfile} from '../models/user-profile';
import {environment} from '../../../../environments/environment';
import {CookieService} from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class MojProfilService {
  constructor(private http: HttpClient, private cookieService: CookieService) { }

  getUserProfile(): Observable<UserProfile> {
    const token = this.cookieService.get('Authorization');
    const headers = new HttpHeaders().set('Authorization', `${token}`);


    return this.http.get<UserProfile>(`${environment.apiBaseUrl}api/auth/mojprofil`, {
      headers,
      withCredentials: true
    });
  }


  updateUserProfile(userProfile: FormData): Observable<any> {
    const token = this.cookieService.get('Authorization');
    const headers = new HttpHeaders().set('Authorization', `${token}`);

    return this.http.put(`${environment.apiBaseUrl}api/auth/editprofile`, userProfile, {
      headers,
      withCredentials: true
    });
  }



}
