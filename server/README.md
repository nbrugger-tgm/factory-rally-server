# Tgm.Roborally.Server - ASP.NET Core 3.0 Server

This api controlls the flow of a game and provides it's data.
It is desiged to be RESTfull so the structure works simmilar as file system.
The service will run and only work in a local network, `game.host` is the IP of the Computer hosting the game and will be found via a IP scan

## Run

Linux/OS X:

```
sh build.sh
```

Windows:

```
build.bat
```
## Run in Docker

```
cd src/Tgm.Roborally.Server
docker build -t tgm.roborally.server .
docker run -p 5000:8080 tgm.roborally.server
```
