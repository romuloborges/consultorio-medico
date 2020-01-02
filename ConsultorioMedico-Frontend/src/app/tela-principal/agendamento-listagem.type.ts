export class Agendamento {
    constructor(
      public idAgendamento: string,
      public dataHoraAgendamento: Date,
      public dataHoraRegistro: Date,
      public observacoes : string,
      public medicoListarViewModel: Medico,
      public pacienteListarViewModel: Paciente,
      public consultaViewModel?: Consulta
    ) {}
  }
  
  export class Paciente {
    constructor(
      public idPaciente: string,
      public nomePaciente: string,
      public dataNascimento: Date
    ) {}
  }
  
  export class Medico {
    constructor(public idMedico: string, public nomeMedico: string) {}
  }
  
  export class Consulta {
    constructor(public idConsulta?: string, public dataHoraTerminoConsulta?: Date, public receitaMedica? : string) {}
  }