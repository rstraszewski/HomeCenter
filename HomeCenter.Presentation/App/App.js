///<reference path="../App/Shared/Service.ts" />
///<reference path="../App/Controllers/Home/HomeController.ts" />
///<reference path="../App/Controllers/AddDevice/CreateDeviceController.ts" />
///<reference path="../App/Controllers/DeviceControl/DeviceControlController.ts" />
///<reference path="../App/Controllers/CreateBuilding/CreateBuildingController.ts" />
///<reference path="../App/Controllers/Account/CreateAccountController.ts" />
///<reference path="../App/Controllers/DeviceRelations/DeviceRelationsController.ts" />
///<reference path="../App/Controllers/Media/MediaController.ts" />
///<reference path="../App/Services/MainService.ts" />
///<reference path="../App/Services/DeviceService.ts" />
///<reference path="../App/Services/BuildingService.ts" />
///<reference path="../App/Services/AccountService.ts" />
///<reference path="../App/Shared/Directives.ts" />
var HomeCenter;
(function (HomeCenter) {
    var myApp = new Common.Module('myTutorialApp', ['ui.bootstrap', 'ui.utils', 'cgBusy', 'ngTable', 'cgNotify']);
    myApp.value('cgBusyDefaults', {
        delay: 500
    });
    myApp.addController('mainController', HomeCenter.MainController);
    myApp.addController('createAccountController', HomeCenter.CreateAccountController);
    myApp.addController('deviceControlController', HomeCenter.DeviceControlController);
    myApp.addController('createBuildingController', HomeCenter.CreateBuildingController);
    myApp.addController('createBuildingPopupController', HomeCenter.CreateBuildingPopupController);
    myApp.addController('addDeviceController', HomeCenter.CreateDeviceController);
    myApp.addController('createFloorPopupController', HomeCenter.CreateFloorPopupController);
    myApp.addController('createRoomPopupController', HomeCenter.CreateRoomPopupController);
    myApp.addController('deviceRelationsController', HomeCenter.DeviceRelationsController);
    myApp.addController('mediaController', HomeCenter.MediaController);

    myApp.addService('buildingService', HomeCenter.BuildingService);
    myApp.addService('mainService', HomeCenter.MainService);
    myApp.addService('deviceService', HomeCenter.DeviceService);
    myApp.addService('accountService', HomeCenter.AccountService);

    myApp.addDirective('ngSlider', HomeCenter.Slider);
    myApp.addDirective('ngSelect', HomeCenter.Select);
    myApp.addDirective('ngCron', HomeCenter.Cron);
})(HomeCenter || (HomeCenter = {}));
//# sourceMappingURL=App.js.map
