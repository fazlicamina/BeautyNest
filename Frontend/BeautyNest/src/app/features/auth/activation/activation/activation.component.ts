import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../../../environments/environment';
import {NgIf} from '@angular/common';


@Component({
  selector: 'app-activation',
  standalone: true,
  imports: [
    NgIf
  ],
  templateUrl: './activation.component.html',
  styleUrl: './activation.component.css'
})



export class ActivationComponent implements OnInit {
  message: string = 'Aktivacija u toku...';

  constructor(private route: ActivatedRoute, private http: HttpClient, private router: Router) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      const email = params['email'];
      let token = params['token'];


      if (email && token) {

        token = encodeURIComponent(token);

        console.log("Token poslat na backend:", token);

        this.http.post(`${environment.apiBaseUrl}api/auth/confirm-email`, { email, token }, { responseType: 'text' })
          .subscribe({
            next: (response) => {
              this.message = response;
              setTimeout(() => this.router.navigate(['/login']), 3000);
            },
            error: (error) => {
              console.error("Activation error:", error);
              if (error.error) {
                console.error("Server response:", error.error);
              }
              this.message = 'Greška pri aktivaciji naloga.';
            }
          });
      } else {
        this.message = 'Pogrešan link za aktivaciju.';
      }
    });
  }


}
