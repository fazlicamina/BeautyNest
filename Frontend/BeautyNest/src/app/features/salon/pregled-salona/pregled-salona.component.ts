import {Component, OnDestroy, OnInit} from '@angular/core';
import {SalonService} from '../services/salon.service';
import {Observable, Subscription} from 'rxjs';
import {ActivatedRoute, RouterLink} from '@angular/router';
import {Salon} from '../models/salon';
import {AsyncPipe, DatePipe, NgClass, NgForOf, NgIf} from '@angular/common';
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
    RouterLink,
    DatePipe,
    NgClass
  ],
  templateUrl: './pregled-salona.component.html',
  styleUrl: './pregled-salona.component.css'
})
export class PregledSalonaComponent implements OnInit, OnDestroy{

  id:number | null=null;
  paramsSubscription?:Subscription;
  salon$?:Observable<Salon>;
  kategorijeUsluga: KategorijaUsluge[] = [];

  selectedUsluge: { naziv: string; cijena: number }[] = [];
  totalCijena: number = 0;


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

  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
  }

}
