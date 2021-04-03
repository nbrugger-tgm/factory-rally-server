# Installation and Usage

> While the application supports Linux, Windows and MAC. There are no installers for mac and linux so you have to configure this for yourself at the moment.<br>
> Dependencie : dotnetcore 3.1

1. Download the `robo-rally-server.zip` from the [release section](https://github.com/FactoryRally/game-controller/releases).

2. Enable autostart (optional)

  * Windows
    * `WIN`+`R`  -> `shell:autostart`+`ENTER`
    * Autostart
      1. Move the extracted content of the `zip` directly into the opened folder
  * Linux [\[WIP\]](https://github.com/FactoryRally/game-controller/issues/22)
3. Execute the server manually (optional)
    * Windows: extract the zip and double click the `exe` file
    * Linux/MacOs:
        * Download `dotnetcore 3.1`
        * `./Tgm.Roborally.Server`

  ### Config file [\[WIP\]](https://github.com/FactoryRally/game-controller/issues/23)

  The config file defines the behaviour of the server.

  Location: `<user home>/robo-rally-server/settings.cfg`

  The path when using **docker** is `/game/settings.cfg`

  The file will be created with default values once the server is started the first time

  #### Values

| Variable               | Description                                                  | type       | default                                                 |
| ---------------------- | ------------------------------------------------------------ | ---------- | ------------------------------------------------------- |
| *max-games*            | The number of games this server hosts (0 for unlimited; only allowed with `only one lobby`) | number     | 1                                                       |
| *max-players*          | The maximum number of players per game                       | number     | 4                                                       |
| *player-names-visible* | If true players can see each the name of each other          | boolean    | true                                                    |
| *names*                | A list of names for the games                                | Text Array | ["Game 1",<br />"Game 2",<br />"Game 3",<br />"Game 4"] |
| *password*             | The password needed to play (*null* for no password)         | text       | *null*                                                  |
| *fill-with-bots*       | If true open places will be filled with bots                 | boolean    | false                                                   |
| *open-map-repo*        | if true players can commit maps                              | boolean    | false                                                   |
| *map-repo-location*    | The location where the maps are located (without / in front relative to exeuteable) | path       | maps                                                    |
| only-one-lobby         | If this is set to true, only one joinable game at a time will exist.<br />This is useful for servers with many `max-games` | boolean    |                                                         |