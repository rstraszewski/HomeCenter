///<reference path="../../../Scripts/typings/requirejs/require.d.ts" />
///<reference path="../../../Scripts/typings/angularjs/angular.d.ts" /> 
///<reference path="../../../Scripts/typings/angular-ui/angular-ui-router.d.ts" /> 
///<reference path="../../../Scripts/typings/angular-ui-bootstrap/angular-ui-bootstrap.d.ts" /> 
///<reference path="../../Shared/Common.ts" /> 
///<reference path="../../Shared/Service.ts" /> 
///<reference path="../../Services/BuildingService.ts" /> 
///<reference path="../../Shared/Service.ts" /> 
module HomeCenter {
    export interface IAddDeviceControllerScope extends Common.IScope {
        deviceTypes: IDeviceType[];
        choosedDeviceType: IDeviceType;
        buildings: IBuilding[];
        choosedBuilding: IBuilding;
        choosedFloor: IFloor;
        floors: IFloor[];
        choosedRoom: IRoom;
        rooms: IRoom[];
        groups: IGroup[];
        choosedGroup: IGroup;
        addresses: number[]
        createGroup: boolean;
        deviceName: string;
        deviceAddress: number;
        groupName: string;
        groupNumber: number;
        groupDescription: string;
        deviceList: IDevice[];
        promise: ng.IPromise<any>;
    }

    export class CreateDeviceController extends Common.Controller {
        scope: IAddDeviceControllerScope;
        buildingService: BuildingService;
        deviceService: DeviceService;
        notify: ng.cgNotify.INotifyService;
        static $inject = ['$scope', 'buildingService', 'deviceService','notify'];

        constructor($scope: IAddDeviceControllerScope, buildingService: BuildingService, deviceService: DeviceService, notify: ng.cgNotify.INotifyService) {
            super($scope);
            this.buildingService = buildingService;
            this.deviceService = deviceService;
            this.scope.createGroup = false;
            this.notify = notify;
            this.scope.groupNumber = 0;
            this.scope.promise = this.deviceService.getDeviceTypes().success((deviceTypes: IDeviceType[]) => {
                if (deviceTypes) {
                    this.scope.deviceTypes = deviceTypes;
                    this.scope.deviceTypes.unshift({
                        Id: null, Name: "", Type: "", Description: "", IsVirtual:null
                });  
                    this.scope.choosedDeviceType = deviceTypes[0];
                }
            });
            
            this.scope.promise = this.buildingService.getAllBuildings().success((buildings: IBuilding[]) => {
                if (buildings) {
                    this.scope.buildings = buildings;
                    this.scope.buildings.unshift({Id:null, Name:"", Floors: []});                  
                    this.scope.choosedBuilding = buildings[0];

                    this.scope.floors = this.scope.choosedBuilding.Floors;
                    this.scope.floors.unshift({ Id: null, Name: "", Rooms: [] });
                    this.scope.choosedFloor = this.scope.floors[0];

                    this.scope.rooms = this.scope.choosedFloor.Rooms;
                    this.scope.rooms.unshift({ Id: null, Name: ""});
                    this.scope.choosedRoom = this.scope.rooms[0];
                }
            });

            this.scope.promise = this.deviceService.getAddresses().success(result => {
                this.scope.addresses = result;
                //this.scope.addresses.unshift(null);
                this.scope.deviceAddress = this.scope.addresses[0];
            });

            this.refresh();

        }

        removeDevice(deviceId: number) {
            this.scope.promise = this.deviceService.removeDevice(deviceId).success(() => {
                this.deviceService.getDevicesForUI().success(result => {
                    this.notify("Device has been removed!");
                    this.refresh();
                });
            });
        }

        change(): void {
        }

        selectedBuilding(): void {
            if (this.scope.choosedBuilding) {
                this.scope.floors = this.scope.choosedBuilding.Floors;
                this.scope.floors.unshift({ Id: null, Name: "", Rooms: [] });
                this.scope.choosedFloor = this.scope.floors[0];
                this.scope.rooms = this.scope.choosedFloor.Rooms;
                this.scope.rooms.unshift({ Id: null, Name: "" });
                this.scope.choosedFloor = this.scope.floors[0];
                this.scope.choosedRoom = this.scope.rooms[0];
            } 
        }

        selectedFloor(): void {
            if (this.scope.choosedFloor) {
                this.scope.rooms = this.scope.choosedFloor.Rooms;
                this.scope.rooms.unshift({ Id: null, Name: "" });
                this.scope.choosedRoom = this.scope.rooms[0];
            } 
        }

        canAdd() {
            if ((!this.scope.createGroup) && (
                this.scope.groups &&
                this.scope.choosedGroup != this.scope.groups[0] &&
                    this.scope.rooms &&
                    this.scope.buildings &&
                    this.scope.floors &&
                    this.scope.deviceTypes &&
                    this.scope.choosedRoom != this.scope.rooms[0] &&
                    this.scope.choosedBuilding != this.scope.buildings[0] &&
                    this.scope.choosedFloor != this.scope.floors[0] &&
                    this.scope.choosedDeviceType != this.scope.deviceTypes[0] &&
                    this.scope.deviceAddress &&
                    this.scope.deviceName))
                return true;
            else if ((this.scope.createGroup) && (
                this.scope.groupNumber &&
                this.scope.groupName &&
                this.scope.groupDescription &&
                this.scope.rooms &&
                this.scope.buildings &&
                this.scope.floors &&
                this.scope.deviceTypes &&
                this.scope.choosedRoom != this.scope.rooms[0] && 
                    this.scope.choosedBuilding != this.scope.buildings[0] &&
                    this.scope.choosedFloor!=this.scope.floors[0] &&
                    this.scope.choosedDeviceType !=this.scope.deviceTypes[0] &&
                    this.scope.deviceAddress &&
                    this.scope.deviceName))
                return true;
            else {
                return false;
            }
        }

        changeGroup(deviceId:number, groupId: number) {
            this.scope.promise = this.deviceService.changeGroup(deviceId, groupId).success(() => {
                this.notify("Group for device has been changed!");
            });
        }

        refresh() {
            this.deviceService.getDevices().success(result => {
                this.scope.deviceList = result;
            });

            this.scope.promise = this.deviceService.getGroups().success((groups: IGroup[]) => {
                if (groups) {
                    this.scope.groups = groups;
                    this.scope.groups.unshift({ Id: null, Number: null, Name: "", Description: "" });
                    this.scope.choosedGroup = groups[0];
                }
            });
        }

        addDevice() {
            var device: IDeviceInput = null;
            var group: IGroupInput = null;
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

            this.scope.promise = this.deviceService.createDevice(device, group).success(() => {
                this.notify("Device has been added!");
                this.refresh();
            });
        }


    }
}