using System;
using System.Collections.Generic;
using System.Text;
using ConsultorioMedico.Domain.Entity;

namespace ConsultorioMedico.Domain.Repository
{
    public interface IEnderecoRepository
    {
        bool CadastrarEndereco(Endereco endereco);
        bool AtualizarEndereco(Endereco endereco);
        bool VerificaExistenciaEndereco(Endereco endereco);

        bool DeletarEndereco(Endereco endereco);
    }
}
