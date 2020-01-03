import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GerenciarConsultaComponent } from './gerenciar-consulta.component';

describe('GerenciarConsultaComponent', () => {
  let component: GerenciarConsultaComponent;
  let fixture: ComponentFixture<GerenciarConsultaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GerenciarConsultaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GerenciarConsultaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
