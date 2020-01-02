import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthComponent } from './auth/auth.component';
import { TelaPrincipalComponent } from './tela-principal/tela-principal.component';
import { AgendarConsultaComponent } from './agendar-consulta/agendar-consulta.component';
import { ListarAgendamentosHojeComponent } from './listar-agendamentos-hoje/listar-agendamentos-hoje.component';
import { ListaAgendamentosComponent } from './lista-agendamentos/lista-agendamentos.component';
import { CadastrarEditarPacienteComponent } from './cadastrar-editar-paciente/cadastrar-editar-paciente.component';
import { ListaPacientesComponent } from './lista-pacientes/lista-pacientes.component';
import { GerenciarConsultaComponent } from './gerenciar-consulta/gerenciar-consulta.component';


const routes: Routes = [
  { path: '', component: AuthComponent },
  { path: 'principal', component: TelaPrincipalComponent,
    children: [{ path: 'agendarConsulta', component: AgendarConsultaComponent },
               { path: 'listarAgendamentosHoje', component: ListarAgendamentosHojeComponent },
               { path: 'listarAgendamentos', component: ListaAgendamentosComponent },
               { path: 'gerenciarPaciente', component: CadastrarEditarPacienteComponent },
               { path: 'listarPacientes', component: ListaPacientesComponent },
               { path: 'gerenciarConsulta', component: GerenciarConsultaComponent }]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
