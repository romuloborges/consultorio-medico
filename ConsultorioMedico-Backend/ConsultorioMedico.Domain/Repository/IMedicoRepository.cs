using System;
using System.Collections.Generic;
using System.Text;
using ConsultorioMedico.Domain.Entity;

namespace ConsultorioMedico.Domain.Repository
{
    public interface IMedicoRepository
    {
        bool CadastrarMedico(Medico medico);
        bool AtualizarMedico(Medico medico);
        IEnumerable<Medico> ObterTodosMedicosComEndereco();
        IEnumerable<Medico> ObterTodosMedicosSemEndereco();
        IEnumerable<Medico> BuscarMedicoPorNome(string nome);
        string ObterNomeMedico(Guid id);
        Medico BuscarMedicoPorCrm(int crm);
        Medico BuscarMedicoPorCpf(string cpf);
        Medico BuscarMedicoPorRg(string rg);
        bool DeletarMedico(Medico medico);
    }
}
