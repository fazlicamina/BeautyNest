import {Kategorija} from '../../kategorija/models/kategorija';
import {KategorijaUsluge} from './kategorija-usluge';

export interface Salon {
  id: number;
  naziv: string;
  adresa: string;
  kontaktTelefon: string;
  email: string;
  prosjecnaOcjena: number;
  opis: string;
  radnoVrijemeOd: string;
  radnoVrijemeDo: string;
  subotaRadna: boolean;
  naslovnaFotografija: string;
  kategorije: Kategorija[];
  kategorijeUsluga:KategorijaUsluge[];
  gradId:number;
  nazivGrada:string;
  slike: { id: number; url: string }[];
  jeOmiljeni?: boolean;
}
