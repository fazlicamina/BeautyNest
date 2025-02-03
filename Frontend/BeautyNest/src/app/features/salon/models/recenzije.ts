import {Klijent} from './klijent';

export interface Recenzija {
  id: number;
  klijentId: string;
  ocjena: number;
  tekst: string;
  usluge: { id: number; naziv: string }[];
  klijent?: Klijent;
  datumRecenzije:string;
  rezervacijaId:number;
  slike?:string[];
}
