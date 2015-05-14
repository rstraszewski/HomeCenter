///<reference path="../../App/Shared/Service.ts" />
///<reference path="../../Scripts/typings/angularjs/angular.d.ts" /> 
///<reference path="../../Scripts/TypeLite.Net4.d.ts" /> 
module HomeCenter
{
    export class BuildingService extends Common.Service
    {
        static $inject = ['$http'];

        constructor(httpRequest: ng.IHttpService)
        {
            super(httpRequest);
        }

        createBuilding(building: IBuildingInput): ng.IHttpPromise<any>
        {
            return this.HttpRequest.post("/CreateBuilding/CreateBuilding", building);
        }

        createBuildings(buildings: IBuildingInput[]): ng.IHttpPromise<any>
        {
            return this.HttpRequest.post("/CreateBuilding/CreateBuildings", buildings);
        }

        createFloor(floor: IFloorInput, buildingId: number): ng.IHttpPromise<any>
        {
            return this.HttpRequest.post("/CreateBuilding/CreateFloor",
                {
                    "floor": floor,
                    "buildingId": buildingId
                });
        }

        createFloors(floors: IFloorInput[], buildingId: number): ng.IHttpPromise<any>
        {
            return this.HttpRequest.post("/CreateBuilding/CreateFloors",
                {
                    "floors": floors,
                    "buildingId": buildingId
                });
        }

        createRoom(room: IRoomInput, floorId: number, buildingId: number): ng.IHttpPromise<any>
        {
            return this.HttpRequest.post("/CreateBuilding/CreateRoom",
                {
                    "room": room,
                    "floorId": floorId,
                    "buildingId": buildingId
                });
        }

        createRooms(rooms: IRoomInput[], floorId: number, buildingId: number): ng.IHttpPromise<any>
        {
            return this.HttpRequest.post("/CreateBuilding/CreateRooms",
                {
                    "rooms": rooms,
                    "floorId": floorId,
                    "buildingId": buildingId
                });
        }

        removeBuilding(buildingId: number): ng.IHttpPromise<any>
        {
            return this.HttpRequest.post("/CreateBuilding/RemoveBuilding",
                {
                    "buildingId": buildingId
                });
        }

        removeFloor(floorId: number, buildingId: number): ng.IHttpPromise<any>
        {
            return this.HttpRequest.post("/CreateBuilding/RemoveFloor",
                {
                    "floorId": floorId,
                    "buildingId": buildingId
                });
        }

        removeRoom(roomId: number, floorId: number, buildingId: number): ng.IHttpPromise<any>
        {
            return this.HttpRequest.post("/CreateBuilding/RemoveRoom",
                {
                    "roomId": roomId,
                    "floorId": floorId,
                    "buildingId": buildingId
                });
        }

        getAllBuildings(): ng.IHttpPromise<IBuilding[]>
        {
            return this.HttpRequest.get("/DeviceControl/GetBuildings");
        }
    }
}