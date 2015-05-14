///<reference path="../../../Scripts/typings/requirejs/require.d.ts" />
///<reference path="../../../Scripts/typings/jqueryui/jqueryui.d.ts" />
///<reference path="../../../Scripts/TypeLite.Net4.d.ts" />
///<reference path="../../../Scripts/Enums.ts" />
///<reference path="../../Shared/Common.ts" />
///<reference path="../../Services/DeviceService.ts" />
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var HomeCenter;
(function (HomeCenter) {
    var DeviceControlController = (function (_super) {
        __extends(DeviceControlController, _super);
        function DeviceControlController($scope, deviceService, $interval) {
            var _this = this;
            _super.call(this, $scope);

            this.interval = $interval;
            this.deviceService = deviceService;
            this.getConstructorAjax();
            this.initiateDevice();
            this.slidingFlag = false;

            //this.webSocket = new WebSocket("ws://156.17.230.169:4649/WebSite");
            this.webSocket = new WebSocket("ws://" + window.location.hostname + ":4649/WebSite");

            this.webSocket.onclose = function () {
                _this.startDeviceInterval();
                _this.webSocket = new WebSocket("ws://localhost:4649/WebSite");
            };
            this.webSocket.onmessage = function (event) {
                //alert(event.data);
                _this.refreshDevice(JSON.parse(event.data));
            };
            this.startDeviceInterval();
        }
        DeviceControlController.prototype.refreshDevice = function (device) {
            var tempArray = this.scope.deviceList;

            this.scope.deviceList.forEach(function (value, index) {
                if (device.Id == value.Id) {
                    //alert(device.Name);
                    //tempArray[index] = device;
                    console.log(device.Id + " " + device.DeviceState);
                    value.DeviceState = device.DeviceState;
                    //tempArray[index].DeviceState = device.DeviceState;
                }
            });

            //this.scope.deviceList = tempArray;
            this.scope.$apply();
        };

        DeviceControlController.prototype.getConstructorAjax = function () {
            var _this = this;
            this.scope.promise = this.deviceService.getBuildings().success(function (result) {
                return _this.scope.buildingList = result;
            });
        };

        DeviceControlController.prototype.initiateDevice = function () {
            var _this = this;
            this.scope.promise = this.deviceService.getDevicesForUI().success(function (result) {
                _this.scope.deviceList = result;
            });
        };

        DeviceControlController.prototype.startDeviceInterval = function () {
            var _this = this;
            this.deviceService.getDevicesForUI().success(function (result) {
                if (_this.slidingFlag == false) {
                    _this.scope.deviceList = result;
                }
                if (_this.webSocket.readyState != 1)
                    setTimeout(function () {
                        return _this.startDeviceInterval();
                    }, 3000);
            });
        };

        DeviceControlController.prototype.getDevicesInRoom = function (room) {
            var devicesInRoom = [];
            if (this.scope.deviceList)
                this.scope.deviceList.forEach(function (device) {
                    return (device.RoomId == room.Id) ? devicesInRoom.push(device) : {};
                });
            return devicesInRoom;
        };

        DeviceControlController.prototype.switchDeviceState = function (device) {
            if (device.DeviceState > 0) {
                this.deviceService.updateDeviceState(device.Id, device.MinValue);
                device.DeviceState = device.MinValue;
            } else {
                this.deviceService.updateDeviceState(device.Id, device.MaxValue);
                device.DeviceState = device.MaxValue;
            }
        };

        DeviceControlController.prototype.stop = function (device) {
            this.deviceService.updateDeviceState(device.Id, device.DeviceState).success(function () {
                //this.startDeviceInterval();
            });

            //this.startDeviceInterval();
            this.slidingFlag = false;
        };

        DeviceControlController.prototype.start = function () {
            this.slidingFlag = true;
            //this.interval.cancel(this.deviceUpdateInterval);
        };

        DeviceControlController.prototype.roomContainsDevice = function (roomId) {
            var isDeviceInRoom = false;
            if (this.scope.deviceList) {
                this.scope.deviceList.forEach(function (device) {
                    if (device.RoomId == roomId) {
                        isDeviceInRoom = true;
                    }
                });
            }
            return isDeviceInRoom;
        };
        DeviceControlController.$inject = ['$scope', 'deviceService', '$interval'];
        return DeviceControlController;
    })(Common.Controller);
    HomeCenter.DeviceControlController = DeviceControlController;
})(HomeCenter || (HomeCenter = {}));
//# sourceMappingURL=DeviceControlController.js.map
