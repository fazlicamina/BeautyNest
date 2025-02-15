import {Component, OnInit} from '@angular/core';
import {RezervacijaService} from '../../salon/services/rezervacija.service';
import {DatePipe, NgForOf, NgIf} from '@angular/common';
import Modal from 'bootstrap/js/dist/modal';
import {FormsModule} from '@angular/forms';
import {RecenzijeService} from '../../salon/services/recenzije.service';
import {Router} from '@angular/router';
import {ToastserviceService} from '../../../core/services/toastservice.service';

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

  currentPage: number = 1;
  totalPages: number = 1;

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

  trenutniFilter: boolean | null = null;

  constructor(private rezervacijaService: RezervacijaService,
              private recenzijeService: RecenzijeService,
              private router: Router,
              private toastService : ToastserviceService) {}

  ngOnInit(): void {
    this.ucitajRezervacije(1);
  }


  promijeniTab(filter: boolean | null) {
    this.trenutniFilter = filter;
    this.ucitajRezervacije(1);
  }



  ucitajRezervacije(page: number) {
    const pageSize = 5;

    this.rezervacijaService.getMojeRezervacije(page, pageSize, this.trenutniFilter).subscribe(
      (data) => {
        console.log('API odgovor:', data);
        if (data && data.items) {
          this.rezervacije = data.items;
          this.totalPages = data.totalPages;
          this.currentPage = page;
        } else {
          this.rezervacije = [];
        }
      },
      (error) => {
        console.error('Greška pri dohvaćanju rezervacija:', error);
        this.rezervacije = [];
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
    const files: FileList = event.target.files;
    if (files.length > 0) {
      this.recenzija.slike = Array.from(files);
    }
  }


  submitRecenzija() {
    if (!this.recenzija.tekst.trim()) {
      alert("Recenzija ne može biti prazna.");
      return;
    }

    this.recenzijeService.dodajRecenziju(this.recenzija, this.recenzija.slike).subscribe({
      next: () => {
        this.toastService.showSuccessToast("Uspješno ste dodali recenziju!");
        const modalElement = document.getElementById('recenzijaModal');
        if (modalElement) {
          const modalInstance = Modal.getInstance(modalElement);
          modalInstance?.hide();


          setTimeout(() => {
            window.location.reload();
          }, 1500);
        }
      },
      error: (error) => {
        console.error('Greška prilikom slanja recenzije:', error);
        this.toastService.showErrorToast("Došlo je do greške. Pokušajte ponovo!");
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
      }, 500);
    });
  }


  protected readonly Math = Math;
}
