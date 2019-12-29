import { Component, OnInit } from '@angular/core';
import { ListarPaciente } from '../shared/listar-paciente.service';
import { PacienteParaListagem } from '../shared/paciente-para-listar.type';
import { MedicoParaListagem } from '../shared/medico-para-listar.type';
import { ListarMedico } from '../shared/listar-medico.service';
import { PacienteParaAgendamento } from '../shared/paciente-para-agendamento.type';
import { NgForm, FormControl } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { ListarAgendamentoService } from '../listar-agendamentos.service';
import { AgendamentoParaCadastrar } from '../shared/agendamento-para-cadastrar.type';
import Swal from 'sweetalert2';
import { error } from 'util';
import { Agendamento } from '../tela-principal/agendamento-listagem.type';

@Component({
  selector: 'app-agendar-consulta',
  templateUrl: './agendar-consulta.component.html',
  styleUrls: ['./agendar-consulta.component.css']
})
export class AgendarConsultaComponent implements OnInit {

  pacienteParaAgendar : PacienteParaAgendamento = null;
  listaPacientes : PacienteParaListagem[] = [];
  listaMedicos : MedicoParaListagem[] = [];

  dataHoje = new Date();

  // Campos usados para iniciar o formulário quando este é usado para edição
  hora : string;
  paciente : number;
  medico : number;
  observacoes : string;
  agendamento : Agendamento = null;
  // data1 = new Date(2019, 12, 30);
  data1;
  
  constructor(private listarPaciente : ListarPaciente, private listarMedico : ListarMedico, private agendamentoService : ListarAgendamentoService) { }

  ngOnInit() {
    setTimeout(function () {
      console.log('Test');
    }, 1000/60);
    this.popularListaPacientes(function() { console.log('fui'); });
    setTimeout(function () {
      console.log('Test');
    }, 1000/60);
    this.popularListaMedicos(function() { console.log('fui também'); });
    setTimeout(function () {
      console.log('Test');
    }, 1000/60);
    this.inicio(function() { console.log('fui haha'); });
  }

  filtro = (d: Date): boolean => {
    const day = d.getDay();
    return day !== 0 && day !== 6;
  }

  popularListaPacientes(_callback) {
    this.listarPaciente.obterTodosPacientes().subscribe(p => {
      this.listaPacientes = p;
      console.log(p);
    });
    _callback();
  }

  popularListaMedicos(_callback) {
    this.listarMedico.obterTodosMedicos().subscribe(m => {
      this.listaMedicos = m;
      console.log(m);
    });
    _callback();
  }

  pacienteSelecionado(i : number) {
    this.listarPaciente.obterPacienteParaAgendamento(this.listaPacientes[i].id).subscribe(paciente => {
      this.pacienteParaAgendar = paciente;
      console.log(paciente);
    });
  }

  inicio(_callback) {
    if(this.agendamentoService.agendamentoTransferencia != null) {
      this.agendamento = this.agendamentoService.agendamentoTransferencia;

      console.log(this.agendamento);
      this.data1 = this.agendamento.dataHoraAgendamento;
      console.log(this.listaPacientes.length);
      for (let i = 0; i < this.listaPacientes.length; i++) {
        if(this.agendamento.pacienteListarViewModel.idPaciente == this.listaPacientes[i].id) {
          this.paciente = i;
          this.pacienteSelecionado(i);
          console.log('Achei o paciente');
          break;
        }
      }
      for (let i = 0; i < this.listaMedicos.length; i++) {
        if(this.agendamento.medicoListarViewModel.idMedico == this.listaMedicos[i].idMedico) {
          this.medico = i;
          console.log('Achei o médico');
          break;
        }
      }
      this.observacoes = this.agendamento.observacoes;
    }
    this.agendamentoService.agendamentoTransferencia = null;
    _callback();
  }

  onSubmit(agendamentoForm : NgForm) {
    let agendamento = new AgendamentoParaCadastrar(new Date(agendamentoForm.value.data.toISOString().substring(0, 10) + ' ' + agendamentoForm.value.hora), new Date(), agendamentoForm.value.observacoes , this.listaMedicos[agendamentoForm.value.medico].idMedico, this.listaPacientes[agendamentoForm.value.paciente].id);
    console.log(agendamento);
    this.agendamentoService.cadastrarAgendamento(agendamento).subscribe(resultado => {
      console.log(resultado);
      (resultado.id == 1) ? Swal.fire({title: 'Sucesso', icon: 'success', text: resultado.texto}) : Swal.fire({title: 'Ops...', icon: 'error', text: resultado.texto});
    })
    this.pacienteParaAgendar = null;
    agendamentoForm.reset();
    this.agendamentoService.agendamentoTransferencia = null;
  }

}
