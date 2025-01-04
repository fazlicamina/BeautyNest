export interface RezervacijaRequest {
  salonId: number;
  userId: number;
  datumRezervacije: string;
  vrijemePocetka: string;
  uslugaIds: number[];
}
