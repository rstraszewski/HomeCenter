 
 
 

 


declare module HomeCenter {
interface IDeviceRelationDescriptionModel {
  Id: number;
  Description: string;
}
interface IStateRelationModel {
  StateRelation: HomeCenter.StateRelation;
  StateRelationDescription: string;
}
interface IUsernameValidationModel {
  Message: string;
  IsCorrect: boolean;
}
interface IBuildingInput {
  Name: string;
}
interface IBuilding {
  Id: number;
  Name: string;
  Floors: HomeCenter.IFloor[];
}
interface IFloor {
  Name: string;
  Id: number;
  Rooms: HomeCenter.IRoom[];
}
interface IRoom {
  Name: string;
  Id: number;
}
interface IDeviceInput {
  DeviceTypeId: number;
  RoomId: number;
  BuildingId: number;
  FloorId: number;
  GroupId: number;
  Address: number;
  Name: string;
}
interface IDevice {
  Id: number;
  DeviceState: number;
  RoomId: number;
  GroupId: number;
  Description: string;
  Changeable: string;
  GroupName: string;
  MaxValue: number;
  MinValue: number;
  Binary: boolean;
  DeviceName: string;
}
interface IDeviceJob {
  Id: number;
  GroupId: number;
  State: number;
  IsCron: boolean;
  DateTime: Date;
  CronExpression: string;
}
interface IDeviceJobInput {
  GroupId: number;
  State: number;
  IsCron: boolean;
  DateTime: number;
  CronExpression: string;
}
interface IDeviceRelationInput {
  GroupActionId: number;
  GroupReactionId: number;
  IfStateValue: number;
  ThenStateValue: number;
  StateRelation: HomeCenter.StateRelation;
}
interface IDeviceTypeInput {
}
interface IDeviceType {
  Type: string;
  Id: number;
  Description: string;
  IsVirtual: boolean;
}
interface IFloorInput {
  Name: string;
}
interface IGroupInput {
  Number: number;
  Name: string;
  Description: string;
}
interface IGroup {
  Id: number;
  Number: number;
  Name: string;
  Description: string;
}
interface IRoomInput {
  Name: string;
}
interface IDeviceRelation {
  Id: number;
  GroupActionId: number;
  GroupReactionId: number;
  IfStateValue: number;
  ThenStateValue: number;
  StateRelation: HomeCenter.StateRelation;
  StateRelationDescription: string;
}
interface IHistoryLog {
  Username: string;
  DateTime: string;
  Description: string;
  Id: number;
}
}


