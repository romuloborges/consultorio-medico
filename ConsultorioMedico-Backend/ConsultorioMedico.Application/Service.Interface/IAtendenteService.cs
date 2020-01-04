using ConsultorioMedico.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.Service.Interface
{
    public interface IAtendenteService
    {
        Mensagem CadastrarAtendente(AtendenteCadastroViewModel atendenteCadastroViewModel);
    }
}
