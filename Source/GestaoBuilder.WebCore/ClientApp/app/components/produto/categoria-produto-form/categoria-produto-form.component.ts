import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { ModuloService } from '../../shared/modulo.service';
import 'rxjs/Rx';

@Component({
    selector: 'categoria-produto-form',
    templateUrl:'./categoria-produto-form.component.html'
})
export class CategoriaProdutoFormComponent implements OnInit, OnDestroy {
    categoriaForm: FormGroup;
    successfulSave: boolean;
    errors: string[];
    id: string;
    categorias: Observable<any[]>;
    private sub: any;

    constructor(private fb: FormBuilder, private http: Http, private route: ActivatedRoute, private service: ModuloService) { }

    ngOnInit() {
        this.errors = [];
        this.sub = this.route.params.subscribe(params => {
            this.id = params['id']; // (+) converts string 'id' to a number
        });

        console.log('form em branco');
        this.categoriaForm = this.fb.group({
            categoria_pai: [''],
            codigo: ['', [Validators.required, Validators.minLength(4)]],
            descricao: ['', Validators.required],
            anotacao: ['']
        });

        // Preencher ao mudar o texto da categoria pai
        (<FormControl>this.categoriaForm.controls['categoria_pai'])
            .valueChanges.subscribe(
                changes => this.filterCategoriaPai(changes));

        if (this.id && this.id > '0') {
            this.service.getCategoria(this.id)
                .then(c => {
                    console.log('preencheu o form que veio do id: ' + this.id);
                });
        }
    }

    ngOnDestroy(): void {
        this.sub.unsubscribe();
    }

    filterCategoriaPai(val: string) {
        console.log('filter categoria pai: ' + val);
        this.service.getCategorias(val)
            .subscribe(p => {
                this.categorias = p;
            });
    }

    displayFn(sector: any): string {
        return sector ? sector.descricao : sector;
    }

    onSubmit() {
        if (this.categoriaForm.valid) {
            var categoria = this.categoriaForm.getRawValue();
            this.service.salvarCategoria(categoria,
                (value) => this.successfulSave = true,
                (err) => {
                    this.successfulSave = false,
                    this.errors.push(err);
                });
            //console.log(JSON.stringify(categoria));
        }
    }
}