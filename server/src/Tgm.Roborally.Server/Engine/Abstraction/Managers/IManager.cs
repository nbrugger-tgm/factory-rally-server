using System;
using System.Data;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Abstraction.Managers {
	public interface IManager {
		/// <summary>
		/// This method is called when the GameLogic is ready and all fieds are initalized. Now the game can be accessed and you can do things like responding to other managers
		/// </summary>
		public void Setup();
	}
}