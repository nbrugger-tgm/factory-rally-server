### Events & Updates
Sometimes you would like to know what the Api is doing and what changed since the last fetch. This is called events.

Detching one or more updates includes three mayor steps:
- Waiting for the nexte event
- receiving the type of the event
- fetch the right event by its type

> Note that the code below is `pseudo code` in JS, means the names of the classes and functions are not the one from the api

**Waiting for the nexte event**
```javascript
var wait = true; // false if you do not like to block the thread
var type = eventAPI.fetchNextEventType(game_id,wait);
if(type == EventType.MOVE_EVENT){//you could also use switch
     var moveEvent = eventAPI.fetchNextMovementEvent(game_id);
     ... 
}
```