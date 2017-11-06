import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { ModuloService } from '../../core/shared/modulo.service';
import { Pagina, Componente, Modulo, LISTAR_CATEGORIA, SALVAR_CATEGORIA } from '../../core/shared/modulo.model';
import 'rxjs/Rx';

@Component({
    selector: 'produto-form',
    templateUrl: './produto-form.component.html'
})
export class ProdutoFormComponent {
    produtoForm: FormGroup;
    successfulSave: boolean;
    errors: string[];
    id: string;
    categorias: Observable<any[]>;
    private sub: any;
    pagina: Pagina;

    constructor(private fb: FormBuilder, private http: Http, private route: ActivatedRoute, private service: ModuloService) { }

    ngOnInit() {
        this.errors = [];
        this.sub = this.route.params.subscribe(params => {
            this.id = params['id']; // (+) converts string 'id' to a number
        });

        this.pagina = new Pagina('PAG0001', '');

        console.log('form em branco');
        this.produtoForm = this.fb.group({
            categoria: ['', Validators.required],
            codigo: ['', [Validators.required, Validators.minLength(4)]],
            nome: ['', Validators.required],
            preco: ['', Validators.required],
            anotacao: ['']
        });

        // Preencher ao mudar o texto da categoria
        (<FormControl>this.produtoForm.controls['categoria'])
            .valueChanges.subscribe(
            changes => this.filterCategoria(changes));

        if (this.id && this.id > '0') {
            //this.service.getCategoria(this.id)
            //    .then(c => {
            //        console.log('preencheu o form que veio do id: ' + this.id);
            //    });
        }
    }

    ngOnDestroy(): void {
        this.sub.unsubscribe();
    }

    filterCategoria(val: string) {
        console.log('filter categoria: ' + val);
        var modulo = new Modulo(LISTAR_CATEGORIA, this.pagina, Componente.prototype);
        this.service.getByFilter(modulo, { filter: val },
            ok => {
                this.categorias = ok;
            },
            err => {
            });
        //.subscribe(p => {
        //    this.categorias = p;
        //});
    }

    displayFnCategoria(sector: any): string {
        return sector ? sector.descricao : sector;
    }

    onSubmit() {
        if (this.produtoForm.valid) {
            var produto = this.produtoForm.getRawValue();
            console.log(JSON.stringify(produto));
            var modulo = new Modulo(SALVAR_CATEGORIA, this.pagina, Component.prototype);
            this.service.save(modulo, produto,
                (idInserted, value) => this.successfulSave = true,
                (err) => {
                    this.successfulSave = false;
                    this.errors.push(err.text);
                });
        }
    }
}
