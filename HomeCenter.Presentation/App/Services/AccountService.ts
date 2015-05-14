///<reference path="../../App/Shared/Service.ts" />
///<reference path="../../Scripts/typings/angularjs/angular.d.ts" /> 
///<reference path="../../Scripts/TypeLite.Net4.d.ts" /> 

module HomeCenter {
    export class AccountService extends Common.Service {
        static $inject = ['$http'];

        constructor(httpRequest: ng.IHttpService) {
            super(httpRequest);
        }

        getUsernameValidation(username: string): ng.IHttpPromise<IUsernameValidationModel> {
            return this.HttpRequest.post("/Account/IsUsernameValid", { username: username });
        }

        getRoles(): ng.IHttpPromise<string[]> {
            return this.HttpRequest.get("/Account/GetRoles");
        }

        getRolesForUser(username:string): ng.IHttpPromise<string[]> {
            return this.HttpRequest.post("/Account/GetRolesForUser", { username: username });
        }

        getRoomAccessForUser(username: string): ng.IHttpPromise<string[]> {
            return this.HttpRequest.post("/Account/GetRoomAccessForUser", { username: username });
        }

        getUsernames(): ng.IHttpPromise<string[]> {
            return this.HttpRequest.get("/Account/GetAllUsernames");
        }

        changeRoles(choosedUsername: string, rolesForChoosedUser: string[], roomAccessForChoosedUser: string[]): ng.IHttpPromise<any> {
            return this.HttpRequest.post("/Account/ChangeRoles", {
                username: choosedUsername,
                roles: rolesForChoosedUser,
                roomAccess: roomAccessForChoosedUser
            });
        }
    }
} 