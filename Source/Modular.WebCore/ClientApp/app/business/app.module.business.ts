import { NgModule } from '@angular/core';
//import { CommonModule } from '@angular/common';
//import { FormsModule, ReactiveFormsModule } from '@angular/forms';
//import { Http, HttpModule, RequestOptions, XHRBackend } from '@angular/http';
//import { RouterModule } from '@angular/router';

// Components
import { DashboardComponent  } from './home/dashboard.component';
import { CategoriaProdutoFormComponent } from './produto/categoria-produto/categoria-produto-form.component';
import { CategoriaProdutoListComponent } from './produto/categoria-produto/categoria-produto-list.component';
import { ProdutoFormComponent } from './produto/produto-form.component';

@NgModule({
    declarations: [
        DashboardComponent,
        CategoriaProdutoListComponent,
        CategoriaProdutoFormComponent,
        ProdutoFormComponent
    ]
})
export class AppModuleBusiness {
}