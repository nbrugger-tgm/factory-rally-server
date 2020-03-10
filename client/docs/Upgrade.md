# Tgm.Roborally.Api.Model.Upgrade
A upgrade is a module making a robot stronger
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Cost** | **int** | The energy cost to buy this upgrade | [default to 2]
**Name** | **string** | The ame to display for this Upgrade. ***Not*** unique (identifying) | 
**Permanent** | **bool** | if true the card belongs to the player for the rest of the game | [optional] [default to true]
**Description** | **string** | A description about the effect of the card. Variables are using the format &#x60;{name}&#x60; where *name* refers to the names in &#x60;values&#x60;.  | [optional] [default to "null"]
**Rounds** | **int** | If the Upgrade is not permanent this variable defines the number of rounds this Upgrade works | [optional] 
**Values** | [**List&lt;Pair&gt;**](Pair.md) | Defines number values for the upgrade.&lt;br&gt;Example: Effect: \&quot;You have {registers} additonal Registers\&quot;&lt;br&gt; &#x60;{registers}&#x60; is the number of the regsiters (that will be added) and the exact value will be defined in here (&#x60;values&#x60;) | [optional] 
**Type** | **UpgradeType** |  | 
**Id** | **int** | The id of an upgrade. **Unique** | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

