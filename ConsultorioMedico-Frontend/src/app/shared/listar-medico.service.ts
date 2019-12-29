import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { MedicoParaListagem } from './medico-para-listar.type';
import { applicationUrl } from './constantes';

@Injectable ({
    providedIn: 'root'
})

export class ListarMedico {
    
    constructor(private httpClient : HttpClient) {}

    obterTodosMedicos() {
        return this.httpClient.get<MedicoParaListagem[]>(`${applicationUrl}/medico/`);
    }    

}