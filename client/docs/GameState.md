# Tgm.Roborally.Api.Model.GameState
The phase the game is currently in * `Lobby`: Players are able to join, Bots able to be added. Host can decide to start the game and leave the phase * `Planning`: **[BETA - Not in game]** Players can choose their type of robot/bots bots autopick * `Playing`: The game is running and the players can do interactions * `Break`: The game is paused by the host. Players can still do interactions but they wont be executed as long as the break lasts * `Finished` : The game is over and there is a winner. This is the time to save the game for a replay 
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

