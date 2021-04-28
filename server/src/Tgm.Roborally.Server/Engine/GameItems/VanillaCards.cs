using System;
using System.Collections.Generic;
using Tgm.Roborally.Server.Engine.Abstraction.Adders;
using Tgm.Roborally.Server.Engine.Abstraction.Managers;
using Tgm.Roborally.Server.Engine.Statement;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.GameItems {
	public class VanillaCards : IItemLoader {
		public void AddUpgrades(Add<Upgrade> add) {
			add(new Upgrade {
				Cost        = 2,
				Description = "Move an additional {fields} fields into a direction of your choice",
				Id          = 0,
				Values      = new List<Pair> {new Pair("fields", 2)},
				Type        = UpgradeType.Generator,
				Permanent   = false,
				Name        = "I like to move it move it, ...",
				Rounds      = 1
			});
			add(new Upgrade {
				Cost        = 3,
				Description = "Move an additional {fields} fields into a direction of your choice",
				Id          = 1,
				Values      = new List<Pair> {new Pair("fields", 3)},
				Type        = UpgradeType.Generator,
				Permanent   = false,
				Name        = "I like to move it move it, MOVE IT!",
				Rounds      = 1
			});
		}

		public void AddProgrammingCards(IItemLoader.AddCommands add) {
			add(6, new MoveCommand(3) {
				Name = "Move 3"
			});

			add(12, new MoveCommand(2) {
				Name = "Move 2"
			});

			add(18, new MoveCommand(1) {
				Name = "Move 1"
			});

			add(9, new RotateCommand(Rotation.Left, 1) {
				Name = "Turn Left"
			});

			add(9, new RotateCommand(Rotation.Right, 1) {
				Name = "Turn Right"
			});

			add(6, new RotateCommand(Rotation.Right, 2) {
				Name = "U-Turn"
			});

			add(6, new RobotShootCommand() {
				Name = "Shoot"
			});

			add(100, new RepeatCommand() {
				Name = "Repeat"
			});
		}
	}
}