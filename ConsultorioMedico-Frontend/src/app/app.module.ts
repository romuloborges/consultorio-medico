import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material';
import { MatToolbarModule } from '@angular/material';
import { MatMenuModule } from '@angular/material/menu';
import { MatSelectModule } from '@angular/material';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthComponent } from './auth/auth.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { TelaPrincipalComponent } from './tela-principal/tela-principal.component';
import { DatePipe } from '@angular/common';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { ReactiveFormsModule } from '@angular/forms';
import { AgendarConsultaComponent } from './agendar-consulta/agendar-consulta.component';
import { MAT_DATE_LOCALE } from '@angular/material';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';

@NgModule({
  declarations: [
    AppComponent,
    AuthComponent,
    TelaPrincipalComponent,
    AgendarConsultaComponent
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
    NgxMaterialTimepickerModule
  ],
  providers: [DatePipe, { provide: MAT_DATE_LOCALE, useValue: 'pt-BR' }],
  bootstrap: [AppComponent]
})
export class AppModule { }
