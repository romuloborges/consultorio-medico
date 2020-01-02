import { Component, OnInit } from '@angular/core';
import { UsuarioLogado } from '../shared/type/usuario.type';
import { Router } from '@angular/router';
import { AgendamentoService } from '../shared/services/agendamento.service';

@Component({
  selector: 'app-tela-principal',
  templateUrl: './tela-principal.component.html',
  styleUrls: ['./tela-principal.component.css']
})
export class TelaPrincipalComponent implements OnInit {

  usuario : UsuarioLogado;
  nomeUsuario : string;

  constructor(private router : Router, private agendamentoService : AgendamentoService) { 
    this.usuario = JSON.parse(localStorage.getItem('UsuarioLogado'));
    this.nomeUsuario = this.usuario.nome;
  }

  ngOnInit() {
    this.router.navigate(['principal/listarAgendamentosHoje']);
  }

  clicar() {
    //this.router.navigate(['/agendarConsulta']);
    this.agendamentoService.obterAgendamentosDataAtual();
  }

  deslogar() {
    localStorage.removeItem('UsuarioLogado');
    this.router.navigate(['/']);
  }

  mudarRota(rota : string) {
    console.log(rota);
    this.router.navigate([rota]);
  }

}
