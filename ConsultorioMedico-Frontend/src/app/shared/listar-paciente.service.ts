import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { PacienteParaListagem } from './paciente-para-listar.type';
import { applicationUrl } from './constantes';
import { PacienteParaAgendamento } from './paciente-para-agendamento.type';
import { map } from 'rxjs/operators';
import { DatePipe } from '@angular/common';
import { Paciente } from '../cadastrar-editar-paciente/paciente.type';
import { Mensagem } from './mensagem.type';
import { PacienteTabelaListar } from '../lista-pacientes/paciente-tabela-listar.type';

@Injectable ({
    providedIn: 'root'
})

export class ListarPaciente {

    format = "dd/MM/yyyy";
    
    constructor(private httpClient : HttpClient, private datePipe : DatePipe) {}

    obterTodosPacientes() {
        return this.httpClient.get<PacienteParaListagem[]>(`${applicationUrl}/paciente/`);
    }

    obterPacienteParaAgendamento(id : string) {
        return this.httpClient.get<PacienteParaAgendamento>(`${applicationUrl}/paciente/${id}`);
    }

    obterPacientesListaPaciente() {
        return this.httpClient.get<PacienteTabelaListar[]>(`${applicationUrl}/paciente/pacientesCompletos`);
    }

    cadastrarPaciente(paciente : Paciente) {
        return this.httpClient.post<Mensagem>(`${applicationUrl}/paciente/`, paciente);
    }

    excluirPaciente(id : string) {
        return this.httpClient.delete<Mensagem>(`${applicationUrl}/paciente?idPaciente=${id}`);
    }

}