import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { applicationUrl } from '../shared/constantes';
import { UsuarioLogado } from '../shared/usuario.type';
import { Md5 } from 'ts-md5/dist/Md5';

@Injectable({
    providedIn: 'root'
})

export class AuthService {

    constructor(private httpClient : HttpClient) {

    }

    realizarLogin(email : string, senha : string) {
        return this.httpClient.post<UsuarioLogado>(`${applicationUrl}/usuario/validar/`, { email, senha });
    }

}