export class Pagina {
    constructor(public codigo: string, public modulo_principal: string) { }
}

export class Componente {
    constructor(public codigo: string, public pagina: Pagina) { }
}

export class Modulo {
    constructor(public codigo: string, public pagina: Pagina, public componente: Componente) { }
}

export const LISTAR_CATEGORIA = 'AGR001';
export const SALVAR_CATEGORIA = 'AGR003';