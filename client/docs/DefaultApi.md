# Tgm.Roborally.Api.Api.DefaultApi

All URIs are relative to *http://localhost:5050/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ChooseRobot**](DefaultApi.md#chooserobot) | **PATCH** /games/{game_id}/players/{player_id} | Set Robots
[**GetRobotStats**](DefaultApi.md#getrobotstats) | **GET** /games/{game_id}/entitys/robots/{robot_id}/info | Get Robot Informations


<a name="chooserobot"></a>
# **ChooseRobot**
> void ChooseRobot (int gameId, int playerId, List<Robots> robots = null)

Set Robots

Stes the type of robot(s) the player is controlling

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
            config.BasePath = "http://localhost:5050/v1";
            // Configure API key authorization: Player-Token-Access
            config.AddApiKey("uid", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("uid", "Bearer");

            var apiInstance = new DefaultApi(config);
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
                Debug.Print("Exception when calling DefaultApi.ChooseRobot: " + e.Message );
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

[Player-Token-Access](../README.md#Player-Token-Access)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getrobotstats"></a>
# **GetRobotStats**
> RobotInfo GetRobotStats (int gameId, string robotId)

Get Robot Informations

Returns the status and info about the robot

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class GetRobotStatsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost:5050/v1";
            // Configure API key authorization: Player-Token-Access
            config.AddApiKey("uid", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("uid", "Bearer");

            var apiInstance = new DefaultApi(config);
            var gameId = 56;  // int | 
            var robotId = robotId_example;  // string | 

            try
            {
                // Get Robot Informations
                RobotInfo result = apiInstance.GetRobotStats(gameId, robotId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DefaultApi.GetRobotStats: " + e.Message );
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
 **robotId** | **string**|  | 

### Return type

[**RobotInfo**](RobotInfo.md)

### Authorization

[Player-Token-Access](../README.md#Player-Token-Access)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

