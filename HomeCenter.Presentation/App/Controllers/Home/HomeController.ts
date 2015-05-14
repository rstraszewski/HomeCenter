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
module HomeCenter {
    export interface IMainControllerScope extends Common.IScope {
        tableParams: ngTable.ngTableParams;
        usernames: string[];
        choosedUsername: string;
        toOpened: boolean;
        fromOpened: boolean;
        fromDateTime: Date;
        toDateTime: Date;
        promise: ng.IPromise<any>;
    }
    
    export class MainController extends Common.Controller {
        scope: IMainControllerScope;
        mainService: MainService;
        accountService: AccountService;
        static $inject = ['$scope', 'mainService', 'ngTableParams', 'accountService'];

        constructor($scope: IMainControllerScope, mainService: MainService, ngTableParams: any, accountService: AccountService) {
            super($scope);
            this.mainService = mainService;
            this.accountService = accountService;

            this.scope.promise = this.accountService.getUsernames().success((usernames: string[]) => {
                this.scope.usernames = usernames;
                this.scope.usernames.unshift("");
                this.scope.choosedUsername = this.scope.usernames[0];
            });

            this.scope.promise = this.mainService.getLogsLength(this.scope.choosedUsername,
                this.scope.fromDateTime ? this.scope.fromDateTime.getTime() : null,
                this.scope.toDateTime ? this.scope.toDateTime.getTime() : null).success(total => {
                this.scope.tableParams = new ngTableParams({
                    page: 1,            // show first page
                    count: 10           // count per page
                },
                    {
                        total: total, // length of data
                        getData: ($defer, params) => {
                            //alert(params.page() + " " + params.count());
                            this.scope.promise = mainService.getSpecificLogs(params.page(),
                                params.count(),
                                this.scope.choosedUsername,
                                this.scope.fromDateTime ? this.scope.fromDateTime.getTime() : null,
                                this.scope.toDateTime ? this.scope.toDateTime.getTime() : null).success(data => {
                                $defer.resolve(data);
                            });
                            
                        }
                    });
            });
            

        }
        filter() {
            this.reload();
        }

        reload() {
            this.scope.promise = this.mainService.getLogsLength(this.scope.choosedUsername,
                this.scope.fromDateTime ? this.scope.fromDateTime.getTime() : null,
                this.scope.toDateTime ? this.scope.toDateTime.getTime() : null).success(total => {
                    this.scope.tableParams.total(total);
                    if (this.scope.tableParams.page() == 1)
                        this.scope.tableParams.reload();
                    else {
                        this.scope.tableParams.page(1);
                    }
                });
            
        }

        toOpen($event) {
            $event.preventDefault();
            $event.stopPropagation();
            this.scope.toOpened = true;
        }
        fromOpen($event) {
            $event.preventDefault();
            $event.stopPropagation();
            this.scope.fromOpened = true;
        }



    }
}