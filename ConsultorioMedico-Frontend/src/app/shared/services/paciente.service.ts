import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { applicationUrl } from '../constantes/constantes';
import { DatePipe } from '@angular/common';
import { Mensagem } from '../type/mensagem.type';
import { PacienteEditar, PacienteParaListagem, PacienteParaAgendamento, PacienteTabelaListar, Paciente } from '../type/paciente.type';

@Injectable ({
    providedIn: 'root'
})

export class PacienteService {

    format = "dd/MM/yyyy";
    pacienteTransferencia : PacienteEditar = null;

    modoLeitura: boolean;
    modoEdicao: boolean;
    
    constructor(private httpClient : HttpClient, private datePipe : DatePipe) {}

    cadastrarPaciente(paciente : Paciente) {
        return this.httpClient.post<Mensagem>(`${applicationUrl}/paciente/`, paciente);
    }

    atualizarPaciente(paciente : PacienteEditar) {
        return this.httpClient.put<Mensagem>(`${applicationUrl}/paciente/`, paciente);
    }

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

    obterPacienteParaRegistrarConsulta(id: string) {
        return this.httpClient.get<Paciente>(`${applicationUrl}/paciente/pacienteParaRegistrarConsulta?id=${id}`);
    }

    obterPacienteCompleto(id : string) {
        return this.httpClient.get<PacienteEditar>(`${applicationUrl}/paciente/obterPacienteCompleto?id=${id}`);
    }

    excluirPaciente(id : string) {
        return this.httpClient.delete<Mensagem>(`${applicationUrl}/paciente?idPaciente=${id}`);
    }

}