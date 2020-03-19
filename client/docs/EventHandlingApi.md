# Tgm.Roborally.Api.Api.EventHandlingApi

All URIs are relative to *http://game.host/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**FetchNextDamageEvent**](EventHandlingApi.md#fetchnextdamageevent) | **GET** /games/{game_id}/events/damage | Get next / last damage event
[**FetchNextLazerHitEvent**](EventHandlingApi.md#fetchnextlazerhitevent) | **GET** /games/{game_id}/events/lazer-hit | Get next / last Lazer hit event
[**FetchNextMapEvent**](EventHandlingApi.md#fetchnextmapevent) | **GET** /games/{game_id}/events/map | Get next / last map event
[**FetchNextMovementEvent**](EventHandlingApi.md#fetchnextmovementevent) | **GET** /games/{game_id}/events/movement | Get next / last movement event
[**FetchNextPushEvent**](EventHandlingApi.md#fetchnextpushevent) | **GET** /games/{game_id}/events/push | Get next / last push event
[**FetchNextShootEvent**](EventHandlingApi.md#fetchnextshootevent) | **GET** /games/{game_id}/events/shoot | Get next / last shoot event
[**FetchNextShutdownEvent**](EventHandlingApi.md#fetchnextshutdownevent) | **GET** /games/{game_id}/events/shutdown | Get next / last shutdown event
[**TraceEvent**](EventHandlingApi.md#traceevent) | **GET** /games/{game_id}/events/type | trace event


<a name="fetchnextdamageevent"></a>
# **FetchNextDamageEvent**
> DamageEvent FetchNextDamageEvent (int gameId)

Get next / last damage event

Returns the next unfetched event of the damage type.  If the event is not of the damage type you will get a `400` status and the event stays unfetched

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class FetchNextDamageEventExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://game.host/v1";
            // Configure API key authorization: Player-Access-Token
            config.AddApiKey("pat", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("pat", "Bearer");

            var apiInstance = new EventHandlingApi(config);
            var gameId = 56;  // int | 

            try
            {
                // Get next / last damage event
                DamageEvent result = apiInstance.FetchNextDamageEvent(gameId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EventHandlingApi.FetchNextDamageEvent: " + e.Message );
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

[**DamageEvent**](DamageEvent.md)

### Authorization

[Player-Access-Token](../README.md#Player-Access-Token)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **404** | No unfetched event |  -  |
| **417** | The next event is not a movement event |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="fetchnextlazerhitevent"></a>
# **FetchNextLazerHitEvent**
> LazerHitEvent FetchNextLazerHitEvent (int gameId)

Get next / last Lazer hit event

Returns the next unfetched event of the lazer hit type.  If the event is not of the lazer hit type you will get a `400` status and the event stays unfetched

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class FetchNextLazerHitEventExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://game.host/v1";
            // Configure API key authorization: Player-Access-Token
            config.AddApiKey("pat", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("pat", "Bearer");

            var apiInstance = new EventHandlingApi(config);
            var gameId = 56;  // int | 

            try
            {
                // Get next / last Lazer hit event
                LazerHitEvent result = apiInstance.FetchNextLazerHitEvent(gameId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EventHandlingApi.FetchNextLazerHitEvent: " + e.Message );
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

[**LazerHitEvent**](LazerHitEvent.md)

### Authorization

[Player-Access-Token](../README.md#Player-Access-Token)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **404** | No unfetched event |  -  |
| **417** | The next event is not a movement event |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="fetchnextmapevent"></a>
# **FetchNextMapEvent**
> MapEvent FetchNextMapEvent (int gameId)

Get next / last map event

Returns the next unfetched event of the  Map Event type. Map Events activeata all active components of a type at once  If the event is not of the map event type you will get a `400` status and the event stays unfetched

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class FetchNextMapEventExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://game.host/v1";
            // Configure API key authorization: Player-Access-Token
            config.AddApiKey("pat", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("pat", "Bearer");

            var apiInstance = new EventHandlingApi(config);
            var gameId = 56;  // int | 

            try
            {
                // Get next / last map event
                MapEvent result = apiInstance.FetchNextMapEvent(gameId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EventHandlingApi.FetchNextMapEvent: " + e.Message );
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

[**MapEvent**](MapEvent.md)

### Authorization

[Player-Access-Token](../README.md#Player-Access-Token)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **404** | No unfetched event |  -  |
| **417** | The next event is not a movement event |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="fetchnextmovementevent"></a>
# **FetchNextMovementEvent**
> MovementEvent FetchNextMovementEvent (int gameId)

Get next / last movement event

Returns the next unfetched event of the movement type.  If the event is not of the movement type you will get a `400` status and the event stays unfetched

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class FetchNextMovementEventExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://game.host/v1";
            // Configure API key authorization: Player-Access-Token
            config.AddApiKey("pat", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("pat", "Bearer");

            var apiInstance = new EventHandlingApi(config);
            var gameId = 56;  // int | 

            try
            {
                // Get next / last movement event
                MovementEvent result = apiInstance.FetchNextMovementEvent(gameId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EventHandlingApi.FetchNextMovementEvent: " + e.Message );
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

[**MovementEvent**](MovementEvent.md)

### Authorization

[Player-Access-Token](../README.md#Player-Access-Token)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **404** | No unfetched event |  -  |
| **417** | The next event is not a movement event |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="fetchnextpushevent"></a>
# **FetchNextPushEvent**
> PushEvent FetchNextPushEvent (int gameId)

Get next / last push event

Returns the next unfetched event of the push type.  If the event is not of the push  type you will get a `400` status and the event stays unfetched

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class FetchNextPushEventExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://game.host/v1";
            // Configure API key authorization: Player-Access-Token
            config.AddApiKey("pat", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("pat", "Bearer");

            var apiInstance = new EventHandlingApi(config);
            var gameId = 56;  // int | 

            try
            {
                // Get next / last push event
                PushEvent result = apiInstance.FetchNextPushEvent(gameId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EventHandlingApi.FetchNextPushEvent: " + e.Message );
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

[**PushEvent**](PushEvent.md)

### Authorization

[Player-Access-Token](../README.md#Player-Access-Token)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **404** | No unfetched event |  -  |
| **417** | The next event is not a movement event |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="fetchnextshootevent"></a>
# **FetchNextShootEvent**
> ShootEvent FetchNextShootEvent (int gameId)

Get next / last shoot event

Returns the next unfetched event of the movement type.  If the event is not of the movement type you will get a `400` status and the event stays unfetched

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class FetchNextShootEventExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://game.host/v1";
            // Configure API key authorization: Player-Access-Token
            config.AddApiKey("pat", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("pat", "Bearer");

            var apiInstance = new EventHandlingApi(config);
            var gameId = 56;  // int | 

            try
            {
                // Get next / last shoot event
                ShootEvent result = apiInstance.FetchNextShootEvent(gameId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EventHandlingApi.FetchNextShootEvent: " + e.Message );
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

[**ShootEvent**](ShootEvent.md)

### Authorization

[Player-Access-Token](../README.md#Player-Access-Token)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **404** | No unfetched event |  -  |
| **417** | The next event is not a movement event |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="fetchnextshutdownevent"></a>
# **FetchNextShutdownEvent**
> ShutdownEvent FetchNextShutdownEvent (int gameId)

Get next / last shutdown event

Returns the next unfetched event of the movement type.  If the event is not of the movement type you will get a `400` status and the event stays unfetched

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class FetchNextShutdownEventExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://game.host/v1";
            // Configure API key authorization: Player-Access-Token
            config.AddApiKey("pat", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("pat", "Bearer");

            var apiInstance = new EventHandlingApi(config);
            var gameId = 56;  // int | 

            try
            {
                // Get next / last shutdown event
                ShutdownEvent result = apiInstance.FetchNextShutdownEvent(gameId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EventHandlingApi.FetchNextShutdownEvent: " + e.Message );
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

[**ShutdownEvent**](ShutdownEvent.md)

### Authorization

[Player-Access-Token](../README.md#Player-Access-Token)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **404** | No unfetched event |  -  |
| **417** | The next event is not a movement event |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="traceevent"></a>
# **TraceEvent**
> InlineResponse200 TraceEvent (int gameId, bool? batch = null, bool? wait = null)

trace event

All events needed by the client are accessible here. (Usefull for animations) More about this function is found in the [regarding Github Issue](https://github.com/FactoryRally/game-controller/issues/6)  **This function only returns the type of the event you need to fetch the data seperately** > Read more at [api-usage.md](https://github.com/FactoryRally/game-controller/blob/master/documentation/rest/api-usage.md#events- -updates)

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Tgm.Roborally.Api.Api;
using Tgm.Roborally.Api.Client;
using Tgm.Roborally.Api.Model;

namespace Example
{
    public class TraceEventExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://game.host/v1";
            // Configure API key authorization: Player-Access-Token
            config.AddApiKey("pat", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("pat", "Bearer");

            var apiInstance = new EventHandlingApi(config);
            var gameId = 56;  // int | 
            var batch = true;  // bool? | If true you will get all past events at once (optional)  (default to false)
            var wait = true;  // bool? | If true the server will not responde until a event is added to the queue  Rrequires less traffic but might impacts the servers performance or cause timeouts at the client (optional)  (default to false)

            try
            {
                // trace event
                InlineResponse200 result = apiInstance.TraceEvent(gameId, batch, wait);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EventHandlingApi.TraceEvent: " + e.Message );
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
 **batch** | **bool?**| If true you will get all past events at once | [optional] [default to false]
 **wait** | **bool?**| If true the server will not responde until a event is added to the queue  Rrequires less traffic but might impacts the servers performance or cause timeouts at the client | [optional] [default to false]

### Return type

[**InlineResponse200**](InlineResponse200.md)

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

