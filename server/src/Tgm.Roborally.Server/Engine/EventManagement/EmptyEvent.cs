using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine {
	public class EmptyEvent : Event {
		public EmptyEvent(EventType type) {
			Type = type;
		}

		private EventType Type           { get; }
		public  EventType GetEventType() => Type;
	}
}