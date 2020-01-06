import { Component, Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { applicationUrl } from '../constantes/constantes';
import { DatePipe } from '@angular/common';
import { AgendamentoParaCadastrar, AgendamentoParaEditar, AgendamentoListagem } from '../type/agendamento.type';
import { Mensagem } from '../type/mensagem.type';

@Injectable({
  providedIn: 'root'
})

export class AgendamentoService {
  
  rota = "agendamento";
  FORMAT = "yyyy-MM-dd HH:mm:ss";

  // Agendamento passado do componente de listagem para o de edição
  agendamentoTransferencia : AgendamentoListagem = null;

  modoLeitura: boolean;
  modoEdicao: boolean;

  constructor(private datePipe: DatePipe, private httpClient: HttpClient) {

  }

  cadastrarAgendamento(agendamento: AgendamentoParaCadastrar) {
    return this.httpClient.post<Mensagem>(`${applicationUrl}/${this.rota}/cadastrar/`, agendamento);
  }

  atualizarAgendamento(agendamento: AgendamentoParaEditar) {
    return this.httpClient.put<Mensagem>(`${applicationUrl}/${this.rota}/atualizar`, agendamento);
  }

  obterAgendamentosDataAtual(id: string) {
    return this.httpClient.get<AgendamentoListagem[]>(`${applicationUrl}/${this.rota}/obterAgendamentosDataAgendada?dataAgendada=${(new Date()).toISOString()}&id=${id}`);
  }
  obterAgendamentosComFiltro(dataHoraInicio : Date, dataHoraFim : Date, idPaciente : string, idMedico : string) {
    return this.httpClient.get<AgendamentoListagem[]>(`${applicationUrl}/${this.rota}?dataHoraInicio=${dataHoraInicio}&dataHoraFim=${dataHoraFim}&idPaciente=${idPaciente}&idMedico=${idMedico}`);
  }

  excluirAgendamento(idAgendamento : string) {
    return this.httpClient.delete<Mensagem>(`${applicationUrl}/${this.rota}/${idAgendamento}`);
  }

}