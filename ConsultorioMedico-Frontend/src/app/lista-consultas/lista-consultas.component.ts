import { Component, OnInit } from '@angular/core';
import { ConsultaService } from '../shared/consulta.service';
import { PacienteParaListagem } from '../shared/paciente-para-listar.type';
import { ListarPaciente } from '../shared/listar-paciente.service';

@Component({
  selector: 'app-lista-consultas',
  templateUrl: './lista-consultas.component.html',
  styleUrls: ['./lista-consultas.component.css']
})
export class ListaConsultasComponent implements OnInit {

  constructor(private consultaService: ConsultaService, private pacienteService: ListarPaciente) { }

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
