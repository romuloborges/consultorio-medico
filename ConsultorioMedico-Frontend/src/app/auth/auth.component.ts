import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from './auth.service';
import { UsuarioLogado } from '../shared/usuario.type';
import { Swal } from 'sweetalert2';

@Component({
    selector: 'app-auth',
    templateUrl: './auth.component.html',
    styleUrls: ['./auth.component.css']
})
export class AuthComponent {

    usuarioLogado : UsuarioLogado;

    constructor(private authService : AuthService) {

    }

    onSubmit(form : NgForm) {
        const email = form.value.email;
        const senha = form.value.senha;

        this.authService.realizarLogin(email, senha).subscribe(
            usuario => {
                this.usuarioLogado = usuario;
                console.log(usuario);
            }
        );

        form.reset();
    }

}