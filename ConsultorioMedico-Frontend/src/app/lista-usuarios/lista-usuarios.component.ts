import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { UsuarioListar } from '../shared/type/usuario.type';
import { UsuarioService } from '../shared/services/usuario.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-lista-usuarios',
  templateUrl: './lista-usuarios.component.html',
  styleUrls: ['./lista-usuarios.component.css']
})
export class ListaUsuariosComponent implements OnInit {

  colunas: string[] = ['Id.', 'E-mail', 'Nome do usuário', 'Tipo', 'Ações'];

  dataSource: MatTableDataSource<UsuarioListar>;

  constructor(private usuarioService: UsuarioService) { }

  ngOnInit() {
    this.popularTabelaUsuarios();
  }

  popularTabelaUsuarios() {
    this.usuarioService.obterTodosUsuarios().subscribe(lista => {
      this.dataSource = new MatTableDataSource<UsuarioListar>(lista);
      console.log(lista);
    });
  }

  excluirUsuario(i: number) {
    let usuario = this.dataSource.data[i];
    if(usuario.tipo != 'Administrador') {
      Swal.fire({
        title: 'Deseja realmente excluir?',
        text: "Você não poderá reverter esta ação!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, excluir!',
        cancelButtonText: 'Cancelar'
      }).then((result) => {
        if (result.value) {
          this.usuarioService.deletarUsuario(usuario.id).subscribe(resultado => {
            console.log(resultado);
            if (resultado.id == 1) {
              this.dataSource.data.splice(i, 1);
              this.dataSource = new MatTableDataSource<UsuarioListar>(this.dataSource.data);
              Swal.fire('Excluído!', resultado.texto, 'success');
            } else {
              Swal.fire('Ops...', resultado.texto, 'error');
            }
          });
        }
      });
    }
  }

}
