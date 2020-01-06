import { Component, OnInit, ViewChild } from '@angular/core';
import { PacienteService } from '../shared/services/paciente.service';
import { MatTableDataSource, MatPaginator } from '@angular/material';
import { UsuarioLogado } from '../shared/type/usuario.type';
import Swal from 'sweetalert2';
import { NgForm } from '@angular/forms';
import { isUndefined } from 'util';
import { Router } from '@angular/router';
import { PacienteTabelaListar } from '../shared/type/paciente.type';

@Component({
  selector: 'app-lista-pacientes',
  templateUrl: './lista-pacientes.component.html',
  styleUrls: ['./lista-pacientes.component.css']
})
export class ListaPacientesComponent implements OnInit {

  usuario: UsuarioLogado;
  colunas: string[] = ['Id.', 'Nome', 'CPF', 'Telefone', 'E-mail', 'Data de Nascimento', 'Cidade', 'Consultas agendadas', 'Consultas realizadas', 'Ações'];
  dataSource: MatTableDataSource<PacienteTabelaListar>;

  constructor(private pacienteService: PacienteService, private route: Router) { }

  ngOnInit() {
    this.usuario = JSON.parse(sessionStorage.getItem('UsuarioLogado'));
  }

  onSubmit(pesquisarForm : NgForm) {
    const nome = isUndefined(pesquisarForm.value.nome) ? 'naoha' : pesquisarForm.value.nome;
    const cpf = isUndefined(pesquisarForm.value.cpf) ? 'naoha' : pesquisarForm.value.cpf;
    const dataInicio = isUndefined(pesquisarForm.value.dataInicio) ? "0001-01-01T00:00:00" : pesquisarForm.value.dataInicio.toISOString(); 
    const dataFim = isUndefined(pesquisarForm.value.dataFim) ? "0001-01-01T00:00:00" : pesquisarForm.value.dataFim.toISOString();

    if(dataInicio <= dataFim) {
      this.pacienteService.obterPacientesComFiltro(nome, cpf, dataInicio, dataFim).subscribe(lista => {
        this.dataSource = new MatTableDataSource<PacienteTabelaListar>(lista);
        console.log(lista);
      });
    }

  }

  visualizarPaciente(i: number) {
    this.pacienteService.obterPacienteCompleto(this.dataSource.data[i].idPaciente).subscribe(paciente => {
      this.pacienteService.pacienteTransferencia = paciente;
      console.log(paciente);
      console.log(this.pacienteService.pacienteTransferencia);
      this.pacienteService.modoLeitura = true;
      this.pacienteService.modoEdicao = false;
      this.route.navigate(['/principal/gerenciarPaciente']);
    });
  }

  editarPaciente(i: number) {
    this.pacienteService.obterPacienteCompleto(this.dataSource.data[i].idPaciente).subscribe(paciente => {
      this.pacienteService.pacienteTransferencia = paciente;
      console.log(paciente);
      console.log(this.pacienteService.pacienteTransferencia);
      this.pacienteService.modoLeitura = false;
      this.pacienteService.modoEdicao = true;
      this.route.navigate(['/principal/gerenciarPaciente']);
    });
  }

  excluirPaciente(i: number) {
    if (this.dataSource.data[i].quantidadeConsultas > 0 || this.dataSource.data[i].quantidadeAgendamentosPendentes > 0) {
      Swal.fire({ title: 'Não foi possível realizar esta operação', text: 'Você não pode excluir um paciente que já teve uma consulta registrada ou possui agendamento pendente!', icon: 'error' });
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
          this.pacienteService.excluirPaciente(this.dataSource.data[i].idPaciente).subscribe(resultado => {
            console.log(resultado);
            if (resultado.id == 1) {
              this.dataSource.data.splice(i, 1);
              this.dataSource = new MatTableDataSource<PacienteTabelaListar>(this.dataSource.data);
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
