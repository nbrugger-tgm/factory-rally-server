# Tgm.Roborally.Api.Model.Game
A Game is like a lobby, people can join/leave.<br> A Game is created by a host who does *not* needs to attend the game as a player but in the most cases he will. This is *read-only*
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **int** | **Unique**&lt;br&gt; This is the parameter a game is identified by | [optional] 
**Name** | **string** | The name is **unique** but it should ***not*** be used as identifer (it&#39;s not natively supportet) It is used to display the game&#39;s name | 
**Players** | **List&lt;string&gt;** | The list of players attending the game. (Only contains the name of the players) | [optional] 
**RuntimeInfo** | **GameState** |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

