## v1.0.0 (2021-02-05)

### Refactor

- reference new client
- fix typo in AI name
- **oas**: replace inline definition with ref
- **oas**: change path of **BuyUpgrade**
- **oas**: rename ErrorMessage

### Fix

- **autogen**: Make TimeElapsed event an event
- **autogen**: replace bad admin access
- add missing `time elapsed` event type
- missing import
- adapt to new events

### Perf

- add end of game clean

### Feat

- implement end of game
- **full** upgrade shop phase implementation
- implement `getAvainableActions`
- add entity actions
- add `TimeElapsedEvent`
- add option to pass (do nothing)
- **oas**: add resusable path parameters
- refuse bad startgame
- commit buy upgrade events
- add empty ki
- add `name`, `max-players` and  `current-players` to game info
- add missing event data classes
- **events**: remove typesave endpoints

### BREAKING CHANGE

- #9

## v0.9.0 (2021-02-04)

### Fix

- bad enum references
- **autogen**: adapt ActionType according to documentation
- pat datatype

### Feat

- dissable admin authorisation for easy testing

## v0.8.2 (2021-02-03)

### Fix

- remove wrong authentication tag

## v0.8.1 (2021-02-03)

### Refactor

- remove bad testcase
- add client as submodule
- remove client code from this repo

## v0.8.0 (2021-02-02)

### Refactor

- **autogen**: replace authorisation attributes
- sync display oas

### Feat

- improve startup message
- add version to main file
- add authoriation check to all endpoints
- add programming card endpoints
- add robot and register backend
- implement `getRobots` endpoint
- implement robot register endpoints

### Fix

- bad regex

## v0.7.0 (2021-02-01)

### Feat

- implement upgrade endpoints
- add upgrade phase
- add name to game status response
- add robot ownership check

## v0.6.0 (2021-02-01)

### Feat

- add upgrade implementation

## v0.5.2 (2021-02-01)

### Fix

- empty event response

## v0.5.1 (2021-02-01)

### Fix

- consumer not passing GRP

## v0.5.0 (2021-01-31)

### Feat

- implement upgrades
- adding Upgrade manager

### Fix

- **autogen**: bad parameter mapping

## v0.4.3 (2021-01-31)

### Fix

- bad parameter mapping

## v0.4.2 (2021-01-31)

### Fix

- bad regex preventing consumer registration

## v0.4.1 (2021-01-30)

### Fix

- nullpointer for any event

## v0.4.0 (2021-01-30)

### Feat

- consumer events
- consumer pat authorisation
- consumer handling
- add consumer registration to OAS

### Fix

- player id not zero
- add response to `register consumer`
- insecure type conversion at authentication

## v0.3.0 (2021-01-26)

### Feat

- started games now "run" endlessly without exception

### Fix

- lock in (pick robot) exception
- **autogen**: empty(null) controlled entities list
- change != to ==

### Refactor

- stop forwarding GamePhaseChanged to phases

## v0.2.2 (2021-01-26)

### Fix

- empty entity manager

## v0.2.1 (2021-01-26)

### Fix

- **cz**: broken version
- notification of general events
- action phase enum conversion

### Feat

- exceptionless general phase notification
- add info to BadEventException

## v0.2.1b2 (2021-01-26)

### Fix

- NullPrt on empty password (on join)

## v0.1.2b1 (2021-01-26)

### Fix

- `GamePhaseChange` causing crash

### Feat

- disable `hardware-attached`
- enable bad event and action notifications
- implement `/v1/games/{game_id}/map`

### Refactor

- **autogen**: make EntityUseUpgradeAction an event
- adapt version in OAS

## v0.2.1b0 (2021-01-22)

### Fix

- pop -> peek
- add missing name mapping
- not returning game id after creation
- add missing auth tag
- authentication crash with invald game id
- NullPrt on event
- **autogen**: enable RobotPickEvent

### Feat

- implement robot picking/assignment

### Refactor

- change accessors
- **autogen**: replace wrongly generated attributes by extending

## v0.2.1a1 (2021-01-22)

### Fix

- fire event on join
- event-type fetching using pop instead of peek
- bad (inversed) typecheck

### Feat

- add thread event notification
- add missing events
- add player display name
- add missing event types

## v0.2.1a0 (2020-09-25)
