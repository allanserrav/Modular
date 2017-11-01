import { Injectable } from '@angular/core';
import 'rxjs/Rx';

function _window(): any {
    // return the native window obj
    return window;
}

@Injectable()
export class WindowRefService {

    get nativeWindow(): any {
        console.log('nativeWindow');
        return _window();
    }

}
