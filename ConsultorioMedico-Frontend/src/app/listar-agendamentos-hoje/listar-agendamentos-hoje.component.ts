import { Component, OnInit } from '@angular/core';
import { AgendamentoService } from '../shared/services/agendamento.service';
import { UsuarioLogado } from '../shared/type/usuario.type';
import { ConsultaService } from '../shared/services/consulta.service';
import { Router } from '@angular/router';
import { AgendamentoListagem } from '../shared/type/agendamento.type';

@Component({
  selector: 'app-listar-agendamentos-hoje',
  templateUrl: './listar-agendamentos-hoje.component.html',
  styleUrls: ['./listar-agendamentos-hoje.component.css']
})
export class ListarAgendamentosHojeComponent implements OnInit {

  colunas = ['Id', 'Paciente', 'Data de Nascimento', 'Médico', 'Hora agendada', 'Observações', 'Data e hora do término'];
  dataSource : AgendamentoListagem[];

  dataHoje = new Date().toLocaleDateString();

  usuario: UsuarioLogado;

  constructor(private route: Router, private agendamentoService : AgendamentoService, private consultaService: ConsultaService) { }

  ngOnInit() {
    this.usuario = JSON.parse(localStorage.getItem('UsuarioLogado'));
    if(this.usuario.tipo == 'Médico') {
      this.colunas = ['Id', 'Paciente', 'Data de Nascimento', 'Médico', 'Hora agendada', 'Observações', 'Data e hora do término', 'Registrar atendimento'];
    }
    this.obterAgendamentosDataAtual();
  }

  obterAgendamentosDataAtual() {
    this.agendamentoService.obterAgendamentosDataAtual().subscribe((res: AgendamentoListagem[]) => {
      this.dataSource = res;
      console.log(res);
    });;
  }

  registrarAtendimento(i: number) {
    this.consultaService.agendamento = this.dataSource[i];
    this.route.navigate(['/principal/gerenciarConsulta']);
  }

}
