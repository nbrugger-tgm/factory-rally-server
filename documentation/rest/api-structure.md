---
tags: [REST, API, Structure]
---

# API Structure

The sructure of the API follows the "rules" of REST. Additionaly it follows a File Tree Design.
## Rest
Many Web APIs call themself REST(ful) APIs. But not many of them meet the rules, we do.
#### Whats Rest
Rest is kind of a design pattern for Web APIsTo say it simple in rest everything is a resource. So you do ***not*** have actions you can call from the api. So `PUT /client/{email}/send-message` is not a REST Api Call because you do not treat `/send-message` as a resource.
> A resource only allowes *CRUD* Operations (Create Read Update Delete) and in the most cases it is a list or an object

So the "RESTfull" Way to send a message would be something like `POST /client/{email}/chat/messages`. Because in this case you treat `messages` as a list and you Post a new message into it (Update)

> ***Sidenote*** : 
it is not allways neccesarry to make a web api Restfull. In many cases it is even a bad idea to do so

## File Tree
Our API can be used as a File Tree (File System) from outside. This makes it easy to use and more familiar to people who dont work quite often with Web APIs.

The only path wich does *not* behaves like a file or folder is `/games/{game_id}/lazy_updated`. But this action is not neccesary to use.

This makes it easier to interact with this API as you know file system and you know how a FS behaves and what you can do with it

> Keep in mind that many files are `read-only`! 
### What are files and folders
#### By Documentation
In the documentation you can see the diference; when there is a `/` at the end of the path it is not a file.

OR

Is there a *sub path*? IF yes its a Folder.<br>
Eg: `/upgrades` is a folder because there also is a path `/upgrades/shop`

if this trailing '/' is missing or there is no subpath in your documentation you can use the 2 other ways to get this information


#### By Usage
When you do a request on a path you can tell wether it is a file or folder most of the time. And even if you can`t say it clear by the response, logic will tell you.

Generaly it could be said that if an object is returned it is a file. if it is a list it is very likely to be a folder
#### By Logic
If you get a list of primitive types you can ensure it is a Folder or not by going throught the folllowing steps
* Does it makes sense that there are more than just references/ids/primitives
  * "yes" : Is there a place i can get the object by the id
    * "yes" : **FILE**
    * "no" : **FOLDER**
  * "no" : **FILE**
  * "i dont know" :  Does it makes sense to put other variables or values into here?
    * "yes" : **FILE**
    * "no" : **FOLDER**
### Structure
**Syntax** : `name` type | description of content | `read/write` permission

We added the `add` and `remove` permission for folders. `write` on folders means `add/remove`
* `/` Folder | Root
  * `games/` Folder | Contains all running or open games | `read-only`
    * `{game-id}/` Folder | Contains all files and folders for a game
      * `status` File | Contains the current state of the game | `read-only`
      * `actions` File | Contains all actions of this game | `read/write`
      * `players/` Folder | Contains a file for each player | `read/write`
        * `{player-id}` File | Contains player information | `read`
      * `upgrades/` Folder | Contains the upgrade shop and all upgrades | `read`
        * `shop` File | Contains information about the shop | `read/write`
        * `{upgrade-id}` Folder | Contains the data about the upgrade | `read`
      
