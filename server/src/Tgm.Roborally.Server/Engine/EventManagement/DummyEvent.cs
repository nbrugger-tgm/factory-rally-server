using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine {
	public class DummyEvent : Event {
		public readonly object Information;

		public readonly string THIS_IS_A_DUMMY_EVENT =
			"!!!IMPORTANT!!! This is only a dummy event so it is WIP and will be replaced soon (TM)";

		private readonly EventType Type;

		public DummyEvent(EventType type, object information) {
			Information = information;
			Type        = type;
		}

		public EventType GetEventType() => Type;
	}
}