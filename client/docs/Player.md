# Tgm.Roborally.Api.Model.Player
A player attending in a game. #### Warning This is **not** permanent. It is created and removed with the game (or with you joining and leaving the game)
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **int** | This id uniquely identifys the player (in a game).   **Not** to be confused with the &#x60;uid&#x60; which is used for authentication | 
**ControlledEntities** | **List&lt;int&gt;** | The list of entities controlled by this player | 
**OnTurn** | **bool** | Îf this is true rhe player is able to interact at the moment | [optional] [default to false]
**Active** | **bool** | Defines if the player is actively playing. If this is false the player does random moves. This is only false if the player disconnects | [optional] [default to true]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

