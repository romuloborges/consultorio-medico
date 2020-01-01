import { Component, Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { applicationUrl } from './shared/constantes';
import { Agendamento, Paciente, Medico, Consulta } from './tela-principal/agendamento-listagem.type';
import { map } from 'rxjs/operators';
import { DatePipe } from '@angular/common';
import { AgendamentoParaCadastrar } from './shared/agendamento-para-cadastrar.type';
import { Mensagem } from './shared/mensagem.type';
import { AgendamentoParaEditar } from './shared/agendamento-para-editar.type';

@Injectable({
  providedIn: 'root'
})

export class ListarAgendamentoService {
  
  rotaAgendamento = "agendamento";
  FORMAT = "yyyy-MM-dd HH:mm:ss";

  // Agendamento passado do componente de listagem para o de edição
  agendamentoTransferencia : Agendamento = null;

  constructor(private datePipe: DatePipe, private httpClient: HttpClient) {

  }

  obterAgendamentosDataAtual() {
    return this.httpClient.get<Agendamento[]>(`${applicationUrl}/agendamento/${(new Date()).toISOString()}`);
  }

  cadastrarAgendamento(agendamento: AgendamentoParaCadastrar) {
    return this.httpClient.post<Mensagem>(`${applicationUrl}/${this.rotaAgendamento}/cadastrar/`, agendamento);
  }

  obterAgendamentosComFiltro(dataHoraInicio : Date, dataHoraFim : Date, idPaciente : string, idMedico : string, jaConsultados : boolean) {
    return this.httpClient.get<Agendamento[]>(`${applicationUrl}/${this.rotaAgendamento}?dataHoraInicio=${dataHoraInicio}&dataHoraFim=${dataHoraFim}&idPaciente=${idPaciente}&idMedico=${idMedico}&jaConsultados=${jaConsultados}`);
  }

  excluirAgendamento(idAgendamento : string) {
    return this.httpClient.delete<Mensagem>(`${applicationUrl}/${this.rotaAgendamento}/${idAgendamento}`);
  }

  atualizarAgendamento(agendamento: AgendamentoParaEditar) {
    return this.httpClient.put<Mensagem>(`${applicationUrl}/${this.rotaAgendamento}/atualizar`, agendamento);
  }

}