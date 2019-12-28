import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { PacienteParaListagem } from './paciente-para-listar.type';
import { applicationUrl } from './constantes';
import { PacienteParaAgendamento } from './paciente-para-agendamento.type';
import { map } from 'rxjs/operators';
import { DatePipe } from '@angular/common';

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

}