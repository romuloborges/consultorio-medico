import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { AtendenteCadastro } from '../type/atendente.type';
import { applicationUrl } from '../constantes/constantes';
import { Mensagem } from '../type/mensagem.type';

@Injectable({
    providedIn: 'root'
})

export class AtendenteService {
    
    constructor(private httpClient: HttpClient) {}

    cadastrarAtendente(atendente: AtendenteCadastro) {
        return this.httpClient.post<Mensagem>(`${applicationUrl}/atendente/cadastrar`, atendente);
    }

}