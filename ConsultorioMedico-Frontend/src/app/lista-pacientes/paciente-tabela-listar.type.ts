export class PacienteTabelaListar {
    constructor(public idPaciente: string, public nome: string, public cpf: string, public telefone: string, public email: string, public dataNascimento: Date, public localidade: string, public quantidadeConsultas: number, public quantidadeAgendamentosPendentes: number) {}
}