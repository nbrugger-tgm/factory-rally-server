using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Tgm.Roborally.Server.Models;
using Action = System.Action;

namespace Tgm.Roborally.Server.Engine
{
	public class GameActionHandler
	{
		public List<ActionType> queue { get; } = new List<ActionType>();
		private int _queuePos;
		public int QueuePos => _queuePos;
		private Dictionary<ActionType, Action> ActionMap = new Dictionary<ActionType, Action>();

		[MethodImpl(MethodImplOptions.Synchronized)]
		public void executeNext()
		{
			ActionType key = queue[_queuePos];
			_queuePos++;
			ActionMap[key].Invoke();
		}

		private GameLogic _ref;

		public GameActionHandler(GameLogic game)
		{
			_ref = game;
			InitActionMap();
		}

		private void InitActionMap()
		{
			ActionMap[ActionType.PAUSE] = () => { _ref.State = GameState.BREAK; };
			ActionMap[ActionType.PAUSE] = () => { _ref.State = _ref.LastState; };
		}

		public void Add(ActionType t) => queue.Add(t);
		public List<ActionType> Pending => queue.Where((i, e) => e >= _queuePos).ToList();
		public List<ActionType> Executed => queue.Where((i, e) => e < _queuePos).ToList();
	}
}