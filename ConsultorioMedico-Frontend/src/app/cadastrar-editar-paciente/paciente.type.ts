import { Endereco } from '../shared/endereco.type';

export class Paciente {
    constructor(public nome : string, public nomeSocial : string, public dataNascimento : Date, public sexo : string, public cpf : string, public rg : string, public telefone : string, public email : string, public endereco : Endereco) {}
}