import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { applicationUrl } from '../constantes/constantes';
import { MedicoParaListagem, MedicoCadastro } from '../type/medico.type';
import { Mensagem } from '../type/mensagem.type';

@Injectable ({
    providedIn: 'root'
})

export class MedicoService {
    
    constructor(private httpClient : HttpClient) {}

    cadastrarMedico(medico: MedicoCadastro) {
        return this.httpClient.post<Mensagem>(`${applicationUrl}/medico/cadastrar`, medico);
    }

    obterTodosMedicos() {
        return this.httpClient.get<MedicoParaListagem[]>(`${applicationUrl}/medico/`);
    }    

}