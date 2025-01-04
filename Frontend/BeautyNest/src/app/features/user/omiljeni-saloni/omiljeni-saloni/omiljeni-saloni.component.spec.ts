import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OmiljeniSaloniComponent } from './omiljeni-saloni.component';

describe('OmiljeniSaloniComponent', () => {
  let component: OmiljeniSaloniComponent;
  let fixture: ComponentFixture<OmiljeniSaloniComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OmiljeniSaloniComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OmiljeniSaloniComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
