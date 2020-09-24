
# Tgm.Roborally.Api.Model.GameInfo

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**PassedTime** | **int** | The time passed since the game started in secconds. If the game is not started it will be &#x60;0&#x60; | [default to -1]
**State** | **GameState** |  | 
**HardwareCompatible** | **bool** | Not every game can be connected to hardware (for example to many bots)  If this is true it means you can use this game with hardware | [default to false]
**HardwareAttached** | **bool** | Is a hardware boead connected | [default to false]
**PlayerOnTurn** | **int** | This id uniquely identifys the player (in a game).   **Not** to be confused with the &#x60;uid&#x60; which is used for authentication | 

[[Back to Model list]](../README.md#documentation-for-models)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to README]](../README.md)

