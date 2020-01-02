import { Component, OnInit } from '@angular/core';
import { ListarAgendamentoService } from '../listar-agendamentos.service';
import { Agendamento } from '../tela-principal/agendamento-listagem.type';
import { UsuarioLogado } from '../shared/usuario.type';
import { ConsultaService } from '../shared/consulta.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-listar-agendamentos-hoje',
  templateUrl: './listar-agendamentos-hoje.component.html',
  styleUrls: ['./listar-agendamentos-hoje.component.css']
})
export class ListarAgendamentosHojeComponent implements OnInit {

  colunas = ['Id', 'Paciente', 'Data de Nascimento', 'Médico', 'Hora agendada', 'Observações', 'Data e hora do término'];
  dataSource : Agendamento[];

  dataHoje = new Date().toLocaleDateString();

  usuario: UsuarioLogado;

  constructor(private route: Router, private agendamentoService : ListarAgendamentoService, private consultaService: ConsultaService) { }

  ngOnInit() {
    this.usuario = JSON.parse(localStorage.getItem('UsuarioLogado'));
    if(this.usuario.tipo == 'Médico') {
      this.colunas = ['Id', 'Paciente', 'Data de Nascimento', 'Médico', 'Hora agendada', 'Observações', 'Data e hora do término', 'Registrar atendimento'];
    }
    this.obterAgendamentosDataAtual();
  }

  obterAgendamentosDataAtual() {
    this.agendamentoService.obterAgendamentosDataAtual().subscribe((res: Agendamento[]) => {
      this.dataSource = res;
      console.log(res);
    });;
  }

  registrarAtendimento(i: number) {
    this.consultaService.agendamento = this.dataSource[i];
    this.route.navigate(['/principal/gerenciarConsulta']);
  }

}
