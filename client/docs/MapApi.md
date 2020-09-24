# Tgm.Roborally.Api.Api.MapApi

All URIs are relative to *http://game.host/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetGameMap**](MapApi.md#getgamemap) | **GET** /games/{game_id}/map | Get Map
[**GetMapInfo**](MapApi.md#getmapinfo) | **GET** /games/{game_id}/map/info | Get Map info
[**GetTile**](MapApi.md#gettile) | **GET** /games/{game_id}/map/tiles/{x}/{y} | Get tile



## GetGameMap

> Map GetGameMap (string gameId)

Get Map

Returns the map of this specific game including the tiles (data)

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class GetGameMapExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://game.host/v1";
            // Configure API key authorization: player-auth
            Configuration.Default.AddApiKey("pat", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("pat", "Bearer");

            var apiInstance = new MapApi(Configuration.Default);
            var gameId = gameId_example;  // string | 

            try
            {
                // Get Map
                Map result = apiInstance.GetGameMap(gameId);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling MapApi.GetGameMap: " + e.Message );
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
 **gameId** | **string**|  | 

### Return type

[**Map**](Map.md)

### Authorization

[player-auth](../README.md#player-auth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **404** | Not Found |  -  |
| **500** | Internal Server Error |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetMapInfo

> MapInfo GetMapInfo (int gameId)

Get Map info

Get meta information abouzt the map of the game

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class GetMapInfoExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://game.host/v1";
            // Configure API key authorization: player-auth
            Configuration.Default.AddApiKey("pat", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("pat", "Bearer");

            var apiInstance = new MapApi(Configuration.Default);
            var gameId = 56;  // int | 

            try
            {
                // Get Map info
                MapInfo result = apiInstance.GetMapInfo(gameId);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling MapApi.GetMapInfo: " + e.Message );
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

[**MapInfo**](MapInfo.md)

### Authorization

[player-auth](../README.md#player-auth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetTile

> Tile GetTile (int gameId, string x, string y)

Get tile

Inspect a tile of the map

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class GetTileExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://game.host/v1";
            // Configure API key authorization: player-auth
            Configuration.Default.AddApiKey("pat", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("pat", "Bearer");

            var apiInstance = new MapApi(Configuration.Default);
            var gameId = 56;  // int | 
            var x = x_example;  // string | 
            var y = y_example;  // string | 

            try
            {
                // Get tile
                Tile result = apiInstance.GetTile(gameId, x, y);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling MapApi.GetTile: " + e.Message );
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
 **x** | **string**|  | 
 **y** | **string**|  | 

### Return type

[**Tile**](Tile.md)

### Authorization

[player-auth](../README.md#player-auth)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

