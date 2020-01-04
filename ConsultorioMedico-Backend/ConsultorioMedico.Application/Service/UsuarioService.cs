using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
<<<<<<< HEAD
using ConsultorioMedico.Application.ViewModel.Usuario;
using ConsultorioMedico.Domain.Entity;
=======
>>>>>>> develop

namespace ConsultorioMedico.Application.Service
{
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepository usuarioRepository;
<<<<<<< HEAD
        private IAtendenteRepository atendenteRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository, IAtendenteRepository atendenteRepository)
        {
            this.usuarioRepository = usuarioRepository;
            this.atendenteRepository = atendenteRepository;
=======

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
>>>>>>> develop
        }
        public UsuarioLogadoViewModel ValidarUsuario(UsuarioViewModel usuarioViewModel)
        {
            UsuarioLogadoViewModel usuarioLogado = null;
            string nome = "";
            string senha = "";
            
            // Passando a senha que está em MD5 para SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] data = sha256.ComputeHash(Encoding.UTF8.GetBytes(usuarioViewModel.senha));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                senha = sBuilder.ToString();
            }

            var usuario = this.usuarioRepository.VerificarExistenciaUsuario(usuarioViewModel.email, senha);
            
<<<<<<< HEAD
            if (usuario != null && usuario.Ativado)
=======
            if (usuario != null)
>>>>>>> develop
            {
                if (usuario.Medico != null)
                {
                    nome = usuario.Medico.Nome;
                } else if (usuario.Atendente != null)
                {
                    nome = usuario.Atendente.Nome;
                } else
                {
                    nome = "Administrador";
                }
                usuarioLogado = new UsuarioLogadoViewModel(usuario.Email, nome, usuario.Tipo);
            }

            return usuarioLogado;
        }
<<<<<<< HEAD

        public IEnumerable<UsuarioListarViewModel> ObterTodosUsuariosAtivos()
        {
            var lista = this.usuarioRepository.ObterTodosUsuariosAtivos();
            var listaUsuariosAtivos = new List<UsuarioListarViewModel>();
            string nome;

            foreach(Usuario u in lista)
            {
                nome = "";
                if (!u.Tipo.Equals("Administrador") && u.Ativado) {
                    if (u.Medico != null)
                    {
                        nome = u.Medico.Nome;
                    }
                    else if (u.Atendente != null)
                    {
                        nome = u.Atendente.Nome;
                    }
                    listaUsuariosAtivos.Add(new UsuarioListarViewModel(u.IdUsuario.ToString(), u.Email, nome, u.Tipo));
                }
            }

            return listaUsuariosAtivos;
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
                usuario.Ativado = false;
                resultado = this.usuarioRepository.AtualizarUsuario(usuario);

                if (!resultado)
                {
                    return new Mensagem(0, "Falha ao deletar usuário!");
                }
            }

            return new Mensagem(1, "Usuário deletado com sucesso!");
        }
=======
>>>>>>> develop
    }
}
