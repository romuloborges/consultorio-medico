using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConsultorioMedico.Domain.Entity;

namespace ConsultorioMedico.Domain.Repository
{
    public interface IMedicoRepository
    {
        Task<bool> CadastrarMedico(Medico medico);
        Task<bool> AtualizarMedico(Medico medico);
        Task<IEnumerable<Medico>> ObterTodosMedicosComEndereco();
        Task<IEnumerable<Medico>> ObterTodosMedicosAtivosSemEndereco();
        Task<IEnumerable<Medico>> BuscarMedicoPorNome(string nome);
        //Task<string> ObterNomeMedico(Guid id);
        Task<Medico> BuscarMedicoPorCrm(int crm);
        Task<Medico> BuscarMedicoPorCpf(string cpf);
        Task<Medico> BuscarMedicoPorRg(string rg);
        Task<bool> DeletarMedico(Medico medico);
    }
}
