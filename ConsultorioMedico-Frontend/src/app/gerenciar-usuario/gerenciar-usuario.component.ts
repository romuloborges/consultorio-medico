import { Component, OnInit } from '@angular/core';
import { UsuarioCadastro } from '../shared/type/usuario.type';
import { sexo } from '../shared/constantes/constantes';
import { Endereco, EnderecoViaCep } from '../shared/type/endereco.type';
import { isUndefined } from 'util';
import Swal from 'sweetalert2';
import { ViaCepService } from '../shared/services/viacep.service';
import { NgForm } from '@angular/forms';
import { AtendenteCadastro } from '../shared/type/atendente.type';
import { MedicoCadastro } from '../shared/type/medico.type';
import { MedicoService } from '../shared/services/medico.service';
import { AtendenteService } from '../shared/services/atendente.service';
import { Md5 } from 'ts-md5/dist/Md5';

@Component({
  selector: 'app-gerenciar-usuario',
  templateUrl: './gerenciar-usuario.component.html',
  styleUrls: ['./gerenciar-usuario.component.css']
})
export class GerenciarUsuarioComponent implements OnInit {

  tipos: string[] = ['Atendente', 'Médico'];
  tipoEscolhido: string;

  sexo = sexo;
  sexoEscolhido: Number;
  mascaraTelefoneFixo = '(00)0000-0000';
  mascaraCelular = '(00)00000-0000';
  carregarEndereco = false;
  enderecoViaCep: EnderecoViaCep;
  endereco: Endereco;

  // Usada pra definir a data mínina de nascimento do médico ou atendente. Foi considerado que um médico ou atendente
  // deve ter pelo menos 18 anos
  dataMax = new Date();

  // Expressões regulares para validação dos campos
  validaCpf = /^[0-9]{11}$/;
  validaRg = /^[0-9]{9}$/;
  validaCelular = /^[0-9]{11}$/;
  validarNumero = /^[0-9 a-zA-Z ç]+$/;
  validarBairroComplementoLogradouro = /^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$/

  // https://emailregex.com/
  validarEmail = /(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])/;

  // Atributos usados para desabilitar os campos que tiverem as informações carregadas pelo ViaCep
  desabilitarLogradouro = false;
  desabilitarComplemento = false;
  desabilitarBairro = false;
  desabilitarCidade = false;
  desabilitarUf = false;

  constructor(private viaCepService: ViaCepService, private medicoService: MedicoService, private atendenteService: AtendenteService) { }

  ngOnInit() {
    this.dataMax.setFullYear(this.dataMax.getFullYear() - 18);
  }

  filtro = (d: Date): boolean => {
    return d <= (this.dataMax);
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

  onSubmit(cadastrarForm: NgForm) {
    let endereco: Endereco = { cep: cadastrarForm.value.cep, logradouro: cadastrarForm.value.logradouro, numero: cadastrarForm.value.numero, complemento: cadastrarForm.value.complemento, bairro: cadastrarForm.value.bairro, localidade: cadastrarForm.value.localidade, uf: cadastrarForm.value.uf };

    // Validação com mensagens específicas
    if (this.validaCpf.test(cadastrarForm.value.cpf)) {
      if (this.validaRg.test(cadastrarForm.value.rg)) {
        if (this.validaCelular.test(cadastrarForm.value.telefone)) {
          if (new Date(cadastrarForm.value.data) <= (new Date())) {
            if (this.validarBairroComplementoLogradouro.test(cadastrarForm.value.bairro)) {
              if (this.validarBairroComplementoLogradouro.test(cadastrarForm.value.complemento)) {
                if (this.validarBairroComplementoLogradouro.test(cadastrarForm.value.logradouro)) {
                  if (this.validarNumero.test(cadastrarForm.value.numero)) {
                    if (this.validarEmail.test(cadastrarForm.value.emailUsuario) && this.validarEmail.test(cadastrarForm.value.email)) {
                      if (cadastrarForm.value.senha == cadastrarForm.value.senhaConfirmar) {
                        if (cadastrarForm.value.tipo == 0) {
                          let usuario: UsuarioCadastro = new UsuarioCadastro(cadastrarForm.value.emailUsuario, (Md5.hashStr(cadastrarForm.value.senha) as string), 'Atendente');
                          let atendente: AtendenteCadastro = new AtendenteCadastro(cadastrarForm.value.nome, cadastrarForm.value.data, this.sexo[cadastrarForm.value.sexo].charAt(0), cadastrarForm.value.cpf, cadastrarForm.value.rg, cadastrarForm.value.email, cadastrarForm.value.telefone, endereco, usuario);

                          this.atendenteService.cadastrarAtendente(atendente).subscribe(resultado => {
                            console.log(resultado);
                            if(resultado.id == 1) {
                              Swal.fire({ title: 'Sucesso', icon: 'success', text: resultado.texto });
                              cadastrarForm.resetForm();
                              this.desabilitarCampos();
                              this.carregarEndereco = false;
                              this.endereco = null;
                            } else {
                              Swal.fire({ title: 'Ops...', icon: 'error', text: resultado.texto });
                            }
                          });
                        } else if(cadastrarForm.value.tipo == 1) {
                          let usuario: UsuarioCadastro = new UsuarioCadastro(cadastrarForm.value.emailUsuario, (Md5.hashStr(cadastrarForm.value.senha) as string), 'Médico');
                          let medico: MedicoCadastro = new MedicoCadastro(cadastrarForm.value.nome, cadastrarForm.value.cpf, cadastrarForm.value.rg, cadastrarForm.value.crm, cadastrarForm.value.data, this.sexo[cadastrarForm.value.sexo].charAt(0), cadastrarForm.value.telefone, cadastrarForm.value.email, endereco, usuario);
                          console.log(medico);
                          this.medicoService.cadastrarMedico(medico).subscribe(resultado => {
                            console.log(resultado);
                            if(resultado.id == 1) {
                              Swal.fire({ title: 'Sucesso', icon: 'success', text: resultado.texto });
                              cadastrarForm.resetForm();
                              this.desabilitarCampos();
                              this.carregarEndereco = false;
                              this.endereco = null;
                            } else {
                              Swal.fire({ title: 'Ops...', icon: 'error', text: resultado.texto });
                            }
                          });
                        }
                      } else {
                        Swal.fire({ title: 'Ops..', text: 'As senhas não são iguais!', icon: 'warning' });
                      }
                    } else {
                      Swal.fire({ title: 'Ops..', text: 'O e-mail não possui um formato válido', icon: 'warning' });
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
  }

  trocar(j: number) {
    console.log(j);
    if (j == 1) {
      this.tipoEscolhido = 'Medico';
    } else {
      this.tipoEscolhido = 'Atendente';
    }
  }

}
