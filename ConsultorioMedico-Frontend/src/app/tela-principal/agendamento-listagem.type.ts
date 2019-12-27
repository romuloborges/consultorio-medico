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

export class Scheduling {
    constructor(
      public schedulingId: string,
      public schedulingDateTime: Date,
      public registrationDateTime: Date,
      public person: Person,
      public doctor: Doctor,
      public consulta?: Consulta
    ) {}
  }
  
  export class Person {
    constructor(
      public personId: string,
      public personName: string,
      public birthDate: Date
    ) {}
  }
  
  export class Doctor {
    constructor(public doctorId: string, public doctorName: string) {}
  }
  
  export class Consulta {
    constructor(public consultaId?: string, public consultaDateTime?: Date, public observacoes? : string) {}
  }