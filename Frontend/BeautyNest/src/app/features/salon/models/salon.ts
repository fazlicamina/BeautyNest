import {Kategorija} from '../../kategorija/models/kategorija';

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
}
