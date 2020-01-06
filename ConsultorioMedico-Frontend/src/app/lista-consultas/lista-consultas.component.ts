import { Component, OnInit } from '@angular/core';
import { ConsultaService } from '../shared/services/consulta.service';
import { PacienteService } from '../shared/services/paciente.service';
import { PacienteParaListagem, PacienteAgendamentoListagem } from '../shared/type/paciente.type';
import { MatTable, MatTableDataSource } from '@angular/material';
import { ConsultaListar, ConsultaAgendamentoListagem } from '../shared/type/consulta.type';
import { NgForm } from '@angular/forms';
import { isUndefined } from 'util';
import Swal from 'sweetalert2';
import { AgendamentoListagem } from '../shared/type/agendamento.type';
import { MedicoAgendamentoListagem } from '../shared/type/medico.type';
import { Router } from '@angular/router';
import { UsuarioLogado } from '../shared/type/usuario.type';

@Component({
  selector: 'app-lista-consultas',
  templateUrl: './lista-consultas.component.html',
  styleUrls: ['./lista-consultas.component.css']
})
export class ListaConsultasComponent implements OnInit {

  usuario: UsuarioLogado;

  dataSource: MatTableDataSource<ConsultaListar>;

  listaPacientes: PacienteParaListagem[];

  filtrarPorDataConsulta: boolean = true;
  filtrarPorDataAgendamento: boolean = false;
  filtrarPorPaciente: boolean = false;

  colunas: string[] = ['Id.', 'Nome do paciente', 'Data de Nascimento', 'Nome do médico', 'Data e hora agendada', 'Data e hora término', 'Ações'];

  constructor(private router: Router, private consultaService: ConsultaService, private pacienteService: PacienteService) { }

  ngOnInit() {
    this.usuario = JSON.parse(sessionStorage.getItem('UsuarioLogado'));
  }

  filtro = (d: Date): boolean => {
    const day = d.getDay();
    return day !== 0 && day !== 6 && d <= (new Date());
  }

  carregarListaPacientes() {
    if (this.filtrarPorPaciente) {
      this.pacienteService.obterTodosPacientes().subscribe(lista => {
        this.listaPacientes = lista;
        console.log(lista);
      });
    }
  }

  onSubmit(pesquisarForm: NgForm) {
    const dataHoraTerminoConsulta = isUndefined(pesquisarForm.value.dataConsulta) ? "0001-01-01T00:00:00" : pesquisarForm.value.dataConsulta.toISOString();
    const dataHoraAgendamento = isUndefined(pesquisarForm.value.dataAgendamento) ? "0001-01-01T00:00:00" : pesquisarForm.value.dataAgendamento.toISOString();
    const idPaciente = isUndefined(pesquisarForm.value.paciente) ? 'naoha' : this.listaPacientes[pesquisarForm.value.paciente].id;

    this.consultaService.obterConsultasCompletasComFiltro(dataHoraTerminoConsulta, dataHoraAgendamento, idPaciente).subscribe(lista => {
      this.dataSource = new MatTableDataSource<ConsultaListar>(lista);
      console.log(lista);
    });
  }

  excluirConsulta(i: number) {
    if (this.usuario.tipo == 'Médico') {
      let consulta = this.dataSource.data[i];
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
          this.consultaService.deletarConsulta(consulta.idConsulta).subscribe(resultado => {
            console.log(resultado);
            if (resultado.id == 1) {
              this.dataSource.data.splice(i, 1);
              this.dataSource = new MatTableDataSource<ConsultaListar>(this.dataSource.data);
              Swal.fire('Excluído!', resultado.texto, 'success');
            } else {
              Swal.fire('Ops...', resultado.texto, 'error');
            }
          });
        }
      });
    } else {
      Swal.fire({title: 'Ops...', text: 'Você não possui permissão para essa operação', icon: 'error'});
    }
  }

  editarConsulta(i: number) {
    if (this.usuario.tipo == 'Médico') {
      let agendamento = this.dataSource.data[i].agendamentoParaListagemDeConsultaViewModel;
      let medico = new MedicoAgendamentoListagem(agendamento.medico.idMedico, agendamento.medico.nomeMedico);
      let paciente = new PacienteAgendamentoListagem(agendamento.paciente.idPaciente, agendamento.paciente.nomePaciente, agendamento.paciente.dataNascimento);
      let consulta = new ConsultaAgendamentoListagem(this.dataSource.data[i].idConsulta, this.dataSource.data[i].dataHoraTerminoConsulta, this.dataSource.data[i].receitaMedica, new Date(this.dataSource.data[i].duracaoConsulta));

      this.consultaService.agendamento = new AgendamentoListagem(agendamento.idAgendamento, agendamento.dataHoraAgendamento, agendamento.dataHoraRegistro, agendamento.observacoes, medico, paciente, consulta);
      this.consultaService.modoLeitura = false;
      this.consultaService.modoEdicao = true;

      this.router.navigate(['/principal/gerenciarConsulta']);
    } else {
      Swal.fire({title: 'Ops...', text: 'Você não possui permissão para essa operação', icon: 'error'});
    }
  }

  visualizarConsulta(i: number) {
    let agendamento = this.dataSource.data[i].agendamentoParaListagemDeConsultaViewModel;
    let medico = new MedicoAgendamentoListagem(agendamento.medico.idMedico, agendamento.medico.nomeMedico);
    let paciente = new PacienteAgendamentoListagem(agendamento.paciente.idPaciente, agendamento.paciente.nomePaciente, agendamento.paciente.dataNascimento);
    let consulta = new ConsultaAgendamentoListagem(this.dataSource.data[i].idConsulta, this.dataSource.data[i].dataHoraTerminoConsulta, this.dataSource.data[i].receitaMedica, new Date(this.dataSource.data[i].duracaoConsulta));

    this.consultaService.agendamento = new AgendamentoListagem(agendamento.idAgendamento, agendamento.dataHoraAgendamento, agendamento.dataHoraRegistro, agendamento.observacoes, medico, paciente, consulta);
    this.consultaService.modoLeitura = true;
    this.consultaService.modoEdicao = false;

    this.router.navigate(['/principal/gerenciarConsulta']);
  }

}
