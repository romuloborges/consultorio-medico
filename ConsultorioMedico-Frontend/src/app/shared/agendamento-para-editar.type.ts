export class AgendamentoParaEditar {
    constructor(public idAgendamento: string, public dataHoraAgendamento : Date, public dataHoraRegistro : Date, public observacoes : string, public idMedico : string, public idPaciente : string) {}
}