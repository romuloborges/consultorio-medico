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
  
  rotaAgendamento = "agendamento";
  FORMAT = "yyyy-MM-dd HH:mm:ss";

  // Agendamento passado do componente de listagem para o de edição
  agendamentoTransferencia : AgendamentoListagem = null;

  constructor(private datePipe: DatePipe, private httpClient: HttpClient) {

  }

  obterAgendamentosDataAtual() {
    return this.httpClient.get<AgendamentoListagem[]>(`${applicationUrl}/agendamento/${(new Date()).toISOString()}`);
  }

  cadastrarAgendamento(agendamento: AgendamentoParaCadastrar) {
    return this.httpClient.post<Mensagem>(`${applicationUrl}/${this.rotaAgendamento}/cadastrar/`, agendamento);
  }

  obterAgendamentosComFiltro(dataHoraInicio : Date, dataHoraFim : Date, idPaciente : string, idMedico : string, jaConsultados : boolean) {
    return this.httpClient.get<AgendamentoListagem[]>(`${applicationUrl}/${this.rotaAgendamento}?dataHoraInicio=${dataHoraInicio}&dataHoraFim=${dataHoraFim}&idPaciente=${idPaciente}&idMedico=${idMedico}&jaConsultados=${jaConsultados}`);
  }

  excluirAgendamento(idAgendamento : string) {
    return this.httpClient.delete<Mensagem>(`${applicationUrl}/${this.rotaAgendamento}/${idAgendamento}`);
  }

  atualizarAgendamento(agendamento: AgendamentoParaEditar) {
    return this.httpClient.put<Mensagem>(`${applicationUrl}/${this.rotaAgendamento}/atualizar`, agendamento);
  }

}