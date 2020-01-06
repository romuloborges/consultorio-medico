import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UsuarioListar } from '../type/usuario.type';
import { applicationUrl } from '../constantes/constantes';
import { Mensagem } from '../type/mensagem.type';

@Injectable ({
    providedIn: 'root'
})

export class UsuarioService {
    
    constructor(private httpClient: HttpClient) {}


    obterTodosUsuarios() {
        return this.httpClient.get<UsuarioListar[]>(`${applicationUrl}/usuario/obterTodosUsuariosAtivos`);
    }

    deletarUsuario(id: string) {
        return this.httpClient.delete<Mensagem>(`${applicationUrl}/usuario/deletarUsuario?id=${id}`);
    }

}