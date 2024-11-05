import { TestBed } from '@angular/core/testing';

import { KategorijaUslugeService } from './kategorija-usluge.service';

describe('KategorijaUslugeService', () => {
  let service: KategorijaUslugeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(KategorijaUslugeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
