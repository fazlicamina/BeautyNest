import {Component, OnInit} from '@angular/core';
import {MojProfilService} from '../services/moj-profil.service';
import {UserProfile} from '../models/user-profile';
import {NgIf} from '@angular/common';
import {AuthService} from '../../auth/services/auth.service';
import {User} from '../../auth/models/user';

@Component({
  selector: 'app-moj-profil',
  standalone: true,
  imports: [
    NgIf
  ],
  templateUrl: './moj-profil.component.html',
  styleUrl: './moj-profil.component.css'
})
export class MojProfilComponent implements OnInit{

  userProfile: UserProfile | null = null;
  errorMessage: string | null = null;
  roles: string[] = [];
  constructor(private mojProfilService: MojProfilService) {
  }
  ngOnInit(): void {


    this.mojProfilService.getUserProfile().subscribe({
      next: (data) => {
        this.userProfile = data;
        this.roles=data.roles;
      },
      error: (err) => {
        this.errorMessage = 'Neuspješno učitavanje profila. Provjerite vašu konekciju.';
      }
    });
  }

}
