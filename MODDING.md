# Modding

## DLL Modding [WIP]

> Note that this feature is WORK IN PROGRESS and not implemented yet 

Mods can created as a standalone Project by using the `Tgm.Roborally.Server.dll` as library. Exporting the mod as DLL and placing it into the mod folder of the server. 

## Internal Modding

The code for internal modding works the same way as External DLL Modding. The difference is that the Mods are directly included and compiled into the Server Sourcecode/Binary.

### How to do this?

1. Open/Clone [`game-controller`](https://github.com/FactoryRally/game-controller) repo

2. The server source-code is located in `server/src/Tgm.Roborally.Server/`

3. Create a folder with the name of your Mod in the `Mods` folder

4. Create a file  `<name_of_your_mod>Mod.cs` in the created folder. How to implement this class is explained in the next section 

5. Add an instance of your mod class to the `Programm.cs` class.
   In this class you will find a Mod array 

   ```c#
   private static readonly Mod[] InternalMods = {
       new Vanilla()
       //TODO: Insert other internal mods here like optional expansions or such
   };
   ```

   

## Guide

The base class of your Mod should extend the `Mod` class in the `Tgm.Roborally.Server.Engine.Abstraction` namespace.

Beside the `Name` property noting **needs** to be implemented. But the `Mod` class contains many methods that you can overwrite which are all documented. Overwriting this methods replaces existing implementations. 

Example:

```c#
public GamePhase? StartingPhase(GamePhase? arg) => new LobbyPhase();
```

This will replace the start GamePhase implementation with the `LobbyPhase` implementation

For information about the internal classes read the `DEVELOPING.md`

## Errors and Whoopsies

If something doesnt work