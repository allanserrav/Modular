import { Routes, RouterModule } from '@angular/router';

// Components
import { HomeComponent } from './components/home/home.component';
import { CategoriaProdutoFormComponent } from './components/produto/categoria-produto-form/categoria-produto-form.component';
import { CategoriaProdutoListComponent } from './components/produto/categoria-produto-list/categoria-produto-list.component';
import { ProdutoMenuComponent } from './components/produto/produto-menu.component';
import { ProdutoFormComponent } from './components/produto/produto-form.component';

export const routing = RouterModule.forRoot([
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'categoria-produto-list', component: CategoriaProdutoListComponent },
    { path: 'categoria-produto-form/:id', component: CategoriaProdutoFormComponent },
    { path: 'produto-menu', component: ProdutoMenuComponent },
    { path: 'produto-form/:id', component: ProdutoFormComponent },
    { path: '**', redirectTo: 'home' }
]);


/*
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that
can be found in the LICENSE file at http://angular.io/license
*/