export interface UsuarioLogado {
    id: string;
    email : string;
    nome : string;
    tipo: string;
}

export class UsuarioCadastro {
    constructor(public email: string, public senha: string, public tipo: string) {}
}

export class UsuarioListar {
    constructor(public id: string, public email: string, public senha: string, public tipo: string) {}
}