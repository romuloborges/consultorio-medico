import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { PacienteParaListagem } from './paciente-para-listar.type';
import { applicationUrl } from './constantes';
import { PacienteParaAgendamento } from './paciente-para-agendamento.type';
import { map } from 'rxjs/operators';
import { DatePipe } from '@angular/common';
import { Paciente, PacienteEditar } from '../cadastrar-editar-paciente/paciente.type';
import { Mensagem } from './mensagem.type';
import { PacienteTabelaListar } from '../lista-pacientes/paciente-tabela-listar.type';

@Injectable ({
    providedIn: 'root'
})

export class ListarPaciente {

    format = "dd/MM/yyyy";
    pacienteTransferencia : PacienteEditar = null;
    
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

    obterPacientesComFiltro(nome : string, cpf : string, dataInicio : Date, dataFim : Date) {
        return this.httpClient.get<PacienteTabelaListar[]>(`${applicationUrl}/paciente/pacientesComFiltro?nome=${nome}&cpf=${cpf}&dataInicio=${dataInicio}&dataFim=${dataFim}`);
    }

    obterPacienteCompleto(id : string) {
        return this.httpClient.get<PacienteEditar>(`${applicationUrl}/paciente/obterPacienteCompleto?id=${id}`);
    }

    cadastrarPaciente(paciente : Paciente) {
        return this.httpClient.post<Mensagem>(`${applicationUrl}/paciente/`, paciente);
    }

    atualizarPaciente(paciente : PacienteEditar) {
        return this.httpClient.put<Mensagem>(`${applicationUrl}/paciente/`, paciente);
    }

    excluirPaciente(id : string) {
        return this.httpClient.delete<Mensagem>(`${applicationUrl}/paciente?idPaciente=${id}`);
    }

}