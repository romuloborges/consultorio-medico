import { Component, OnInit } from '@angular/core';
import { uf, sexo } from '../shared/constantes';

@Component({
  selector: 'app-cadastrar-editar-paciente',
  templateUrl: './cadastrar-editar-paciente.component.html',
  styleUrls: ['./cadastrar-editar-paciente.component.css']
})
export class CadastrarEditarPacienteComponent implements OnInit {

  uf = uf;
  sexo = sexo;

  constructor() { }

  ngOnInit() {
  }

}
