# Tgm.Roborally.Api.Api.MapRepoApi

All URIs are relative to *http://game.host/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**DeleteMap**](MapRepoApi.md#deletemap) | **DELETE** /maps/{map_name} | Delete Map
[**GetMap**](MapRepoApi.md#getmap) | **GET** /maps/{map_name} | Get map
[**GetMaps**](MapRepoApi.md#getmaps) | **GET** /maps/ | Get Map Names
[**SaveMap**](MapRepoApi.md#savemap) | **POST** /maps/ | Save Map



## DeleteMap

> void DeleteMap (string mapName)

Delete Map

Delete a map by its name

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class DeleteMapExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://game.host/v1";
            // Configure API key authorization: admin-access
            Configuration.Default.AddApiKey("skey", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("skey", "Bearer");

            var apiInstance = new MapRepoApi(Configuration.Default);
            var mapName = mapName_example;  // string | 

            try
            {
                // Delete Map
                apiInstance.DeleteMap(mapName);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling MapRepoApi.DeleteMap: " + e.Message );
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
 **mapName** | **string**|  | 

### Return type

void (empty response body)

### Authorization

[admin-access](../README.md#admin-access)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** | Deleted |  -  |
| **404** | Not Found |  -  |
| **500** | Internal Server Error |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## GetMap

> MapInfo GetMap (string mapName)

Get map

Get a map by its name

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class GetMapExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://game.host/v1";
            // Configure API key authorization: admin-access
            Configuration.Default.AddApiKey("skey", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("skey", "Bearer");

            var apiInstance = new MapRepoApi(Configuration.Default);
            var mapName = mapName_example;  // string | 

            try
            {
                // Get map
                MapInfo result = apiInstance.GetMap(mapName);
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling MapRepoApi.GetMap: " + e.Message );
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
 **mapName** | **string**|  | 

### Return type

[**MapInfo**](MapInfo.md)

### Authorization

[admin-access](../README.md#admin-access)

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


## GetMaps

> List&lt;string&gt; GetMaps ()

Get Map Names

Returns a list of all map names

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class GetMapsExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://game.host/v1";
            // Configure API key authorization: admin-access
            Configuration.Default.AddApiKey("skey", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("skey", "Bearer");

            var apiInstance = new MapRepoApi(Configuration.Default);

            try
            {
                // Get Map Names
                List<string> result = apiInstance.GetMaps();
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling MapRepoApi.GetMaps: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

This endpoint does not need any parameter.

### Return type

**List<string>**

### Authorization

[admin-access](../README.md#admin-access)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)


## SaveMap

> void SaveMap (Map map = null)

Save Map

Saves a map to the repository

### Example

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class SaveMapExample
    {
        public static void Main()
        {
            Configuration.Default.BasePath = "http://game.host/v1";
            // Configure API key authorization: admin-access
            Configuration.Default.AddApiKey("skey", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("skey", "Bearer");

            var apiInstance = new MapRepoApi(Configuration.Default);
            var map = new Map(); // Map | The map to save (optional) 

            try
            {
                // Save Map
                apiInstance.SaveMap(map);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling MapRepoApi.SaveMap: " + e.Message );
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
 **map** | [**Map**](Map.md)| The map to save | [optional] 

### Return type

void (empty response body)

### Authorization

[admin-access](../README.md#admin-access)

### HTTP request headers

- **Content-Type**: application/json
- **Accept**: Not defined

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#)
[[Back to API list]](../README.md#documentation-for-api-endpoints)
[[Back to Model list]](../README.md#documentation-for-models)
[[Back to README]](../README.md)

