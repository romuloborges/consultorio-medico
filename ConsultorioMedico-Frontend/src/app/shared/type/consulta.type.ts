import { AgendamentoParaListagemDeConsulta } from './agendamento.type';

export class ConsultaCadastrar {
    constructor(public dataHoraTerminoConsulta: Date, public receitaMedica: string, public duracaoConsulta: Date, public idAgendamento: string) {}
}
export class ConsultaAgendamentoListagem {
    constructor(public idConsulta?: string, public dataHoraTerminoConsulta?: Date, public receitaMedica? : string, public duracaoConsulta?: Date) {}
}

export class ConsultaListar {
    constructor(public idConsulta: string, public dataHoraTerminoConsulta: Date, public receitaMedica: string, public duracaoConsulta: Date, public agendamentoParaListagemDeConsultaViewModel: AgendamentoParaListagemDeConsulta) {}
}

export class ConsultaAtualizar {
    constructor(public idConsulta: string, public dataHoraTerminoConsulta: Date, public receitaMedica: string, public duracaoConsulta: Date, public idAgendamento: string) {}
}
