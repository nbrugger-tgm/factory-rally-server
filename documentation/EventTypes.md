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

### Map event

Should get called whenever the map does something
Infos:

- What happens (Rotator, Stomper, Pusher, Laser, hole, conveyor belt,.. )
- Infos about how it happens and what the result is

### Damage event

Should get called whenever a player takes damage

- Who took dmg
- Why did he took dmg
- How much dmg did he take

### Shoot event

Should get called when a player shoots

- Who shoot (Maybe Data pos of shootstart and direction)

### Hit event

Should get called when a player gets hit with a laser

- Who shoot (which player or map laser?)
- Who got hit

### Shutdown event

Should get called when a player starts rebooting. (Can get caused throught death or selfreboot)

### Reboot event

Should get called when a player is rebooted