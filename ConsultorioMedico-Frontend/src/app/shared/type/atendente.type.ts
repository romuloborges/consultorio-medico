import { Endereco } from './endereco.type';
import { UsuarioCadastro } from './usuario.type';

export class AtendenteCadastro {
    constructor(public nome: string, public dataNascimento: Date, public sexo: string, public cpf: string, public rg: string, public email: string, public telefone: string, public endereco: Endereco, public usuario: UsuarioCadastro) {}
}