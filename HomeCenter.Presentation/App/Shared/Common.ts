///<reference path="../../Scripts/typings/angularjs/angular.d.ts" /> 
///<reference path="../../Scripts/typings/angularjs/angular-route.d.ts" />


module Common 
{
    'use strict';
    export interface IScope extends ng.IScope {
        vm: Controller;
    }

    export class Controller {
        scope: IScope;
        constructor($scope: IScope) {
            this.scope = $scope;
        }
    }

    export class Module {
        private app: ng.IModule;

        constructor(name: string, modules: Array<string>) {
            this.app = angular.module(name, modules);
        }

        addController(name: string, controller: Function) {
            this.app.controller(name, controller);
        }

        addService(name: string, service: Function) {
            this.app.service(name, service);
        }

        addDirective(name: string, directive : ng.IDirectiveFactory)
        {
            this.app.directive(name, directive);
        }

        value(name: string, value: any) {
            this.app.value(name, value);
        }
    }
}