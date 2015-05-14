///<reference path="../../../Scripts/typings/requirejs/require.d.ts" />
///<reference path="../../../Scripts/typings/angularjs/angular.d.ts" />
///<reference path="../../../Scripts/typings/angular-ui/angular-ui-router.d.ts" />
///<reference path="../../../Scripts/typings/angular-ui-bootstrap/angular-ui-bootstrap.d.ts" />
///<reference path="../../../Scripts/TypeLite.Net4.d.ts" />
///<reference path="../../Shared/Common.ts" />
///<reference path="../../Shared/Service.ts" />
///<reference path="../../Services/AccountService.ts" />
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var HomeCenter;
(function (HomeCenter) {
    var CreateAccountController = (function (_super) {
        __extends(CreateAccountController, _super);
        function CreateAccountController($scope, accountService, buildingService) {
            var _this = this;
            _super.call(this, $scope);
            this.accountService = accountService;
            this.scope.usernameValidation = true;
            this.buildingService = buildingService;
            this.buildingService.getAllBuildings().success(function (buildings) {
                _this.scope.buildings = buildings;
            });
            this.accountService.getRoles().success(function (roles) {
                _this.scope.userRoles = roles;
            });
            this.accountService.getUsernames().success(function (usernames) {
                _this.scope.usernames = usernames;
                _this.scope.usernames.unshift("");
                _this.scope.choosedUsername = _this.scope.usernames[0];
            });
        }
        CreateAccountController.prototype.usernameChange = function () {
            var _this = this;
            this.accountService.getUsernameValidation(this.scope.username).success(function (result) {
                _this.scope.usernameInfo = result.Message;
                _this.scope.usernameValidation = result.IsCorrect;
            });
        };
        CreateAccountController.prototype.userValidation = function () {
            return !this.scope.usernameValidation || !this.scope.username || this.scope.username.length == 0;
        };

        CreateAccountController.prototype.chooseUser = function () {
            var _this = this;
            if (this.scope.choosedUsername && this.scope.choosedUsername != "") {
                this.accountService.getRolesForUser(this.scope.choosedUsername).success(function (userRoles) {
                    _this.scope.rolesForChoosedUser = userRoles;
                });
                this.accountService.getRoomAccessForUser(this.scope.choosedUsername).success(function (userRoomAccess) {
                    _this.scope.roomsForChoosedUser = userRoomAccess;
                });
            } else {
                this.scope.rolesForChoosedUser = [];
            }
        };
        CreateAccountController.prototype.isChecked = function (role) {
            if ($.inArray(role, this.scope.rolesForChoosedUser) == -1)
                return false;
            return true;
        };

        CreateAccountController.prototype.isCheckboxDisabled = function () {
            if (!this.scope.choosedUsername || this.scope.choosedUsername == '')
                return true;
            return false;
        };

        CreateAccountController.prototype.isCheckedRoom = function (room) {
            if ($.inArray(room.Id.toString(), this.scope.roomsForChoosedUser) == -1)
                return false;
            return true;
        };

        CreateAccountController.prototype.isCheckboxDisabledRoom = function () {
            if (($.inArray('User', this.scope.rolesForChoosedUser) > -1) && (($.inArray('Administrator', this.scope.rolesForChoosedUser) == -1)))
                return false;
            return true;
        };

        CreateAccountController.prototype.changeRoles = function () {
            this.accountService.changeRoles(this.scope.choosedUsername, this.scope.rolesForChoosedUser, this.scope.roomsForChoosedUser);
        };

        CreateAccountController.prototype.checkboxChange = function (role, $event) {
            var checkbox = $event.target;
            if (checkbox.checked) {
                this.scope.rolesForChoosedUser.push(role);
            } else {
                this.scope.rolesForChoosedUser.splice(this.scope.rolesForChoosedUser.indexOf(role), 1);
            }
        };

        CreateAccountController.prototype.checkboxChangeRoom = function (room, $event) {
            var checkbox = $event.target;
            if (checkbox.checked) {
                this.scope.roomsForChoosedUser.push(room.Id.toString());
            } else {
                this.scope.roomsForChoosedUser.splice(this.scope.roomsForChoosedUser.indexOf(room.Id.toString()), 1);
            }
        };

        CreateAccountController.prototype.saveRoleValidation = function () {
            if (this.scope.choosedUsername && this.scope.choosedUsername != "")
                return false;
            return true;
        };
        CreateAccountController.$inject = ['$scope', 'accountService', 'buildingService'];
        return CreateAccountController;
    })(Common.Controller);
    HomeCenter.CreateAccountController = CreateAccountController;
})(HomeCenter || (HomeCenter = {}));
//# sourceMappingURL=CreateAccountController.js.map
