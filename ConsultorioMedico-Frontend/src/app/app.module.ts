import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material';
import { FormsModule } from '@angular/forms';
import { LayoutModule } from '@angular/cdk/layout';
import { MatButtonModule } from '@angular/material';
import { MatToolbarModule } from '@angular/material';
import { MatMenuModule } from '@angular/material/menu';
import { MatSelectModule } from '@angular/material';
import { MatTableModule } from '@angular/material';
import { MatCheckboxModule } from '@angular/material';
import { MatCardModule } from '@angular/material';
import { NgxMaskModule, IConfig } from 'ngx-mask';

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
    CadastrarEditarPacienteComponent
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
    NgxMaterialTimepickerModule,
    NgxMaskModule.forRoot(options)
  ],
  providers: [DatePipe, { provide: MAT_DATE_LOCALE, useValue: 'pt-BR' }],
  bootstrap: [AppComponent]
})
export class AppModule { }
