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
  selectedGradId: number | null=null;

  constructor(private salonService:SalonService,
              private kategorijaUslugeService : KategorijaUslugeService,
              private gradService:GradService, private route: ActivatedRoute,
              private router:Router) {
  }

  ngOnInit(): void {

    this.salonService.getAllSaloni().subscribe((saloni) => {
      this.saloni = saloni;

      this.saloni.forEach((salon) => {
        this.kategorijaUslugeService.getKategorijeUslugaBySalonId(salon.id).subscribe((kategorije) => {
          salon.kategorijeUsluga = kategorije;

        });
      });


    });


    this.gradService.getGradovi().subscribe(
      (data) => {
        this.gradovi = data;
      }
    );

    this.route.params.subscribe(params => {
      this.selectedGradId = +params['cityId'] || null;
      this.getSaloniByGradId();
    });

  }

  applyFilter(): void {
    if (this.selectedGradId) {
      this.router.navigate(['/pretraga', { cityId: this.selectedGradId }]);
    } else {
      this.getSaloniByGradId();
    }
  }

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



}
