///<reference path="../../../Scripts/typings/requirejs/require.d.ts" />
///<reference path="../../../Scripts/typings/angularjs/ng-table.d.ts" />
///<reference path="../../../Scripts/typings/angular-notify/angular-notify.d.ts" />
///<reference path="../../../Scripts/Enums.ts" /> 
///<reference path="../../../App/Shared/Common.ts" />
///<reference path="../../Services/DeviceService.ts" />


module HomeCenter {
    export interface IDeviceRelationsController extends Common.IScope {
        deviceRelations: IDeviceRelationDescriptionModel[];
        groups: IGroup[];
        choosedActionGroup: IGroup;
        choosedReactionGroup: IGroup;
        stateRelations: IStateRelationModel[];
        choosedStateRelation: IStateRelationModel;
        ifStateValue: number;
        thenStateValue: number;

        choosedDateForJob: Date;
        choosedTimeForJob: Date;
        choosedGroupForJob: IGroup;
        choosedStateForJob: number;
        isCron: boolean;
        cronExpression: string;
        jobs: IDeviceJob[];
        calendarOpen: boolean;
    }



    export class DeviceRelationsController extends Common.Controller {
        scope: IDeviceRelationsController;
        deviceService: DeviceService;
        notify: ng.cgNotify.INotifyService;
        static $inject = ['$scope', 'deviceService', 'notify'];

        constructor($scope: IDeviceRelationsController, deviceService: DeviceService, notify:ng.cgNotify.INotifyService) {
            super($scope);
            this.deviceService = deviceService;
            this.scope.isCron = false;
            this.refreshDeviceRelations();
            this.scope.choosedTimeForJob = new Date();
            this.scope.choosedDateForJob = new Date();
            this.notify = notify;
            this.deviceService.getStateRelations().success(result => {
                this.scope.stateRelations = result;
                this.scope.stateRelations.unshift({ StateRelation: null, StateRelationDescription: "" });
                this.scope.choosedStateRelation = this.scope.stateRelations[0];
            });
            this.deviceService.getGroups().success(result => {
                this.scope.groups = result;
                this.scope.groups.unshift({ Id: null, Number: null, Name: "", Description: "" });
                this.scope.choosedActionGroup = this.scope.groups[0];
                this.scope.choosedReactionGroup = this.scope.groups[0];
                this.scope.choosedGroupForJob = this.scope.groups[0];
            });
            this.deviceService.getAllJobs().success(result => {
                this.scope.jobs = result;
            });

        }

        addJob() {
            this.scope.cronExpression = $("#cron").val();
            var job: IDeviceJobInput = {
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

            if(job.IsCron&&job.CronExpression!='')
                this.deviceService.addJob(job).success(() => {
                    this.notify("Job added successfully.");
                    this.deviceService.getAllJobs().success(result => {
                        this.scope.jobs = result;
                    });
                });
            else {
                this.notify({
                    message: "You need to specify a cron expression!",
                    classes: "alert-error"
                });
            }
        }

        canCreate() {

            if (this.scope.groups &&
                this.scope.stateRelations &&
                this.scope.choosedActionGroup != this.scope.groups[0] &&
                this.scope.choosedReactionGroup != this.scope.groups[0] &&
                this.scope.ifStateValue != null &&
                this.scope.thenStateValue != null &&
                this.scope.choosedStateRelation != this.scope.stateRelations[0]) {
                return true;
            }
            return false;
        }

        canAddJob() {
            this.scope.cronExpression = $("#cron").val();
            if (this.scope.groups && (this.scope.choosedGroupForJob != this.scope.groups[0] &&
                this.scope.choosedStateForJob &&
                ((this.scope.isCron) ||
                ((!this.scope.isCron && this.scope.choosedTimeForJob &&
                this.scope.choosedDateForJob))))) {
                return true;
            }
            return false;
        }

        calendarOpen($event) {
            $event.preventDefault();
            $event.stopPropagation();
            this.scope.calendarOpen = true;
        }

        createRelation() {
            var relation: IDeviceRelationInput = {
                StateRelation: this.scope.choosedStateRelation.StateRelation,
                GroupActionId: this.scope.choosedActionGroup.Id,
                GroupReactionId: this.scope.choosedReactionGroup.Id,
                IfStateValue: this.scope.ifStateValue,
                ThenStateValue: this.scope.thenStateValue
            };
            this.deviceService.createRelation(relation).success(() => {
                this.notify("Relation created successfully.");
               
                this.refreshDeviceRelations();
            });
        }

        onIsCronChange() {
            if (this.scope.isCron) {
                $('#timepicker').attr("disabled");
                //$('#timepicker').find('*').attr("disabled");
            }
        }

        refreshDeviceRelations() {
            this.deviceService.getDeviceRelations().success(result => {
                this.scope.deviceRelations = result;
            });
        }

        removeRelation(id: number) {
            this.deviceService.removeDeviceRelations(id).success(() => {
                this.notify("Relation removed successfully.");
                this.refreshDeviceRelations();
            });

        }


        removeJob(id: number) {
            this.deviceService.removeJob(id).success(() => {
                this.notify("Job removed successfully.");
                this.deviceService.getAllJobs().success(result => {
                    this.scope.jobs = result;
                });
            });

        }




    }
} 