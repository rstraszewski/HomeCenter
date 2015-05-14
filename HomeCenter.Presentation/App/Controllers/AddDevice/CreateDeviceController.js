var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
///<reference path="../../../Scripts/typings/requirejs/require.d.ts" />
///<reference path="../../../Scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../Scripts/typings/angular-ui/angular-ui-router.d.ts" />
///<reference path="../../../Scripts/typings/angular-ui-bootstrap/angular-ui-bootstrap.d.ts" />
///<reference path="../../Shared/Common.ts" />
///<reference path="../../Shared/Service.ts" />
///<reference path="../../Services/BuildingService.ts" />
///<reference path="../../Shared/Service.ts" />
var HomeCenter;
(function (HomeCenter) {
    var CreateDeviceController = (function (_super) {
        __extends(CreateDeviceController, _super);
        function CreateDeviceController($scope, buildingService, deviceService, notify) {
            var _this = this;
            _super.call(this, $scope);
            this.buildingService = buildingService;
            this.deviceService = deviceService;
            this.scope.createGroup = false;
            this.notify = notify;
            this.scope.groupNumber = 0;
            this.scope.promise = this.deviceService.getDeviceTypes().success(function (deviceTypes) {
                if (deviceTypes) {
                    _this.scope.deviceTypes = deviceTypes;
                    _this.scope.deviceTypes.unshift({
                        Id: null, Name: "", Type: "", Description: "", IsVirtual: null
                    });
                    _this.scope.choosedDeviceType = deviceTypes[0];
                }
            });

            this.scope.promise = this.buildingService.getAllBuildings().success(function (buildings) {
                if (buildings) {
                    _this.scope.buildings = buildings;
                    _this.scope.buildings.unshift({ Id: null, Name: "", Floors: [] });
                    _this.scope.choosedBuilding = buildings[0];

                    _this.scope.floors = _this.scope.choosedBuilding.Floors;
                    _this.scope.floors.unshift({ Id: null, Name: "", Rooms: [] });
                    _this.scope.choosedFloor = _this.scope.floors[0];

                    _this.scope.rooms = _this.scope.choosedFloor.Rooms;
                    _this.scope.rooms.unshift({ Id: null, Name: "" });
                    _this.scope.choosedRoom = _this.scope.rooms[0];
                }
            });

            this.scope.promise = this.deviceService.getAddresses().success(function (result) {
                _this.scope.addresses = result;

                //this.scope.addresses.unshift(null);
                _this.scope.deviceAddress = _this.scope.addresses[0];
            });

            this.refresh();
        }
        CreateDeviceController.prototype.removeDevice = function (deviceId) {
            var _this = this;
            this.scope.promise = this.deviceService.removeDevice(deviceId).success(function () {
                _this.deviceService.getDevicesForUI().success(function (result) {
                    _this.notify("Device has been removed!");
                    _this.refresh();
                });
            });
        };

        CreateDeviceController.prototype.change = function () {
        };

        CreateDeviceController.prototype.selectedBuilding = function () {
            if (this.scope.choosedBuilding) {
                this.scope.floors = this.scope.choosedBuilding.Floors;
                this.scope.floors.unshift({ Id: null, Name: "", Rooms: [] });
                this.scope.choosedFloor = this.scope.floors[0];
                this.scope.rooms = this.scope.choosedFloor.Rooms;
                this.scope.rooms.unshift({ Id: null, Name: "" });
                this.scope.choosedFloor = this.scope.floors[0];
                this.scope.choosedRoom = this.scope.rooms[0];
            }
        };

        CreateDeviceController.prototype.selectedFloor = function () {
            if (this.scope.choosedFloor) {
                this.scope.rooms = this.scope.choosedFloor.Rooms;
                this.scope.rooms.unshift({ Id: null, Name: "" });
                this.scope.choosedRoom = this.scope.rooms[0];
            }
        };

        CreateDeviceController.prototype.canAdd = function () {
            if ((!this.scope.createGroup) && (this.scope.groups && this.scope.choosedGroup != this.scope.groups[0] && this.scope.rooms && this.scope.buildings && this.scope.floors && this.scope.deviceTypes && this.scope.choosedRoom != this.scope.rooms[0] && this.scope.choosedBuilding != this.scope.buildings[0] && this.scope.choosedFloor != this.scope.floors[0] && this.scope.choosedDeviceType != this.scope.deviceTypes[0] && this.scope.deviceAddress && this.scope.deviceName))
                return true;
            else if ((this.scope.createGroup) && (this.scope.groupNumber && this.scope.groupName && this.scope.groupDescription && this.scope.rooms && this.scope.buildings && this.scope.floors && this.scope.deviceTypes && this.scope.choosedRoom != this.scope.rooms[0] && this.scope.choosedBuilding != this.scope.buildings[0] && this.scope.choosedFloor != this.scope.floors[0] && this.scope.choosedDeviceType != this.scope.deviceTypes[0] && this.scope.deviceAddress && this.scope.deviceName))
                return true;
            else {
                return false;
            }
        };

        CreateDeviceController.prototype.changeGroup = function (deviceId, groupId) {
            var _this = this;
            this.scope.promise = this.deviceService.changeGroup(deviceId, groupId).success(function () {
                _this.notify("Group for device has been changed!");
            });
        };

        CreateDeviceController.prototype.refresh = function () {
            var _this = this;
            this.deviceService.getDevices().success(function (result) {
                _this.scope.deviceList = result;
            });

            this.scope.promise = this.deviceService.getGroups().success(function (groups) {
                if (groups) {
                    _this.scope.groups = groups;
                    _this.scope.groups.unshift({ Id: null, Number: null, Name: "", Description: "" });
                    _this.scope.choosedGroup = groups[0];
                }
            });
        };

        CreateDeviceController.prototype.addDevice = function () {
            var _this = this;
            var device = null;
            var group = null;
            if (!this.scope.createGroup) {
                device = {
                    GroupId: this.scope.choosedGroup.Id,
                    RoomId: this.scope.choosedRoom.Id,
                    BuildingId: this.scope.choosedBuilding.Id,
                    FloorId: this.scope.choosedFloor.Id,
                    DeviceTypeId: this.scope.choosedDeviceType.Id,
                    Address: !this.scope.choosedDeviceType.IsVirtual ? this.scope.deviceAddress : null,
                    Name: this.scope.deviceName
                };
            } else {
                group = {
                    Number: this.scope.groupNumber,
                    Description: this.scope.groupDescription,
                    Name: this.scope.groupName
                };
                device = {
                    GroupId: null,
                    RoomId: this.scope.choosedRoom.Id,
                    BuildingId: this.scope.choosedBuilding.Id,
                    FloorId: this.scope.choosedFloor.Id,
                    DeviceTypeId: this.scope.choosedDeviceType.Id,
                    Address: !this.scope.choosedDeviceType.IsVirtual ? this.scope.deviceAddress : null,
                    Name: this.scope.deviceName
                };
            }

            this.scope.promise = this.deviceService.createDevice(device, group).success(function () {
                _this.notify("Device has been added!");
                _this.refresh();
            });
        };
        CreateDeviceController.$inject = ['$scope', 'buildingService', 'deviceService', 'notify'];
        return CreateDeviceController;
    })(Common.Controller);
    HomeCenter.CreateDeviceController = CreateDeviceController;
})(HomeCenter || (HomeCenter = {}));
//# sourceMappingURL=CreateDeviceController.js.map
