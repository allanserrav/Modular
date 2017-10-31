
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/Rx';

@Injectable()
export class ProdutoService {
    constructor(private http: Http) {

    }

    getCategorias()
    {
        this.http.get('api/modulo/processar?modulo=AGR103&pagina=2&componente=5')
            .map(response => response.json())
            .toPromise();
    }
}
