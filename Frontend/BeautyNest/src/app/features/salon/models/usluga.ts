import {KategorijaUsluge} from './kategorija-usluge';

export interface Usluga {
  id: number;
  naziv: string;
  cijena: number;
  trajanje: string;
  kategorijaUslugeId: number;
  kategorijaUsluge?: KategorijaUsluge;
}

