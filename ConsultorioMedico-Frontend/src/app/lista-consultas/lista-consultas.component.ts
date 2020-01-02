import { Component, OnInit } from '@angular/core';
import { ConsultaService } from '../shared/services/consulta.service';
import { PacienteService } from '../shared/services/paciente.service';
import { PacienteParaListagem } from '../shared/type/paciente.type';

@Component({
  selector: 'app-lista-consultas',
  templateUrl: './lista-consultas.component.html',
  styleUrls: ['./lista-consultas.component.css']
})
export class ListaConsultasComponent implements OnInit {

  constructor(private consultaService: ConsultaService, private pacienteService: PacienteService) { }

  listaPacientes: PacienteParaListagem[];

  filtrarPorDataConsulta: boolean = true;
  filtrarPorDataAgendamento: boolean = false;
  filtrarPorPaciente: boolean = false;

  colunas: string[];

  ngOnInit() {
  }

  carregarListaPacientes() {
    if(this.filtrarPorPaciente) {
      this.pacienteService.obterTodosPacientes().subscribe(lista => {
        this.listaPacientes = lista;
        console.log(lista);
      });
    }
  }

}
