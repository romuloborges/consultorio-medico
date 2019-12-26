using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

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
            var usuario = this.usuarioRepository.VerificarExistenciaUsuario(usuarioViewModel.email, usuarioViewModel.senha);
            
            if (usuario != null)
            {
                if (usuario.Medico != null)
                {
                    nome = usuario.Medico.Nome;
                } else if (usuario.Atendente != null)
                {
                    nome = usuario.Atendente.Nome;
                }
                usuarioLogado = new UsuarioLogadoViewModel(usuario.Email, nome, usuario.Tipo);
            }

            return usuarioLogado;
        }
    }
}
