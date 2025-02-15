import {Component, OnInit} from '@angular/core';
import {Observable} from 'rxjs';
import {Salon} from '../../salon/models/salon';
import {Kategorija} from '../../kategorija/models/kategorija';
import {SalonService} from '../../salon/services/salon.service';
import {AsyncPipe, CurrencyPipe, NgClass, NgForOf, NgIf} from '@angular/common';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import {KategorijaUsluge} from '../../salon/models/kategorija-usluge';
import {KategorijaUslugeService} from '../../salon/services/kategorija-usluge.service';
import {Grad} from '../../salon/models/grad';
import {GradService} from '../../salon/services/grad.service';
import {FormsModule} from '@angular/forms';
import {KategorijaService} from '../../kategorija/services/kategorija.service';
import {consumerDestroy} from '@angular/core/primitives/signals';
import {Usluga} from '../../salon/models/usluga';
import {User} from '../../auth/models/user';
import {AuthService} from '../../auth/services/auth.service';
import {ToastserviceService} from '../../../core/services/toastservice.service';


@Component({
  selector: 'app-pretraga',
  standalone: true,
  imports: [
    AsyncPipe,
    NgForOf,
    RouterLink,
    FormsModule,
    CurrencyPipe,
    NgIf,
    NgClass
  ],
  templateUrl: './pretraga.component.html',
  styleUrl: './pretraga.component.css'
})
export class PretragaComponent implements OnInit{

  user?:User;

  saloni:Salon[]=[];
  gradovi:Grad[]=[];
  kategorije:Kategorija[]=[];
  selectedGradId: number | null=null;
  selectedKategorijaId: number | null = null;
  nazivSalona:string='';

  omiljeniSaloniIds: Set<number> = new Set();

  constructor(private salonService:SalonService,
              private kategorijaUslugeService : KategorijaUslugeService,
              private gradService:GradService, private route: ActivatedRoute,
              private router:Router, private kategorijaService:KategorijaService,
              private  authService: AuthService,
              private toastService:ToastserviceService) {
  }

  ngOnInit(): void {
    this.user = this.authService.getUser();
    this.loadInitialData();

    if (this.user) { // Provjera da li postoji prijavljeni korisnik
      this.salonService.getOmiljeniSaloni().subscribe((omiljeniSaloni) => {
        this.omiljeniSaloniIds = new Set(omiljeniSaloni.map((s) => s.id));
        this.loadSearchParams();
      });
    } else {
      this.loadSearchParams();
    }
  }

  loadSearchParams(): void {
    this.route.queryParams.subscribe((params) => {
      this.selectedGradId = +params['cityId'] || null;
      this.selectedKategorijaId = +params['kategorijaId'] || null;
      this.nazivSalona = params['naziv'] || '';
      this.applyFilters();
    });
  }


  loadInitialData(): void {
    this.gradService.getGradovi().subscribe((data) => (this.gradovi = data));
    this.kategorijaService.getAllKategorije().subscribe((data) => (this.kategorije = data));
  }


  applyFilters(): void {
    this.salonService.getAllSaloni().subscribe((saloni) => {
      console.log('Dohvaćeni saloni:', saloni); // Dodajte log za provjeru

      this.saloni = saloni
        .filter((salon) => {
          const matchesCity = !this.selectedGradId || salon.gradId === this.selectedGradId;
          const matchesCategory = !this.selectedKategorijaId || salon.kategorije.some((k) => k.id === this.selectedKategorijaId);
          const matchesName = !this.nazivSalona || salon.naziv.toLowerCase().includes(this.nazivSalona.toLowerCase());
          return matchesCity && matchesCategory && matchesName;
        })
        .map((salon) => ({
          ...salon,
          jeOmiljeni: this.omiljeniSaloniIds.has(salon.id),
        }));

      console.log('Filtrirani saloni:', this.saloni);
    });
  }


  toggleOmiljeniSalon(salon: any): void {
    this.salonService.toggleOmiljeniSalon(salon.id).subscribe({
      next: (response: any) => {
        if (response.message === 'Salon je uspješno dodan u omiljene.') {
          salon.jeOmiljeni = true;
          this.omiljeniSaloniIds.add(salon.id);
          this.toastService.showSuccessToast('Dodano u omiljene!')
        } else if (response.message === 'Salon je uklonjen iz omiljenih.') {
          salon.jeOmiljeni = false;
          this.omiljeniSaloniIds.delete(salon.id);
          this.toastService.showSuccessToast('Uklonjeno iz omiljenih!')
        }
      },
      error: (err) => {
        console.error('Došlo je do greške prilikom ažuriranja omiljenih:', err);
      }
    });
  }



  setFilterParams(): void {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {
        cityId: this.selectedGradId,
        kategorijaId: this.selectedKategorijaId,
        naziv: this.nazivSalona,
      },
      queryParamsHandling: 'merge'
    });
  }

  onResetFilters(): void {

    this.selectedGradId = null;
    this.selectedKategorijaId = null;
    this.nazivSalona='';

    this.applyFilters();
  }


}
