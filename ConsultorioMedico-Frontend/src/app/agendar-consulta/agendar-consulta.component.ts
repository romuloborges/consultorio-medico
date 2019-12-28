import { Component, OnInit } from '@angular/core';
import { ListarPaciente } from '../shared/listar-paciente.service';
import { PacienteParaListagem } from '../shared/paciente-para-listar.type';
import { MedicoParaListagem } from '../shared/medico-para-listar.type';
import { ListarMedico } from '../shared/listar-medico.service';
import { PacienteParaAgendamento } from '../shared/paciente-para-agendamento.type';
import { NgForm } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { ListarAgendamentoService } from '../listar-agendamentos.service';
import { AgendamentoParaCadastrar } from '../shared/agendamento-para-cadastrar.type';

@Component({
  selector: 'app-agendar-consulta',
  templateUrl: './agendar-consulta.component.html',
  styleUrls: ['./agendar-consulta.component.css']
})
export class AgendarConsultaComponent implements OnInit {

  pacienteParaAgendar : PacienteParaAgendamento = null;
  listaPacientes : PacienteParaListagem[];
  listaMedicos : MedicoParaListagem[];

  dataHoje = new Date();

  constructor(private listarPaciente : ListarPaciente, private listarMedico : ListarMedico, private agendamentoService : ListarAgendamentoService) { }

  ngOnInit() {
    this.popularListaPacientes();
    this.popularListaMedicos();
  }

  filtro = (d: Date): boolean => {
    const day = d.getDay();
    return day !== 0 && day !== 6;
  }

  popularListaPacientes() {
    this.listarPaciente.obterTodosPacientes().subscribe(p => {
      this.listaPacientes = p;
      console.log(p);
    });
  }

  popularListaMedicos() {
    this.listarMedico.obterTodosMedicos().subscribe(m => {
      this.listaMedicos = m;
      console.log(m);
    });
  }

  pacienteSelecionado(i : number) {
    this.listarPaciente.obterPacienteParaAgendamento(this.listaPacientes[i].id).subscribe(paciente => {
      this.pacienteParaAgendar = paciente;
      console.log(paciente);
    });
  }

  onSubmit(agendamentoForm : NgForm) {
    let agendamento = new AgendamentoParaCadastrar(new Date(agendamentoForm.value.data.toISOString().substring(0, 10) + ' ' + agendamentoForm.value.hora), new Date(), this.listaMedicos[agendamentoForm.value.medico].idMedico, this.listaPacientes[agendamentoForm.value.paciente].id);
    console.log(agendamento);
    this.agendamentoService.cadastrarAgendamento(agendamento).subscribe(resultado => {
      console.log(resultado);
    })
  }

}
