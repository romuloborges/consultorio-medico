import { Component, OnInit } from '@angular/core';
import { UsuarioLogado } from '../shared/usuario.type';
import { Router } from '@angular/router';
import { ListarAgendamentoService } from '../listar-agendamentos.service';

@Component({
  selector: 'app-tela-principal',
  templateUrl: './tela-principal.component.html',
  styleUrls: ['./tela-principal.component.css']
})
export class TelaPrincipalComponent implements OnInit {

  usuario : UsuarioLogado;
  nomeUsuario : string;

  constructor(private router : Router, private listarAgendamento : ListarAgendamentoService) { 
    this.usuario = JSON.parse(localStorage.getItem('UsuarioLogado'));
    this.nomeUsuario = this.usuario.nome;
  }

  ngOnInit() {
    
  }

  clicar() {
    this.listarAgendamento.obterAgendamentosDataAtual();
  }

  deslogar() {
    localStorage.removeItem('UsuarioLogado');
    this.router.navigate(['/']);
  }

}
