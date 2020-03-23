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
		private GameLogic game;
		private List<ActionType> _Queue { get; } = new List<ActionType>();

		
		private int _queuePos;
		public int QueuePos => _queuePos;
		private Dictionary<ActionType, Action> ActionMap = new Dictionary<ActionType, Action>();

		[MethodImpl(MethodImplOptions.Synchronized)]
		public void ExecuteNext()
		{
			ActionType type = _Queue[_queuePos++];
			ActionMap[type].Invoke();
			//TODO: game.ComittEvent(new GameActionEvent(type));
		}


		public GameActionHandler(GameLogic game)
		{
			this.game = game;
			InitActionMap();
		}

		private void InitActionMap()
		{
			ActionMap[ActionType.PAUSE] = () => { game.State = GameState.BREAK; };
			ActionMap[ActionType.UNPAUSE] = () => { game.State = game.LastState; };
			//TODO: Change to Start_Game ActionMap[ActionType.UNPAUSE] = () => { game. };
		}

		
		[MethodImpl(MethodImplOptions.Synchronized)]
		public void Add(ActionType t) => _Queue.Add(t);
		public List<ActionType> Pending => _Queue.Where((e, index) => index >= _queuePos).ToList();
		public List<ActionType> Executed => _Queue.Where((e, index) => index < _queuePos).ToList();
		public List<Tgm.Roborally.Server.Models.Action> Queue => _Queue.Select((e,i) => new Tgm.Roborally.Server.Models.Action(){Index = i,Executed = i<_queuePos,Type = e,}).ToList();
	}
}