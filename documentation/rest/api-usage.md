### Events & Updates
Sometimes you would like to know what the Api is doing and what changed since the last fetch. This is called events.

Detching one or more updates includes three mayor steps:
- Waiting for the nexte event
- receiving the type of the event
- fetch the right event by its type

> Note that the code below is `pseudo code` in JS, means the names of the classes and functions are not the one from the api

```javascript
var wait = true; // false if you do not like to block the thread
var type = eventAPI.fetchNextEventType(game_id,wait);
if(type == EventType.MOVE_EVENT){//you could also use switch
     var moveEvent = eventAPI.fetchNextMovementEvent(game_id);
     ... 
}
```
This system is needed to provide type savety in languages such as Java and C#

if you do not like to wait you will need something like this
```javascript
var wait = false; // false if you do not like to block the thread
try{
    var type = eventAPI.fetchNextEventType(game_id,wait);
}catch(e){
    if(e.statusCode == 302){
        console.log("No Event avinable");
    }
}
if(type == EventType.MOVE_EVENT){//you could also use switch
     var moveEvent = eventAPI.fetchNextMovementEvent(game_id);
     ... 
}
```