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
import { GerenciarUsuarioComponent } from './gerenciar-usuario/gerenciar-usuario.component';
import { TelaAdminComponent } from './tela-admin/tela-admin.component';
import { ListaUsuariosComponent } from './lista-usuarios/lista-usuarios.component';
import { ListaConsultasComponent } from './lista-consultas/lista-consultas.component';


const routes: Routes = [
  { path: '', component: AuthComponent },
  { path: 'principal', component: TelaPrincipalComponent,
    children: [{ path: 'agendarConsulta', component: AgendarConsultaComponent },
               { path: 'listarAgendamentosHoje', component: ListarAgendamentosHojeComponent },
               { path: 'listarAgendamentos', component: ListaAgendamentosComponent },
               { path: 'gerenciarPaciente', component: CadastrarEditarPacienteComponent },
               { path: 'listarPacientes', component: ListaPacientesComponent },
               { path: 'gerenciarConsulta', component: GerenciarConsultaComponent },
               { path: 'listarConsultas', component: ListaConsultasComponent }]},
  { path: 'gerenciarUsuarios', component: TelaAdminComponent,
    children: [{ path: 'cadastrarUsuario', component: GerenciarUsuarioComponent },
               { path: 'listarUsuarios', component: ListaUsuariosComponent }]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
