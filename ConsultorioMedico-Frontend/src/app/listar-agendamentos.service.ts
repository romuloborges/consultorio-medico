import { Component, Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { applicationUrl } from './shared/constantes';
import { Agendamento, Paciente, Medico, Consulta } from './tela-principal/agendamento-listagem.type';
import { map } from 'rxjs/operators';
import { DatePipe } from '@angular/common';
import { AgendamentoParaCadastrar } from './shared/agendamento-para-cadastrar.type';
import { Mensagem } from './shared/mensagem.type';

@Injectable({
  providedIn: 'root'
})

export class ListarAgendamentoService {
  
  rotaAgendamento = "agendamento";
  FORMAT = "yyyy-MM-dd HH:mm:ss";

  // Agendamento passado do componente de listagem para o de edição
  agendamentoTransferencia : Agendamento = null;

  constructor(private datePipe: DatePipe, private httpClient: HttpClient) {

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
    return this.httpClient
      // .get<Agendamento[]>(`${applicationUrl}/agendamento/${(new Date()).toISOString()}`)
      .get<Agendamento[]>(`${applicationUrl}/agendamento/${(new Date()).toISOString()}`);
      // .pipe(
      //   map((schArr: any[]) => {
      //     const resp: Agendamento[] = [];
      //     schArr.forEach(sch => {
      //       resp.push(
      //         new Agendamento(
      //           sch.idAgendamento,
      //           new Date(
      //             this.datePipe.transform(sch.dataHoraAgendamento, this.FORMAT)
      //           ),
      //           new Date(
      //             this.datePipe.transform(sch.dataHoraRegistro, this.FORMAT)
      //           ),
      //           sch.observacoes,
      //           new Medico(
      //             sch.medicoListarViewModel.idMedico,
      //             sch.medicoListarViewModel.nomeMedico
      //           ),
      //           new Paciente(
      //             sch.pacienteListarViewModel.idPaciente,
      //             sch.pacienteListarViewModel.nomePaciente,
      //             new Date(
      //               this.datePipe.transform(
      //                 sch.pacienteListarViewModel.dataNascimento,
      //                 this.FORMAT
      //               )
      //             )
      //           ),
      //           sch.consultaViewModel != null
      //             ? new Consulta(
      //               sch.consultaViewModel.consultaId,
      //               new Date(
      //                 this.datePipe.transform(
      //                   sch.consultaViewModel.consultaDataHora,
      //                   this.FORMAT
      //                 )
      //               ),
      //               sch.consultaViewModel.receitaMedica
      //             )
      //             : null
      //         )
      //       );
      //     });
      //     return resp;
      //   })
      // );
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

  cadastrarAgendamento(agendamento: AgendamentoParaCadastrar) {
    return this.httpClient.post<Mensagem>(`${applicationUrl}/${this.rotaAgendamento}/cadastrar/`, agendamento);
  }

  obterAgendamentosComFiltro(dataHoraInicio : Date, dataHoraFim : Date, idPaciente : string, idMedico : string) {
    return this.httpClient.get<Agendamento[]>(`${applicationUrl}/${this.rotaAgendamento}/${dataHoraInicio}/${dataHoraFim}/${idPaciente}/${idMedico}`);
  }

  excluirAgendamento(idAgendamento : string) {
    return this.httpClient.delete<Mensagem>(`${applicationUrl}/${this.rotaAgendamento}/${idAgendamento}`);
  }

}