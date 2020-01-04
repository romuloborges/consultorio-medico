import { MedicoAgendamentoListagem, MedicoParaListagem } from './medico.type';
import { PacienteAgendamentoListagem } from './paciente.type';
import { ConsultaAgendamentoListagem } from './consulta.type';

export class AgendamentoParaCadastrar {
    constructor(public dataHoraAgendamento : Date, public dataHoraRegistro : Date, public observacoes : string, public idMedico : string, public idPaciente : string) {}
}

export class AgendamentoParaEditar {
    constructor(public idAgendamento: string, public dataHoraAgendamento : Date, public dataHoraRegistro : Date, public observacoes : string, public idMedico : string, public idPaciente : string) {}
}

export class AgendamentoListagem {
    constructor(
      public idAgendamento: string,
      public dataHoraAgendamento: Date,
      public dataHoraRegistro: Date,
      public observacoes : string,
      public medicoListarViewModel: MedicoAgendamentoListagem,
      public pacienteListarViewModel: PacienteAgendamentoListagem,
      public consultaViewModel?: ConsultaAgendamentoListagem
    ) {}
  }

export class AgendamentoParaListagemDeConsulta {
  constructor(public idAgendamento: string, public dataHoraAgendamento: Date, public dataHoraRegistro: Date, public observacoes: string, public medico: MedicoParaListagem, public paciente: PacienteAgendamentoListagem) {}
}
