import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { ConsultaCadastrar } from '../type/consulta.type';
import { Mensagem } from '../type/mensagem.type';
import { applicationUrl } from '../constantes/constantes';
import { AgendamentoListagem } from '../type/agendamento.type';

@Injectable ({
    providedIn: 'root'
})

export class ConsultaService {

    agendamento: AgendamentoListagem = null;

    constructor(private httpClient: HttpClient) {}

    cadastrarConsulta(consulta: ConsultaCadastrar) {
        return this.httpClient.post<Mensagem>(`${applicationUrl}/consulta/`, consulta);
    }

}