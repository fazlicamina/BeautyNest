import {Component, OnInit} from '@angular/core';
import {RezervacijaService} from '../../salon/services/rezervacija.service';
import {DatePipe, NgForOf, NgIf} from '@angular/common';
import Modal from 'bootstrap/js/dist/modal';
import {FormsModule} from '@angular/forms';
import {RecenzijeService} from '../../salon/services/recenzije.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-moje-rezervacije',
  standalone: true,
  imports: [
    DatePipe,
    NgForOf,
    NgIf,
    FormsModule
  ],
  templateUrl: './moje-rezervacije.component.html',
  styleUrl: './moje-rezervacije.component.css'
})
export class MojeRezervacijeComponent implements OnInit{

  rezervacije: any[] = [];
  trenutniDatum: Date = new Date();
  rezervacijaZaBrisanje: number | null = null;

  rezervacijaZaRecenziju: number | null = null;

  recenzija: any = {
    rezervacijaId: 0,
    ocjena: 5,
    tekst: '',
    slike: []
  };


  constructor(private rezervacijaService: RezervacijaService,
              private recenzijeService: RecenzijeService,
              private router: Router) {}

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

  openRecenzijaModal(id: number) {
    this.rezervacijaZaRecenziju = id;
    this.recenzija = { rezervacijaId: id, ocjena: 5, tekst: '', slike: [] };
    const modalElement = document.getElementById('recenzijaModal');
    if (modalElement) {
      const modal = new Modal(modalElement);
      modal.show();
    }
  }

  onFileSelected(event: any) {
    const files = event.target.files;
    if (files.length > 0) {
      this.recenzija.slike = [];
      for (let file of files) {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => {
          this.recenzija.slike.push(reader.result?.toString().split(',')[1]); // Uklanja 'data:image/...;base64,'
        };
      }
    }
  }

  submitRecenzija() {
    this.recenzijeService.dodajRecenziju(this.recenzija).subscribe({
      next: () => {
        alert('Recenzija uspješno dodana!');
        this.recenzija = { rezervacijaId: 0, ocjena: 5, tekst: '', slike: [] }; // Reset forme
        const modalElement = document.getElementById('recenzijaModal');
        if (modalElement) {
          const modalInstance = Modal.getInstance(modalElement);
          modalInstance?.hide();
          window.location.reload();
        }
      },
      error: (error) => {
        console.error('Greška prilikom slanja recenzije:', error);
        alert('Došlo je do greške prilikom dodavanja recenzije.');
      }
    });
  }


  pogledajRecenziju(rezervacija: any) {
    const salonId = rezervacija.salonId;
    const rezId = rezervacija.id;

    this.router.navigate([`/saloni/${salonId}`], { fragment: rezId }).then(() => {
      setTimeout(() => {
        const element = document.getElementById(rezId);
        if (element) {
          element.scrollIntoView({ behavior: 'smooth', block: 'start' });
        }
      }, 500); // Sačekaj malo da se sadržaj učita
    });
  }


}
