var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
///<reference path="../../App/Shared/Service.ts" />
///<reference path="../../Scripts/typings/angularjs/angular.d.ts" />
var HomeCenter;
(function (HomeCenter) {
    (function (DeviceType) {
        DeviceType[DeviceType["LightSwitch"] = 0] = "LightSwitch";
        DeviceType[DeviceType["LightDimmer"] = 1] = "LightDimmer";
    })(HomeCenter.DeviceType || (HomeCenter.DeviceType = {}));
    var DeviceType = HomeCenter.DeviceType;

    var MainService = (function (_super) {
        __extends(MainService, _super);
        //HttpRequest: ng.IHttpService;
        function MainService(httpRequest) {
            _super.call(this, httpRequest);
        }
        MainService.prototype.getLogs = function () {
            return this.HttpRequest.get("/Home/GetLogs");
        };

        MainService.prototype.getSpecificLogs = function (page, count, username, fromDateTime, toDateTime) {
            return this.HttpRequest.post("/Home/GetLogsForPage", { page: page, count: count, username: username, fromDateTime: fromDateTime, toDateTime: toDateTime });
        };

        MainService.prototype.getLogsLength = function (username, fromDateTime, toDateTime) {
            return this.HttpRequest.post("/Home/GetLogsLength", { username: username, fromDateTime: fromDateTime, toDateTime: toDateTime });
        };
        MainService.$inject = ['$http'];
        return MainService;
    })(Common.Service);
    HomeCenter.MainService = MainService;
})(HomeCenter || (HomeCenter = {}));
//# sourceMappingURL=MainService.js.map
