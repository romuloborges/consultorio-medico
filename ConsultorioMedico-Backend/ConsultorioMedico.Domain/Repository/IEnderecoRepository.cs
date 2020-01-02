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
        Endereco BuscarEnderecoPorId(Guid id);
        Guid BuscaIdEndereco(Endereco endereco);
        // Retorna quantas entidades dependem do endereço com o id passado
        int QuantidadeReferenciasEndereco(Guid id);
        bool DeletarEndereco(Endereco endereco);
    }
}
