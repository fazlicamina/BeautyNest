import {Component, OnInit} from '@angular/core';
import {MojProfilService} from '../services/moj-profil.service';
import {UserProfile} from '../models/user-profile';
import {NgIf} from '@angular/common';
import {AuthService} from '../../auth/services/auth.service';
import {User} from '../../auth/models/user';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-moj-profil',
  standalone: true,
  imports: [
    NgIf,
    FormsModule
  ],
  templateUrl: './moj-profil.component.html',
  styleUrl: './moj-profil.component.css'
})
export class MojProfilComponent implements OnInit{

  userProfile: UserProfile | null = null;
  errorMessage: string | null = null;
  roles: string[] = [];

  isEditing: boolean = false;


  constructor(private mojProfilService: MojProfilService) {
  }
  ngOnInit(): void {
  this.loadUserProfile();

  }


  loadUserProfile(): void {
    this.mojProfilService.getUserProfile().subscribe({
      next: (data) => {
        this.userProfile = data;
        this.roles = data.roles;
      },
      error: (err) => {
        this.errorMessage = 'Neuspješno učitavanje profila. Provjerite vašu konekciju.';
      }
    });
  }

  toggleEdit(): void {
    this.isEditing = true;
  }

  cancelEdit(): void {
    this.isEditing = false;
    this.loadUserProfile();
  }

  saveChanges(): void {
    if (this.userProfile) {
      this.mojProfilService.updateUserProfile(this.userProfile).subscribe({
        next: () => {
          this.isEditing = false;
          alert('Profil uspješno ažuriran.');
        },
        error: (err) => {
          alert('Došlo je do greške prilikom ažuriranja profila.');
        }
      });
    }
  }

}
