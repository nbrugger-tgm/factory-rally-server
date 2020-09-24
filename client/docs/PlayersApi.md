# Tgm.Roborally.Api.Api.PlayersApi

All URIs are relative to *http://game.host/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ChooseRobot**](PlayersApi.md#chooserobot) | **PATCH** /games/{game_id}/players/{player_id} | Set Robots
[**GetAllPlayers**](PlayersApi.md#getallplayers) | **GET** /games/{game_id}/players/ | Get all players
[**GetPlayer**](PlayersApi.md#getplayer) | **GET** /games/{game_id}/players/{player_id} | Get player
[**Join**](PlayersApi.md#join) | **POST** /games/{game_id}/players/ | Join game
[**KickPlayer**](PlayersApi.md#kickplayer) | **DELETE** /games/{game_id}/players/{player_id} | Remove Player


<a name="chooserobot"></a>
# **ChooseRobot**
> void ChooseRobot (int gameId, int playerId, List<Robots> robots = null)

Set Robots

# DEPRECATET > This feature is useless in this version. It will be usefull in newer versions  Sets the type of robot(s) the player is controlling

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class ChooseRobotExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://game.host/v1";
            // Configure API key authorization: player-auth
            config.AddApiKey("pat", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("pat", "Bearer");

            var apiInstance = new PlayersApi(config);
            var gameId = 56;  // int | 
            var playerId = 56;  // int | 
            var robots = new List<Robots>(); // List<Robots> | The robots assigned to the player (optional) 

            try
            {
                // Set Robots
                apiInstance.ChooseRobot(gameId, playerId, robots);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PlayersApi.ChooseRobot: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **gameId** | **int**|  | 
 **playerId** | **int**|  | 
 **robots** | [**List&lt;Robots&gt;**](Robots.md)| The robots assigned to the player | [optional] 

### Return type

void (empty response body)

### Authorization

[player-auth](../README.md#player-auth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getallplayers"></a>
# **GetAllPlayers**
> List&lt;int&gt; GetAllPlayers (int gameId)

Get all players

Returns the index of all players

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class GetAllPlayersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://game.host/v1";
            var apiInstance = new PlayersApi(config);
            var gameId = 56;  // int | 

            try
            {
                // Get all players
                List<int> result = apiInstance.GetAllPlayers(gameId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PlayersApi.GetAllPlayers: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **gameId** | **int**|  | 

### Return type

**List<int>**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getplayer"></a>
# **GetPlayer**
> Player GetPlayer (int gameId, int playerId)

Get player

Get closer information about the player

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class GetPlayerExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://game.host/v1";
            var apiInstance = new PlayersApi(config);
            var gameId = 56;  // int | 
            var playerId = 56;  // int | 

            try
            {
                // Get player
                Player result = apiInstance.GetPlayer(gameId, playerId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PlayersApi.GetPlayer: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **gameId** | **int**|  | 
 **playerId** | **int**|  | 

### Return type

[**Player**](Player.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="join"></a>
# **Join**
> JoinResponse Join (int gameId, string password = null)

Join game

Join the given game. You will get your ID by doing this, if you already in the game you can get your ID again if you lost it.<br> The id is neccessary for any further API calls

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class JoinExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://game.host/v1";
            var apiInstance = new PlayersApi(config);
            var gameId = 56;  // int | 
            var password = password_example;  // string | The password of the game if the lobby is password protected (optional) 

            try
            {
                // Join game
                JoinResponse result = apiInstance.Join(gameId, password);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PlayersApi.Join: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **gameId** | **int**|  | 
 **password** | **string**| The password of the game if the lobby is password protected | [optional] 

### Return type

[**JoinResponse**](JoinResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Joined |  -  |
| **401** | Wrong/No password |  -  |
| **404** | Not Found |  -  |
| **409** | Not Joinable |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="kickplayer"></a>
# **KickPlayer**
> void KickPlayer (int gameId, int playerId)

Remove Player

Removes a player from the game. This can be done by the player itsself or by the host.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class KickPlayerExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://game.host/v1";
            // Configure API key authorization: admin-access
            config.AddApiKey("skey", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("skey", "Bearer");
            // Configure API key authorization: player-auth
            config.AddApiKey("pat", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("pat", "Bearer");

            var apiInstance = new PlayersApi(config);
            var gameId = 56;  // int | 
            var playerId = 56;  // int | 

            try
            {
                // Remove Player
                apiInstance.KickPlayer(gameId, playerId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PlayersApi.KickPlayer: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **gameId** | **int**|  | 
 **playerId** | **int**|  | 

### Return type

void (empty response body)

### Authorization

[admin-access](../README.md#admin-access), [player-auth](../README.md#player-auth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

