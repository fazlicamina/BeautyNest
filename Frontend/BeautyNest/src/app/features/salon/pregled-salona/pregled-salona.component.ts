import {Component, OnDestroy, OnInit} from '@angular/core';
import {SalonService} from '../services/salon.service';
import {Observable, Subscription} from 'rxjs';
import {ActivatedRoute, RouterLink} from '@angular/router';
import {Salon} from '../models/salon';
import {AsyncPipe, NgForOf, NgIf} from '@angular/common';
import {KategorijaUslugeService} from '../services/kategorija-usluge.service';
import {KategorijaUsluge} from '../models/kategorija-usluge';
import {Grad} from '../models/grad';

@Component({
  selector: 'app-pregled-salona',
  standalone: true,
  imports: [
    AsyncPipe,
    NgIf,
    NgForOf,
    RouterLink
  ],
  templateUrl: './pregled-salona.component.html',
  styleUrl: './pregled-salona.component.css'
})
export class PregledSalonaComponent implements OnInit, OnDestroy{

  id:number | null=null;
  paramsSubscription?:Subscription;
  salon$?:Observable<Salon>;
  kategorijeUsluga: KategorijaUsluge[] = [];


  activeTab: number = 0;


  constructor(private salonService:SalonService, private route:ActivatedRoute,
              private kategorijaUslugeService : KategorijaUslugeService) {
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




  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
  }

}
