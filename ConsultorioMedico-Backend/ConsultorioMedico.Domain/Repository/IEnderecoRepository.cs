using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConsultorioMedico.Domain.Entity;

namespace ConsultorioMedico.Domain.Repository
{
    public interface IEnderecoRepository
    {
        Task<bool> CadastrarEndereco(Endereco endereco);
        Task<bool> AtualizarEndereco(Endereco endereco);
        // Se o endereço já existe, retorna o ID para ele, caso contrário retorna o Guid vazio
        Task<Endereco> BuscarEnderecoPorId(Guid id);
        Task<Guid> BuscaIdEndereco(Endereco endereco);
        // Retorna quantas entidades dependem do endereço com o id passado
        Task<int> QuantidadeReferenciasEndereco(Guid id);
        Task<bool> DeletarEndereco(Endereco endereco);
    }
}
