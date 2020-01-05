using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using ConsultorioMedico.Application.ViewModel.Usuario;
using ConsultorioMedico.Domain.Entity;

namespace ConsultorioMedico.Application.Service
{
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepository usuarioRepository;
        private IAtendenteRepository atendenteRepository;
        private IMedicoRepository medicoRepository;
        private IAgendamentoRepository agendamentoRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository, IAtendenteRepository atendenteRepository, IMedicoRepository medicoRepository, IAgendamentoRepository agendamentoRepository)
        {
            this.usuarioRepository = usuarioRepository;
            this.atendenteRepository = atendenteRepository;
            this.medicoRepository = medicoRepository;
            this.agendamentoRepository = agendamentoRepository;
        }
        public UsuarioLogadoViewModel ValidarUsuario(string email, string senha)
        {
            UsuarioLogadoViewModel usuarioLogado = null;
            string nome = "";
            string senhaFinal = "";
            string id = "";
            
            // Passando a senha que está em MD5 para SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] data = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                senhaFinal = sBuilder.ToString();
            }

            var usuario = this.usuarioRepository.VerificarExistenciaUsuario(email, senhaFinal);
            
            if (usuario != null)
            {
                if (usuario.Medico != null)
                {
                    nome = usuario.Medico.Nome;
                    id = usuario.Medico.IdMedico.ToString();
                } else if (usuario.Atendente != null)
                {
                    nome = usuario.Atendente.Nome;
                    id = usuario.Atendente.IdAtendente.ToString();
                } else
                {
                    nome = "Administrador";
                    id = Guid.Empty.ToString();
                }
                usuarioLogado = new UsuarioLogadoViewModel(id, usuario.Email, nome, usuario.Tipo);
            }

            return usuarioLogado;
        }

        public IEnumerable<UsuarioListarViewModel> ObterTodosUsuarios()
        {
            var lista = this.usuarioRepository.ObterTodosUsuarios();
            var listaUsuarios = new List<UsuarioListarViewModel>();
            string nome;

            foreach(Usuario u in lista)
            {
                nome = "";
                if (!u.Tipo.Equals("Administrador")) {
                    if (u.Medico != null)
                    {
                        nome = u.Medico.Nome;
                    }
                    else if (u.Atendente != null)
                    {
                        nome = u.Atendente.Nome;
                    }
                    listaUsuarios.Add(new UsuarioListarViewModel(u.IdUsuario.ToString(), u.Email, nome, u.Tipo));
                }
            }

            return listaUsuarios;
        }

        public Mensagem DeletarUsuario(string id)
        {
            var usuario = this.usuarioRepository.ObterUsuarioPorId(new Guid(id));
            bool resultado = true;

            if(usuario == null)
            {
                return new Mensagem(0, "Este usuário não existe!");
            }

            if(usuario.Atendente != null)
            {
                resultado = this.usuarioRepository.DeletarUsuario(usuario);
                if (!resultado)
                {
                    return new Mensagem(0, "Falha ao deletar usuário!");
                }
                
                resultado = this.atendenteRepository.DeletarAtendente(usuario.Atendente);
                if (!resultado)
                {
                    return new Mensagem(0, "Falha ao deletar usuário!");
                }
            } else if(usuario.Medico != null)
            {
                resultado = this.usuarioRepository.DeletarUsuario(usuario);
                if (!resultado)
                {
                    return new Mensagem(0, "Falha ao deletar usuário!");
                }

                if(this.agendamentoRepository.QuantidadeAgendamentosMedico(usuario.Medico.IdMedico) > 0)
                {
                    usuario.Medico.Ativado = !usuario.Medico.Ativado;
                    resultado = this.medicoRepository.AtualizarMedico(usuario.Medico);

                    if (!resultado)
                    {
                        return new Mensagem(0, "Falha ao desativar médico!");
                    }
                } else
                {
                    resultado = this.medicoRepository.DeletarMedico(usuario.Medico);

                    if (!resultado)
                    {
                        return new Mensagem(0, "Falha ao deletar médico!");
                    }
                }
            }

            return new Mensagem(1, "Usuário deletado com sucesso!");
        }
    }
}
