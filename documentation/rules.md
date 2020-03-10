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
   - Every player has to place the maximum of the programming cards he is able to. (all players at the same time)
3. The power-down-announce phase:
   - Each player can now tell that they want to power down after this round
4. The register phase:
   - Now the robots gets moved like their programming cards tell them too in five register phases. 
     - The robots get moved once after another so the priority beacon tells them in which order they have to move.
     - When every robot has moved the board elements are triggered.
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
Temporary cards can be used at special situations (or always depending on the upgrade) and you can have a maximum amount of three.

##### Refill

If there are still all upgrade cards left in shop they will all be removed from the match. Otherwise, so many cards will be added to the card shop so that there are as many as players again.

##### Purchase

In this phase everyone can buy **one** upgrade card. Again the sequence for purchasing is specified by the priority beacon. These cards get bought with the energy cubes. The price of each upgrade card is written on the card. If you want to buy a card but you have to many of these type already you are able to exchange a old card with a new one.

#### Programming Phase

1. First of all every player gets 9 programming cards (or less depending on the missing health more about this later on). 
2. Every player has a short amount of time to look at the cards.
3. Now every has to place 5 (or less depending on the missing health) in the order they should get used in the register phase. 
4. When the first player is ready all other players have 30 seconds left to place their cards. 
5. If registers are empty after the timer ran out, they will be filled with random cards from the players hand.

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

- <u>Board elements:</u> Board elements that only trigger at the end of the register phase will now be activated
- <u>Wipe registers:</u> Discard all Program cards from registers that aren’t locked. 
- <u>Power up:</u> Players whose robots were powered down this turn will be powered up again if they had not been destroyed or got powered down again.
- <u>Re-enter robots:</u> All robots that were destroyed in this round re-enter the game at the entry point on the tile on which they last lived. They will look in the direction the player wants to and get 2 damage tokens. If a robot would re-enter on the same space as another robot, they are in Virtual Mode. 

### Checkpoints

Checkpoints can interact with the robots as well as with the map.

#### Robots

If a robot is at a checkpoint at the end of a register phase, it receives it and can make its way to the next one. Obviously, the checkpoints must be completed in the correct order, from the one with the lowest number to the one with the highest.

#### Map

Checkpoints can be affected by the map which means they can be moved by it. Following board elements can affect it:

- <u>Conveyor belts:</u> They can move the checkpoint around and can even move the checkpoint to a player so that the player may wait for it.
- <u>Pushers:</u> Pushers also affect Checkpoints and move them away.

If a board element would throw the checkpoint into a hole, out of the map or another unreachable place it wont get affected by this board element.

### Damage and destruction

When a player’s robot is damaged they get a damage token which means that they lose one health. A robot has a maximum of 10 health-points.

<u>Loss of program cards:</u>  Because of getting hit by lasers, getting stomped or damaged due other things the robots memory save gets affected so that thy get one fewer Program card for each damage token they have. 

<u>Locked registers:</u> If a robot has 5 or more damage tokens, its registers begin to lock up starting with register 5 and working down to register 1. Once a register is locked, the Program card in that register cannot be discarded. This means that from now on this programming card is stuck in the register and is executed every round. This can only be removed when the register is no longer locked.

<u>Destruction:</u> A robot is destroyed when it got the 10th damage token which means it has 0 health points left. A robot can also be instantly destroyed by certain board elements or by moving off the edge of the board. There may be option cards which can prevent you from getting destroyed. The robot will re-enter play at the end of the turn. 

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

### Winning the game

The winner of the game is the player whose robot was the first one getting all checkpoint-marks. If one player won the game the others may continue to see who will get 2nd, 3rd ...

### Board Elements

On the map are many different board elements. Some of them are getting triggered after each register phase and some will only get triggered after the register phase is over. There are also some which will get triggered when ever a robot is on the tile no matter if it ends its register their or not.

#### Register phase elements

These are all elements which will get triggered after each register in the order they will get triggered:

- <u>Double conveyor belts:</u> move any robot resting on them two spaces in the direction of the arrows. 
- <u>Single conveyor belts:</u> move any robot resting on them one space in the direction of the arrows. 
- <u>Pushers:</u> push any robot resting on them into the next space in the direction the push panel faces. Pushers are treated as wall so that robots cant pass through them. The tiles activate only in the register that corresponds to the number on them. 
- <u>Gears:</u> rotate robots resting on them 90 degrees in the direction of the arrows.
- <u>Trapdoors:</u> opens up and destroys a robot. The tiles activate only in the register that corresponds to the number on them. 
- <u>Stompers:</u> will deal 2 damage to the robot. Further more a robot will stay stomped for this register phase so that it always has the least priority. The tiles activate only in the register that corresponds to the number on them.
- <u>Lasers:</u> will deal 1 damage to a robot. (See more at "Laser")

#### End of turn elements

- <u>Radiation:</u> A robot on a radiation space takes 1 point of damage. 
- <u>Repair sites:</u> A robot on a repair site repairs damage points depending on how strong the repair field is. (1 or 2)
- <u>Checkpoints:</u> A robot on a flag repairs 1 point of damage. 

#### Always triggered elements

