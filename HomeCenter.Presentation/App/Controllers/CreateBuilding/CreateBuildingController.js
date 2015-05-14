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
///<reference path="../../../App/Shared/Common.ts" />
///<reference path="../../Services/BuildingService.ts" />
var HomeCenter;
(function (HomeCenter) {
    var CreateBuildingController = (function (_super) {
        __extends(CreateBuildingController, _super);
        function CreateBuildingController($scope, $modal, buildingService, mainService, notify) {
            _super.call(this, $scope);
            this.buildingService = buildingService;
            this.mainService = mainService;
            this.modal = $modal;
            this.notify = notify;
            this.getBuildings();
        }
        CreateBuildingController.prototype.getBuildings = function () {
            var _this = this;
            this.scope.promise = this.buildingService.getAllBuildings().success(function (result) {
                return _this.scope.buildings = result;
            });
        };

        CreateBuildingController.prototype.createBuilding = function () {
            var _this = this;
            var modalInstance = this.modal.open({
                templateUrl: 'addBuildingPopup.html',
                controller: 'createBuildingPopupController as vm'
            });
            modalInstance.result.finally(function () {
                return _this.getBuildings();
            });
        };

        CreateBuildingController.prototype.createFloor = function (building) {
            var _this = this;
            var modalInstance = this.modal.open({
                templateUrl: 'addFloorPopup.html',
                controller: 'createFloorPopupController as vm',
                resolve: {
                    building: function () {
                        return building;
                    }
                }
            });
            modalInstance.result.finally(function () {
                return _this.getBuildings();
            });
        };

        CreateBuildingController.prototype.createRoom = function (floor, building) {
            var _this = this;
            var modalInstance = this.modal.open({
                templateUrl: 'addRoomPopup.html',
                controller: 'createRoomPopupController as vm',
                resolve: {
                    building: function () {
                        return building;
                    },
                    floor: function () {
                        return floor;
                    }
                }
            });
            modalInstance.result.finally(function () {
                return _this.getBuildings();
            });
        };

        CreateBuildingController.prototype.removeBuilding = function (building) {
            var _this = this;
            this.scope.promise = this.buildingService.removeBuilding(building.Id).success(function () {
                _this.notify("Building removed successfully");
                _this.getBuildings();
            });
        };
        CreateBuildingController.prototype.removeFloor = function (floor, building) {
            var _this = this;
            this.scope.promise = this.buildingService.removeFloor(floor.Id, building.Id).success(function () {
                _this.notify("Floor removed successfully");
                _this.getBuildings();
            });
        };
        CreateBuildingController.prototype.removeRoom = function (room, floor, building) {
            var _this = this;
            this.scope.promise = this.buildingService.removeRoom(room.Id, floor.Id, building.Id).success(function () {
                _this.notify("Room removed successfully");
                _this.getBuildings();
            });
        };
        CreateBuildingController.$inject = ['$scope', '$modal', 'buildingService', 'mainService', 'notify'];
        return CreateBuildingController;
    })(Common.Controller);
    HomeCenter.CreateBuildingController = CreateBuildingController;

    var CreateBuildingPopupController = (function (_super) {
        __extends(CreateBuildingPopupController, _super);
        function CreateBuildingPopupController($scope, $modalInstance, buildingService, notify) {
            _super.call(this, $scope);
            this.buildingService = buildingService;
            this.modalInstance = $modalInstance;
            this.scope.buildingsToAdd = [{ "Name": "" }];
            this.notify = notify;
        }
        CreateBuildingPopupController.prototype.add = function () {
            var _this = this;
            var isEveryNameFilled = this.scope.buildingsToAdd.every(function (element) {
                return element.Name.length != 0;
            });
            if (isEveryNameFilled) {
                this.buildingService.createBuildings(this.scope.buildingsToAdd).success(function () {
                    _this.notify("Buildings added succesfully!");
                    _this.modalInstance.close();
                });
            } else {
                this.notify("You need to give every building a name!");
            }
        };
        CreateBuildingPopupController.prototype.pop = function (index) {
            this.scope.buildingsToAdd = this.scope.buildingsToAdd.filter(function (v, i) {
                return i != index;
            });
        };
        CreateBuildingPopupController.prototype.addRow = function () {
            this.scope.buildingsToAdd.push({ "Name": "" });
        };
        CreateBuildingPopupController.prototype.addBuilding = function (index, building) {
            var _this = this;
            var isThisNameFilled = building.Name.length != 0;
            if (isThisNameFilled) {
                this.buildingService.createBuilding(building).success(function () {
                    _this.notify("Building added succesfully!");
                });
                this.scope.buildingsToAdd = this.scope.buildingsToAdd.filter(function (v, i) {
                    return i != index;
                });
            } else {
                this.notify("You need to give this building a name!");
            }
        };
        CreateBuildingPopupController.prototype.cancel = function () {
            this.modalInstance.close();
        };
        CreateBuildingPopupController.$inject = ['$scope', '$modalInstance', 'buildingService', 'notify'];
        return CreateBuildingPopupController;
    })(Common.Controller);
    HomeCenter.CreateBuildingPopupController = CreateBuildingPopupController;

    var CreateFloorPopupController = (function (_super) {
        __extends(CreateFloorPopupController, _super);
        function CreateFloorPopupController($scope, $modalInstance, buildingService, building, notify) {
            _super.call(this, $scope);
            this.buildingService = buildingService;
            this.modalInstance = $modalInstance;
            this.scope.floorsToAdd = [{ "Name": "" }];
            this.scope.building = building;
            this.notify = notify;
        }
        CreateFloorPopupController.prototype.add = function () {
            var _this = this;
            var isEveryNameFilled = this.scope.floorsToAdd.every(function (element) {
                return element.Name.length != 0;
            });
            if (isEveryNameFilled) {
                //this.modalInstance.close();
                this.buildingService.createFloors(this.scope.floorsToAdd, this.scope.building.Id).success(function () {
                    _this.notify("Floors created successfully");
                    _this.modalInstance.close();
                });
            } else {
                this.notify("You need to give every floor a name!");
            }
        };
        CreateFloorPopupController.prototype.pop = function (index) {
            this.scope.floorsToAdd = this.scope.floorsToAdd.filter(function (v, i) {
                return i != index;
            });
        };
        CreateFloorPopupController.prototype.addRow = function () {
            this.scope.floorsToAdd.push({ "Name": "" });
        };
        CreateFloorPopupController.prototype.addFloor = function (index, floor) {
            var _this = this;
            var isThisNameFilled = floor.Name.length != 0;
            if (isThisNameFilled) {
                this.buildingService.createFloor(floor, this.scope.building.Id).success(function () {
                    _this.notify("Floor created successfully");
                });
                this.scope.floorsToAdd = this.scope.floorsToAdd.filter(function (v, i) {
                    return i != index;
                });
            } else {
                this.notify("You need to give this floor a name!");
            }
        };
        CreateFloorPopupController.prototype.cancel = function () {
            this.modalInstance.close();
        };
        CreateFloorPopupController.$inject = ['$scope', '$modalInstance', 'buildingService', 'building', 'notify'];
        return CreateFloorPopupController;
    })(Common.Controller);
    HomeCenter.CreateFloorPopupController = CreateFloorPopupController;

    var CreateRoomPopupController = (function (_super) {
        __extends(CreateRoomPopupController, _super);
        function CreateRoomPopupController($scope, $modalInstance, buildingService, floor, building, notify) {
            _super.call(this, $scope);
            this.buildingService = buildingService;
            this.modalInstance = $modalInstance;
            this.scope.roomsToAdd = [{ "Name": "" }];
            this.scope.building = building;
            this.scope.floor = floor;
            this.notify = notify;
        }
        CreateRoomPopupController.prototype.add = function () {
            var _this = this;
            var isEveryNameFilled = this.scope.roomsToAdd.every(function (element) {
                return element.Name.length != 0;
            });
            if (isEveryNameFilled) {
                this.buildingService.createRooms(this.scope.roomsToAdd, this.scope.floor.Id, this.scope.building.Id).success(function () {
                    _this.notify("Rooms created successfully!");
                    _this.modalInstance.close();
                });
            } else {
                this.notify("You need to add name to every room!");
            }
        };
        CreateRoomPopupController.prototype.pop = function (index) {
            this.scope.roomsToAdd = this.scope.roomsToAdd.filter(function (v, i) {
                return i != index;
            });
        };
        CreateRoomPopupController.prototype.addRow = function () {
            this.scope.roomsToAdd.push({ "Name": "" });
        };
        CreateRoomPopupController.prototype.addRoom = function (index, room) {
            var _this = this;
            var isThisNameFilled = room.Name.length != 0;
            if (isThisNameFilled) {
                this.buildingService.createRoom(room, this.scope.floor.Id, this.scope.building.Id).success(function () {
                    _this.notify("Room created successfully!");
                });
                this.scope.roomsToAdd = this.scope.roomsToAdd.filter(function (v, i) {
                    return i != index;
                });
            } else {
                this.notify({ message: "You need to give this room a name!", classes: "alert-error" });
            }
        };
        CreateRoomPopupController.prototype.cancel = function () {
            this.modalInstance.close();
        };
        CreateRoomPopupController.$inject = ['$scope', '$modalInstance', 'buildingService', 'floor', 'building', 'notify'];
        return CreateRoomPopupController;
    })(Common.Controller);
    HomeCenter.CreateRoomPopupController = CreateRoomPopupController;
})(HomeCenter || (HomeCenter = {}));
//# sourceMappingURL=CreateBuildingController.js.map
