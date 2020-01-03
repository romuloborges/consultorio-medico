export class ConsultaCadastrar {
    constructor(public dataHoraTerminoConsulta: Date, public receitaMedica: string, public idAgendamento: string) {}
}
export class ConsultaAgendamentoListagem {
    constructor(public idConsulta?: string, public dataHoraTerminoConsulta?: Date, public receitaMedica? : string) {}
  }