export interface RezervacijaRequest {
  salonId: number;
  klijentId: string;
  userId: number;
  datumRezervacije: string;
  vrijemePocetka: string;
  uslugaIds: number[];
}
