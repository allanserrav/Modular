//import {
//    Component,
//    ElementRef,
//    ComponentMetadata
//} from '@angular/core';
//import { isPresent } from '@angular/core/src/facade/lang';

//export function PaginaComponent(annotation: any) {
//    return function (target: Function) {
//        var parentTarget = Object.getPrototypeOf(target.prototype).constructor;
//        var parentAnnotations = Reflect.getMetadata('annotations', parentTarget);

//        console.log('annotation decorator pagina value: ' + annotation['pagina']);

//        if (parentAnnotations) {
//            var parentAnnotation = parentAnnotations[0];
//            Object.keys(parentAnnotation).forEach(key => {
//                console.log('decorator key: ' + key);
//                console.log('parent annotation decorator value: ' + parentAnnotation[key]);
//                if (isPresent(parentAnnotation[key])) {
//                    console.log('annotation decorator value: ' + annotation[key]);
//                    // verify is annotation typeof function
//                    if (typeof annotation[key] === 'function') {
//                        annotation[key] = annotation[key].call(this, parentAnnotation[key]);
//                    } else if (
//                        // force override in annotation base
//                        !isPresent(annotation[key])
//                    ) {
//                        annotation[key] = parentAnnotation[key];
//                    }
//                }
//            });
//        }

//        var metadata = new ComponentMetadata(annotation);

//        Reflect.defineMetadata('annotations', [metadata], target);
//        //Reflect.defineMetadata('teste', 'teste text', target);
//        return class extends target {
//            public teste = "new property text";
//            //name = "override";
//        };
//    }
//}