import { Component, OnInit } from '@angular/core';
import { ListarAgendamentoService } from '../listar-agendamentos.service';
import { ListarPaciente } from '../shared/listar-paciente.service';
import { PacienteParaListagem } from '../shared/paciente-para-listar.type';
import { ListarMedico } from '../shared/listar-medico.service';
import { MedicoParaListagem } from '../shared/medico-para-listar.type';
import { NgForm } from '@angular/forms';
import { Agendamento } from '../tela-principal/agendamento-listagem.type';
import Swal from 'sweetalert2';
import { isUndefined } from 'util';
import { stringify } from 'querystring';
import { UsuarioLogado } from '../shared/usuario.type';
import { Router } from '@angular/router';

@Component({
  selector: 'app-lista-agendamentos',
  templateUrl: './lista-agendamentos.component.html',
  styleUrls: ['./lista-agendamentos.component.css']
})
export class ListaAgendamentosComponent implements OnInit {

  filtrarPorData = true;
  filtrarPorPaciente = false;
  filtrarPorMedico = false;

  listaPacientes : PacienteParaListagem[];
  listaMedicos: MedicoParaListagem[];

  dataSource : Agendamento[];
  colunas = ['Paciente', 'Data de Nascimento', 'Médico', 'Data e hora agendada', 'Observações', 'Data e hora do término', 'Ações'];

  usuario : UsuarioLogado;

  constructor(private route : Router, private pacienteService : ListarPaciente, private medicoService : ListarMedico, private agendamentoService : ListarAgendamentoService) { }

  ngOnInit() {
    this.usuario = JSON.parse(localStorage.getItem('UsuarioLogado'));
  }

  filtro = (d: Date): boolean => {
    const day = d.getDay();
    return day !== 0 && day !== 6;
  }

  carregarListaPacientes() {
    if(this.filtrarPorPaciente) {
      this.pacienteService.obterTodosPacientes().subscribe(lista => {
        this.listaPacientes = lista;
        console.log(lista);
      });
    }
  }

  carregarListaMedicos() {
    if(this.filtrarPorMedico) {
      this.medicoService.obterTodosMedicos().subscribe(lista => {
        this.listaMedicos = lista;
        console.log(lista);
      });
    }
  }

  onSubmit(pesquisarForm : NgForm) {

    let requisicao = (!isUndefined(pesquisarForm.value.dataInicio) && !isUndefined(pesquisarForm.value.dataFim)) ? pesquisarForm.value.dataInicio.toISOString() + '/' + pesquisarForm.value.dataFim.toISOString() + '/' : '';
    requisicao += !isUndefined(pesquisarForm.value.paciente) ? this.listaPacientes[pesquisarForm.value.paciente].id + '/' : '';
    requisicao += !isUndefined(pesquisarForm.value.medico) ? this.listaMedicos[pesquisarForm.value.medico].idMedico : '';

    console.log(pesquisarForm.value.paciente);
    const dataInicio = isUndefined(pesquisarForm.value.dataInicio) ? "0001-01-01T00:00:00" : pesquisarForm.value.dataInicio.toISOString(); 
    const dataFim = isUndefined(pesquisarForm.value.dataFim) ? "0001-01-01T00:00:00" : pesquisarForm.value.dataFim.toISOString();
    const idPaciente = isUndefined(pesquisarForm.value.paciente) ? 'naoha' : this.listaPacientes[pesquisarForm.value.paciente].id;
    const idMedico = isUndefined(pesquisarForm.value.medico) ? 'naoha' : this.listaMedicos[pesquisarForm.value.medico].idMedico;
    
    if((dataInicio == null && dataFim == null) || (dataInicio <= dataFim)){
      this.agendamentoService.obterAgendamentosComFiltro(dataInicio, dataFim, idPaciente, idMedico).subscribe(lista => {
        this.dataSource = lista;
        console.log(lista);
      });
    } else {
      Swal.fire('Uso incorreto dos campos!', 'A data final não pode ser antes da data de início', "warning");
    }
  }

  editarAgendamento(indice : number) {
    this.agendamentoService.agendamentoTransferencia = this.dataSource[indice];
    this.route.navigate(['principal/agendarConsulta']);
  }

  excluirAgendamento(indice : number) {
    if(this.dataSource[indice].consultaViewModel != null && this.usuario.tipo != 'Médico') {
      Swal.fire({
        title: 'Não foi possível realizar esta operação',
        text: 'Você não possui permissão para excluir um agendamento que já teve sua consulta registrada',
        icon: 'warning'
      });
    } else {
      Swal.fire({
        title: 'Deseja realmente excluir?',
        text: "Você não poderá reverter esta ação!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, excluir!',
        cancelButtonText: 'Cancelar'
      }).then((result) => {
        if (result.value) {
          this.agendamentoService.excluirAgendamento(this.dataSource[indice].idAgendamento).subscribe(resultado => {
            console.log(resultado);
            if(resultado.id == 1) {
              this.dataSource.splice(indice, 1);
              console.log(this.dataSource);
              Swal.fire('Excluído!', resultado.texto, 'success');
            } else {
              Swal.fire('Ops...', resultado.texto, 'error');
            }
          });
        }
      });
    }
  }

}
