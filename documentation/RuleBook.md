## RoboRally Rules

### Setup

1. First of all you have to host a match where your friends can join to.
2. Together you have to choose a course which you want to play with. This can even be a custom course which only the host has to have installed.
3. Now every player chooses a robot.
4. After that every player has to place its Robot on the map. The order is random.

### Play

The game is played in rounds. Each round is made out of 5 phases:

1. The upgrade phase:
   - During the upgrade phase each player can buy upgrade cards for the energy cubes he has.
2. The programming phase:
   - Each player gets an amount of programming cards. 
   - Every player has to place the maximum of the programming cards he is able to.
3. The power-down-announce phase:
   - Each player can now tell that they want to power down after this round
4. The register phase:
   - Now the robots gets moved like their programming cards tell them too in five register phases. 
     - The robots get moved once after another so the priority beacon tells them in which order they have to move.
     - When every robot has moved the board elements gets triggered.
5. End of turn effects:
   - There are map fields which only do something after the 5th register phase *More here*

### Phases

#### Upgrade Phase

At the start of every round is the upgrade phase.

##### Cards

There are two card types:

- Permanent upgrade
- Temporary upgrade

Permanent cards gives the robot a buff and you can have a maximum amount of three.
Temporary cards can always be played and you can have a maximum amount of three.

##### Refill

If there are still all upgrade cards left in shop they will all get removed from the match. Otherwise, so many cards will be added to the card shop so that there are as many as players again.

##### Purchase

In this phase everyone can buy **one** upgrade card. Again the sequence for purchasing is specified by the priority beacon. These cards get bought with the energy cubes. The price of each upgrade card is written on the card. If you want to buy a card but you have to many of these type already you are able to put one away.

#### Programming phase

1. First of all every player gets 9 programming cards (or less depending on the missing health). 
2. Every player has a short amount of time to look at the cards.
3. Now every has to place 5 (or less depending on the missing health) in the order they should get used in the register phase. 
4. When the first player is ready all other players have 30 seconds left to place their cards. 
5. If a register is empty, it will get filled with a random card from the players hand.

#### Power-down-announce Phase

In this phase every player has the possibility to announce that they want to power down their robot after that round.

#### Register Phase

In the register phase the robots as well as the map starts moving. 

1. First of all the first card of each player will get activated. All players will move in the order the priority beacon tell them too.
   - Robots can interact with other robots
2. When every robot has completed the move from the current register the board elements are getting triggered.
3. All robots which are now still able to use their laser will do so and due this may damage other robots.
4. Now everything is repeated until all 5 registers have been completed.

#### End of turn effects

Once all five register phases have been completed the end of turn effects will get activated.

- <u>RADIATION:</u> A robot on a radiation space takes 1 point of damage. 
- <u>REPAIR SITES:</u> A robot on a repair site repairs damage points depending on how strong the repair field is.
- <u>CHECKPOINTS:</u> A robot on a flag repairs 1 point of damage. 
- <u>WIPE REGISTERS:</u> Discard all Program cards from registers that aren’t locked. 
- <u>POWER DOWN:</u> Players whose robots were powered down this turn will be powered again if they had not been destroyed or got powered down again.
- <u>RETURN ROBOTS TO PLAY:</u> All robots that were destroyed in this round re-enter the game at the entry point on the tile on which they last lived. They will look in the direction the player wants to and get 2 damage tokens. If a robot would re-enter on the same space as another robot, they are in Virtual Mode. 

### Checkpoints

If a robot is at a checkpoint at the end of a register phase, it receives it and can make its way to the next one. Obviously, the checkpoints must be completed in the correct order, from the one with the lowest number to the one with the highest.

### Damage and destruction

When a player’s robot is damaged they get a damage token which means that they lose one health. A robot has a maximum of 10 health-points.

<u>LOSS OF PROGRAM CARDS:</u>  Because of getting hit by lasers, getting stomped or damaged due other things the robots memory save gets affected so that thy get one fewer Program card for each damage token they have. 

<u>LOCKED REGISTERS:</u> If a robot has 5 or more damage tokens, its registers begin to lock up starting with register 5 and working down to register 1. Once a register is locked, the Program card in that register cannot be discarded. This means that from now on this programming card is stuck in the register and is executed every round. This can only be removed when the register is no longer locked.

<u>DESTRUCTION:</u> A robot is destroyed when it got the 10th damage token which means it has 0 health points left. A robot can also be instantly destroyed by certain board elements or by moving off the edge of the board. There may be option cards which can prevent you from getting destroyed. The robot will re-enter play at the end of the turn. 

| Health points | Effect                                              |
| :-----------: | --------------------------------------------------- |
|       9       | Dealt 8 Program cards                               |
|       8       | Dealt 7 Program cards                               |
|       7       | Dealt 6 Program cards                               |
|       6       | Dealt 5 Program cards                               |
|       5       | Dealt 4 Program cards, lock register 5              |
|       4       | Dealt 3 Program cards, lock registers 5 and 4       |
|       3       | Dealt 2 Program cards, lock registers 5, 4 and 3    |
|       2       | Dealt 1 Program cards, lock registers 5, 4, 3 and 2 |
|       1       | Dealt 0 Program cards, lock all registers           |
|       0       | Destruction                                         |

### 