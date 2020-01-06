import { Component, OnInit } from '@angular/core';
import { UsuarioLogado } from '../shared/type/usuario.type';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tela-admin',
  templateUrl: './tela-admin.component.html',
  styleUrls: ['./tela-admin.component.css']
})
export class TelaAdminComponent implements OnInit {

  usuario: UsuarioLogado;
  nomeUsuario: string;

  constructor(private router: Router) { }

  ngOnInit() {
    this.usuario = JSON.parse(sessionStorage.getItem('UsuarioLogado'));
    this.nomeUsuario = this.usuario.nome;
    this.router.navigate(['/gerenciarUsuarios/listarUsuarios']);
  }

  deslogar() {
    sessionStorage.removeItem('UsuarioLogado');
    this.router.navigate(['/']);
  }

}
