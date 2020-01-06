import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { ConsultaCadastrar, ConsultaListar, ConsultaAtualizar } from '../type/consulta.type';
import { Mensagem } from '../type/mensagem.type';
import { applicationUrl } from '../constantes/constantes';
import { AgendamentoListagem } from '../type/agendamento.type';

@Injectable ({
    providedIn: 'root'
})

export class ConsultaService {

    agendamento: AgendamentoListagem = null;
    modoLeitura: boolean;
    modoEdicao: boolean;

    constructor(private httpClient: HttpClient) {}

    cadastrarConsulta(consulta: ConsultaCadastrar) {
        return this.httpClient.post<Mensagem>(`${applicationUrl}/consulta/`, consulta);
    }

    atualizarConsulta(consulta: ConsultaAtualizar) {
        return this.httpClient.put<Mensagem>(`${applicationUrl}/consulta/atualizarConsulta`, consulta);
    }

    obterConsultasCompletasComFiltro(dataHoraTerminoConsulta: Date, dataHoraAgendamento: Date, idPaciente: string) {
        return this.httpClient.get<ConsultaListar[]>(`${applicationUrl}/consulta/obterConsultasCompletasComFiltro?dataHoraTerminoConsulta=${dataHoraTerminoConsulta}&dataHoraAgendamento=${dataHoraAgendamento}&idPaciente=${idPaciente}`);
    }

    deletarConsulta(idConsulta: string) {
        return this.httpClient.delete<Mensagem>(`${applicationUrl}/consulta/deletarConsulta?id=${idConsulta}`);
    }

}