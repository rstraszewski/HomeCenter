var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
///<reference path="../../App/Shared/Service.ts" />
///<reference path="../../Scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../Scripts/TypeLite.Net4.d.ts" />
var HomeCenter;
(function (HomeCenter) {
    var DeviceService = (function (_super) {
        __extends(DeviceService, _super);
        function DeviceService(httpRequest) {
            _super.call(this, httpRequest);
        }
        DeviceService.prototype.getDevicesForUI = function () {
            return this.HttpRequest.get("/DeviceControl/GetDevicesForUi");
        };

        DeviceService.prototype.getDevices = function () {
            return this.HttpRequest.get("/AddDevice/GetDevices");
        };

        DeviceService.prototype.getBuildings = function () {
            return this.HttpRequest.get("/DeviceControl/GetBuildings");
        };

        DeviceService.prototype.updateDeviceState = function (deviceId, deviceState) {
            return this.HttpRequest.post("/DeviceControl/UpdateDeviceStateFromUi", {
                "deviceId": deviceId,
                "deviceState": deviceState
            });
        };

        DeviceService.prototype.switchDeviceState = function (deviceId) {
            return this.HttpRequest.post("/DeviceControl/SwitchDeviceState", {
                "deviceId": deviceId
            });
        };

        DeviceService.prototype.getDeviceTypes = function () {
            return this.HttpRequest.get("/DeviceControl/GetDeviceTypes");
        };

        DeviceService.prototype.getGroups = function () {
            return this.HttpRequest.get("/DeviceControl/GetGroups");
        };

        DeviceService.prototype.createDevice = function (device, group) {
            return this.HttpRequest.post("/AddDevice/CreateDevice", { device: device, group: group });
        };

        DeviceService.prototype.removeDevice = function (deviceId) {
            return this.HttpRequest.post("/AddDevice/RemoveDevice", { deviceId: deviceId });
        };

        DeviceService.prototype.getDeviceRelations = function () {
            return this.HttpRequest.get("/DeviceRelations/GetDeviceRelations");
        };

        DeviceService.prototype.getStateRelations = function () {
            return this.HttpRequest.get("/DeviceRelations/GetStateRelations");
        };

        DeviceService.prototype.createRelation = function (deviceRelation) {
            return this.HttpRequest.post("/DeviceRelations/CreateRelation", { deviceRelation: deviceRelation });
        };

        DeviceService.prototype.removeDeviceRelations = function (id) {
            return this.HttpRequest.post("/DeviceRelations/RemoveRelation", { deviceRelationId: id });
        };

        DeviceService.prototype.addJob = function (deviceJob) {
            return this.HttpRequest.post("/DeviceRelations/AddDeviceJob", { deviceJob: deviceJob });
        };

        DeviceService.prototype.removeJob = function (id) {
            return this.HttpRequest.post("/DeviceRelations/RemoveDeviceJob", { id: id });
        };

        DeviceService.prototype.getAllJobs = function () {
            return this.HttpRequest.get("/DeviceRelations/GetAllJobs");
        };

        DeviceService.prototype.changeGroup = function (deviceId, groupId) {
            return this.HttpRequest.post("/AddDevice/ChangeGroup", { deviceId: deviceId, groupId: groupId });
        };

        DeviceService.prototype.getAddresses = function () {
            return this.HttpRequest.get("/AddDevice/GetAddresses");
        };
        DeviceService.$inject = ['$http'];
        return DeviceService;
    })(Common.Service);
    HomeCenter.DeviceService = DeviceService;
})(HomeCenter || (HomeCenter = {}));
//# sourceMappingURL=DeviceService.js.map
