
import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'action-item',
    templateUrl: './action-item.component.html'
})
export class ActionItemComponent {
    @Input() obj: any;

    constructor(private router: Router) {
    }

    navigateToDetail() {
        this.router.navigate(['categoria-produto-form', this.obj.id]);
    }
}
