import { Component, OnInit } from '@angular/core';
import { ListarPaciente } from '../shared/listar-paciente.service';
import { PacienteParaListagem } from '../shared/paciente-para-listar.type';
import { MedicoParaListagem } from '../shared/medico-para-listar.type';
import { ListarMedico } from '../shared/listar-medico.service';
import { PacienteParaAgendamento } from '../shared/paciente-para-agendamento.type';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-agendar-consulta',
  templateUrl: './agendar-consulta.component.html',
  styleUrls: ['./agendar-consulta.component.css']
})
export class AgendarConsultaComponent implements OnInit {

  pacienteParaAgendar : PacienteParaAgendamento = null;
  listaPacientes : PacienteParaListagem[];
  listaMedicos : MedicoParaListagem[];

  constructor(private listarPaciente : ListarPaciente, private listarMedico : ListarMedico) { }

  ngOnInit() {
    this.popularListaPacientes();
    this.popularListaMedicos();
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
    alert(agendamentoForm.value.medico);
  }

}
