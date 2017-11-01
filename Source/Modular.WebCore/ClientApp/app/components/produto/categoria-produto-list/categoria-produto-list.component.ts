
import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ModuloService } from '../../shared/modulo.service';

@Component({
    selector: 'categoria-produto-list',
    templateUrl: './categoria-produto-list.component.html'
})
export class CategoriaProdutoListComponent {
    @Input() categorias: Observable<any>;

    constructor(private service: ModuloService) {
    }

    ngOnInit() {
        //this.categorias = this.http.post('/api/modulo/processar_async?modulo=CAT004&pagina=2&componente=5', JSON.stringify({}), options)
        //    .map(res => res.json())
        //    .catch((error: any) => Observable.throw(error.json().error || 'Server error'));
        this.service.getCategorias('')
            .subscribe(p => this.categorias = p);
    }
}
