export class AgendamentoParaCadastrar {
    constructor(public dataHoraAgendamento : Date, public dataHoraRegistro : Date, public observacoes : string, public idMedico : string, public idPaciente : string) {}
}