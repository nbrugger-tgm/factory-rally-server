# Tgm.Roborally.Api.Model.RobotInfoAllOf
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**EnergyCubes** | **int** | The number of avainable energy cubes | [optional] [default to 3]
**Health** | **int** | The remaining health points | [optional] [default to 10]
**Active** | **bool** | True if the robot is not in rebooting mode | [optional] [default to true]
**Virtual** | **bool** | If the robot is in virtual mode | [optional] [default to false]
**Priority** | **int** | The priority of this player. Higher is more priority. 1 &#x3D; lowest. max &#x3D; number of players | [optional] 
**OnTurn** | **bool** | True if the robot is currently active (executing a register) | [optional] 
**IsMine** | **bool** | True if you are the one controlling the robot | [optional] 
**HandCards** | **int** | The cards in the hand of the robot | [optional] 
**Attitude** | **int** | The height level of the robot | [optional] [default to 0]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

