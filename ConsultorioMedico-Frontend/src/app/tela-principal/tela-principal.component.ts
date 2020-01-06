import { Component, OnInit } from '@angular/core';
import { UsuarioLogado } from '../shared/type/usuario.type';
import { Router } from '@angular/router';
import { AgendamentoService } from '../shared/services/agendamento.service';
import { PacienteService } from '../shared/services/paciente.service';

@Component({
  selector: 'app-tela-principal',
  templateUrl: './tela-principal.component.html',
  styleUrls: ['./tela-principal.component.css']
})
export class TelaPrincipalComponent implements OnInit {

  usuario : UsuarioLogado;
  nomeUsuario : string;

  constructor(private router : Router, private agendamentoService : AgendamentoService, private pacienteService: PacienteService) { 
    
  }

  ngOnInit() {
    this.usuario = JSON.parse(sessionStorage.getItem('UsuarioLogado'));
    this.nomeUsuario = this.usuario.nome;
    this.router.navigate(['principal/listarAgendamentosHoje']);
  }

  agendarConsulta() {
    this.agendamentoService.modoEdicao = false;
    this.agendamentoService.modoLeitura = false;
    this.router.navigate(['/principal/agendarConsulta']);
    // routerLink="agendarConsulta"
  }

  cadastrarPaciente() {
    this.pacienteService.modoEdicao = false;
    this.pacienteService.modoLeitura = false;
    this.router.navigate(['/principal/gerenciarPaciente']);
    // routerLink="gerenciarPaciente"
  }

  deslogar() {
    sessionStorage.removeItem('UsuarioLogado');
    this.router.navigate(['/']);
  }

  mudarRota(rota : string) {
    console.log(rota);
    this.router.navigate([rota]);
  }

}
