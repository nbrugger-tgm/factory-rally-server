#nullable enable
using System;
using Tgm.Roborally.Server.Engine.Abstraction.Managers;

namespace Tgm.Roborally.Server.Engine.Abstraction.Modloader {
	public partial class Modloader{
		private interface IManagerLoader {
			Type Type { get; }
			void Load(Mod name, GameLogic game);
		}


		private class ManagerLoader<T> : IManagerLoader where T : IManager {
			private readonly Func<Mod, ManagerImplementationProvider<T>> _provider;
			private readonly Modloader                                   _ref;

			public Type Type => typeof(T);

			public void Load(Mod mod, GameLogic game) {
				(T?, string?) tuple = _ref.GetManagerTuple<T>();
				LoadModOverwrite(mod.Name, game, _provider(mod), ref tuple);
				_ref.SetManagerTuple(tuple);
			}

			public ManagerLoader(Modloader @ref, Func<Mod, ManagerImplementationProvider<T>> provider) {
				_provider = provider;
				_ref      = @ref;
			}
		}
		private delegate T? ManagerImplementationProvider<T> (GameLogic logic, T? prev) where T : IManager;
	}
}