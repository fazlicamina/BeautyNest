import {AfterViewInit, Component, model, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {SalonService} from '../services/salon.service';
import {Observable, Subscription} from 'rxjs';
import {ActivatedRoute, RouterLink} from '@angular/router';
import {Salon} from '../models/salon';
import {AsyncPipe, CurrencyPipe, DatePipe, NgClass, NgForOf, NgIf} from '@angular/common';
import {KategorijaUslugeService} from '../services/kategorija-usluge.service';
import {KategorijaUsluge} from '../models/kategorija-usluge';
import Modal from 'bootstrap/js/dist/modal';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {AuthService} from '../../auth/services/auth.service';
import {CookieService} from 'ngx-cookie-service';
import {MatDatepicker, MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import {ViewEncapsulation } from '@angular/core';
import {MatCard} from '@angular/material/card';

@Component({
  selector: 'app-pregled-salona',
  standalone: true,
  imports: [
    AsyncPipe,
    NgIf,
    NgForOf,
    RouterLink,
    DatePipe,
    NgClass,
    FormsModule,
    ReactiveFormsModule,
    CurrencyPipe,
    MatDatepickerModule,
    MatNativeDateModule,
    MatInputModule,
    MatCard
  ],
  templateUrl: './pregled-salona.component.html',
  styleUrl: './pregled-salona.component.css',
  encapsulation: ViewEncapsulation.None
})
export class PregledSalonaComponent implements OnInit, OnDestroy{

  id:number | null=null;
  paramsSubscription?:Subscription;
  salon$?:Observable<Salon>;
  kategorijeUsluga: KategorijaUsluge[] = [];

  selectedUsluge: { naziv: string; cijena: number }[] = [];
  totalCijena: number = 0;

  activeTab: number = 0;
  modalModel = { username: '', password: '' };
  loginErrorMessage: string | null = null;


  //rezervacija
  selectedDate: Date | null = null;
  selectedTime: string | null = null;
  availableTimes: string[] = [];


  constructor(private salonService:SalonService, private route:ActivatedRoute,
              private kategorijaUslugeService : KategorijaUslugeService,
private authService:AuthService, private cookieService:CookieService){
  }


  //rezervacija kalendar funkcije
  occupiedTimes: { [key: string]: string[] } = {
    '2024-12-02': ['10:00', '11:00', '14:00'],
    '2024-12-03': ['09:00', '15:00']
  };


  onDateChange(date: Date): void {
    this.selectedDate = date;
    const dateKey = this.formatDateKey(date);

    // Provjera da je occupiedTimes definiran za dateKey
    const allTimes = ['09:00', '10:00', '11:00', '12:00', '13:00', '14:00', '15:00'];
    this.availableTimes = allTimes.filter(time => !this.occupiedTimes[dateKey]?.includes(time));
  }


  onTimeSelect(time: string): void {
    this.selectedTime = time;
  }



  confirmReservation(): void {
    if (this.selectedDate && this.selectedTime) {
      alert(`Rezervirali ste termin: ${this.selectedTime} na datum: ${this.selectedDate.toLocaleDateString()}`);
    }
  }

  private formatDateKey(date: Date): string {
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');
    return `${year}-${month}-${day}`;
  }


  rezervisi(): void {
    if (!this.authService.isAuthenticated()) {
      const modalElement = document.getElementById('loginModal');
      if (modalElement) {
        const bootstrapModal = new Modal(modalElement);
        bootstrapModal.show();
      }
    } else {
      // Prikaz prvog modala
      const modalElement = document.getElementById('rezervacijaModal1');
      if (modalElement) {
        const bootstrapModal = new Modal(modalElement);
        bootstrapModal.show();

        const nextStepBtn = document.getElementById('nextStepBtn');
        nextStepBtn?.addEventListener('click', () => {
          bootstrapModal.hide(); // Zatvori prvi modal

          // Otvori drugi modal
          const nextModalElement = document.getElementById('rezervacijaModal2');
          if (nextModalElement) {
            const nextModal = new Modal(nextModalElement);
            nextModal.show();
          }
        });
      }
    }
  }




  ngOnInit(): void {

    this.paramsSubscription = this.route.paramMap
      .subscribe({
        next: (params) => {
          const idString = params.get('id');
          this.id = idString ? Number(idString) : null;
        }
      });

    if(this.id){
      this.salon$=this.salonService.getSalonById(this.id);
      this.fetchKategorijeUsluga(this.id);
    }

  }



  setActiveTab(index: number): void {
    this.activeTab = index;
  }

  private fetchKategorijeUsluga(salonId: number): void {
    this.kategorijaUslugeService.getKategorijeUslugaBySalonId(salonId).subscribe(
      (data) => {
        this.kategorijeUsluga = data;

      },
      (error) => {
        console.error('Greška prilikom dohvata kategorija usluga:', error);
      }
    );
  }


  isSelected(usluga: { naziv: string; cijena: number }): boolean {
    return this.selectedUsluge.some(u => u.naziv === usluga.naziv);
  }

  toggleUsluga(usluga: { naziv: string; cijena: number }): void {
    const index = this.selectedUsluge.findIndex(u => u.naziv === usluga.naziv);

    if (index !== -1) {
      this.selectedUsluge.splice(index, 1);
    } else {
      this.selectedUsluge.push(usluga);
    }

    this.updateTotalCijena();
  }

  updateTotalCijena(): void {
    this.totalCijena = this.selectedUsluge.reduce((sum, usluga) => sum + usluga.cijena, 0);
  }


  removeUsluga(usluga: any): void {
    const index = this.selectedUsluge.indexOf(usluga);
    if (index > -1) {
      this.selectedUsluge.splice(index, 1);
      this.updateTotalCijena();
    }
  }


  //login modal
  onModalLoginSubmit(): void {
    this.authService.login(this.modalModel).subscribe({
      next: (response) => {
        this.cookieService.set(
          'Authorization',
          `Bearer ${response.token}`,
          undefined,
          '/',
          undefined,
          true,
          'Strict'
        );

        this.authService.setUser({
          username: response.username,
          roles: response.roles,
        });


        this.closeLoginModal();
        this.showModal('Prijava uspješna!');



      },
      error: (err) => {
        console.error('Greška prilikom prijave', err);
        this.loginErrorMessage = 'Neispravno korisničko ime ili lozinka!';

      },
    });
  }

  private showModal(message: string): void {
    const modalBody = document.getElementById('modalBody2');
    if (modalBody) {
      modalBody.innerText = message;
    }

    const modalElement = document.getElementById('loginInfo');
    if (modalElement) {
      const bootstrapModal = new Modal(modalElement);
      bootstrapModal.show();

      setTimeout(() => {
        bootstrapModal.hide();
      }, 1500);

    }
  }

  closeLoginModal(): void {
    const modalElement = document.getElementById('loginModal');
    if (modalElement) {
      const bootstrapModal = Modal.getInstance(modalElement);
      bootstrapModal?.hide();
    }
  }


  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();


  }

  protected readonly close = close;
  protected readonly model = model;
}
