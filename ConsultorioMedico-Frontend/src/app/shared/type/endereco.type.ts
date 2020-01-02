export class Endereco {
    constructor(public cep : string, public logradouro : string, public numero : string, public complemento : string, public bairro : string, public localidade : string, public uf : string) {}
}

export class EnderecoEditar {
    constructor(public id : string, public cep : string, public logradouro : string, public numero : string, public complemento : string, public bairro : string, public localidade : string, public uf : string) {}
}

export class EnderecoViaCep {
    // constructor(public cep : string, public logradouro : string, public complemento : string, public bairro : string, public localidade : string, public uf : string, public unidade : string, public ibge : string, public gia : string, public erro? : boolean) {}
    cep : string;
    logradouro : string;
    complemento : string;
    bairro : string;
    localidade : string;
    uf : string;
    unidade : string;
    ibge : string;
    gia : string;
    numero? : string;
    erro? : boolean;
}