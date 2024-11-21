import { Routes } from '@angular/router';
import {HomeComponent} from './features/public/home/home.component';
import {PregledSalonaComponent} from './features/salon/pregled-salona/pregled-salona.component';
import {PretragaComponent} from './features/public/pretraga/pretraga.component';
import {LoginComponent} from './features/auth/login/login.component';


export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'saloni/:id', component: PregledSalonaComponent },
  { path: 'pretraga', component: PretragaComponent },
  //{ path: 'pretraga/grad/:cityId', component: PretragaComponent },
  //{ path: 'pretraga/kategorija/:kategorijaId', component: PretragaComponent }
  { path: 'login', component: LoginComponent }

];
