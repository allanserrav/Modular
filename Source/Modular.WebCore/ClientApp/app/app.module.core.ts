import { NgModule } from '@angular/core';
import { Http, HttpModule, RequestOptions, XHRBackend } from '@angular/http';

// Services
import { HttpService } from './core/shared/http.service';
import { WindowRefService } from './core/shared/windowref.service';
import { ModuloService } from './core/shared/modulo.service';

// Modules
@NgModule({
    imports: [
        HttpModule,
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
export class AppModuleCore {
}
