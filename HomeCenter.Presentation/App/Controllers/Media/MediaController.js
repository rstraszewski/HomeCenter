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
    var MediaController = (function (_super) {
        __extends(MediaController, _super);
        function MediaController($scope, buildingService, deviceService) {
            _super.call(this, $scope);
            this.buildingService = buildingService;
            this.deviceService = deviceService;
            this.websocket = new WebSocket("ws://192.168.3.2:8000");
            this.websocket.onclose = function () {
                return new WebSocket("ws://192.168.3.2:50000");
            };
        }
        MediaController.prototype.creatMsg = function (cmd) {
            return JSON.stringify({ Request: cmd });
        };

        MediaController.prototype.Left = function () {
            this.websocket.send(this.creatMsg("Left"));
        };
        MediaController.prototype.Right = function () {
            this.websocket.send(this.creatMsg("Right"));
        };
        MediaController.prototype.Up = function () {
            this.websocket.send(this.creatMsg("Up"));
        };
        MediaController.prototype.Down = function () {
            this.websocket.send(this.creatMsg("Down"));
        };

        MediaController.prototype.Play = function () {
            this.websocket.send(this.creatMsg("Enter"));
        };

        MediaController.prototype.Back = function () {
            this.websocket.send(this.creatMsg("Back"));
        };
        MediaController.prototype.Pause = function () {
            this.websocket.send(this.creatMsg("Pause"));
        };
        MediaController.prototype.Stop = function () {
            this.websocket.send(this.creatMsg("Stop"));
        };

        MediaController.prototype.VolumeUp = function () {
            this.websocket.send(this.creatMsg("VolumeUp"));
        };
        MediaController.prototype.VolumeDown = function () {
            this.websocket.send(this.creatMsg("VolumeDown"));
        };
        MediaController.prototype.Mute = function () {
            this.websocket.send(this.creatMsg("Mute"));
        };
        MediaController.$inject = ['$scope', 'buildingService', 'deviceService'];
        return MediaController;
    })(Common.Controller);
    HomeCenter.MediaController = MediaController;
})(HomeCenter || (HomeCenter = {}));
//# sourceMappingURL=MediaController.js.map
