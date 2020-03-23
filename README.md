# Game Controller
The Game Controller is an Web API controlling the logic/flow of one (or multiple) game sessions

## OAS -TODO

Fehlercodes :

- [x] Game Not Found 404
- [x] Game Not Joinable 409 Conflict
- [ ] Player not Removable 400 Invalid Request
- [x] Cannot Start game 30x ~ Wrong state

Extend hardware compatibility conditions 

- [x] Player ID not negative
- [x] Get Actions executed/pending options
- [x] Add Player "active" attribute
- [x] Add `ActionType` `Start Game`
- [x] Mark `PATCH /games/{game_id}/players/{player_id}` as deprecated
- [ ] Make `GET /games/{game_id}/actions` return whole actions not only types

Events

- [x] GameActionEvent
- [x] RobotPickEvent

## Structure

The implementation of the different phases is found in `Tgm.Roborally.Server.Engine.Phases` and the game cycle is found in `Tgm.Roborally.Server.Engine.GameThread`.