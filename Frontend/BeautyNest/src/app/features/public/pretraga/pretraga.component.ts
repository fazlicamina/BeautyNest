import {Component, OnInit} from '@angular/core';
import {Observable} from 'rxjs';
import {Salon} from '../../salon/models/salon';
import {Kategorija} from '../../kategorija/models/kategorija';
import {SalonService} from '../../salon/services/salon.service';
import {AsyncPipe, NgForOf} from '@angular/common';
import {RouterLink} from '@angular/router';
import {KategorijaUsluge} from '../../salon/models/kategorija-usluge';
import {KategorijaUslugeService} from '../../salon/services/kategorija-usluge.service';


@Component({
  selector: 'app-pretraga',
  standalone: true,
  imports: [
    AsyncPipe,
    NgForOf,
    RouterLink
  ],
  templateUrl: './pretraga.component.html',
  styleUrl: './pretraga.component.css'
})
export class PretragaComponent implements OnInit{

  saloni:Salon[]=[];

  constructor(private salonService:SalonService,
              private kategorijaUslugeService : KategorijaUslugeService) {
  }

  ngOnInit(): void {
    this.salonService.getAllSaloni().subscribe((saloni) => {
      this.saloni = saloni;

      this.saloni.forEach((salon) => {
        this.kategorijaUslugeService.getKategorijeUslugaBySalonId(salon.id).subscribe((kategorije) => {
          salon.kategorijeUsluga = kategorije;



          salon.kategorijeUsluga.forEach((kategorija) => {
            this.kategorijaUslugeService.getUslugeByKategorijaId(kategorija.id).subscribe((usluge) => {
              kategorija.usluge = usluge;
            });
          });




        });
      });
    });
  }




}
