///<reference path="../../../Scripts/typings/requirejs/require.d.ts" />
///<reference path="../../../Scripts/typings/angularjs/angular.d.ts" /> 
///<reference path="../../../Scripts/typings/angular-ui/angular-ui-router.d.ts" /> 
///<reference path="../../../Scripts/typings/angular-ui-bootstrap/angular-ui-bootstrap.d.ts" /> 
///<reference path="../../../Scripts/TypeLite.Net4.d.ts" /> 
///<reference path="../../Shared/Common.ts" /> 
///<reference path="../../Shared/Service.ts" /> 
///<reference path="../../Services/AccountService.ts" /> 

module HomeCenter {
    export interface ICreateAccountScope extends Common.IScope {
        usernameValidation: boolean;
        usernameInfo: string;
        username: string;
        message: string;
        userRoles: string[];
        usernames: string[];
        choosedUsername: string;
        rolesForChoosedUser: string[];
        roomsForChoosedUser: string[];
        buildings:IBuilding[];
    }

    export class CreateAccountController extends Common.Controller {
        scope: ICreateAccountScope;
        accountService: AccountService;
        buildingService: BuildingService;
        roles: any;
        static $inject = ['$scope', 'accountService', 'buildingService'];
        
        constructor($scope: ICreateAccountScope, accountService: AccountService, buildingService: BuildingService ) {
            super($scope);
            this.accountService = accountService;
            this.scope.usernameValidation = true;
            this.buildingService = buildingService;
            this.buildingService.getAllBuildings().success(buildings => {
                this.scope.buildings = buildings;
            });
            this.accountService.getRoles().success((roles: string[]) => {
                this.scope.userRoles = roles;
            });
            this.accountService.getUsernames().success((usernames: string[]) => {
                this.scope.usernames = usernames;
                this.scope.usernames.unshift("");
                this.scope.choosedUsername = this.scope.usernames[0];
            });



        }

        usernameChange(): void {
            this.accountService.getUsernameValidation(this.scope.username).success((result: IUsernameValidationModel) => {
                this.scope.usernameInfo = result.Message;
                this.scope.usernameValidation = result.IsCorrect;
            });
        }
        userValidation() {
            return !this.scope.usernameValidation || !this.scope.username || this.scope.username.length == 0;

        }


        chooseUser(): void {
            if (this.scope.choosedUsername && this.scope.choosedUsername != "") {
                this.accountService.getRolesForUser(this.scope.choosedUsername).success((userRoles: string[]) => {
                    this.scope.rolesForChoosedUser = userRoles;
                });
                this.accountService.getRoomAccessForUser(this.scope.choosedUsername).success((userRoomAccess: string[]) => {
                    this.scope.roomsForChoosedUser = userRoomAccess;
                });
            } else {
                this.scope.rolesForChoosedUser = [];
            }
        }
        isChecked(role: string): boolean {
            if ($.inArray(role, this.scope.rolesForChoosedUser)==-1)
                return false;
            return true;
        }

        isCheckboxDisabled() {
            if (!this.scope.choosedUsername || this.scope.choosedUsername == '')
                return true;
            return false;
        }

        isCheckedRoom(room: IRoom): boolean {
            if ($.inArray(room.Id.toString(), this.scope.roomsForChoosedUser) == -1)
                return false;
            return true;
        }

        isCheckboxDisabledRoom() {
                
            if (($.inArray('User', this.scope.rolesForChoosedUser) > -1) && (($.inArray('Administrator', this.scope.rolesForChoosedUser) == -1)))
                return false;
            return true;
        }

        changeRoles() {
            this.accountService.changeRoles(this.scope.choosedUsername, this.scope.rolesForChoosedUser, this.scope.roomsForChoosedUser);
        }

        checkboxChange(role: string, $event: Event) {
            var checkbox = <HTMLInputElement>$event.target;
            if (checkbox.checked) {
                this.scope.rolesForChoosedUser.push(role);
            } else {
                this.scope.rolesForChoosedUser.splice(this.scope.rolesForChoosedUser.indexOf(role), 1);
            }
        }

        checkboxChangeRoom(room: IRoom, $event: Event) {
            var checkbox = <HTMLInputElement>$event.target;
            if (checkbox.checked) {
                this.scope.roomsForChoosedUser.push(room.Id.toString());
            } else {
                this.scope.roomsForChoosedUser.splice(this.scope.roomsForChoosedUser.indexOf(room.Id.toString()), 1);
            }
        }

        saveRoleValidation() {
            if (this.scope.choosedUsername && this.scope.choosedUsername != "")
                return false;
            return true;
        }
    }
}