import { Component, OnInit, ViewChild } from '@angular/core';
import { ListarPaciente } from '../shared/listar-paciente.service';
import { MatTableDataSource, MatPaginator } from '@angular/material';
import { PacienteTabelaListar } from './paciente-tabela-listar.type';
import { UsuarioLogado } from '../shared/usuario.type';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-lista-pacientes',
  templateUrl: './lista-pacientes.component.html',
  styleUrls: ['./lista-pacientes.component.css']
})
export class ListaPacientesComponent implements OnInit {

  usuario: UsuarioLogado;
  colunas: string[] = ['Id.', 'Nome', 'CPF', 'Telefone', 'E-mail', 'Data de Nascimento', 'Cidade', 'Consultas agendadas', 'Consultas realizadas', 'Ações'];
  dataSource: MatTableDataSource<PacienteTabelaListar>;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  constructor(private pacienteService: ListarPaciente) { }

  ngOnInit() {
    this.usuario = JSON.parse(localStorage.getItem('UsuarioLogado'));
    this.pacienteService.obterPacientesListaPaciente().subscribe(lista => {
      this.dataSource = new MatTableDataSource<PacienteTabelaListar>(lista);
      this.dataSource.paginator = this.paginator;
      console.log(lista);
    });
  }

  excluirPaciente(i: number) {
    if (this.dataSource.data[i].quantidadeConsultas > 0) {
      if (this.usuario.tipo == 'Médico') {
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
                Swal.fire('Excluído!', resultado.texto, 'success');
              } else {
                Swal.fire('Ops...', resultado.texto, 'error');
              }
            });
          }
        });
      } else {
        Swal.fire({ title: 'Não foi possível realizar esta operação', text: 'Você não possui permissão para excluir um paciente que já teve uma consulta registrada', icon: 'warning' });
      }
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
