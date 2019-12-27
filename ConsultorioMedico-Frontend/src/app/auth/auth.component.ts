import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from './auth.service';
import { UsuarioLogado } from '../shared/usuario.type';
import Swal from 'sweetalert2';
import { Md5 } from 'ts-md5/dist/Md5';
import { Router } from '@angular/router';

@Component({
    selector: 'app-auth',
    templateUrl: './auth.component.html',
    styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {

    usuarioLogado;

    constructor(private authService : AuthService, private router : Router) {

    }

    ngOnInit(): void {
        
    }

    onSubmit(form : NgForm) {
        const email = form.value.email;
        const senha = (Md5.hashStr(form.value.senha) as string);

        this.authService.realizarLogin(email, senha).subscribe(
            usuario => {
                if(usuario != null) {
                    this.usuarioLogado = usuario;
                    localStorage.setItem('UsuarioLogado', JSON.stringify(this.usuarioLogado));
                    Swal.fire({
                        icon: 'success',
                        text: 'Login realizado com sucesso!'
                    })
                    this.router.navigate(['/principal']);
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Usuário ou senha incorretos!'
                    })
                    form.reset();
                }
                console.log(usuario);
            },
        );
    }

}