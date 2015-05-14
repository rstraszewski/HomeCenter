///<reference path="../../Scripts/typings/angularjs/angular.d.ts" /> 
module Common
{
    export class Service
    {
        static $inject = ['$http'];
        HttpRequest: ng.IHttpService;

        constructor(httpRequest: ng.IHttpService)
        {
            this.HttpRequest = httpRequest;
        }
    }
}