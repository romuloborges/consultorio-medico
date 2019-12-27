import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthComponent } from './auth/auth.component';
import { TelaPrincipalComponent } from './tela-principal/tela-principal.component';


const routes: Routes = [
  { path: '', component: AuthComponent },
  { path: 'principal', component: TelaPrincipalComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
