import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListarAgendamentosHojeComponent } from './listar-agendamentos-hoje.component';

describe('ListarAgendamentosHojeComponent', () => {
  let component: ListarAgendamentosHojeComponent;
  let fixture: ComponentFixture<ListarAgendamentosHojeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListarAgendamentosHojeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListarAgendamentosHojeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
