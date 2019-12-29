// export class Scheduling {
//     schedulingId : string;
//     dataHoraAgendamento : Date;
//     dataHoraRegistro : Date;
//     person : Person;
//     doctor : Doctor;
//     consulta? : Consulta;
// }

// export class Person {
//     personId : string;
//     personName : string;
//     birthDate : Date;
// }

// export class Doctor {
//     doctorId : string;
//     doctorName : string;
// }

// export class Consulta {
//     consultaId : string;
//     consultaDateTime : Date;
// }

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
// export class Person {
//   personId: string;
//   personName: string;
//   birthDate: Date;

//   constructor(init: Person) {
//     this.personId = init.personId;
//     this.personName = init.personName;
//     this.birthDate = parseDate(init.birthDate);
//   }
// }

// export class Consulta {
//   consultaId: string;
//   consultaDateTime: Date;

//   constructor(init: Consulta) {
//     this.consultaId = init.consultaId;
//     this.consultaDateTime = parseDate(init.consultaDateTime);
//   }
// }

// export class Doctor {
//   doctorId: string;
//   doctorName: string;

//   constructor(init: Doctor) {
//     this.doctorId = init.doctorId;
//     this.doctorName = init.doctorName;
//   }
// }


// export class Scheduling {
//   schedulingId: string;
//   schedulingDateTime: Date;
//   registrationDateTime: Date;
//   person: Person;
//   doctor: Doctor;
//   consulta?: Consulta;

//   constructor(init: Scheduling) {

//     this.schedulingId = init.schedulingId;

//     this.schedulingDateTime = parseDate(init.schedulingDateTime);
//     this.registrationDateTime = parseDate(init.registrationDateTime);

//     this.person = init.person !== undefined ? new Person(init.person) : undefined;
//     this.consulta = init.consulta !== undefined ? new Consulta(init.consulta) : undefined;
//     this.doctor = init.doctor !== undefined ? new Doctor(init.doctor) : undefined;
//   }
// }



// export function parseDate(str: string | Date): Date {
//   if (str !== undefined && str !== null) {
//     return new Date(str);
//   }
//   return undefined;
// }