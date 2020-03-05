## EventTypes

In the game-logic are multiple EventTypes. These should give the program which visualizes the game the ability to react on these events and give a User-output. There are following EventTypes.

### Move event

Should get called whenever a player moves.
Infos:

- Who moves
- Where is he right now
- Where does he move

### Push event

Should get caller whenever a player gets pushed
Infos:

- Who pushes
- Who gets pushed
- How many fields
- Direction of push

### Map event

Should get called whenever the map does something
Infos:

- What happens (Rotator, Stomper, Pusher, Laser, hole, conveyor belt,.. )

### Damage event

Should get called whenever a player takes damage

- Who took dmg
- How much dmg did he take

### Shoot event

Should get called when a player shoots

- Who shoot (Maybe Data pos of shootstart and direction)

### Shutdown event

Should get called when a player starts rebooting.

* the robot

### Reboot event

Should get called when a player is rebooted

* the robot

### Buy upgrade event

when a player obtains an upgrade

* Who got the upgrade
* Upgrade Card

### Fill register

When a player fills a robots register

* Player who filles the register
* Affected Robot
* Register number

### Timer start event

When a player is done filling all registers

* Player who was the fastest
* Time left

### Timer end event

When the timer runs out

### Complete Checkpoint

When a player successfull reached a checkpoint

* Player
* Robot

### Scan Event

When the Prio beacon is scanning

* Who got detected as the nearest robot