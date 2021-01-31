using System.Collections.Generic;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Managers {
	public class UpgradeManager {
		private Dictionary<int, Upgrade> _pool = new Dictionary<int, Upgrade>();
		public  List<int>                Ids => new List<int>(_pool.Keys);
	}
}