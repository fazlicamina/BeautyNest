import {Component, OnInit} from '@angular/core';
import {Observable} from 'rxjs';
import {Salon} from '../../salon/models/salon';
import {Kategorija} from '../../kategorija/models/kategorija';
import {SalonService} from '../../salon/services/salon.service';
import {AsyncPipe, NgForOf} from '@angular/common';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import {KategorijaUsluge} from '../../salon/models/kategorija-usluge';
import {KategorijaUslugeService} from '../../salon/services/kategorija-usluge.service';
import {Grad} from '../../salon/models/grad';
import {GradService} from '../../salon/services/grad.service';
import {FormsModule} from '@angular/forms';
import {KategorijaService} from '../../kategorija/services/kategorija.service';
import {consumerDestroy} from '@angular/core/primitives/signals';


@Component({
  selector: 'app-pretraga',
  standalone: true,
  imports: [
    AsyncPipe,
    NgForOf,
    RouterLink,
    FormsModule
  ],
  templateUrl: './pretraga.component.html',
  styleUrl: './pretraga.component.css'
})
export class PretragaComponent implements OnInit{

  saloni:Salon[]=[];
  gradovi:Grad[]=[];
  kategorije:Kategorija[]=[];
  selectedGradId: number | null=null;
  selectedKategorijaId: number | null = null;

  constructor(private salonService:SalonService,
              private kategorijaUslugeService : KategorijaUslugeService,
              private gradService:GradService, private route: ActivatedRoute,
              private router:Router, private kategorijaService:KategorijaService) {
  }

  ngOnInit(): void {

    this.loadInitialData();

    this.route.queryParams.subscribe((params) => {
      this.selectedGradId = +params['cityId'] || null;
      this.selectedKategorijaId = +params['kategorijaId'] || null;
      this.applyFilters();
    });
   /* //prikazuje salone i kategorije usluga
    this.salonService.getAllSaloni().subscribe((saloni) => {
      this.saloni = saloni;

      this.saloni.forEach((salon) => {
        this.kategorijaUslugeService.getKategorijeUslugaBySalonId(salon.id).subscribe((kategorije) => {
          salon.kategorijeUsluga = kategorije;
        //dodati da prikaze i uslugu koju pretrazujemo
        });
      });
    });

    //dohvaca gradove
    this.gradService.getGradovi().subscribe(
      (data) => {
        this.gradovi = data;
      }
    );

    //dohvaca kategorije
    this.kategorijaService.getAllKategorije().subscribe(
      (data) => {
        this.kategorije = data;
      }
    );

    //ID kategorije selektovane na homepage smjesta u varijablu
    this.route.params.subscribe(params => {

      this.selectedGradId = +params['cityId'] || null;
      console.log('Selektovani grad:', this.selectedGradId);
      this.getSaloniByGradId();
    });

    this.route.paramMap.subscribe(params => {
      const kategorijaId = params.get('kategorijaId');

      // Ako postoji kategorijaId u URL-u, postavite ga
      if (kategorijaId) {
        this.selectedKategorijaId = +kategorijaId;
        console.log('Pretraga prema kategoriji:', this.selectedKategorijaId);
        this.getSaloniByKategorijaId();
      }
    });*/

  }

  /*//filtrira po selektovanom gradu
  applyFilter(): void {
    if (this.selectedGradId) {
      this.router.navigate(['/pretraga', { cityId: this.selectedGradId }]);
    } else {
      this.getSaloniByGradId();
    }
  }

  //filtrira salone po ID-u grada, ako nije selektovan onda sve prikaze
  getSaloniByGradId(): void {
    if (this.selectedGradId) {
      this.salonService.getAllSaloni().subscribe((saloni) => {
        this.saloni = saloni.filter(salon => salon.gradId === this.selectedGradId);
      });
    } else {
      this.salonService.getAllSaloni().subscribe((saloni) => {
        this.saloni = saloni;
      });
    }
  }

  getSaloniByKategorijaId(): void {
    if (this.selectedKategorijaId) {
      this.salonService.getAllSaloni().subscribe((saloni) => {
        // Filtrira salone koji imaju odgovarajuću kategoriju
        this.saloni = saloni.filter(salon =>
          salon.kategorije && salon.kategorije.some(kategorija => kategorija.id === this.selectedKategorijaId)
        );
      });
    } else {
      // Ako nije selektovana kategorija, prikazuje sve salone
      this.salonService.getAllSaloni().subscribe((saloni) => {
        this.saloni = saloni;
      });
    }
  }*/

  loadInitialData(): void {
    // Dohvati sve gradove i kategorije
    this.gradService.getGradovi().subscribe((data) => (this.gradovi = data));
    this.kategorijaService.getAllKategorije().subscribe((data) => (this.kategorije = data));
  }

  applyFilters(): void {
    this.salonService.getAllSaloni().subscribe((saloni) => {
      this.saloni = saloni.filter((salon) => {
        const matchesCity = !this.selectedGradId || salon.gradId === this.selectedGradId;
        const matchesCategory =
          !this.selectedKategorijaId || salon.kategorije.some((k) => k.id === this.selectedKategorijaId);
        return matchesCity && matchesCategory;
      });
    });
  }

  // Funkcija za postavljanje query parametara prilikom filtriranja
  setFilterParams(): void {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {
        cityId: this.selectedGradId,
        kategorijaId: this.selectedKategorijaId
      },
      queryParamsHandling: 'merge' // Održava postojeće parametre prilikom izmjene
    });
  }

  onResetFilters(): void {
    // Poništava odabrane filtere
    this.selectedGradId = null;
    this.selectedKategorijaId = null;

    // Poziva pretragu bez filtera
    this.applyFilters();
  }


}
