using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsultorioMedico.Application.Service
{
    public class AtendenteService : IAtendenteService
    {
        private IAtendenteRepository atendenteRepository;
        private IEnderecoRepository enderecoRepository;
        private IUsuarioRepository usuarioRepository;

        private readonly string cpfSemMascara = "^[0-9]{11}$";
        private readonly string cpfComMascara = "^[0-9]{3}\\.[0-9]{3}\\.[0-9]{3}-[0-9]{2}";

        private readonly string rgSemMascara = "^[0-9]{8}([0-9]|[A-Z]{2})$";
        private readonly string rgComMascara = "^[0-9]{2}\\.[0-9]{3}\\.[0-9]{3}-([0-9]|[A-Z]{2})$";

        private readonly string celularSemMascara = "^[0-9]{11}$";
        private readonly string celularComMascara = "^\\([0-9]{2}\\)[0-9]{5}-[0-9]{4}$";

        private readonly string cepSemMascara = "^[0-9]{8}$";
        private readonly string cepComMascara = "^[0-9]{5}-[0-9]{3}$";
        public AtendenteService(IAtendenteRepository atendenteRepository, IEnderecoRepository enderecoRepository, IUsuarioRepository usuarioRepository)
        {
            this.atendenteRepository = atendenteRepository;
            this.enderecoRepository = enderecoRepository;
            this.usuarioRepository = usuarioRepository;
        }
        public async Task<Mensagem> CadastrarAtendente(AtendenteCadastroViewModel atendenteCadastroViewModel)
        {
            if (!Regex.IsMatch(atendenteCadastroViewModel.Cpf, cpfComMascara))
            {
                if (Regex.IsMatch(atendenteCadastroViewModel.Cpf, cpfSemMascara))
                {
                    atendenteCadastroViewModel.Cpf = atendenteCadastroViewModel.Cpf.Substring(0, 3) + "." + atendenteCadastroViewModel.Cpf.Substring(3, 3) + "." + atendenteCadastroViewModel.Cpf.Substring(6, 3) + "-" + atendenteCadastroViewModel.Cpf.Substring(9, 2);
                }
                else
                {
                    return new Mensagem(0, "CPF não possui o formato correto!");
                }
            }

            if (!Regex.IsMatch(atendenteCadastroViewModel.Rg, rgComMascara))
            {
                if (Regex.IsMatch(atendenteCadastroViewModel.Rg, rgSemMascara))
                {
                    atendenteCadastroViewModel.Rg = atendenteCadastroViewModel.Rg.Substring(0, 2) + "." + atendenteCadastroViewModel.Rg.Substring(2, 3) + "." + atendenteCadastroViewModel.Rg.Substring(5, 3) + "-" + atendenteCadastroViewModel.Rg.Substring(8);
                }
                else
                {
                    return new Mensagem(0, "RG não possui o formato correto!");
                }
            }

            if (!Regex.IsMatch(atendenteCadastroViewModel.Telefone, celularComMascara))
            {
                if (Regex.IsMatch(atendenteCadastroViewModel.Telefone, celularSemMascara))
                {
                    atendenteCadastroViewModel.Telefone = "(" + atendenteCadastroViewModel.Telefone.Substring(0, 2) + ")" + atendenteCadastroViewModel.Telefone.Substring(2, 5) + "-" + atendenteCadastroViewModel.Telefone.Substring(7);
                }
                else
                {
                    return new Mensagem(0, "Telefone não possui o formato correto!");
                }
            }

            if (!Regex.IsMatch(atendenteCadastroViewModel.Endereco.Cep, cepComMascara))
            {
                if (Regex.IsMatch(atendenteCadastroViewModel.Endereco.Cep, cepSemMascara))
                {
                    atendenteCadastroViewModel.Endereco.Cep = atendenteCadastroViewModel.Endereco.Cep.Substring(0, 5) + "-" + atendenteCadastroViewModel.Endereco.Cep.Substring(5);
                }
                else
                {
                    return new Mensagem(0, "CEP não possui o formato correto!");
                }
            }

            if (await this.atendenteRepository.BuscarAtendentePorCpf(atendenteCadastroViewModel.Cpf) != null)
            {
                return new Mensagem(0, "Já existe uma atendente com esse CPF registrado!");
            }

            if (await this.atendenteRepository.BuscarAtendentePorRg(atendenteCadastroViewModel.Rg) != null)
            {
                return new Mensagem(0, "Já existe uma atendente com esse RG registrado!");
            }

            if (await this.usuarioRepository.ObterUsuarioPorEmail(atendenteCadastroViewModel.Usuario.Email) != null)
            {
                return new Mensagem(0, "Já existe um usuário cadastrado com esse e-mail!");
            }

            bool resultado = true;
            Endereco endereco = new Endereco(atendenteCadastroViewModel.Endereco.Cep, atendenteCadastroViewModel.Endereco.Logradouro, atendenteCadastroViewModel.Endereco.Numero, atendenteCadastroViewModel.Endereco.Complemento, atendenteCadastroViewModel.Endereco.Bairro, atendenteCadastroViewModel.Endereco.Localidade, atendenteCadastroViewModel.Endereco.Uf);
            Guid id = await this.enderecoRepository.BuscaIdEndereco(endereco);

            if (id == Guid.Empty)
            {
                resultado = await this.enderecoRepository.CadastrarEndereco(endereco);
                id = await this.enderecoRepository.BuscaIdEndereco(endereco);
            }

            if (!resultado)
            {
                return new Mensagem(0, "Falha ao cadastrar atendente!");
            }

            Atendente atendente = new Atendente(atendenteCadastroViewModel.Nome, atendenteCadastroViewModel.DataNascimento, atendenteCadastroViewModel.Sexo, atendenteCadastroViewModel.Cpf, atendenteCadastroViewModel.Rg, atendenteCadastroViewModel.Email, atendenteCadastroViewModel.Telefone, id);

            resultado = await this.atendenteRepository.CadastrarAtendente(atendente);

            if (!resultado)
            {
                return new Mensagem(0, "Falha ao cadastrar atendente!");
            }

            Atendente atendenteResultado = await this.atendenteRepository.BuscarAtendentePorCpf(atendenteCadastroViewModel.Cpf);

            if (atendenteResultado == null)
            {
                return new Mensagem(0, "Falha ao cadastrar atendente!");
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] data = sha256.ComputeHash(Encoding.UTF8.GetBytes(atendenteCadastroViewModel.Usuario.Senha));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                atendenteCadastroViewModel.Usuario.Senha = sBuilder.ToString();
            }

            Usuario usuario = new Usuario(atendenteCadastroViewModel.Usuario.Email, atendenteCadastroViewModel.Usuario.Senha, "Atendente", null, atendenteResultado.IdAtendente);

            resultado = await this.usuarioRepository.CadastrarUsuario(usuario);

            if (!resultado)
            {
                return new Mensagem(0, "Falha ao cadastrar atendente!");
            }

            return new Mensagem(1, "Atendente cadastrada com sucesso!");

        }
    }
}
