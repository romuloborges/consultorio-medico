﻿using System;
using System.Collections.Generic;
using System.Text;
using ConsultorioMedico.Domain.Entity;

namespace ConsultorioMedico.Domain.Repository
{
    public interface IMedicoRepository
    {
        bool CadastrarMedico(Medico medico);
        bool AtualizarMedico(Medico medico);
        IEnumerable<Medico> ObterTodosMedicos();
        IEnumerable<Medico> BuscarMedicoPorNome(string nome);
        Medico BuscarMedicoPorCrm(int crm);
        Medico BuscarMedicoPorCpf(string cpf);
        bool DeletarMedico(Medico medico);
    }
}
