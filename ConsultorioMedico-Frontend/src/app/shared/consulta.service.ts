import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Agendamento } from '../tela-principal/agendamento-listagem.type';
import { ConsultaCadastrar } from './consulta.type';
import { Mensagem } from './mensagem.type';
import { applicationUrl } from './constantes';

@Injectable ({
    providedIn: 'root'
})

export class ConsultaService {

    agendamento: Agendamento = null;

    constructor(private httpClient: HttpClient) {}

    cadastrarConsulta(consulta: ConsultaCadastrar) {
        return this.httpClient.post<Mensagem>(`${applicationUrl}/consulta/`, consulta);
    }

}