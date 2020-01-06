import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule, MatPaginatorModule } from '@angular/material';
import { FormsModule } from '@angular/forms';
import { LayoutModule } from '@angular/cdk/layout';
import { MatButtonModule } from '@angular/material';
import { MatToolbarModule } from '@angular/material';
import { MatMenuModule } from '@angular/material/menu';
import { MatSelectModule } from '@angular/material';
import { MatTableModule } from '@angular/material';
import { MatCheckboxModule } from '@angular/material';
import { MatCardModule } from '@angular/material';
import { MatTabsModule } from '@angular/material/tabs';
import { NgxMaskModule, IConfig } from 'ngx-mask';
import { MatRadioModule } from '@angular/material/radio';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthComponent } from './auth/auth.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { TelaPrincipalComponent } from './tela-principal/tela-principal.component';
import { DatePipe, registerLocaleData } from '@angular/common';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { ReactiveFormsModule } from '@angular/forms';
import { AgendarConsultaComponent } from './agendar-consulta/agendar-consulta.component';
import { MAT_DATE_LOCALE } from '@angular/material';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { ListarAgendamentosHojeComponent } from './listar-agendamentos-hoje/listar-agendamentos-hoje.component';
import { ListaAgendamentosComponent } from './lista-agendamentos/lista-agendamentos.component';
import { CadastrarEditarPacienteComponent } from './cadastrar-editar-paciente/cadastrar-editar-paciente.component';

import localeBr from '@angular/common/locales/br';
import localeBRExtra from '@angular/common/locales/extra/br';
import { ListaPacientesComponent } from './lista-pacientes/lista-pacientes.component';
import { GerenciarConsultaComponent } from './gerenciar-consulta/gerenciar-consulta.component';
import { ListaConsultasComponent } from './lista-consultas/lista-consultas.component';
import { GerenciarUsuarioComponent } from './gerenciar-usuario/gerenciar-usuario.component';
import { TelaAdminComponent } from './tela-admin/tela-admin.component';
import { ListaUsuariosComponent } from './lista-usuarios/lista-usuarios.component';

export const options: Partial<IConfig> | (() => Partial<IConfig>) = {};
registerLocaleData(localeBr, 'pt-BR', localeBRExtra);

@NgModule({
  declarations: [
    AppComponent,
    AuthComponent,
    TelaPrincipalComponent,
    AgendarConsultaComponent,
    ListarAgendamentosHojeComponent,
    ListaAgendamentosComponent,
    CadastrarEditarPacienteComponent,
    ListaPacientesComponent,
    GerenciarConsultaComponent,
    ListaConsultasComponent,
    GerenciarUsuarioComponent,
    TelaAdminComponent,
    ListaUsuariosComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    MatInputModule,
    MatIconModule,
    BrowserAnimationsModule,
    FormsModule,
    MatButtonModule,
    MatToolbarModule,
    MatMenuModule,
    MatDatepickerModule,
    MatNativeDateModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatTableModule,
    LayoutModule,
    MatCheckboxModule,
    MatCardModule,
    MatPaginatorModule,
    MatTabsModule,
    MatRadioModule,
    NgxMaterialTimepickerModule,
    NgxMaskModule.forRoot(options)
  ],
  providers: [DatePipe, { provide: MAT_DATE_LOCALE, useValue: 'pt-BR' }],
  bootstrap: [AppComponent]
})
export class AppModule { }
