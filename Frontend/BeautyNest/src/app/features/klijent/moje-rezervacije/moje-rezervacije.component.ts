import {Component, OnInit} from '@angular/core';
import {RezervacijaService} from '../../salon/services/rezervacija.service';
import {DatePipe, NgForOf, NgIf} from '@angular/common';
import Modal from 'bootstrap/js/dist/modal';

@Component({
  selector: 'app-moje-rezervacije',
  standalone: true,
  imports: [
    DatePipe,
    NgForOf,
    NgIf
  ],
  templateUrl: './moje-rezervacije.component.html',
  styleUrl: './moje-rezervacije.component.css'
})
export class MojeRezervacijeComponent implements OnInit{

  rezervacije: any[] = [];
  trenutniDatum: Date = new Date();

  rezervacijaZaBrisanje: number | null = null;

  constructor(private rezervacijaService: RezervacijaService) {}

  ngOnInit(): void {
    this.ucitajRezervacije();
  }

  ucitajRezervacije() {
    this.rezervacijaService.getMojeRezervacije().subscribe(
      (data) => {
        this.rezervacije = data;
      },
      (error) => {
        console.error('Greška pri dohvaćanju rezervacija:', error);
      }
    );
  }

  openModal(id: number) {
    this.rezervacijaZaBrisanje = id;
    const modalElement = document.getElementById('confirmModal');
    if (modalElement){
     const modal = new Modal(modalElement);
    modal.show();}
  }

  confirmCancel() {
    if (this.rezervacijaZaBrisanje !== null) {
      console.log(this.rezervacijaZaBrisanje);
      this.rezervacijaService.otkaziRezervaciju(this.rezervacijaZaBrisanje).subscribe(
        () => {
          this.rezervacije = this.rezervacije.filter(r => r.id !== this.rezervacijaZaBrisanje);
          this.rezervacijaZaBrisanje = null;
        },
        (error) => {
          console.error('Greška pri otkazivanju rezervacije:', error);
        }
      );
    }
  }

  getNadolazeceRezervacije(): any[] {
    return this.rezervacije.filter(rezervacija => new Date(rezervacija.datumRezervacije) > this.trenutniDatum);
  }

  getZavrseneRezervacije(): any[] {
    return this.rezervacije.filter(rezervacija => new Date(rezervacija.datumRezervacije) < this.trenutniDatum);
  }

  isZavrsena(rezervacija: any): boolean {
    return new Date(rezervacija.datumRezervacije) < this.trenutniDatum;
  }

  isOtkazivanjeMoguce(rezervacija: any): boolean {
    const datumRezervacije = new Date(rezervacija.datumRezervacije);
    const razlikaUDanima = Math.floor((datumRezervacije.getTime() - this.trenutniDatum.getTime()) / (1000 * 60 * 60 * 24));
    return razlikaUDanima >= 2; // Otkazivanje je moguće ako je razlika 2 ili više dana
  }

}
