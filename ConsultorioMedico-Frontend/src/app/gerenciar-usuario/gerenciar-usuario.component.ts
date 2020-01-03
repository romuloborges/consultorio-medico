import { Component, OnInit } from '@angular/core';
import { UsuarioLogado } from '../shared/type/usuario.type';
import { sexo } from '../shared/constantes/constantes';
import { Endereco, EnderecoViaCep } from '../shared/type/endereco.type';
import { isUndefined } from 'util';
import Swal from 'sweetalert2';
import { ViaCepService } from '../shared/services/viacep.service';

@Component({
  selector: 'app-gerenciar-usuario',
  templateUrl: './gerenciar-usuario.component.html',
  styleUrls: ['./gerenciar-usuario.component.css']
})
export class GerenciarUsuarioComponent implements OnInit {

  usuario : UsuarioLogado;
  nomeUsuario : string;

  tipos: string[] = ['Atendente', 'Médico'];
  tipo: string;
  
  sexo = sexo;
  sexoEscolhido : Number;
  mascaraTelefoneFixo = '(00)0000-0000';
  mascaraCelular = '(00)00000-0000';
  carregarEndereco = false;
  enderecoViaCep: EnderecoViaCep;
  endereco: Endereco;

  // Expressões regulares para validação dos campos
  validaCpf = /^[0-9]{11}$/;
  validaRg = /^[0-9]{9}$/;
  validaCelular = /^[0-9]{11}$/;
  validarNumero = /^[0-9 a-zA-Z ç]+$/;
  validarBairroComplementoLogradouro = /^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$/

  // Atributos usados para desabilitar os campos que tiverem as informações carregadas pelo ViaCep
  desabilitarLogradouro = false;
  desabilitarComplemento = false;
  desabilitarBairro = false;
  desabilitarCidade = false;
  desabilitarUf = false;

  constructor(private viaCepService: ViaCepService) { }

  ngOnInit() {
    this.usuario = JSON.parse(localStorage.getItem('UsuarioLogado'));
    this.nomeUsuario = this.usuario.nome;
  }

  filtro = (d: Date): boolean => {
    return d <= (new Date());
  }

  desabilitarCampos() {
    if (this.endereco.logradouro == '') {
      this.desabilitarLogradouro = false;
    } else {
      this.desabilitarLogradouro = true;
    }

    if (this.endereco.complemento == '') {
      this.desabilitarComplemento = false;
    } else {
      this.desabilitarComplemento = true;
    }

    if (this.endereco.bairro == '') {
      this.desabilitarBairro = false;
    } else {
      this.desabilitarBairro = true;
    }

    this.desabilitarCidade = true;
    this.desabilitarUf = true;
  }

  consultarCep(cep: string) {
    if (cep.length == 9) {
      this.viaCepService.obterEndereco(cep).subscribe(endereco => {
        if (isUndefined(endereco.erro)) {
          console.log(endereco);

          this.endereco = new Endereco(endereco.cep, endereco.logradouro, '', endereco.complemento, endereco.bairro, endereco.localidade, endereco.uf);

          this.desabilitarCampos();
          this.carregarEndereco = true;
        } else {
          Swal.fire({ title: 'Ops..', text: 'Este CEP não existe', icon: 'warning' });
          this.carregarEndereco = false;
        }
      });
    }
  }

}
