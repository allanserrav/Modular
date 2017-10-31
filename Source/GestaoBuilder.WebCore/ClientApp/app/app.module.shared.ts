import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Http, HttpModule, RequestOptions, XHRBackend } from '@angular/http';
import { RouterModule } from '@angular/router';

// Components
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { HeaderComponent } from './components/shared/header/header.component';
import { CategoriaProdutoFormComponent } from './components/produto/categoria-produto-form/categoria-produto-form.component';
import { CategoriaProdutoListComponent } from './components/produto/categoria-produto-list/categoria-produto-list.component';
import { ActionItemComponent } from './components/shared/action-item.component';
import { ProdutoMenuComponent } from './components/produto/produto-menu.component';
import { ProdutoFormComponent } from './components/produto/produto-form.component';

// Services
import { HttpService } from './components/shared/http.service';
import { WindowRefService } from './components/shared/windowref.service';
import { ModuloService } from './components/shared/modulo.service';

// Modules
import { AppModuleMaterial } from './app.module.material';
import { routing } from './app.module.routing';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        HeaderComponent,
        CategoriaProdutoListComponent,
        CategoriaProdutoFormComponent,
        ActionItemComponent,
        ProdutoMenuComponent,
        ProdutoFormComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        AppModuleMaterial,
        routing
    ],
    providers: [
        ModuloService,
        WindowRefService,
        {
            provide: Http,
            useFactory: (backend: XHRBackend, options: RequestOptions) => {
                return new HttpService(backend, options);
            },
            deps: [XHRBackend, RequestOptions]
        },
    ]
})
export class AppModuleShared {
}
