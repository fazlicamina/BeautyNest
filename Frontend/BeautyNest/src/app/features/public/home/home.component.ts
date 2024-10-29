import {Component, OnInit} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {RouterLinkActive} from '@angular/router';
import {RouterLink} from '@angular/router';
import {SalonService} from '../../salon/services/salon.service';
import {Observable} from 'rxjs';
import {Salon} from '../../salon/models/salon';
import {AsyncPipe, NgForOf, NgIf} from '@angular/common';
import {CommonModule} from '@angular/common';
import {Kategorija} from '../../kategorija/models/kategorija';
import {KategorijaService} from '../../kategorija/services/kategorija.service';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    RouterOutlet, RouterLinkActive, RouterLink, AsyncPipe, NgForOf, NgIf, CommonModule
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{

  saloni$?:Observable<Salon[]>;
  kategorije$?:Observable<Kategorija[]>;

  constructor(private salonService:SalonService, private kategorijaService:KategorijaService) {
  }

  ngOnInit(): void {
    this.saloni$=this.salonService.getAllSaloni();
    this.kategorije$=this.kategorijaService.getAllKategorije();
  }
}
