
import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers, Request, RequestMethod } from '@angular/http';
import 'rxjs/Rx';
import { Modulo } from './modulo.model';

export class ModuloError {
    constructor(public code: string, public text: string) {

    }
}

@Injectable()
export class ModuloService {
    constructor(private http: Http) {

    }

    private getErrors(err: any): ModuloError[]
    {
        var errors: ModuloError[];
        errors = [];
        if (err.status === 400) {
            // handle validation error
            let validationErrorDictionary = JSON.parse(err.text());
            for (var fieldName in validationErrorDictionary) {
                if (validationErrorDictionary.hasOwnProperty(fieldName)) {
                    errors.push(new ModuloError('', validationErrorDictionary[fieldName]));
                }
            }
        } else {
            errors.push(new ModuloError('', "something went wrong! Statur error: " + err.status));
        }
        return errors;
    }

    private getOne(modulo: Modulo, filter: any, ok: (value: any) => void, error: (err: ModuloError) => void) {
        var url = '/api/modulo/processar_async?modulo=' + modulo.codigo + '&pagina=' + modulo.pagina.codigo + '&componente=' + modulo.componente.codigo;
        var body = JSON.stringify(filter);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        var options = new RequestOptions({
            method: RequestMethod.Post,
            headers: headers,
        });

        this.http.post(url, body, options)
            .map(res => res.json())
            .subscribe(
            (data) => {
                ok(data);
            },
            (err) => {
                var erros = this.getErrors(err);
                erros.forEach(value => error(value));
            });
    }

    private getList(modulo: Modulo, filter: any, ok: (value: any[]) => void, error: (err: ModuloError) => void) {
        var url = '/api/modulo/processar_async?modulo=' + modulo.codigo + '&pagina=' + modulo.pagina.codigo + '&componente=' + modulo.componente.codigo;
        var body = JSON.stringify(filter);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        var options = new RequestOptions({
            method: RequestMethod.Post,
            headers: headers,
        });

        this.http.post(url, body, options)
            .map(res => res.json())
            .subscribe(
            (data) => {
                ok(data);
            },
            (err) => {
                var erros = this.getErrors(err);
                erros.forEach(value => error(value));
            });
    }

    getById(modulo: Modulo, id: string, ok: (value: any) => void, error: (err: ModuloError) => void) {
        this.getOne(modulo, { id: id }, ok, error);
    }

    getByCodigo(modulo: Modulo, codigo: string, ok: (value: any) => void, error: (err: ModuloError) => void) {
        this.getOne(modulo, { codigo: codigo }, ok, error);
    }

    getByFilter(modulo: Modulo, filter: any, ok: (value: any) => void, error: (err: ModuloError) => void) {
        this.getList(modulo, filter, ok, error);
    }

    getCategorias(filter: string) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        var requestOptions = new RequestOptions({
            method: RequestMethod.Post,
            url: '/api/modulo/processar_async?modulo=CAT004&pagina=2&componente=5',
            headers: headers,
            body: JSON.stringify({})
        });

        return this.http
            .request(new Request(requestOptions))
            .map(res => res.json());
        //.toPromise();
    }

    getCategoria(id: string) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let categoria_id = { id: id };
        var response = this.http.post('/api/modulo/processar_async?modulo=AGR201&pagina=2&componente=5', JSON.stringify(categoria_id), options)
            .map(res => res.json())
            ;
        return response.first().toPromise();
    }

    save(modulo: Modulo, obj: any, ok: (idInsert: string, value: any) => void, error: (err: ModuloError) => void) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        this.http.post('/api/modulo/processar_async?modulo=AGR002&pagina=2&componente=5', JSON.stringify(obj), options)
            .map(res => res.json())
            .subscribe(
            (data) => {
                var idInsert = obj.id ? data.id : String.prototype;
                ok(idInsert, data);
            },
            (err) => {
                var erros = this.getErrors(err);
                erros.forEach(er => error(er));
            });
    }

    salvarCategoria(categoria: any, ok: (value: any) => void, error: (info: any) => void) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        this.http.post('/api/modulo/processar_async?modulo=AGR002&pagina=2&componente=5', JSON.stringify(categoria), options)
            .map(res => res.json())
            .subscribe(
            (data) => ok(data),
            (err) => {
                if (err.status === 400) {
                    // handle validation error
                    let validationErrorDictionary = JSON.parse(err.text());
                    for (var fieldName in validationErrorDictionary) {
                        if (validationErrorDictionary.hasOwnProperty(fieldName)) {
                            //this.errors.push(validationErrorDictionary[fieldName]);
                        }
                    }
                } else {
                    error("something went wrong! Statur error: " + err.status);
                }
            });
    }

    salvarProduto(produto: any, ok: (value: any) => void, error: (info: any) => void) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        this.http.post('/api/modulo/processar_async?modulo=AGR003&pagina=2&componente=5', JSON.stringify(produto), options)
            .map(res => res.json())
            .subscribe(
            (data) => ok(data),
            (err) => {
                if (err.status === 400) {
                    // handle validation error
                    let validationErrorDictionary = JSON.parse(err.text());
                    for (var fieldName in validationErrorDictionary) {
                        if (validationErrorDictionary.hasOwnProperty(fieldName)) {
                            //this.errors.push(validationErrorDictionary[fieldName]);
                        }
                    }
                } else {
                    error("something went wrong! Statur error: " + err.status);
                }
            });
    }
}
