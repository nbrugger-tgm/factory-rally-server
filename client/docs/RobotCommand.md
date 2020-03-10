# Tgm.Roborally.Api.Model.RobotCommand
A command for a robot to execute
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Type** | **Instruction** |  | 
**Parameters** | [**List&lt;Pair&gt;**](Pair.md) | Defines parameters for the instruction.&lt;br&gt;Example: Effect: \&quot;Move {steps} steps forward\&quot;&lt;br&gt; &#x60;{steps}&#x60; is the number of steps the robot will do. And the exact value (of steps) will be defined in here (&#x60;values&#x60;) | [optional] 
**Description** | **string** | A description about the effect of the command. Variables are using the format &#x60;{name}&#x60; where *name* refers to the names in &#x60;values&#x60;.  | [optional] [default to "null"]
**Name** | **string** | The ame to display for this Command. ***Not*** unique (identifying) | [optional] 
**Times** | **int** | Describes how often this command is going to be executed | [optional] [default to 1]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

