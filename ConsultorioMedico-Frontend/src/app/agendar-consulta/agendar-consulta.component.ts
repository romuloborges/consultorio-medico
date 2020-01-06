import { Component, OnInit } from '@angular/core';
import { PacienteService } from '../shared/services/paciente.service';
import { MedicoService } from '../shared/services/medico.service';
import { NgForm } from '@angular/forms';
import { AgendamentoService } from '../shared/services/agendamento.service';
import Swal from 'sweetalert2';
import { AgendamentoParaEditar, AgendamentoParaCadastrar, AgendamentoListagem } from '../shared/type/agendamento.type';
import { PacienteParaAgendamento, PacienteParaListagem } from '../shared/type/paciente.type';
import { MedicoParaListagem } from '../shared/type/medico.type';

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
  agendamento : AgendamentoListagem = null;
  data1 = new Date();

  modoLeitura: boolean;
  modoEdicao: boolean;
  
  constructor(private listarPaciente : PacienteService, private listarMedico : MedicoService, private agendamentoService : AgendamentoService) { }

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
      this.data1 = new Date(this.agendamento.dataHoraAgendamento);
      this.hora = this.data1.getHours().toString() + ":" + this.data1.getMinutes().toString();
      console.log(this.hora);
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

      this.modoEdicao = this.agendamentoService.modoEdicao;
      this.modoLeitura = this.agendamentoService.modoLeitura;
      this.observacoes = this.agendamento.observacoes;
    } else {
      this.pacienteParaAgendar = null;
      this.data1 = null;
      this.hora = null;
      this.paciente = null;
      this.medico = null;
      this.observacoes = null;
      this.modoEdicao = false;
      this.modoLeitura = false;
    }
    this.agendamentoService.agendamentoTransferencia = null;
  }

  onSubmit(agendamentoForm : NgForm) {
    if(this.agendamento == null) {
      let dataAgora = new Date();
      let agendamento = new AgendamentoParaCadastrar(new Date(agendamentoForm.value.data.toISOString().substring(0, 10) + ' ' + agendamentoForm.value.hora), new Date(), agendamentoForm.value.observacoes , this.listaMedicos[agendamentoForm.value.medico].idMedico, this.listaPacientes[agendamentoForm.value.paciente].id);

      if(agendamento.dataHoraAgendamento >= dataAgora) {
        console.log(agendamento);
        
        this.agendamentoService.cadastrarAgendamento(agendamento).subscribe(resultado => {
          console.log(resultado);
          if(resultado.id == 1) {
            Swal.fire({title: 'Sucesso', icon: 'success', text: resultado.texto});
            
            this.pacienteParaAgendar = null;
            agendamentoForm.reset();
            this.agendamento = null;
          } else {
            Swal.fire({title: 'Ops...', icon: 'error', text: resultado.texto});
          }
        })
      } else {
        Swal.fire({title: 'Ops...', text: 'Você não pode agendar uma consulta para um horário que já foi!', icon: 'error'});
      }
    } else {
      let dataAgora = new Date();
      let agendamento = new AgendamentoParaEditar(this.agendamento.idAgendamento, new Date(agendamentoForm.value.data.toISOString().substring(0, 10) + ' ' + agendamentoForm.value.hora), new Date(), agendamentoForm.value.observacoes , this.listaMedicos[agendamentoForm.value.medico].idMedico, this.listaPacientes[agendamentoForm.value.paciente].id);

      if(agendamento.dataHoraAgendamento >= dataAgora) {
        console.log(agendamento);

        this.agendamentoService.atualizarAgendamento(agendamento).subscribe(resultado => {
          console.log(resultado);
          if(resultado.id == 1) {
            Swal.fire({title: 'Sucesso', icon: 'success', text: resultado.texto});
            
            this.pacienteParaAgendar = null;
            agendamentoForm.reset();
            this.agendamento = null;

            this.modoLeitura = false;
            this.modoEdicao = false;
          } else {
            Swal.fire({title: 'Ops...', icon: 'error', text: resultado.texto});
          }
        });
      } else {
        Swal.fire({title: 'Ops...', text: 'Você não pode agendar uma consulta para um horário que já foi!', icon: 'error'});
      }
    }
    // this.pacienteParaAgendar = null;
    // agendamentoForm.reset();
    // this.agendamento = null;
    this.agendamentoService.agendamentoTransferencia = null;
  }

}
