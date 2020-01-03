using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace ConsultorioMedico.Application.Service
{
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepository usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
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
            
            if (usuario != null)
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
    }
}
