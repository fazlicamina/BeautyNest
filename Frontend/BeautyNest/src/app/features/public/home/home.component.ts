import {Component, OnInit} from '@angular/core';
import {Router, RouterOutlet} from '@angular/router';
import {RouterLinkActive} from '@angular/router';
import {RouterLink} from '@angular/router';
import {SalonService} from '../../salon/services/salon.service';
import {map, Observable} from 'rxjs';
import {Salon} from '../../salon/models/salon';
import {AsyncPipe, NgForOf, NgIf} from '@angular/common';
import {CommonModule} from '@angular/common';
import {Kategorija} from '../../kategorija/models/kategorija';
import {KategorijaService} from '../../kategorija/services/kategorija.service';
import {Grad} from '../../salon/models/grad';
import {GradService} from '../../salon/services/grad.service';
import {FormsModule} from '@angular/forms';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    RouterOutlet, RouterLinkActive, RouterLink, AsyncPipe, NgForOf, NgIf, CommonModule, FormsModule
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{

  saloni$?:Observable<Salon[]>;
  kategorije$?:Observable<Kategorija[]>;
  gradovi:Grad[]=[];
  nazivSalona:string | null=null;

  omiljeniSaloniIds: Set<number> = new Set();

  selectedKategorijaId: number | null = null;
  selectedGradId: number | null = null;

  constructor(private salonService:SalonService, private kategorijaService:KategorijaService,
              private gradService:GradService, private router:Router) {
  }


  toggleOmiljeniSalon(salon: any) {
    this.salonService.toggleOmiljeniSalon(salon.id).subscribe({
      next: (response: any) => {
        if (response.message === 'Salon je uspješno dodan u omiljene.') {
          salon.jeOmiljeni = true;
          this.omiljeniSaloniIds.add(salon.id);
        } else if (response.message === 'Salon je uklonjen iz omiljenih.') {
          salon.jeOmiljeni = false;
          this.omiljeniSaloniIds.delete(salon.id);
        }
      },
      error: (err) => {
        console.error('Došlo je do greške prilikom ažuriranja omiljenih:', err);
      }
    });
  }


  ngOnInit(): void {
    // Učitaj sve salone odmah
    this.salonService.getAllSaloni().subscribe((saloni) => {
      this.saloni$ = new Observable((observer) => observer.next(saloni));

      // Pokušaj učitati omiljene salone ako je korisnik autentifikovan
      this.salonService.getOmiljeniSaloni().subscribe({
        next: (omiljeniSaloni) => {
          const omiljeniIds = new Set(omiljeniSaloni.map((s) => s.id));

          // Dodaj oznaku `jeOmiljeni` salonima
          saloni.forEach((salon) => {
            salon.jeOmiljeni = omiljeniIds.has(salon.id);
          });
        },
        error: (err) => {
          console.warn('Korisnik nije autentifikovan ili je došlo do greške prilikom učitavanja omiljenih salona:', err);
        }
      });
    });

    // Učitaj kategorije
    this.kategorije$ = this.kategorijaService.getAllKategorije();

    // Učitaj gradove
    this.gradService.getGradovi().subscribe((data) => {
      this.gradovi = data;
    });
  }



  onClickPretrazi(): void {
    const queryParams: any = {};
    if (this.nazivSalona) {
      queryParams.naziv = this.nazivSalona;
    }
    if (this.selectedGradId) {
      queryParams.cityId = this.selectedGradId;
    }
    if (this.selectedKategorijaId) {
      queryParams.kategorijaId = this.selectedKategorijaId;
    }

    this.router.navigate(['/pretraga'], { queryParams });
  }

  onClickKategorija(kategorijaId: number): void {
    const queryParams: any = {};
    queryParams.kategorijaId = kategorijaId;
    this.router.navigate(['/pretraga'], { queryParams });
  }

}
