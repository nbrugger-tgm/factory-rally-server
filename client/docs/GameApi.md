# Tgm.Roborally.Api.Api.GameApi

All URIs are relative to *http://game.host/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CommitAction**](GameApi.md#commitaction) | **PUT** /games/{game_id}/actions | Commit Action
[**CreateGame**](GameApi.md#creategame) | **POST** /games/ | Create Game
[**GetActions**](GameApi.md#getactions) | **GET** /games/{game_id}/actions | Get games actions
[**GetGameState**](GameApi.md#getgamestate) | **GET** /games/{game_id}/status | Get game status
[**GetGames**](GameApi.md#getgames) | **GET** /games/ | Get all games


<a name="commitaction"></a>
# **CommitAction**
> void CommitAction (int gameId, ActionType action)

Commit Action

Queues an action to be executed

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class CommitActionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://game.host/v1";
            // Configure API key authorization: Host-token-access
            config.AddApiKey("hid", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("hid", "Bearer");

            var apiInstance = new GameApi(config);
            var gameId = 56;  // int | 
            var action = new ActionType(); // ActionType | 

            try
            {
                // Commit Action
                apiInstance.CommitAction(gameId, action);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GameApi.CommitAction: " + e.Message );
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
 **action** | [**ActionType**](ActionType.md)|  | 

### Return type

void (empty response body)

### Authorization

[Host-token-access](../README.md#Host-token-access)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="creategame"></a>
# **CreateGame**
> void CreateGame (GameRules gameRules = null)

Create Game

Creates a random game by your defined rules

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class CreateGameExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://game.host/v1";
            // Configure API key authorization: Host-token-access
            config.AddApiKey("hid", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("hid", "Bearer");

            var apiInstance = new GameApi(config);
            var gameRules = new GameRules(); // GameRules | *Optional* This rules define how your game will behave (optional) 

            try
            {
                // Create Game
                apiInstance.CreateGame(gameRules);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GameApi.CreateGame: " + e.Message );
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
 **gameRules** | [**GameRules**](GameRules.md)| *Optional* This rules define how your game will behave | [optional] 

### Return type

void (empty response body)

### Authorization

[Host-token-access](../README.md#Host-token-access)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getactions"></a>
# **GetActions**
> List&lt;Action&gt; GetActions (int gameId)

Get games actions

Get all (**not robot related**) actions comitted to this game.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class GetActionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://game.host/v1";
            // Configure API key authorization: Host-token-access
            config.AddApiKey("hid", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("hid", "Bearer");

            var apiInstance = new GameApi(config);
            var gameId = 56;  // int | 

            try
            {
                // Get games actions
                List<Action> result = apiInstance.GetActions(gameId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GameApi.GetActions: " + e.Message );
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

[**List&lt;Action&gt;**](Action.md)

### Authorization

[Host-token-access](../README.md#Host-token-access)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getgamestate"></a>
# **GetGameState**
> GameInfo GetGameState (int gameId)

Get game status

Returns the status of a game

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class GetGameStateExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://game.host/v1";
            // Configure API key authorization: Player-Access-Token
            config.AddApiKey("pat", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("pat", "Bearer");

            var apiInstance = new GameApi(config);
            var gameId = 56;  // int | 

            try
            {
                // Get game status
                GameInfo result = apiInstance.GetGameState(gameId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GameApi.GetGameState: " + e.Message );
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

[**GameInfo**](GameInfo.md)

### Authorization

[Player-Access-Token](../README.md#Player-Access-Token)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getgames"></a>
# **GetGames**
> List&lt;int&gt; GetGames (bool? joinable = null, bool? unprotected = null)

Get all games

Returns a list of all hosted games

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class GetGamesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://game.host/v1";
            var apiInstance = new GameApi(config);
            var joinable = true;  // bool? | true: only return joinable games (optional)  (default to false)
            var unprotected = true;  // bool? | true: only display games with no password set (optional)  (default to false)

            try
            {
                // Get all games
                List<int> result = apiInstance.GetGames(joinable, unprotected);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GameApi.GetGames: " + e.Message );
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
 **joinable** | **bool?**| true: only return joinable games | [optional] [default to false]
 **unprotected** | **bool?**| true: only display games with no password set | [optional] [default to false]

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

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

