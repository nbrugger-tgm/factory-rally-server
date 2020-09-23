using System;
using System.Runtime.InteropServices;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine
{
	/// <summary>
	/// Marks a class as an event passable to a game
	/// </summary>
	public interface Event
	{
		/// <summary>
		/// Returns the matching type for the event
		/// </summary>
		/// <returns></returns>
		EventType GetEventType();
	}
}