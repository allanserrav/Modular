
import { Component, ViewEncapsulation } from '@angular/core';
import { Subscription } from "rxjs/Subscription";
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from "rxjs/Observable";

export class ItemMenu {
    constructor(public id: number, public name: string, public navigateTo: string, public paramid: string) { }
}

@Component({
    template: `
    <ul class="items">
      <li *ngFor="let item of itens"
        [class.selected]="isSelected(item)"
        (click)="onSelect(item)">
        <span class="badge">{{item.id}}</span> {{item.name}}
      </li>
    </ul>
  `
})
export class ProdutoMenuComponent {
    itens: ItemMenu[];

    private selectedId: number;
    private sub: Subscription;

    constructor(private route: ActivatedRoute, private router: Router) { }

    ngOnInit() {
        console.log('init produto menu');

        this.itens = [
            new ItemMenu(11, 'Nova categoria', 'categoria-produto-form', '0'),
            new ItemMenu(22, 'Novo produto', 'produto-form', ''),
            new ItemMenu(33, 'Listar categorias', 'categoria-produto-list', ''),
            new ItemMenu(44, 'Listar produtos', '', '')
        ];

        this.sub = this.route
            .params
            .subscribe(params => {
                this.selectedId = +params['id'];
                //this.service.getHeroes()
                //    .then(heroes => this.heroes = heroes);
            });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

    isSelected(item: ItemMenu) {
        console.log('isSelected');
        return item.id === this.selectedId;
    }

    onSelect(item: ItemMenu) {
        console.log('onSelect');
        this.router.navigate(['/' + item.navigateTo, item.paramid]);
    }

}
