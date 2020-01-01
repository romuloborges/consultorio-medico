export class Endereco {
    constructor(public cep : string, public logradouro : string, public numero : string, public complemento : string, public bairro : string, public localidade : string, public uf : string) {}
}

export class EnderecoEditar {
    constructor(public id : string, public cep : string, public logradouro : string, public numero : string, public complemento : string, public bairro : string, public localidade : string, public uf : string) {}
}