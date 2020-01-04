using System;
using System.Collections.Generic;
using System.Text;
using ConsultorioMedico.Domain.Entity;

namespace ConsultorioMedico.Domain.Repository
{
    public interface IAtendenteRepository
    {
        bool CadastrarAtendente(Atendente atendente);
        bool AtualizarAtendente(Atendente atendente);
        Atendente BuscarAtendentePorCpf(string cpf);
        Atendente BuscarAtendentePorRg(string rg);
        Atendente BuscarAtendentePorId(Guid id);
        bool DeletarAtendente(Atendente atendente);
    }
}
