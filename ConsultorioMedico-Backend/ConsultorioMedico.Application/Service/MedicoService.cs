using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Medico;
using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsultorioMedico.Application.Service
{
    public class MedicoService : IMedicoService
    {
        private IMedicoRepository medicoRepository;
        private IUsuarioRepository usuarioRepository;
        private IEnderecoRepository enderecoRepository;

        private readonly string cpfSemMascara = "^[0-9]{11}$";
        private readonly string cpfComMascara = "^[0-9]{3}\\.[0-9]{3}\\.[0-9]{3}-[0-9]{2}";

        private readonly string rgSemMascara = "^[0-9]{8}([0-9]|[A-Z]{2})$";
        private readonly string rgComMascara = "^[0-9]{2}\\.[0-9]{3}\\.[0-9]{3}-([0-9]|[A-Z]{2})$";

        private readonly string celularSemMascara = "^[0-9]{11}$";
        private readonly string celularComMascara = "^\\([0-9]{2}\\)[0-9]{5}-[0-9]{4}$";

        private readonly string cepSemMascara = "^[0-9]{8}$";
        private readonly string cepComMascara = "^[0-9]{5}-[0-9]{3}$";
        public MedicoService(IMedicoRepository medicoRepository, IUsuarioRepository usuarioRepository, IEnderecoRepository enderecoRepository)
        {
            this.medicoRepository = medicoRepository;
            this.usuarioRepository = usuarioRepository;
            this.enderecoRepository = enderecoRepository;
        }

        public async Task<Mensagem> CadastrarMedico(MedicoCadastroViewModel medicoCadastroViewModel)
        {
            if (!Regex.IsMatch(medicoCadastroViewModel.Cpf, cpfComMascara))
            {
                if (Regex.IsMatch(medicoCadastroViewModel.Cpf, cpfSemMascara))
                {
                    medicoCadastroViewModel.Cpf = medicoCadastroViewModel.Cpf.Substring(0, 3) + "." + medicoCadastroViewModel.Cpf.Substring(3, 3) + "." + medicoCadastroViewModel.Cpf.Substring(6, 3) + "-" + medicoCadastroViewModel.Cpf.Substring(9, 2);
                }
                else
                {
                    return new Mensagem(0, "CPF não possui o formato correto!");
                }
            }

            if (!Regex.IsMatch(medicoCadastroViewModel.Rg, rgComMascara))
            {
                if (Regex.IsMatch(medicoCadastroViewModel.Rg, rgSemMascara))
                {
                    medicoCadastroViewModel.Rg = medicoCadastroViewModel.Rg.Substring(0, 2) + "." + medicoCadastroViewModel.Rg.Substring(2, 3) + "." + medicoCadastroViewModel.Rg.Substring(5, 3) + "-" + medicoCadastroViewModel.Rg.Substring(8);
                }
                else
                {
                    return new Mensagem(0, "RG não possui o formato correto!");
                }
            }

            if (!Regex.IsMatch(medicoCadastroViewModel.Telefone, celularComMascara))
            {
                if (Regex.IsMatch(medicoCadastroViewModel.Telefone, celularSemMascara))
                {
                    medicoCadastroViewModel.Telefone = "(" + medicoCadastroViewModel.Telefone.Substring(0, 2) + ")" + medicoCadastroViewModel.Telefone.Substring(2, 5) + "-" + medicoCadastroViewModel.Telefone.Substring(7);
                }
                else
                {
                    return new Mensagem(0, "RG não possui o formato correto!");
                }
            }

            if (!Regex.IsMatch(medicoCadastroViewModel.Endereco.Cep, cepComMascara))
            {
                if (Regex.IsMatch(medicoCadastroViewModel.Endereco.Cep, cepSemMascara))
                {
                    medicoCadastroViewModel.Endereco.Cep = medicoCadastroViewModel.Endereco.Cep.Substring(0, 5) + "-" + medicoCadastroViewModel.Endereco.Cep.Substring(5);
                }
                else
                {
                    return new Mensagem(0, "RG não possui o formato correto!");
                }
            }

            if (await this.medicoRepository.BuscarMedicoPorCpf(medicoCadastroViewModel.Cpf) != null)
            {
                return new Mensagem(0, "Já existe um médico com esse CPF registrado!");
            }

            if(await this.medicoRepository.BuscarMedicoPorCrm(int.Parse(medicoCadastroViewModel.Crm)) != null)
            {
                return new Mensagem(0, "Já existe um médico com esse CRM registrado!");
            }

            if(await this.medicoRepository.BuscarMedicoPorRg(medicoCadastroViewModel.Rg) != null)
            {
                return new Mensagem(0, "Já existe um médico com esse RG registrado!");
            }

            if (await this.usuarioRepository.ObterUsuarioPorEmail(medicoCadastroViewModel.Usuario.Email) != null)
            {
                return new Mensagem(0, "Já existe um usuário cadastrado com esse e-mail!");
            }

            bool resultado = true;
            Endereco endereco = new Endereco(medicoCadastroViewModel.Endereco.Cep, medicoCadastroViewModel.Endereco.Logradouro, medicoCadastroViewModel.Endereco.Numero, medicoCadastroViewModel.Endereco.Complemento, medicoCadastroViewModel.Endereco.Bairro, medicoCadastroViewModel.Endereco.Localidade, medicoCadastroViewModel.Endereco.Uf);
            Guid id = await this.enderecoRepository.BuscaIdEndereco(endereco);

            if (id == Guid.Empty)
            {
                resultado = await this.enderecoRepository.CadastrarEndereco(endereco);
                id = await this.enderecoRepository.BuscaIdEndereco(endereco);
            }

            if (!resultado)
            {
                return new Mensagem(0, "Falha ao cadastrar médico!");
            }

            Medico medico = new Medico(medicoCadastroViewModel.Nome, medicoCadastroViewModel.Cpf, medicoCadastroViewModel.Rg, int.Parse(medicoCadastroViewModel.Crm), medicoCadastroViewModel.DataNascimento, medicoCadastroViewModel.Sexo, medicoCadastroViewModel.Telefone, medicoCadastroViewModel.Email, true, id);

            resultado = await this.medicoRepository.CadastrarMedico(medico);

            if(!resultado)
            {
                return new Mensagem(0, "Falha ao cadastrar médico!");
            }

            Medico medicoResultado = await this.medicoRepository.BuscarMedicoPorCrm(int.Parse(medicoCadastroViewModel.Crm));

            if(medicoResultado == null)
            {
                return new Mensagem(0, "Falha ao cadastrar médico!");
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] data = sha256.ComputeHash(Encoding.UTF8.GetBytes(medicoCadastroViewModel.Usuario.Senha));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                medicoCadastroViewModel.Usuario.Senha = sBuilder.ToString();
            }

            Usuario usuario = new Usuario(medicoCadastroViewModel.Usuario.Email, medicoCadastroViewModel.Usuario.Senha, "Médico", medicoResultado.IdMedico, null);

            resultado = await this.usuarioRepository.CadastrarUsuario(usuario);

            if(!resultado)
            {
                return new Mensagem(0, "Falha ao cadastrar médico!");
            }

            return new Mensagem(1, "Médico cadastrado com sucesso!");
        }

        public async Task<IEnumerable<MedicoMatSelectViewModel>> ObterTodosMedicosParaMatSelect()
        {
            var listaMedicos = await this.medicoRepository.ObterTodosMedicosAtivosSemEndereco();

            var listaMedicosMatSelect = new List<MedicoMatSelectViewModel>();

            foreach(Medico m in listaMedicos)
            {
                listaMedicosMatSelect.Add(new MedicoMatSelectViewModel(m.IdMedico.ToString(), m.Nome));
            }

            return listaMedicosMatSelect.OrderBy(medico => medico.NomeMedico);
        }
    }
}
