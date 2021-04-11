using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Abstraction.Adders {
	/// <summary>
	/// This is a delegate! Use it like this
	/// <code>
	///	Add&lt;Item&gt; hi = new Add&lt;Item&gt;();
	/// hi(itemInstance);
	/// </code>
	/// </summary>
	/// <param name="item">the item to add</param>
	/// <typeparam name="T">the type of the titem t add</typeparam>
	public delegate void Add<in T>(T item);

	/// <summary>
	/// Adds 0..N items with <c>add</c>
	/// </summary>
	/// <param name="add">the methods to use for adding</param>
	/// <typeparam name="T">the type of the items</typeparam>
	public delegate void ItemAdder<out T>(Add<T> add);
}