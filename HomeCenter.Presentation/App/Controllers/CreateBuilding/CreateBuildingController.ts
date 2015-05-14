///<reference path="../../../Scripts/typings/requirejs/require.d.ts" />
///<reference path="../../../Scripts/typings/angularjs/angular.d.ts" /> 
///<reference path="../../../Scripts/typings/angular-ui/angular-ui-router.d.ts" /> 
///<reference path="../../../Scripts/typings/angular-ui-bootstrap/angular-ui-bootstrap.d.ts" /> 
///<reference path="../../../App/Shared/Common.ts" />
///<reference path="../../Services/BuildingService.ts" />
module HomeCenter
{
    export interface ICreateBuildingControllerScope extends Common.IScope
    {
        buildings: IBuilding[];
        promise: ng.IPromise<any>
    }

    export interface ICreateFloorScope extends Common.IScope
    {
        floorsToAdd: IFloorInput[];
        building: IBuilding;
    }

    export interface ICreateRoomScope extends Common.IScope
    {
        roomsToAdd: IRoomInput[];
        building: IBuilding;
        floor: IFloor;
    }

    export interface ICreateBuildingScope extends Common.IScope
    {
        buildingsToAdd: IBuildingInput[];

    }

    export class CreateBuildingController extends Common.Controller
    {
        scope: ICreateBuildingControllerScope;
        buildingService: BuildingService;

        mainService: MainService;
        modal: ng.ui.bootstrap.IModalService;
        notify:ng.cgNotify.INotifyService
        static $inject = ['$scope', '$modal', 'buildingService', 'mainService','notify'];

        constructor($scope: ICreateBuildingControllerScope, $modal: ng.ui.bootstrap.IModalService, buildingService: BuildingService, mainService: MainService, notify: ng.cgNotify.INotifyService)
        {
            super($scope);
            this.buildingService = buildingService;
            this.mainService = mainService;
            this.modal = $modal;
            this.notify = notify;
            this.getBuildings();
        }

        getBuildings()
        {
            this.scope.promise = this.buildingService.getAllBuildings().success(result => this.scope.buildings = result);
        }

        createBuilding()
        {
            var modalInstance = this.modal.open({
                templateUrl: 'addBuildingPopup.html',
                controller: 'createBuildingPopupController as vm'
            });
            modalInstance.result.finally(() => this.getBuildings());
        }

        createFloor(building: IBuilding)
        {
            var modalInstance = this.modal.open({
                templateUrl: 'addFloorPopup.html',
                controller: 'createFloorPopupController as vm',
                resolve: 
                {
                    building: () => building
                }
            });
            modalInstance.result.finally(() => this.getBuildings());
        }

        createRoom(floor:IFloor,building: IBuilding)
        {
            var modalInstance = this.modal.open({
                templateUrl: 'addRoomPopup.html',
                controller: 'createRoomPopupController as vm',
                resolve:
                {
                    building: () => building,
                    floor: () => floor
                }
            });
            modalInstance.result.finally(() => this.getBuildings());
        }

        removeBuilding(building: IBuilding)
        {
            this.scope.promise = this.buildingService.removeBuilding(building.Id).success(() => {
                this.notify("Building removed successfully");
                this.getBuildings();
            });
        }
        removeFloor(floor: IFloor, building: IBuilding)
        {
            this.scope.promise = this.buildingService.removeFloor(floor.Id, building.Id).success(() => {
                this.notify("Floor removed successfully");
                this.getBuildings();
            });
        }
        removeRoom(room: IRoom, floor: IFloor, building: IBuilding)
        {
            this.scope.promise = this.buildingService.removeRoom(room.Id, floor.Id, building.Id).success(() => {
                this.notify("Room removed successfully");
                this.getBuildings();
            });
        }
    }

    export class CreateBuildingPopupController extends Common.Controller
    {
        scope: ICreateBuildingScope;
        static $inject = ['$scope', '$modalInstance', 'buildingService', 'notify'];
        buildingService: BuildingService;
        notify: ng.cgNotify.INotifyService;
        modalInstance: ng.ui.bootstrap.IModalServiceInstance;
        constructor($scope: ICreateBuildingScope, $modalInstance: ng.ui.bootstrap.IModalServiceInstance, buildingService: BuildingService, notify: ng.cgNotify.INotifyService)
        {
            super($scope);
            this.buildingService = buildingService;
            this.modalInstance = $modalInstance;
            this.scope.buildingsToAdd = [{ "Name": "" }];
            this.notify = notify;
        }
        add()
        {
            var isEveryNameFilled = this.scope.buildingsToAdd.every(element => element.Name.length != 0);
            if (isEveryNameFilled)
            {
                this.buildingService.createBuildings(this.scope.buildingsToAdd).success(() => {
                    this.notify("Buildings added succesfully!");
                    this.modalInstance.close();
                });
            } else
            {
                this.notify("You need to give every building a name!");
            }

        }
        pop(index: number)
        {
            this.scope.buildingsToAdd = this.scope.buildingsToAdd.filter((v, i) => i != index);
        }
        addRow()
        {
            this.scope.buildingsToAdd.push({ "Name": "" });
        }
        addBuilding(index: number, building: IBuildingInput)
        {
            var isThisNameFilled = building.Name.length != 0;
            if (isThisNameFilled)
            {
                this.buildingService.createBuilding(building).success(() => {
                    this.notify("Building added succesfully!");
                });
                this.scope.buildingsToAdd = this.scope.buildingsToAdd.filter((v, i) => i != index);
            } else
            {
                this.notify("You need to give this building a name!");
            }
        }
        cancel()
        {
            this.modalInstance.close();
        }

    }

