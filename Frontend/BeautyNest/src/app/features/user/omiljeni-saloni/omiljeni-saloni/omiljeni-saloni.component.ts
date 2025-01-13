import {Component, OnInit} from '@angular/core';
import {SalonService} from '../../../salon/services/salon.service';
import {NgClass, NgForOf} from '@angular/common';
import {RouterLink} from '@angular/router';
import {GradService} from '../../../salon/services/grad.service';
import {ToastserviceService} from '../../../../core/services/toastservice.service';


@Component({
  selector: 'app-omiljeni-saloni',
  standalone: true,
  imports: [
    NgForOf,
    RouterLink,
    NgClass
  ],
  templateUrl: './omiljeni-saloni.component.html',
  styleUrl: './omiljeni-saloni.component.css'
})
export class OmiljeniSaloniComponent implements OnInit {

  saloni: any[] = [];
  private gradovi: any[] = [];

  constructor(private salonService: SalonService, private gradService:GradService,
              private toastService:ToastserviceService){}

  toggleOmiljeniSalon(salon: any) {
    this.salonService.toggleOmiljeniSalon(salon.id).subscribe((response: any) => {

      if (response.message.includes("dodano")) {
        salon.jeOmiljeni = true;
        this.toastService.showSuccessToast('Dodano u omiljene!')
      } else {
        salon.jeOmiljeni = false;
        // Ukloni salon sa liste ako je uklonjen iz omiljenih
        this.saloni = this.saloni.filter(s => s.id !== salon.id);
        this.toastService.showSuccessToast('Uklonjeno iz omiljenih!')
      }
    });
  }


  private loadOmiljeniSaloni() {
    this.salonService.getOmiljeniSaloni().subscribe((data: any[]) => {
      this.saloni = data.map(salon => ({
        ...salon,
        jeOmiljeni: true,
        nazivGrada: this.gradovi.find(g => g.id === salon.gradId)?.naziv || 'Nepoznato'
      }));
    });
  }

  ngOnInit(): void {
    this.gradService.getGradovi().subscribe((gradovi) => {
      this.gradovi = gradovi;
      this.loadOmiljeniSaloni();
    });
  }

}
