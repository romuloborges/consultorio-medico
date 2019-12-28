import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthComponent } from './auth/auth.component';
import { TelaPrincipalComponent } from './tela-principal/tela-principal.component';
import { AgendarConsultaComponent } from './agendar-consulta/agendar-consulta.component';
import { ListarAgendamentosHojeComponent } from './listar-agendamentos-hoje/listar-agendamentos-hoje.component';


const routes: Routes = [
  { path: '', component: AuthComponent },
  { path: 'principal', component: TelaPrincipalComponent,
    children: [{ path: 'agendarConsulta', component: AgendarConsultaComponent },
               { path: 'listarAgendamentos', component: ListarAgendamentosHojeComponent }]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
