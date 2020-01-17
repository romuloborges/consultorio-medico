using ConsultorioMedico.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorioMedico.Application.Service.Interface
{
    public interface IAtendenteService
    {
        Task<Mensagem> CadastrarAtendente(AtendenteCadastroViewModel atendenteCadastroViewModel);
    }
}
