import { Component, OnInit } from '@angular/core';
import { AgendamentoService } from '../shared/services/agendamento.service';
import { UsuarioLogado } from '../shared/type/usuario.type';
import { ConsultaService } from '../shared/services/consulta.service';
import { Router } from '@angular/router';
import { AgendamentoListagem } from '../shared/type/agendamento.type';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-listar-agendamentos-hoje',
  templateUrl: './listar-agendamentos-hoje.component.html',
  styleUrls: ['./listar-agendamentos-hoje.component.css']
})
export class ListarAgendamentosHojeComponent implements OnInit {

  colunas = ['Id', 'Paciente', 'Data de Nascimento', 'Médico', 'Hora agendada', 'Observações', 'Data e hora do término'];
  dataSource : MatTableDataSource<AgendamentoListagem>;

  dataHoje = new Date().toLocaleDateString();

  usuario: UsuarioLogado;

  constructor(private route: Router, private agendamentoService : AgendamentoService, private consultaService: ConsultaService) { }

  ngOnInit() {
    this.usuario = JSON.parse(sessionStorage.getItem('UsuarioLogado'));
    if(this.usuario.tipo == 'Médico') {
      this.colunas = ['Id', 'Paciente', 'Data de Nascimento', 'Médico', 'Hora agendada', 'Observações', 'Data e hora do término', 'Registrar atendimento'];
    }
    this.obterAgendamentosDataAtual();
  }

  obterAgendamentosDataAtual() {
    let id = this.usuario.tipo == 'Médico' ? this.usuario.id : "";
    this.agendamentoService.obterAgendamentosDataAtual(id).subscribe((res: AgendamentoListagem[]) => {
      this.dataSource = new MatTableDataSource<AgendamentoListagem>(res);
      console.log(res);
    });;
  }

  registrarAtendimento(i: number) {
    this.consultaService.agendamento = this.dataSource.data[i];
    this.consultaService.modoLeitura = false;
    this.consultaService.modoEdicao = false;
    this.route.navigate(['/principal/gerenciarConsulta']);
  }

}
