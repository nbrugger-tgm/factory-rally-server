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
