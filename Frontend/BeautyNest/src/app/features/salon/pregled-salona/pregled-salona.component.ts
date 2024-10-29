import {Component, OnDestroy, OnInit} from '@angular/core';
import {SalonService} from '../services/salon.service';
import {Observable, Subscription} from 'rxjs';
import {ActivatedRoute} from '@angular/router';
import {Salon} from '../models/salon';
import {AsyncPipe, NgForOf, NgIf} from '@angular/common';

@Component({
  selector: 'app-pregled-salona',
  standalone: true,
  imports: [
    AsyncPipe,
    NgIf,
    NgForOf
  ],
  templateUrl: './pregled-salona.component.html',
  styleUrl: './pregled-salona.component.css'
})
export class PregledSalonaComponent implements OnInit, OnDestroy{

  id:number | null=null;
  paramsSubscription?:Subscription;
  salon$?:Observable<Salon>;

  constructor(private salonService:SalonService, private route:ActivatedRoute) {
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
    }

  }

  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
  }

}
