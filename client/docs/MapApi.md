# Tgm.Roborally.Api.Api.MapApi

All URIs are relative to *http://localhost:5050/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetMapInfo**](MapApi.md#getmapinfo) | **GET** /games/{game_id}/map/info | Get Map info
[**GetTile**](MapApi.md#gettile) | **GET** /games/{game_id}/map/tiles/{x}/{y} | Get tile


<a name="getmapinfo"></a>
# **GetMapInfo**
> MapInfo GetMapInfo (string gameId)

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
            Configuration config = new Configuration();
            config.BasePath = "http://localhost:5050/v1";
            // Configure API key authorization: Player-Token-Access
            config.AddApiKey("uid", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("uid", "Bearer");

            var apiInstance = new MapApi(config);
            var gameId = gameId_example;  // string | 

            try
            {
                // Get Map info
                MapInfo result = apiInstance.GetMapInfo(gameId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
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
 **gameId** | **string**|  | 

### Return type

[**MapInfo**](MapInfo.md)

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

<a name="gettile"></a>
# **GetTile**
> Tile GetTile (string gameId, string x, string y)

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
            Configuration config = new Configuration();
            config.BasePath = "http://localhost:5050/v1";
            // Configure API key authorization: Player-Token-Access
            config.AddApiKey("uid", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("uid", "Bearer");

            var apiInstance = new MapApi(config);
            var gameId = gameId_example;  // string | 
            var x = x_example;  // string | 
            var y = y_example;  // string | 

            try
            {
                // Get tile
                Tile result = apiInstance.GetTile(gameId, x, y);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
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
 **gameId** | **string**|  | 
 **x** | **string**|  | 
 **y** | **string**|  | 

### Return type

[**Tile**](Tile.md)

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

