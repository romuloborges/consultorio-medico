using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConsultorioMedico.Domain.Entity;

namespace ConsultorioMedico.Domain.Repository
{
    public interface IAtendenteRepository
    {
        Task<bool> CadastrarAtendente(Atendente atendente);
        Task<bool> AtualizarAtendente(Atendente atendente);
        Task<Atendente> BuscarAtendentePorCpf(string cpf);
        Task<Atendente> BuscarAtendentePorRg(string rg);
        Task<Atendente> BuscarAtendentePorId(Guid id);
        Task<bool> DeletarAtendente(Atendente atendente);
    }
}
