import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PregledSalonaComponent } from './pregled-salona.component';

describe('PregledSalonaComponent', () => {
  let component: PregledSalonaComponent;
  let fixture: ComponentFixture<PregledSalonaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PregledSalonaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PregledSalonaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
