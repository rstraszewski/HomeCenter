///<reference path="../../App/Shared/Service.ts" />
///<reference path="../../Scripts/typings/angularjs/angular.d.ts" /> 
module HomeCenter
{
    export enum DeviceType
    {
        LightSwitch,
        LightDimmer
    }

    

    export class MainService extends Common.Service
    {
        static $inject = ['$http'];
        //HttpRequest: ng.IHttpService;

        constructor(httpRequest: ng.IHttpService)
        {
            super(httpRequest);
        }


        getLogs(): ng.IHttpPromise<Array<IHistoryLog>> {
            return this.HttpRequest.get("/Home/GetLogs");
        }

        getSpecificLogs(page: number, count: number, username: string, fromDateTime: number, toDateTime:number): ng.IHttpPromise<Array<IHistoryLog>> {
            return this.HttpRequest.post("/Home/GetLogsForPage", { page: page, count: count, username: username, fromDateTime: fromDateTime, toDateTime: toDateTime});
        }

        getLogsLength(username: string, fromDateTime: number, toDateTime: number): ng.IHttpPromise<number> {
            return this.HttpRequest.post("/Home/GetLogsLength", { username: username, fromDateTime: fromDateTime, toDateTime: toDateTime });
        }
    }
}