import { Injectable } from '@angular/core';
import {LoginRequest} from '../models/login-request';
import {BehaviorSubject, Observable} from 'rxjs';
import {LoginResponse} from '../models/login-response';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../../environments/environment';
import {User} from '../models/user';
import {CookieService} from 'ngx-cookie-service';
import {RegistrationRequest} from '../models/registration-request';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  $user=new BehaviorSubject<User | undefined>(undefined);

  constructor(private http:HttpClient,
              private cookieService:CookieService) { }

  login(request: LoginRequest):Observable<LoginResponse>{
      return this.http.post<LoginResponse>(`${environment.apiBaseUrl}api/auth/login`,
        {username: request.username,
        password:request.password});
  }

  setUser(user:User):void{
    this.$user.next(user);
    localStorage.setItem('user-id', user.id ?? '');
      localStorage.setItem('user-username', user.username);
      localStorage.setItem('user-roles', user.roles.join(','));
  }

  user():Observable<User | undefined>{
    return this.$user.asObservable();
  }

  getUser():User|undefined{
    const username = localStorage.getItem('user-username');
    const roles=localStorage.getItem('user-roles');
    const id=localStorage.getItem('user-id');

    if(username && roles){
      const user:User={
        id: id,
        username:username,
        roles:roles.split(',')
      };

      return user;
    }
    return undefined;

  }

  logout():void{
    localStorage.clear();
    this.cookieService.delete('Authorization','/');
    this.$user.next(undefined);
  }

  isAuthenticated(): boolean {
    return !!this.getUser();
  }

  register(model: RegistrationRequest ): Observable<any> {
    return this.http.post<any>(`${environment.apiBaseUrl}api/auth/register`, model);
  }

}
