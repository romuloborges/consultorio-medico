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
        // Se o endereço já existe, retorna o ID para ele, caso contrário retorna o Guid vazio
        Guid BuscaIdEndereco(Endereco endereco);

        bool DeletarEndereco(Endereco endereco);
    }
}