- <u>Button:</u> A button is a toggle which will enable or disable something like ramps. Due that robots may get damage cause they wanted to use a ramp and now fall down or may drive against a wall instead of unseeing the ramp.
- <u>Puddle:</u> A robot leaving a puddle gets one movement negated which means that a Move-1 card has no affect, a Move-2 is treated as a Move-1 and so on. Rotation is not affected. It has no affect if the robot in the puddle is pushed.

#### Others

- <u>Pit:</u> A robot which drives into a pit will get destroyed instantly. 
- <u>Walls:</u>  Robots can’t move through walls and lasers can’t shoot through them. Robots that attempt to move through a wall or which would be forced to move through a wall simply stay where they are. 
- <u>Levels:</u> Each map tile is on one level. Robots on the same level can fire on one another, but robots on different levels cannot.  
- <u>Ledges:</u>  A robot on the lower level of a ledge treats the ledge as if it were a wall. A robot on the upper level of the ledge can cross the ledge and if it does so it will take 2 points of damage for each level fallen.
- <u>Ramp:</u> A ramp is like a normal tile when a robot moves on it. However, when the player ends a register phase on the ramp it will slip down to the tile in front of the ramp (if there is a robot it will not slip down). A ramp can only be driven on from two sides (bottom and top), if a robot moves from the side it is treated like a wall. Further more a ramp can be toggled by buttons so that the ramp is disable and treated as a default tile. If a ramp gets disabled while a robot is  standing on it, the robot will get one point of damage.
- <u>One-way walls:</u> One way walls are treated as a wall from one side but ignored from the other one. From left and right it is treated as wall too.

### Conveyor movement

#### Default behaviour

Conveyor belts move your robot in the direction of the arrows. Double-arrowed conveyor belts move robots two spaces and activate before single-arrowed conveyor belts, which move robots one space. Once a robot has moved off a belt, the belt no longer affects that robot.

#### Interaction with other robots

If a conveyor belt would move a robot into a non-conveyor belt space where another robot sits, the robot in motion must stop on the last space of the conveyor belt. It does not push the robot in its way. 

If both robots would end their move on the same conveyor belt space, both robots stay where they are. 

#### Rotating conveyor belts

Some conveyor belts have a curved arrow indicating a rotating section of the belt. Robots rotate 90 degrees in the direction of the curved arrow as they move onto the curved section of the belt. If a robot moves onto the curved section of a conveyor belt by means other than the conveyor belt itself, the robot does not turn 90 degrees. 

If a robot moves onto the curved section of a conveyor belt by means of the conveyor belt, but it moves from a straight arrowed space instead of a curved arrow space, it will not turn 90 degrees. 

### Laser

There are two different types of lasers, the board lasers and the robot lasers. The only difference is that the one laser is shot by the map after each register phase and the other one is shot by the robots after the register phase if they are still alive. The laser can only hit one robot. Lasers normally deal 1 damage but lasers of players could deal more damage if they have special upgrade cards.

### Pushing other robots

If robot **A** would enter a space where another robot (**B**) is standing on, it will push robot **B** into the direction robot **A** is moving till its movement is over. Robots do not change the direction they are facing when they are pushed. Robots can be pushed almost anywhere on the board, including into a pit. They can even be pushed off the side of the board! Robots cannot be pushed through walls. If a robot pushes another robot into a wall, both robots immediately end their movement.

### Falling of the board

If you move off or are pushed off of the board it will get destroyed instantly.

### Virtual Mode

If a robot re-enters play on the same field as another robot, they will enter *Virtual Mode*. A robot in Virtual Mode does not interact with anything but  non-damaging board elements. The robot will remain in Virtual Mode as long as they share a space with another robot. As soon as the robot no longer shares a space with another robot, they become “real”.

### Programming cards

There are 9 different programming cards in the game. These are:

- <u>Move-1:</u> Move your robot in the direction it is facing for one space. 
- <u>Move-2:</u> Move your robot in the direction it is facing for two spaces. 
- <u>Move-3:</u> Move your robot in the direction it is facing for three spaces. 
- <u>Right turn:</u> Turn your robot 90 degrees to the right. The robot remains in its current space.
- <u>Left turn:</u> Turn your robot 90 degrees to the left. The robot remains in its current space.
- <u>U-turn:</u> Turn your robot 180 degrees so it faces the opposite direction. The robot remains in its current space.
- <u>Move back:</u> Move your robot one space back. The robot does not change the direction it is facing. 
- <u>Again:</u> Repeat the programming in your previous register. 
- <u>Power up:</u> Take one energy cube, and place it on your player mat.

### Special programming cards

You may obtain these special programming cards by installing certain temporary upgrades. When you first receive a special programming card, place it in your discard pile. The card will cycle through your programming deck, and you may play them just as you would any other programming card, by placing them in one of your registers during the programming phase. 

There are 6 special programming cards:

- <u>Energy Routine:</u> 
- <u>Sandbox Routine:</u> Choose one of the following actions to perform this register: Move 1, 2, or 3, Back Up, Turn Left, Turn Right, U-Turn
- <u>Weasel Routine:</u> Choose one of the following actions to perform this register: Turn Left Turn Right U-Turn
- <u>Speed Routine:</u> Move your robot 3 spaces in the direction it is facing.
- <u>Heal Routine:</u> Remove a damage point
- <u>Repeat Routine:</u> Repeat the programming in your previous register. 