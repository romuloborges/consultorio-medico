using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsultorioMedico.Application.Service
{
    public class MedicoService : IMedicoService
    {
        private IMedicoRepository medicoRepository;
        public MedicoService(IMedicoRepository medicoRepository)
        {
            this.medicoRepository = medicoRepository;
        }

        public IEnumerable<MedicoMatSelectViewModel> ObterTodosMedicosParaMatSelect()
        {
            var listaMedicos = this.medicoRepository.ObterTodosMedicosSemEndereco();

            var listaMedicosMatSelect = new List<MedicoMatSelectViewModel>();

            foreach(Medico m in listaMedicos)
            {
                listaMedicosMatSelect.Add(new MedicoMatSelectViewModel(m.IdMedico.ToString(), m.Nome));
            }

            return listaMedicosMatSelect.OrderBy(medico => medico.NomeMedico);
        }
    }
}
