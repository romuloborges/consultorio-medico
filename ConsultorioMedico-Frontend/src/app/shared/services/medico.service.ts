import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { applicationUrl } from '../constantes/constantes';
import { MedicoParaListagem } from '../type/medico.type';

@Injectable ({
    providedIn: 'root'
})

export class MedicoService {
    
    constructor(private httpClient : HttpClient) {}

    obterTodosMedicos() {
        return this.httpClient.get<MedicoParaListagem[]>(`${applicationUrl}/medico/`);
    }    

}