    export class CreateFloorPopupController extends Common.Controller
    {
        scope: ICreateFloorScope;
        static $inject = ['$scope', '$modalInstance', 'buildingService', 'building','notify'];
        buildingService: BuildingService;
        modalInstance: ng.ui.bootstrap.IModalServiceInstance;
        notify: ng.cgNotify.INotifyService
        constructor($scope: ICreateFloorScope, $modalInstance: ng.ui.bootstrap.IModalServiceInstance, buildingService: BuildingService, building: IBuilding, notify: ng.cgNotify.INotifyService)
        {
            super($scope);
            this.buildingService = buildingService;
            this.modalInstance = $modalInstance;
            this.scope.floorsToAdd = [{ "Name": "" }];
            this.scope.building = building;
            this.notify=notify;
        }
        add()
        {
            var isEveryNameFilled = this.scope.floorsToAdd.every(element => element.Name.length != 0);
            if (isEveryNameFilled)
            {
                //this.modalInstance.close();
                this.buildingService.createFloors(this.scope.floorsToAdd, this.scope.building.Id).success(() => {
                    this.notify("Floors created successfully");
                    this.modalInstance.close();

                });
            } else
            {
                this.notify("You need to give every floor a name!");
            }

        }
        pop(index: number)
        {
            this.scope.floorsToAdd = this.scope.floorsToAdd.filter((v, i) => i != index);
        }
        addRow()
        {
            this.scope.floorsToAdd.push({ "Name": "" });
        }
        addFloor(index: number, floor: IFloorInput)
        {
            var isThisNameFilled = floor.Name.length != 0;
            if (isThisNameFilled)
            {
                this.buildingService.createFloor(floor, this.scope.building.Id).success(() => {
                    this.notify("Floor created successfully");
                });
                this.scope.floorsToAdd = this.scope.floorsToAdd.filter((v, i) => i != index);
            } else
            {
                this.notify("You need to give this floor a name!");
            }
        }
        cancel()
        {
            this.modalInstance.close();
        }

    }


    export class CreateRoomPopupController extends Common.Controller
    {
        scope: ICreateRoomScope;
        static $inject = ['$scope', '$modalInstance', 'buildingService','floor', 'building','notify'];
        buildingService: BuildingService;
        modalInstance: ng.ui.bootstrap.IModalServiceInstance;
        notify: ng.cgNotify.INotifyService;
        constructor($scope: ICreateRoomScope, $modalInstance: ng.ui.bootstrap.IModalServiceInstance, buildingService: BuildingService, floor: IFloor, building: IBuilding, notify: ng.cgNotify.INotifyService)
        {
            super($scope);
            this.buildingService = buildingService;
            this.modalInstance = $modalInstance;
            this.scope.roomsToAdd = [{ "Name": "" }];
            this.scope.building = building;
            this.scope.floor = floor;
            this.notify = notify;
        }
        add()
        {
            var isEveryNameFilled = this.scope.roomsToAdd.every(element => element.Name.length != 0);
            if (isEveryNameFilled)
            {
                this.buildingService.createRooms(this.scope.roomsToAdd, this.scope.floor.Id, this.scope.building.Id).success(() => {
                    this.notify("Rooms created successfully!");
                    this.modalInstance.close();
                });
            } else
            {
                this.notify("You need to add name to every room!");
            }

        }
        pop(index: number)
        {
            this.scope.roomsToAdd = this.scope.roomsToAdd.filter((v, i) => i != index);
        }
        addRow()
        {
            this.scope.roomsToAdd.push({ "Name": "" });
        }
        addRoom(index: number, room: IRoomInput)
        {
            var isThisNameFilled = room.Name.length != 0;
            if (isThisNameFilled)
            {
                this.buildingService.createRoom(room, this.scope.floor.Id, this.scope.building.Id).success(() => {
                    this.notify("Room created successfully!");
                });
                this.scope.roomsToAdd = this.scope.roomsToAdd.filter((v, i) => i != index);
            } else
            {
                this.notify({ message: "You need to give this room a name!", classes:"alert-error"});
            }
        }
        cancel()
        {
            this.modalInstance.close();
        }

    }

} 