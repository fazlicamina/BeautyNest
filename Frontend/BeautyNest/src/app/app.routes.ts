import { Routes } from '@angular/router';
import {HomeComponent} from './features/public/home/home.component';
import {PregledSalonaComponent} from './features/salon/pregled-salona/pregled-salona.component';
import {PretragaComponent} from './features/public/pretraga/pretraga.component';
import {LoginComponent} from './features/auth/login/login.component';
import {RegistracijaComponent} from './features/auth/registracija/registracija.component';
import {MojProfilComponent} from './features/user/moj-profil/moj-profil.component';
import {OmiljeniSaloniComponent} from './features/user/omiljeni-saloni/omiljeni-saloni/omiljeni-saloni.component';
import {MojeRezervacijeComponent} from './features/klijent/moje-rezervacije/moje-rezervacije.component';
import {authGuard} from './features/auth/guards/auth.guard';
import {ActivationComponent} from './features/auth/activation/activation/activation.component';


export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'saloni/:id', component: PregledSalonaComponent },
  { path: 'pretraga', component: PretragaComponent },
  { path: 'login', component: LoginComponent },
  { path: 'registracija', component: RegistracijaComponent },
  { path: 'mojprofil', component: MojProfilComponent, canActivate: [authGuard] },
  { path: 'omiljeni', component: OmiljeniSaloniComponent, canActivate: [authGuard], data: { role: 'Klijent' } },
  { path: 'moje-rezervacije', component: MojeRezervacijeComponent, canActivate: [authGuard], data: { role: 'Klijent' } },
  { path: 'activate', component: ActivationComponent }

];
