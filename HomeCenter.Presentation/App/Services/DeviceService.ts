///<reference path="../../App/Shared/Service.ts" />
///<reference path="../../Scripts/typings/angularjs/angular.d.ts" /> 
///<reference path="../../Scripts/TypeLite.Net4.d.ts" /> 
module HomeCenter {
    export class DeviceService extends Common.Service {
        static $inject = ['$http'];

        constructor(httpRequest: ng.IHttpService) {
            super(httpRequest);
        }

        getDevicesForUI(): ng.IHttpPromise<Array<IDevice>> {
            return this.HttpRequest.get("/DeviceControl/GetDevicesForUi");
        }

        getDevices(): ng.IHttpPromise<Array<IDevice>> {
            return this.HttpRequest.get("/AddDevice/GetDevices");
        }

        getBuildings(): ng.IHttpPromise<Array<IBuilding>> {
            return this.HttpRequest.get("/DeviceControl/GetBuildings");
        }

        updateDeviceState(deviceId: number, deviceState: number): ng.IHttpPromise<any> {
            return this.HttpRequest.post("/DeviceControl/UpdateDeviceStateFromUi",
                {
                    "deviceId": deviceId,
                    "deviceState": deviceState
                });
        }

        switchDeviceState(deviceId: number): ng.IHttpPromise<any> {
            return this.HttpRequest.post("/DeviceControl/SwitchDeviceState",
                {
                    "deviceId": deviceId
                });
        }

        getDeviceTypes(): ng.IHttpPromise<Array<IDeviceType>> {
            return this.HttpRequest.get("/DeviceControl/GetDeviceTypes");
        }

        getGroups(): ng.IHttpPromise<Array<IGroup>> {
            return this.HttpRequest.get("/DeviceControl/GetGroups");
        }

        createDevice(device: IDeviceInput, group: IGroupInput): ng.IHttpPromise<any> {
            return this.HttpRequest.post("/AddDevice/CreateDevice", { device: device, group: group });
        }

        removeDevice(deviceId: number): ng.IHttpPromise<any> {
            return this.HttpRequest.post("/AddDevice/RemoveDevice", { deviceId: deviceId });
        }

        getDeviceRelations(): ng.IHttpPromise<IDeviceRelationDescriptionModel[]> {
            return this.HttpRequest.get("/DeviceRelations/GetDeviceRelations");
        }

        getStateRelations(): ng.IHttpPromise<IStateRelationModel[]> {
            return this.HttpRequest.get("/DeviceRelations/GetStateRelations");
        }

        createRelation(deviceRelation: IDeviceRelationInput): ng.IHttpPromise<any> {
            return this.HttpRequest.post("/DeviceRelations/CreateRelation", { deviceRelation: deviceRelation });
        }

        removeDeviceRelations(id: number) {
            return this.HttpRequest.post("/DeviceRelations/RemoveRelation", { deviceRelationId: id });
        }

        addJob(deviceJob: IDeviceJobInput): ng.IHttpPromise<any> {
            return this.HttpRequest.post("/DeviceRelations/AddDeviceJob", { deviceJob: deviceJob });
        }

        removeJob(id: number): ng.IHttpPromise<any> {
            return this.HttpRequest.post("/DeviceRelations/RemoveDeviceJob", { id: id });
        }

        getAllJobs(): ng.IHttpPromise<IDeviceJob[]> {
            return this.HttpRequest.get("/DeviceRelations/GetAllJobs");
        }

        changeGroup(deviceId: number, groupId: number): ng.IHttpPromise<any> {
            return this.HttpRequest.post("/AddDevice/ChangeGroup", { deviceId: deviceId, groupId: groupId });
        }

        getAddresses(): ng.IHttpPromise<number[]> {
            return this.HttpRequest.get("/AddDevice/GetAddresses");
        }
    }
}