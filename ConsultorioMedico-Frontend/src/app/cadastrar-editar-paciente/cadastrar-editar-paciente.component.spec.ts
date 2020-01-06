import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastrarEditarPacienteComponent } from './cadastrar-editar-paciente.component';

describe('CadastrarEditarPacienteComponent', () => {
  let component: CadastrarEditarPacienteComponent;
  let fixture: ComponentFixture<CadastrarEditarPacienteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CadastrarEditarPacienteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CadastrarEditarPacienteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
