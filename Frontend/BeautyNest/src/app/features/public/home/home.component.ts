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
   // this.saloni$=this.salonService.getAllSaloni();
    this.kategorije$=this.kategorijaService.getAllKategorije();

    this.gradService.getGradovi().subscribe(
      (data) => {
        this.gradovi = data;
      }
    );

    this.salonService.getOmiljeniSaloni().subscribe((omiljeniSaloni) => {
      const omiljeniIds = new Set(omiljeniSaloni.map((s) => s.id));

      // Učitaj sve salone i dodaj `jeOmiljeni`
      this.saloni$ = this.salonService.getAllSaloni().pipe(
        map((saloni) =>
          saloni.map((salon) => ({
            ...salon,
            jeOmiljeni: omiljeniIds.has(salon.id), // Dodaj `jeOmiljeni`
          }))
        )
      );
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
