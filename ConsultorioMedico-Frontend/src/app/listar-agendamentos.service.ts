import { Component, Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { applicationUrl } from './shared/constantes';
import { Scheduling, Person, Doctor, Consulta } from './tela-principal/agendamento-listagem.type';
import { map } from 'rxjs/operators';
import { DatePipe } from '@angular/common';

@Injectable ({
    providedIn: 'root'
})

export class ListarAgendamentoService {
    FORMAT = "yyyy-MM-dd HH:mm:ss";
    constructor(private datePipe: DatePipe, private httpClient : HttpClient) {

    }

    obterAgendamentosDataAtual() {
        // alert(new Date(this.datePipe.transform('2019-12-28T14:00:00')));
        // let dataHoje = new Date();
        // let data = dataHoje.getFullYear.toString() + '-' + dataHoje.getMonth.toString() + '-' + dataHoje.getDate.toString();

        // this.httpClient.get<Scheduling>(`${applicationUrl}/agendamento/2019-12-28`).pipe(
        //     map(scheduling => {
        //         console.log(scheduling);
        //         scheduling.dataHoraAgendamento = new Date(scheduling.dataHoraAgendamento);
        //         scheduling.dataHoraRegistro = new Date(scheduling.dataHoraRegistro);
        //         //scheduling.person.birthDate = new Date(scheduling.person.birthDate);
        //         return scheduling;
        //     })
        // ).subscribe(p => {
        //     console.log(p);
        // });
        this.httpClient
      .get<Scheduling[]>(`${applicationUrl}/agendamento/${(new Date()).toISOString()}`)
      .pipe(
        map((schArr: any[]) => {
          const resp: Scheduling[] = [];
          schArr.forEach(sch => {
            resp.push(
              new Scheduling(
                sch.idAgendamento,
                new Date(
                  this.datePipe.transform(sch.dataHoraAgendamento, this.FORMAT)
                ),
                new Date(
                  this.datePipe.transform(sch.dataHoraRegistro, this.FORMAT)
                ),
                new Person(
                  sch.pacienteListarViewModel.idPaciente,
                  sch.pacienteListarViewModel.nomePaciente,
                  new Date(
                    this.datePipe.transform(
                      sch.pacienteListarViewModel.dataNascimento,
                      this.FORMAT
                    )
                  )
                ),
                new Doctor(
                  sch.medicoListarViewModel.idMedico,
                  sch.medicoListarViewModel.nomeMedico
                ),
                sch.consultaViewModel != null
                  ? new Consulta(
                      sch.consultaViewModel.consultaId,
                      sch.consultaViewModel.consultaId
                    )
                  : null
              )
            );
          });
          return resp;
        })
      )
      .subscribe((res: Scheduling[]) => {
        console.log(res);
      });
      // this.httpClient.get<Scheduling[]>(`${applicationUrl}/agendamento/2019-12-28`)
      // .pipe(
      //   map(schedulings => {
      //     const modifiedSchedulings = []
      //     for (const scheduling of schedulings) {
      //       console.log(scheduling);
      //       modifiedSchedulings.push(new Scheduling(scheduling));
      //     }
      //     return modifiedSchedulings;
      //   })
      // ).subscribe((mod : Scheduling[]) => {
      //   console.log(mod);
      // });
  }
}