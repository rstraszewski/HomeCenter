///<reference path="../../App/Shared/Service.ts" />
///<reference path="../../Scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../Scripts/TypeLite.Net4.d.ts" />
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var HomeCenter;
(function (HomeCenter) {
    var AccountService = (function (_super) {
        __extends(AccountService, _super);
        function AccountService(httpRequest) {
            _super.call(this, httpRequest);
        }
        AccountService.prototype.getUsernameValidation = function (username) {
            return this.HttpRequest.post("/Account/IsUsernameValid", { username: username });
        };

        AccountService.prototype.getRoles = function () {
            return this.HttpRequest.get("/Account/GetRoles");
        };

        AccountService.prototype.getRolesForUser = function (username) {
            return this.HttpRequest.post("/Account/GetRolesForUser", { username: username });
        };

        AccountService.prototype.getRoomAccessForUser = function (username) {
            return this.HttpRequest.post("/Account/GetRoomAccessForUser", { username: username });
        };

        AccountService.prototype.getUsernames = function () {
            return this.HttpRequest.get("/Account/GetAllUsernames");
        };

        AccountService.prototype.changeRoles = function (choosedUsername, rolesForChoosedUser, roomAccessForChoosedUser) {
            return this.HttpRequest.post("/Account/ChangeRoles", {
                username: choosedUsername,
                roles: rolesForChoosedUser,
                roomAccess: roomAccessForChoosedUser
            });
        };
        AccountService.$inject = ['$http'];
        return AccountService;
    })(Common.Service);
    HomeCenter.AccountService = AccountService;
})(HomeCenter || (HomeCenter = {}));
//# sourceMappingURL=AccountService.js.map
