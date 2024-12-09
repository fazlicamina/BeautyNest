import { TestBed } from '@angular/core/testing';

import { MojProfilService } from './moj-profil.service';

describe('MojProfilService', () => {
  let service: MojProfilService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MojProfilService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
