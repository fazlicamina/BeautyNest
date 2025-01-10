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
import {RezervacijaService} from '../services/rezervacija.service';
import {RezervacijaRequest} from '../models/rezervacija-request';
import {SlobodniTermin} from '../models/slobodni-termin';
import {HttpParams} from '@angular/common/http';

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
  selectedSalonId: number | null = null;

  selectedUsluge: { id: number; naziv: string; cijena: number }[] = [];

  totalCijena: number = 0;

  activeTab: number = 0;
  modalModel = { username: '', password: '' };
  loginErrorMessage: string | null = null;

  minDate: Date = new Date(); // Blokiraj prošle datume



  //rezervacija
  selectedDate: Date | null = null;
  selectedTime: string | null = null;
  availableTimes: string[] = [];


  constructor(private salonService:SalonService, private route:ActivatedRoute,
              private kategorijaUslugeService : KategorijaUslugeService,
private authService:AuthService, private cookieService:CookieService,
              private rezervacijaService: RezervacijaService){
  }


  loadAvailableTimes(): void {
    if (this.selectedSalonId != null && this.selectedDate != null) {

      //syncanje timezome sa backendom
      const utcDate = new Date(Date.UTC(
        this.selectedDate.getUTCFullYear(),
        this.selectedDate.getUTCMonth(),
        this.selectedDate.getUTCDate()
      ));

      const formattedDate = utcDate.toISOString().split('T')[0];

      const uslugaIds = this.selectedUsluge.map(u => u.id);
      let params = new HttpParams()
        .set('salonId', this.selectedSalonId.toString())
        .set('datum', formattedDate);

      uslugaIds.forEach(id => {
        params = params.append('uslugaIds', id.toString());
      });

      this.rezervacijaService.getDostupniSlotovi(this.selectedSalonId, this.selectedDate, uslugaIds).subscribe(
        (response: any[]) => {
          console.log('Odgovor sa servera:', response);
          if (response && response.length > 0) {
            this.availableTimes = response.map(slot => {

              const [startHours, startMinutes] = slot.start.split(':').map(Number);
              const [endHours, endMinutes] = slot.end.split(':').map(Number);

              const start = `${startHours.toString().padStart(2, '0')}:${startMinutes.toString().padStart(2, '0')}`;
              const end = `${endHours.toString().padStart(2, '0')}:${endMinutes.toString().padStart(2, '0')}`;

              return `${start} - ${end}`;
            });
          } else {
            this.availableTimes = [];
          }
        },
        (error) => {
          console.error('Greška pri dobijanju dostupnih termina:', error);
        }
      );
    }
  }



  onTimeSelect(time: string): void {
    this.selectedTime = time;
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

  onDateChange(date: Date | null): void {
    this.selectedDate = date;
    this.loadAvailableTimes(); // Poziv tek nakon odabira datuma
  }


  ngOnInit(): void {

    this.route.params.subscribe(params => {
      this.selectedSalonId = +params['id'];
      this.loadAvailableTimes();
      this.checkFavoriteStatus();
    });

    this.paramsSubscription = this.route.paramMap
      .subscribe({
        next: (params) => {
          const idString = params.get('id');
          this.id = idString ? Number(idString) : null;
        }
      });

    if(this.id){
      this.selectedSalonId = this.id;
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

  toggleUsluga(usluga: { id:number, naziv: string; cijena: number }): void {
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

  //omiljeni
  isFavorite: boolean = false;

  toggleFavorite(): void {
    if (this.selectedSalonId != null) {
      this.salonService.toggleOmiljeniSalon(this.selectedSalonId).subscribe({
        next: (response: any) => {
          if (response.message === 'Salon je uspješno dodan u omiljene.') {
            this.isFavorite = true;
          } else if (response.message === 'Salon je uklonjen iz omiljenih.') {
            this.isFavorite = false;
          }
        },
        error: (err) => {
          console.error('Došlo je do greške prilikom ažuriranja omiljenih:', err);
        },
      });
    }
  }

  checkFavoriteStatus(): void {
    if (this.selectedSalonId != null) {
      this.salonService.getOmiljeniSaloni().subscribe((omiljeniSaloni) => {
        this.isFavorite = omiljeniSaloni.some(
          (salon) => salon.id === this.selectedSalonId
        );
      });
    }
  }

  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
  }

  protected readonly close = close;
  protected readonly model = model;
}
