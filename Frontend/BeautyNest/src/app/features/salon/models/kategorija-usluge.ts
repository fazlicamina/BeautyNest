import {Usluga} from './usluga';

export interface KategorijaUsluge {
  id: number;
  naziv: string;
  usluge: Usluga[];
}
