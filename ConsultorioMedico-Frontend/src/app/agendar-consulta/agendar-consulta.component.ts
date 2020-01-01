import { Component, OnInit } from '@angular/core';
import { ListarPaciente } from '../shared/listar-paciente.service';
import { PacienteParaListagem } from '../shared/paciente-para-listar.type';
import { MedicoParaListagem } from '../shared/medico-para-listar.type';
import { ListarMedico } from '../shared/listar-medico.service';
import { PacienteParaAgendamento } from '../shared/paciente-para-agendamento.type';
import { NgForm } from '@angular/forms';
import { ListarAgendamentoService } from '../listar-agendamentos.service';
import { AgendamentoParaCadastrar } from '../shared/agendamento-para-cadastrar.type';
import Swal from 'sweetalert2';
import { Agendamento } from '../tela-principal/agendamento-listagem.type';
import { AgendamentoParaEditar } from '../shared/agendamento-para-editar.type';

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
  data1 = new Date();
  
  constructor(private listarPaciente : ListarPaciente, private listarMedico : ListarMedico, private agendamentoService : ListarAgendamentoService) { }

  ngOnInit() {
    this.popularListaPacienteMedicos();
  }

  filtro = (d: Date): boolean => {
    const day = d.getDay();
    return day !== 0 && day !== 6;
  }

  popularListaPacienteMedicos() {
    this.listarPaciente.obterTodosPacientes().subscribe(p => {
      this.listaPacientes = p;
      console.log(p);

      this.listarMedico.obterTodosMedicos().subscribe(m => {
        this.listaMedicos = m;
        console.log(m);
        
        this.carregarInformacoesAgendamento();
      });
    });
  }

  pacienteSelecionado(i : number) {
    this.listarPaciente.obterPacienteParaAgendamento(this.listaPacientes[i].id).subscribe(paciente => {
      this.pacienteParaAgendar = paciente;
      console.log(paciente);
    });
  }

  carregarInformacoesAgendamento() {
    if(this.agendamentoService.agendamentoTransferencia != null) {
      this.agendamento = this.agendamentoService.agendamentoTransferencia;

      console.log(this.agendamento);
      this.data1 = this.agendamento.dataHoraAgendamento;
      this.hora = this.data1.toString().substring(11, 16);
      for (let i = 0; i < this.listaPacientes.length; i++) {
        if(this.agendamento.pacienteListarViewModel.idPaciente == this.listaPacientes[i].id) {
          this.paciente = i;
          this.pacienteSelecionado(i);
          break;
        }
      }
      for (let i = 0; i < this.listaMedicos.length; i++) {
        if(this.agendamento.medicoListarViewModel.idMedico == this.listaMedicos[i].idMedico) {
          this.medico = i;
          break;
        }
      }
      this.observacoes = this.agendamento.observacoes;
    } else {
      this.pacienteParaAgendar = null;
      this.data1 = null;
      this.hora = null;
      this.paciente = null;
      this.medico = null;
      this.observacoes = null;
    }
    this.agendamentoService.agendamentoTransferencia = null;
  }

  onSubmit(agendamentoForm : NgForm) {
    if(this.agendamento == null) {
      let agendamento = new AgendamentoParaCadastrar(new Date(agendamentoForm.value.data.toISOString().substring(0, 10) + ' ' + agendamentoForm.value.hora), new Date(), agendamentoForm.value.observacoes , this.listaMedicos[agendamentoForm.value.medico].idMedico, this.listaPacientes[agendamentoForm.value.paciente].id);

      console.log(agendamento);
      
      this.agendamentoService.cadastrarAgendamento(agendamento).subscribe(resultado => {
        console.log(resultado);
        (resultado.id == 1) ? Swal.fire({title: 'Sucesso', icon: 'success', text: resultado.texto}) : Swal.fire({title: 'Ops...', icon: 'error', text: resultado.texto});
      })
    } else {
      let agendamento = new AgendamentoParaEditar(this.agendamento.idAgendamento, new Date(agendamentoForm.value.data.toISOString().substring(0, 10) + ' ' + agendamentoForm.value.hora), new Date(), agendamentoForm.value.observacoes , this.listaMedicos[agendamentoForm.value.medico].idMedico, this.listaPacientes[agendamentoForm.value.paciente].id);

      console.log(agendamento);

      this.agendamentoService.atualizarAgendamento(agendamento).subscribe(resultado => {
        console.log(resultado);
        (resultado.id == 1) ? Swal.fire({title: 'Sucesso', icon: 'success', text: resultado.texto}) : Swal.fire({title: 'Ops...', icon: 'error', text: resultado.texto});
      });
    }
    this.pacienteParaAgendar = null;
    agendamentoForm.reset();
    this.agendamentoService.agendamentoTransferencia = null;
    this.agendamento = null;
  }

}
