///<reference path="../../Scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../Scripts/typings/angularjs/angular-route.d.ts" />
var Common;
(function (Common) {
    'use strict';

    var Controller = (function () {
        function Controller($scope) {
            this.scope = $scope;
        }
        return Controller;
    })();
    Common.Controller = Controller;

    var Module = (function () {
        function Module(name, modules) {
            this.app = angular.module(name, modules);
        }
        Module.prototype.addController = function (name, controller) {
            this.app.controller(name, controller);
        };

        Module.prototype.addService = function (name, service) {
            this.app.service(name, service);
        };

        Module.prototype.addDirective = function (name, directive) {
            this.app.directive(name, directive);
        };

        Module.prototype.value = function (name, value) {
            this.app.value(name, value);
        };
        return Module;
    })();
    Common.Module = Module;
})(Common || (Common = {}));
//# sourceMappingURL=Common.js.map
