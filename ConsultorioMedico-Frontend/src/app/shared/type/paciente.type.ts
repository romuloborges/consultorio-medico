import { Endereco, EnderecoEditar } from './endereco.type';

export class PacienteParaAgendamento {
    constructor(idPaciente : string, nomePaciente : string, dataNascimento : Date, cpf : string, endereco : Endereco) {}
}

export class PacienteParaListagem {
    constructor(public id : string, public nome : string) {}
}

export class PacienteTabelaListar {
    constructor(public idPaciente: string, public nome: string, public cpf: string, public telefone: string, public email: string, public dataNascimento: Date, public localidade: string, public quantidadeConsultas: number, public quantidadeAgendamentosPendentes: number) {}
}

export class Paciente {
    constructor(public nome : string, public nomeSocial : string, public dataNascimento : Date, public sexo : string, public cpf : string, public rg : string, public telefone : string, public email : string, public endereco : Endereco) {}
}

export class PacienteEditar {
    constructor(public id, public nome : string, public nomeSocial : string, public dataNascimento : Date, public sexo : string, public cpf : string, public rg : string, public telefone : string, public email : string, public endereco : EnderecoEditar) {}
}

export class PacienteAgendamentoListagem {
    constructor(
      public idPaciente: string,
      public nomePaciente: string,
      public dataNascimento: Date
    ) {}
  }