///<reference path="../../../Scripts/typings/requirejs/require.d.ts" />
///<reference path="../../../Scripts/typings/angularjs/angular.d.ts" /> 
///<reference path="../../../Scripts/typings/angular-ui/angular-ui-router.d.ts" /> 
///<reference path="../../../Scripts/typings/angular-ui-bootstrap/angular-ui-bootstrap.d.ts" /> 
///<reference path="../../Shared/Common.ts" /> 
///<reference path="../../Shared/Service.ts" /> 
///<reference path="../../Services/BuildingService.ts" /> 
///<reference path="../../Shared/Service.ts" /> 
module HomeCenter
{
    export interface IMediaControllerScope extends Common.IScope
    {

    }

    export class MediaController extends Common.Controller
    {
        scope: IMediaControllerScope;
        buildingService: BuildingService;
        deviceService: DeviceService;
        websocket: WebSocket;
        static $inject = ['$scope', 'buildingService', 'deviceService'];

        constructor($scope: IMediaControllerScope, buildingService: BuildingService, deviceService: DeviceService)
        {
            super($scope);
            this.buildingService = buildingService;
            this.deviceService = deviceService;
            this.websocket = new WebSocket("ws://192.168.3.2:8000");
            this.websocket.onclose = () => new WebSocket("ws://192.168.3.2:50000");
            

        }

        creatMsg(cmd: string): string
        {
            return JSON.stringify({ Request: cmd });
        }

        Left()
        {
            this.websocket.send(this.creatMsg("Left"));
        }
        Right()
        {
            this.websocket.send(this.creatMsg("Right"));
        }
        Up()
        {
            this.websocket.send(this.creatMsg("Up"));
        }
        Down()
        {
            this.websocket.send(this.creatMsg("Down"));
        }

        Play()
        {
            this.websocket.send(this.creatMsg("Enter"));
        }

        Back()
        {
            this.websocket.send(this.creatMsg("Back"));
        }
        Pause()
        {
            this.websocket.send(this.creatMsg("Pause"));
        }
        Stop()
        {
            this.websocket.send(this.creatMsg("Stop"));
        }

        VolumeUp()
        {
            this.websocket.send(this.creatMsg("VolumeUp"));
        }
        VolumeDown()
        {
            this.websocket.send(this.creatMsg("VolumeDown"));
        }
        Mute()
        {
            this.websocket.send(this.creatMsg("Mute"));
        }
        

    }
} 