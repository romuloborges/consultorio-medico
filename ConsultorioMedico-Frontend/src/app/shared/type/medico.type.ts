import { Endereco } from './endereco.type';
import { UsuarioCadastro } from './usuario.type';

export class MedicoParaListagem {
    constructor(public idMedico: string, public nomeMedico: string) { }
}

export class MedicoAgendamentoListagem {
    constructor(public idMedico: string, public nomeMedico: string) { }
}

export class MedicoCadastro {
    constructor(public nome: string, public cpf: string, public rg: string, public crm: number, public dataNascimento: Date, public sexo: string, public telefone: string, public email: string, public endereco: Endereco, public usuario: UsuarioCadastro) {}
}
