///<reference path="../../../Scripts/typings/requirejs/require.d.ts" />
///<reference path="../../../Scripts/typings/angularjs/ng-table.d.ts" />
///<reference path="../../../Scripts/typings/angular-notify/angular-notify.d.ts" />
///<reference path="../../../Scripts/Enums.ts" />
///<reference path="../../../App/Shared/Common.ts" />
///<reference path="../../Services/DeviceService.ts" />
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var HomeCenter;
(function (HomeCenter) {
    var DeviceRelationsController = (function (_super) {
        __extends(DeviceRelationsController, _super);
        function DeviceRelationsController($scope, deviceService, notify) {
            var _this = this;
            _super.call(this, $scope);
            this.deviceService = deviceService;
            this.scope.isCron = false;
            this.refreshDeviceRelations();
            this.scope.choosedTimeForJob = new Date();
            this.scope.choosedDateForJob = new Date();
            this.notify = notify;
            this.deviceService.getStateRelations().success(function (result) {
                _this.scope.stateRelations = result;
                _this.scope.stateRelations.unshift({ StateRelation: null, StateRelationDescription: "" });
                _this.scope.choosedStateRelation = _this.scope.stateRelations[0];
            });
            this.deviceService.getGroups().success(function (result) {
                _this.scope.groups = result;
                _this.scope.groups.unshift({ Id: null, Number: null, Name: "", Description: "" });
                _this.scope.choosedActionGroup = _this.scope.groups[0];
                _this.scope.choosedReactionGroup = _this.scope.groups[0];
                _this.scope.choosedGroupForJob = _this.scope.groups[0];
            });
            this.deviceService.getAllJobs().success(function (result) {
                _this.scope.jobs = result;
            });
        }
        DeviceRelationsController.prototype.addJob = function () {
            var _this = this;
            this.scope.cronExpression = $("#cron").val();
            var job = {
                GroupId: this.scope.choosedGroupForJob.Id,
                IsCron: this.scope.isCron,
                State: this.scope.choosedStateForJob,
                DateTime: null,
                CronExpression: null
            };
            if (job.IsCron == false) {
                //this.scope.$apply();
                this.scope.choosedDateForJob.setMinutes(0);
                this.scope.choosedDateForJob.setHours(0);
                job.DateTime = this.scope.choosedDateForJob.getTime();
                var hoursInMilisecond = this.scope.choosedTimeForJob.getHours() * 3600000;
                var minutesInMilisecond = this.scope.choosedTimeForJob.getMinutes() * 3600000 / 60;
                job.DateTime += hoursInMilisecond;
                job.DateTime += minutesInMilisecond;
            } else {
                job.CronExpression = this.scope.cronExpression;
            }

            if (job.IsCron && job.CronExpression != '')
                this.deviceService.addJob(job).success(function () {
                    _this.notify("Job added successfully.");
                    _this.deviceService.getAllJobs().success(function (result) {
                        _this.scope.jobs = result;
                    });
                });
            else {
                this.notify({
                    message: "You need to specify a cron expression!",
                    classes: "alert-error"
                });
            }
        };

        DeviceRelationsController.prototype.canCreate = function () {
            if (this.scope.groups && this.scope.stateRelations && this.scope.choosedActionGroup != this.scope.groups[0] && this.scope.choosedReactionGroup != this.scope.groups[0] && this.scope.ifStateValue != null && this.scope.thenStateValue != null && this.scope.choosedStateRelation != this.scope.stateRelations[0]) {
                return true;
            }
            return false;
        };

        DeviceRelationsController.prototype.canAddJob = function () {
            this.scope.cronExpression = $("#cron").val();
            if (this.scope.groups && (this.scope.choosedGroupForJob != this.scope.groups[0] && this.scope.choosedStateForJob && ((this.scope.isCron) || ((!this.scope.isCron && this.scope.choosedTimeForJob && this.scope.choosedDateForJob))))) {
                return true;
            }
            return false;
        };

        DeviceRelationsController.prototype.calendarOpen = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            this.scope.calendarOpen = true;
        };

        DeviceRelationsController.prototype.createRelation = function () {
            var _this = this;
            var relation = {
                StateRelation: this.scope.choosedStateRelation.StateRelation,
                GroupActionId: this.scope.choosedActionGroup.Id,
                GroupReactionId: this.scope.choosedReactionGroup.Id,
                IfStateValue: this.scope.ifStateValue,
                ThenStateValue: this.scope.thenStateValue
            };
            this.deviceService.createRelation(relation).success(function () {
                _this.notify("Relation created successfully.");

                _this.refreshDeviceRelations();
            });
        };

        DeviceRelationsController.prototype.onIsCronChange = function () {
            if (this.scope.isCron) {
                $('#timepicker').attr("disabled");
                //$('#timepicker').find('*').attr("disabled");
            }
        };

        DeviceRelationsController.prototype.refreshDeviceRelations = function () {
            var _this = this;
            this.deviceService.getDeviceRelations().success(function (result) {
                _this.scope.deviceRelations = result;
            });
        };

        DeviceRelationsController.prototype.removeRelation = function (id) {
            var _this = this;
            this.deviceService.removeDeviceRelations(id).success(function () {
                _this.notify("Relation removed successfully.");
                _this.refreshDeviceRelations();
            });
        };

        DeviceRelationsController.prototype.removeJob = function (id) {
            var _this = this;
            this.deviceService.removeJob(id).success(function () {
                _this.notify("Job removed successfully.");
                _this.deviceService.getAllJobs().success(function (result) {
                    _this.scope.jobs = result;
                });
            });
        };
        DeviceRelationsController.$inject = ['$scope', 'deviceService', 'notify'];
        return DeviceRelationsController;
    })(Common.Controller);
    HomeCenter.DeviceRelationsController = DeviceRelationsController;
})(HomeCenter || (HomeCenter = {}));
//# sourceMappingURL=DeviceRelationsController.js.map
