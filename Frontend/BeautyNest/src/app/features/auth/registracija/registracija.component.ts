import { Component } from '@angular/core';
import {FormsModule} from '@angular/forms';
import {RegistrationRequest} from '../models/registration-request';
import {AuthService} from '../services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-registracija',
  standalone: true,
  imports: [
    FormsModule
  ],
  templateUrl: './registracija.component.html',
  styleUrl: './registracija.component.css'
})
export class RegistracijaComponent {

  model: RegistrationRequest;

  constructor(private authService: AuthService, private router :Router) {
    this.model={
      email: '',
      username: '',
      password: '',
      role: 'Klijent'
    };


  }

  onFormSubmit(): void {
    this.authService.register(this.model)
      .subscribe({
        next: (response) => {
          console.log('Registracija uspješna:', response);
          this.router.navigateByUrl('/login');
        },
        error: (err) => {
          console.error('Greška pri registraciji:', err);
        }
      });
  }


}
