import { Component, OnInit } from '@angular/core';
import { Agendamento } from '../tela-principal/agendamento-listagem.type';
import { ListarAgendamentoService } from '../listar-agendamentos.service';

@Component({
  selector: 'app-listar-agendamentos-hoje',
  templateUrl: './listar-agendamentos-hoje.component.html',
  styleUrls: ['./listar-agendamentos-hoje.component.css']
})
export class ListarAgendamentosHojeComponent implements OnInit {

  colunas = ['Paciente', 'Data de Nascimento', 'Médico', 'Data e hora agendada', 'Observações', 'Data e hora do término'];
  dataSource : Agendamento[];

  constructor(private agendamentoService : ListarAgendamentoService) { }

  ngOnInit() {
    this.obterAgendamentosDataAtual();
  }

  obterAgendamentosDataAtual() {
    this.agendamentoService.obterAgendamentosDataAtual().subscribe((res: Agendamento[]) => {
      this.dataSource = res;
      console.log(res);
    });;
  }

}
