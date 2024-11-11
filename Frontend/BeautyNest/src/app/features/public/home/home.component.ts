import {Component, OnInit} from '@angular/core';
import {Router, RouterOutlet} from '@angular/router';
import {RouterLinkActive} from '@angular/router';
import {RouterLink} from '@angular/router';
import {SalonService} from '../../salon/services/salon.service';
import {Observable} from 'rxjs';
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

  selectedKategorijaId: number | null = null;
  selectedGradId: number | null = null;

  constructor(private salonService:SalonService, private kategorijaService:KategorijaService,
              private gradService:GradService, private router:Router) {
  }

  ngOnInit(): void {
    this.saloni$=this.salonService.getAllSaloni();
    this.kategorije$=this.kategorijaService.getAllKategorije();

    this.gradService.getGradovi().subscribe(
      (data) => {
        this.gradovi = data;
      }
    );

  }

  onGradSelect(): void {
    console.log('Selected city ID:', this.selectedGradId);
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
