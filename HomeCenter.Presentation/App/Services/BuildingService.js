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
    var BuildingService = (function (_super) {
        __extends(BuildingService, _super);
        function BuildingService(httpRequest) {
            _super.call(this, httpRequest);
        }
        BuildingService.prototype.createBuilding = function (building) {
            return this.HttpRequest.post("/CreateBuilding/CreateBuilding", building);
        };

        BuildingService.prototype.createBuildings = function (buildings) {
            return this.HttpRequest.post("/CreateBuilding/CreateBuildings", buildings);
        };

        BuildingService.prototype.createFloor = function (floor, buildingId) {
            return this.HttpRequest.post("/CreateBuilding/CreateFloor", {
                "floor": floor,
                "buildingId": buildingId
            });
        };

        BuildingService.prototype.createFloors = function (floors, buildingId) {
            return this.HttpRequest.post("/CreateBuilding/CreateFloors", {
                "floors": floors,
                "buildingId": buildingId
            });
        };

        BuildingService.prototype.createRoom = function (room, floorId, buildingId) {
            return this.HttpRequest.post("/CreateBuilding/CreateRoom", {
                "room": room,
                "floorId": floorId,
                "buildingId": buildingId
            });
        };

        BuildingService.prototype.createRooms = function (rooms, floorId, buildingId) {
            return this.HttpRequest.post("/CreateBuilding/CreateRooms", {
                "rooms": rooms,
                "floorId": floorId,
                "buildingId": buildingId
            });
        };

        BuildingService.prototype.removeBuilding = function (buildingId) {
            return this.HttpRequest.post("/CreateBuilding/RemoveBuilding", {
                "buildingId": buildingId
            });
        };

        BuildingService.prototype.removeFloor = function (floorId, buildingId) {
            return this.HttpRequest.post("/CreateBuilding/RemoveFloor", {
                "floorId": floorId,
                "buildingId": buildingId
            });
        };

        BuildingService.prototype.removeRoom = function (roomId, floorId, buildingId) {
            return this.HttpRequest.post("/CreateBuilding/RemoveRoom", {
                "roomId": roomId,
                "floorId": floorId,
                "buildingId": buildingId
            });
        };

        BuildingService.prototype.getAllBuildings = function () {
            return this.HttpRequest.get("/DeviceControl/GetBuildings");
        };
        BuildingService.$inject = ['$http'];
        return BuildingService;
    })(Common.Service);
    HomeCenter.BuildingService = BuildingService;
})(HomeCenter || (HomeCenter = {}));
//# sourceMappingURL=BuildingService.js.map
