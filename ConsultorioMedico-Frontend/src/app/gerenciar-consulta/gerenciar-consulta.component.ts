import { Component, OnInit } from '@angular/core';
import { PacienteService } from '../shared/services/paciente.service';
import { ConsultaService } from '../shared/services/consulta.service';
import { NgForm } from '@angular/forms';
import Swal from 'sweetalert2';
import { sexo } from '../shared/constantes/constantes';
import { ConsultaCadastrar } from '../shared/type/consulta.type';
import { Paciente } from '../shared/type/paciente.type';
import { AgendamentoListagem } from '../shared/type/agendamento.type';

@Component({
  selector: 'app-gerenciar-consulta',
  templateUrl: './gerenciar-consulta.component.html',
  styleUrls: ['./gerenciar-consulta.component.css']
})
export class GerenciarConsultaComponent implements OnInit {

  paciente: Paciente = null;
  agendamento: AgendamentoListagem = null;
  horaRegistro: string = '';
  horaInicio: string = '';

  time = new Date();
  timeAntigo = new Date();

  sexo = sexo;
  sexoEscolhido: Number;

  segundosInterno = 0;
  segundos = 0;
  minutos = 0;
  horas = 0;
  interval = null;

  constructor(private pacienteService: PacienteService, private consultaService: ConsultaService) { }

  ngOnInit() {
    this.iniciaCampos();
  }

  manipularContador() {
    if(this.interval != null && this.segundosInterno > 0) {
      clearInterval(this.interval);
      this.interval = null;
    } else {
      this.interval = setInterval(() => {
        this.segundosInterno++;
        this.segundos = this.segundosInterno % 60;
        this.minutos = Math.floor(this.segundosInterno / 60);
        this.horas = Math.floor(this.segundosInterno / 3600);
      }, 1000);
    }
  }

  iniciaCampos() {
    if(this.consultaService.agendamento != null) {
      this.agendamento = this.consultaService.agendamento;
      console.log(this.agendamento);
      this.pacienteService.obterPacienteParaRegistrarConsulta(this.consultaService.agendamento.pacienteListarViewModel.idPaciente).subscribe(paciente => {
        this.paciente = paciente;
        this.consultaService.agendamento = null;

        for(let i = 0; i < this.sexo.length; i++) {
          if(this.paciente.sexo == this.sexo[i].charAt(0)) {
            this.sexoEscolhido = i;
            break;
          }
        }

        console.log(paciente);
      });
      this.horaRegistro = this.agendamento.dataHoraRegistro.toString().substring(11, 16);
      this.horaInicio = this.agendamento.dataHoraAgendamento.toString().substring(11, 16);
      console.log(this.horaInicio);
    } else {
      this.agendamento = null;
      this.paciente = null;
    }
  }

  onSubmit(consultaForm: NgForm) {
    if(this.interval == null) {
      if(this.segundosInterno > 0) {
        let consulta = new ConsultaCadastrar(new Date(), consultaForm.value.receitaMedica, this.agendamento.idAgendamento);

        this.consultaService.cadastrarConsulta(consulta).subscribe(resultado => {
          console.log(resultado);
          if(resultado.id == 1) {
            Swal.fire({title: 'Sucesso', text: resultado.texto, icon: 'success'});
          } else {
            Swal.fire({title: 'Ops...', text: resultado.texto, icon: 'error'});
          }
        });
      } else {
        Swal.fire({title: 'Ops...', text: 'Você não pode registrar uma consulta sem duração!', icon: 'warning'})
      }
    } else {
      Swal.fire({title: 'Ops...', text: 'Você deve parar o cronômetro para registrar a consulta!', icon: 'warning'})
    }
  }

}
