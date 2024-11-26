import { Routes } from '@angular/router';
import {HomeComponent} from './features/public/home/home.component';
import {PregledSalonaComponent} from './features/salon/pregled-salona/pregled-salona.component';
import {PretragaComponent} from './features/public/pretraga/pretraga.component';
import {LoginComponent} from './features/auth/login/login.component';
import {RegistracijaComponent} from './features/auth/registracija/registracija.component';


export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'saloni/:id', component: PregledSalonaComponent },
  { path: 'pretraga', component: PretragaComponent },
  { path: 'login', component: LoginComponent },
  { path: 'registracija', component: RegistracijaComponent }
];
