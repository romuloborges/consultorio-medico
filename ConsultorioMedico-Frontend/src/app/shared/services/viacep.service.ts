import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { viacepurl } from '../constantes/constantes';
import { EnderecoViaCep } from '../type/endereco.type';

@Injectable ({
    providedIn: 'root'
})

export class ViaCepService {

    constructor(private httpClient : HttpClient) {}

    obterEndereco(valor : string) {
        //Nova variável "cep" somente com dígitos.
        var cep = valor.replace(/\D/g, '');

        //Verifica se campo cep possui valor informado.
        if (cep != "") {
            //Expressão regular para validar o CEP.
            var validacep = /^[0-9]{8}$/;
            
            //Valida o formato do CEP.
            if(validacep.test(cep)) {
                return this.httpClient.get<EnderecoViaCep>(`${viacepurl}/${cep}/json`);
            }
        }
    }

}