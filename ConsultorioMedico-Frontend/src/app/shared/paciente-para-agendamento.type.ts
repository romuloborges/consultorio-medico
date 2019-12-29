import { Endereco } from './endereco.type';

export class PacienteParaAgendamento {
    constructor(idPaciente : string, nomePaciente : string, dataNascimento : Date, cpf : string, endereco : Endereco) {}
}