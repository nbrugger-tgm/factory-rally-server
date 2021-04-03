using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine {
	/// <summary>
	/// An event without any data. Use this event when you only need to transport the information That something happened and no further data is needed
	/// </summary>
	public class EmptyEvent : Event {
		public EmptyEvent(EventType type) {
			Type = type;
		}

		private EventType Type           { get; }
		public  EventType GetEventType() => Type;
	}
}