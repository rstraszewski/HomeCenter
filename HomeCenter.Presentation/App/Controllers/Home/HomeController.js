var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
///<reference path="../../../Scripts/typings/requirejs/require.d.ts" />
///<reference path="../../../Scripts/typings/angularjs/ng-table.d.ts" />
///<reference path="../../../App/Shared/Common.ts" />
///<reference path="../../Services/MainService.ts" />
//app.controller("MainController", function ($scope) {
//    $scope.text = "asdasd";
//    var vm = this;s
//    vm.sth = "nana";
//});
//import Common = require("Shared/Common");
var HomeCenter;
(function (HomeCenter) {
    var MainController = (function (_super) {
        __extends(MainController, _super);
        function MainController($scope, mainService, ngTableParams, accountService) {
            var _this = this;
            _super.call(this, $scope);
            this.mainService = mainService;
            this.accountService = accountService;

            this.scope.promise = this.accountService.getUsernames().success(function (usernames) {
                _this.scope.usernames = usernames;
                _this.scope.usernames.unshift("");
                _this.scope.choosedUsername = _this.scope.usernames[0];
            });

            this.scope.promise = this.mainService.getLogsLength(this.scope.choosedUsername, this.scope.fromDateTime ? this.scope.fromDateTime.getTime() : null, this.scope.toDateTime ? this.scope.toDateTime.getTime() : null).success(function (total) {
                _this.scope.tableParams = new ngTableParams({
                    page: 1,
                    count: 10
                }, {
                    total: total,
                    getData: function ($defer, params) {
                        //alert(params.page() + " " + params.count());
                        _this.scope.promise = mainService.getSpecificLogs(params.page(), params.count(), _this.scope.choosedUsername, _this.scope.fromDateTime ? _this.scope.fromDateTime.getTime() : null, _this.scope.toDateTime ? _this.scope.toDateTime.getTime() : null).success(function (data) {
                            $defer.resolve(data);
                        });
                    }
                });
            });
        }
        MainController.prototype.filter = function () {
            this.reload();
        };

        MainController.prototype.reload = function () {
            var _this = this;
            this.scope.promise = this.mainService.getLogsLength(this.scope.choosedUsername, this.scope.fromDateTime ? this.scope.fromDateTime.getTime() : null, this.scope.toDateTime ? this.scope.toDateTime.getTime() : null).success(function (total) {
                _this.scope.tableParams.total(total);
                if (_this.scope.tableParams.page() == 1)
                    _this.scope.tableParams.reload();
                else {
                    _this.scope.tableParams.page(1);
                }
            });
        };

        MainController.prototype.toOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            this.scope.toOpened = true;
        };
        MainController.prototype.fromOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            this.scope.fromOpened = true;
        };
        MainController.$inject = ['$scope', 'mainService', 'ngTableParams', 'accountService'];
        return MainController;
    })(Common.Controller);
    HomeCenter.MainController = MainController;
})(HomeCenter || (HomeCenter = {}));
//# sourceMappingURL=HomeController.js.map
