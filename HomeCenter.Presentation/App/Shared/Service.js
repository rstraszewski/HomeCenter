///<reference path="../../Scripts/typings/angularjs/angular.d.ts" />
var Common;
(function (Common) {
    var Service = (function () {
        function Service(httpRequest) {
            this.HttpRequest = httpRequest;
        }
        Service.$inject = ['$http'];
        return Service;
    })();
    Common.Service = Service;
})(Common || (Common = {}));
//# sourceMappingURL=Service.js.map
