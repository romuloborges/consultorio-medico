import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { applicationUrl } from '../shared/constantes/constantes';
import { UsuarioLogado } from '../shared/type/usuario.type';

@Injectable({
    providedIn: 'root'
})

export class AuthService {

    constructor(private httpClient : HttpClient) {
    }

    realizarLogin(email : string, senha : string) {
        return this.httpClient.get<UsuarioLogado>(`${applicationUrl}/usuario/validar?email=${email}&senha=${senha}`);
    }

}