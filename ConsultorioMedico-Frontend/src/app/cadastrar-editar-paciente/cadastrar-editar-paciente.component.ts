import { Component, OnInit } from '@angular/core';
import { uf, sexo } from '../shared/constantes/constantes';
import { ViaCepService } from '../shared/services/viacep.service';
import { isUndefined } from 'util';
import Swal from 'sweetalert2';
import { NgForm } from '@angular/forms';
import { PacienteService } from '../shared/services/paciente.service';
import { Endereco, EnderecoEditar, EnderecoViaCep } from '../shared/type/endereco.type';
import { PacienteEditar, Paciente } from '../shared/type/paciente.type';

@Component({
  selector: 'app-cadastrar-editar-paciente',
  templateUrl: './cadastrar-editar-paciente.component.html',
  styleUrls: ['./cadastrar-editar-paciente.component.css']
})
export class CadastrarEditarPacienteComponent implements OnInit {

  uf = uf;
  sexo = sexo;
  sexoEscolhido: Number;
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
  paciente: PacienteEditar = null;

  modoEdicao: boolean;
  modoLeitura: boolean;

  constructor(private viaCepService: ViaCepService, private pacienteService: PacienteService) { }

  ngOnInit() {
    console.log(this.pacienteService.pacienteTransferencia);
    if (this.pacienteService.pacienteTransferencia != null) {
      this.paciente = this.pacienteService.pacienteTransferencia;
      this.endereco = new Endereco(this.paciente.endereco.cep, this.paciente.endereco.logradouro, this.paciente.endereco.numero, this.paciente.endereco.complemento, this.paciente.endereco.bairro, this.paciente.endereco.localidade, this.paciente.endereco.uf);
      this.carregarEndereco = true;

      for (let i = 0; i < this.sexo.length; i++) {
        if (this.sexo[i].charAt(0) == this.paciente.sexo) {
          this.sexoEscolhido = i;
        }
      }

      this.modoEdicao = this.pacienteService.modoEdicao;
      this.modoLeitura = this.pacienteService.modoLeitura;
      
      this.desabilitarCampos();
      console.log(this.paciente.cpf);
    } else {
      this.modoEdicao = false;
      this.modoLeitura = false;
    }
    this.pacienteService.pacienteTransferencia = null;
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

  onSubmit(pacienteForm: NgForm) {
    let endereco: Endereco = { cep: pacienteForm.value.cep, logradouro: pacienteForm.value.logradouro, numero: pacienteForm.value.numero, complemento: pacienteForm.value.complemento, bairro: pacienteForm.value.bairro, localidade: pacienteForm.value.localidade, uf: pacienteForm.value.uf };
    let paciente = new Paciente(pacienteForm.value.nome, pacienteForm.value.nomeSocial, pacienteForm.value.data, this.sexo[pacienteForm.value.sexo].charAt(0), pacienteForm.value.cpf, pacienteForm.value.rg, pacienteForm.value.telefone, pacienteForm.value.email, endereco);
    // Validação com mensagens específicas
    if (this.validaCpf.test(paciente.cpf)) {
      if (this.validaRg.test(paciente.rg)) {
        if (this.validaCelular.test(paciente.telefone)) {
          if (new Date(paciente.dataNascimento) <= (new Date())) {
            if (this.validarBairroComplementoLogradouro.test(pacienteForm.value.bairro)) {
              if (this.validarBairroComplementoLogradouro.test(pacienteForm.value.complemento)) {
                if (this.validarBairroComplementoLogradouro.test(pacienteForm.value.logradouro)) {
                  if (this.validarNumero.test(pacienteForm.value.numero)) {
                    if (this.paciente != null && this.modoEdicao && !this.modoLeitura) {
                      // Atualizar paciente
                      let enderecoEditar = new EnderecoEditar(this.paciente.endereco.id, pacienteForm.value.cep, pacienteForm.value.logradouro, pacienteForm.value.numero, pacienteForm.value.complemento, pacienteForm.value.bairro, pacienteForm.value.localidade, pacienteForm.value.uf);
                      let pacienteEditar = new PacienteEditar(this.paciente.id, pacienteForm.value.nome, pacienteForm.value.nomeSocial, pacienteForm.value.data, this.sexo[pacienteForm.value.sexo].charAt(0), pacienteForm.value.cpf, pacienteForm.value.rg, pacienteForm.value.telefone, pacienteForm.value.email, enderecoEditar);

                      this.pacienteService.atualizarPaciente(pacienteEditar).subscribe(resultado => {
                        console.log(resultado);
                        if (resultado.id == 1) {
                          Swal.fire({ title: 'Sucesso', icon: 'success', text: resultado.texto });
                          pacienteForm.resetForm();
                          this.desabilitarCampos();
                          this.carregarEndereco = false;
                          this.endereco = null;
                          this.enderecoViaCep = null;
                          this.paciente = null;
                          this.modoEdicao = false;
                          this.modoLeitura = false;
                        } else {
                          Swal.fire({ title: 'Ops...', icon: 'error', text: resultado.texto });
                        }
                      })
                    } else if (!this.modoEdicao && !this.modoLeitura) {
                      // Cadastrar um paciente novo
                      this.pacienteService.cadastrarPaciente(paciente).subscribe(resultado => {
                        console.log(resultado);
                        if (resultado.id == 1) {
                          Swal.fire({ title: 'Sucesso', icon: 'success', text: resultado.texto });
                          pacienteForm.resetForm();
                          this.desabilitarCampos();
                          this.carregarEndereco = false;
                          this.endereco = null;
                          this.enderecoViaCep = null;
                        } else {
                          Swal.fire({ title: 'Ops...', icon: 'error', text: resultado.texto });
                        }
                      });
                    } else {
                      Swal.fire({ title: 'Ops...', text: 'Operação não permitida!', icon: 'error' });
                    }
                  } else {
                    Swal.fire({ title: 'Ops..', text: 'O número do endereço possui caracteres não permitidos!', icon: 'warning' });
                  }
                } else {
                  Swal.fire({ title: 'Ops..', text: 'O logradouro possui caracteres não permitidos!', icon: 'warning' });
                }
              } else {
                Swal.fire({ title: 'Ops..', text: 'O complemento possui caracteres não permitidos!', icon: 'warning' });
              }
            } else {
              Swal.fire({ title: 'Ops..', text: 'O bairro possui caracteres não permitidos!', icon: 'warning' });
            }
          } else {
            console.log(paciente.dataNascimento);
            Swal.fire({ title: 'Ops..', text: 'A data de nascimento não pode ser uma data que ainda não ocorreu!', icon: 'warning' });
          }
        } else {
          Swal.fire({ title: 'Ops..', text: 'Formato incorreto para telefone!', icon: 'warning' });
        }
      } else {
        Swal.fire({ title: 'Ops..', text: 'Formato incorreto para RG!', icon: 'warning' });
      }
    } else {
      Swal.fire({ title: 'Ops..', text: 'Formato incorreto para CPF!', icon: 'warning' });
    }

    // Validação com mensagem genérica
    // if(this.validaCpf.test(paciente.cpf) && this.validaRg.test(paciente.rg) && this.validaCelular.test(paciente.telefone) && paciente.dataNascimento <= (new Date()) && this.validarBairroComplementoLogradouro.test(pacienteForm.value.bairro) && this.validarBairroComplementoLogradouro.test(pacienteForm.value.complemento) && this.validarBairroComplementoLogradouro.test(pacienteForm.value.logradouro) && this.validarNumero.test(pacienteForm.value.numero)) {
    //   this.pacienteService.cadastrarPaciente(paciente).subscribe(resultado => {
    //     console.log(resultado);
    //     if(resultado.id == 1) {
    //       Swal.fire({ title: 'Sucesso', icon: 'success', text: resultado.texto });
    //       pacienteForm.reset();
    //       this.endereco = null;
    //       this.indiceEstado = -1;
    //     } else {
    //       Swal.fire({ title: 'Ops...', icon: 'error', text: resultado.texto });
    //     }
    //   });
    // } else {
    //   Swal.fire({ title: 'Ops...', icon: 'error', text: 'Existem campos mal preenchidos!'});
    // }
  }

}
