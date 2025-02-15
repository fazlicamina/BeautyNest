import {Component, OnInit} from '@angular/core';
import {MojProfilService} from '../services/moj-profil.service';
import {UserProfile} from '../models/user-profile';
import {NgIf} from '@angular/common';
import {AuthService} from '../../auth/services/auth.service';
import {User} from '../../auth/models/user';
import {FormsModule} from '@angular/forms';
import {ToastserviceService} from '../../../core/services/toastservice.service';

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

  constructor(private mojProfilService: MojProfilService, private toastService: ToastserviceService) {
  }
  ngOnInit(): void {
  this.loadUserProfile();

  }

  loadUserProfile(): void {
    this.mojProfilService.getUserProfile().subscribe({
      next: (data) => {
        this.userProfile = data;
        this.roles = data.roles;


        if (this.userProfile?.profilePicture) {
          this.userProfile.profilePictureBase64 = 'data:image/jpeg;base64,' + this.userProfile.profilePicture;
        }
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
      const formData = new FormData();
      formData.append('firstName', this.userProfile.firstName);
      formData.append('lastName', this.userProfile.lastName);
      formData.append('email', this.userProfile.email);

      if (this.userProfile.profilePicture) {
        formData.append('profilePicture', this.userProfile.profilePicture);
      }

      this.mojProfilService.updateUserProfile(formData).subscribe({
        next: () => {
          this.isEditing = false;
          this.toastService.showSuccessToast('Profil je ažuriran!')
        },
        error: (err) => {
          this.toastService.showErrorToast('Došlo je do greške prilikom ažuriranja profila.')
        }
      });
    }
  }

  onFileChange(event: any): void {
    if (event.target.files.length > 0 && this.userProfile) {
      const file = event.target.files[0];

      this.userProfile.profilePicture = file;

      const reader = new FileReader();
      reader.onloadend = () => {

        if (reader.result && this.userProfile) {
          this.userProfile.profilePictureBase64 = reader.result as string;
        }
      };
      reader.readAsDataURL(file);
    }
  }


}
