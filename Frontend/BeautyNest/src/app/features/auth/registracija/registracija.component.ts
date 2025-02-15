import { Component } from '@angular/core';
import {FormsModule} from '@angular/forms';
import {RegistrationRequest} from '../models/registration-request';
import {AuthService} from '../services/auth.service';
import {Router} from '@angular/router';
import {NgIf} from '@angular/common';

@Component({
  selector: 'app-registracija',
  standalone: true,
  imports: [
    FormsModule,
    NgIf
  ],
  templateUrl: './registracija.component.html',
  styleUrl: './registracija.component.css'
})
export class RegistracijaComponent {

  obavijest: boolean = false;
  errorMessage: string = '';
  model: RegistrationRequest;

  constructor(private authService: AuthService, private router: Router) {
    this.model = {
      email: '',
      username: '',
      password: '',
      role: 'Klijent',
      firstName: '',
      lastName: ''
    };
  }

  onFormSubmit(): void {
    this.errorMessage = '';
    this.authService.register(this.model)
      .subscribe({
        next: (response: string) => {
          console.log('Registracija uspješna:', response);
          this.obavijest = true; // Prikazujemo poruku o uspjehu
          setTimeout(() => {
            this.router.navigateByUrl('/login');
          }, 3000);
        },
        error: (err) => {
          console.error('Greška pri registraciji:', err);
          if (err.error && err.error.length > 0) {

            this.errorMessage = err.error.map((e: any) => e.description).join(' ');
          } else {
            this.errorMessage = 'Došlo je do greške pri registraciji.';
          }
        }
      });
  }
}
