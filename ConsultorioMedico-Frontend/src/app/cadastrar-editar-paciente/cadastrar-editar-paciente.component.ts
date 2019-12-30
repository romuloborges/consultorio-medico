import { Component, OnInit } from '@angular/core';
import { uf, sexo } from '../shared/constantes';
import { ViaCepService } from '../shared/viacep.service';
import { isUndefined } from 'util';
import Swal from 'sweetalert2';
import { EnderecoViaCep } from '../shared/endereco-viacep.type';
import { NgForm } from '@angular/forms';
import { ListarPaciente } from '../shared/listar-paciente.service';
import { Paciente } from './paciente.type';
import { Endereco } from '../shared/endereco.type';

@Component({
  selector: 'app-cadastrar-editar-paciente',
  templateUrl: './cadastrar-editar-paciente.component.html',
  styleUrls: ['./cadastrar-editar-paciente.component.css']
})
export class CadastrarEditarPacienteComponent implements OnInit {

  uf = uf;
  sexo = sexo;
  mascaraTelefoneFixo = '(00)0000-0000';
  mascaraCelular = '(00)00000-0000';
  carregarEndereco = false;
  endereco : EnderecoViaCep;
  indiceEstado;

  constructor(private viaCepService : ViaCepService, private pacienteService : ListarPaciente) { }

  ngOnInit() {
    
  }

  filtro = (d: Date): boolean => {
    return d <= (new Date());
  }

  consultarCep(cep : string) {
    if(cep.length == 9) {
      this.viaCepService.obterEndereco(cep).subscribe(endereco => {
        if(isUndefined(endereco.erro)) {
          this.endereco = endereco;
          for(let i = 0; i < this.uf.length; i++) {
            if(this.uf[i] == this.endereco.uf) {
              this.indiceEstado = i;
            }
          }
          this.carregarEndereco = true;
        } else {
          Swal.fire({title: 'Ops..', text: 'Este CEP não existe', icon: 'warning'});
          this.carregarEndereco = false;
        }
      });
    }
  }

  onSubmit(pacienteForm : NgForm) {
    let endereco : Endereco = { cep: pacienteForm.value.cep, logradouro: pacienteForm.value.logradouro, numero: pacienteForm.value.numero, complemento: pacienteForm.value.complemento, bairro: pacienteForm.value.bairro, localidade : pacienteForm.value.localidade, uf : this.uf[pacienteForm.value.uf]};
    let paciente = new Paciente(pacienteForm.value.nome, pacienteForm.value.nomeSocial, pacienteForm.value.data, this.sexo[pacienteForm.value.sexo].charAt(0), pacienteForm.value.cpf, pacienteForm.value.rg, pacienteForm.value.telefone, pacienteForm.value.email, endereco);

    var validacpf = /^[0-9]{11}$/;
    var validarg = /^[0-9]{8}[0-9a-zA-Z]{2}$/;
    var validacelular = /^[0-9]{11}$/;

    if(validacpf.test(paciente.cpf) && validarg.test(paciente.rg) && validacelular.test(paciente.telefone) && paciente.dataNascimento <= (new Date())) {
      this.pacienteService.cadastrarPaciente(paciente).subscribe(resultado => {
        console.log(resultado);
        if(resultado.id == 1) {
          Swal.fire({ title: 'Sucesso', icon: 'success', text: resultado.texto });
          pacienteForm.reset();
          this.endereco = null;
          this.indiceEstado = -1;
        } else {
          Swal.fire({ title: 'Ops...', icon: 'error', text: resultado.texto });
        }
      });
    } else {
      Swal.fire({ title: 'Ops...', icon: 'error', text: 'Existem campos mal preenchidos!'});
    }
  }

}
