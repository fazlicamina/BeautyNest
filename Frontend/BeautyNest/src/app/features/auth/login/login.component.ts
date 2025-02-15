import { Component } from '@angular/core';
import {LoginRequest} from '../models/login-request';
import {FormsModule} from '@angular/forms';
import {AuthService} from '../services/auth.service';
import {CookieService} from 'ngx-cookie-service';
import {Router} from '@angular/router';
import {NgIf} from '@angular/common';
import {HttpErrorResponse} from '@angular/common/http';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    FormsModule,
    NgIf
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  model: LoginRequest;
  errorMessage: string | null = null;

  constructor(
    private authService: AuthService,
    private cookieService: CookieService,
    private router: Router
  ) {
    this.model = {
      username: '',
      password: ''
    };
  }

  onFormSubmit(): void {
    this.errorMessage = null;

    this.authService.login(this.model).subscribe({
      next: (response) => {
        this.cookieService.set('Authorization', `Bearer ${response.token}`,
          undefined, '/', undefined, true, 'Strict');

        this.authService.setUser({
          username: response.username,
          roles: response.roles,
          id: response.id
        });

        this.router.navigateByUrl('/');
      },
      error: (error: HttpErrorResponse) => {
        if (error.status === 400 && error.error?.errors) {
          const errors = Object.values(error.error.errors) as string[];
          this.errorMessage = errors.join(' ');
        } else {
          this.errorMessage = "Došlo je do greške. Pokušajte ponovo.";
        }
      }
    });
  }
}
