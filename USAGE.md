## Using the ReST API

The complete logic is accessible via a HTTP Rest API. The core features are:

* Map repository to save maps
* Host for games

The complete documentation of the API is done in OAS<sup>[[1]](https://www.openapis.org)</sup> and found in `oas/game-engine.<version>.json`. There is a exported pdf called `documentation.pdf`. You can also use the HTML (requires internet) `rapidoc.html` version which looks better and is interactive. You also can create an up to date PDF by using `rapidoc-pdf.html` (also requires active internet connection)

## Explanation

### Events/Actions

Each event and action can only happen/execute at specific times (phases of the game)

#### Events

Events are things a client can react to. They are used to inform clients about changes. They are organized in Queues, where every player has his own queue and he can pop events from this queues and react to them.

The regarding issue can be found [here](https://github.com/FactoryRally/game-controller/issues/6).

#### Actions

Actions are the things players/clients can do with the game /interactions. An example for this would be laying down cards at programming phase

#### Graph

![A graph of all phases and their corresponding actions/events](D:\Users\Nils\Desktop\Schule\ITP\robot-rally\game-controller\documentation\game-cycle-events.png)

> The `UpgradeUsedEvent` is not entered in this graph to reduce retundancy but it can allways occur when `UseUpgrade`Action can be executed

#### Usage

[here](documentation/api-usage.md#Events & Updates)