using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Managers {
	/// <summary>
	/// Manages the global map repository. Singelton
	/// </summary>
	public class MapManager {
		public static readonly MapManager Instance = new MapManager();

		private static readonly DataContractSerializer serializer = new DataContractSerializer(typeof(Map));

		private const string MAP_EXTENSION = "rmap";

		public MapManager() {
			if (Names.Length == 0) Add(BlankMap, "BLANK");
		}

		private string MapExtedionWithDot => $".{MAP_EXTENSION}";

		public DirectoryInfo Directory { get; private set; } =
			new DirectoryInfo(@"D:\Users\Nils\Desktop\Schule\ITP\robot-rally\game-controller\maps\");

		/// <summary>
		/// An empty 10x10 map
		/// </summary>
		public Map BlankMap {
			get {
				Map m = new Map();
				m[0, 0] = new Tile();
				return m;
			}
		}


		/// <summary>
		/// The path of the repository as string
		/// </summary>
		public string RepoPath {
			set => Directory = new DirectoryInfo(value);
			get => Directory.FullName;
		}

		private FileInfo[] MapFiles => Directory.GetFiles($"*{MapExtedionWithDot}").ToArray();

		/// <summary>
		/// The list of all maps
		/// </summary>
		public Map[] Maps =>
			MapFiles.Select(selector: file => {
				FileStream fs = file.OpenRead();
				Map        m  = (Map) serializer.ReadObject(fs);
				fs.Close();
				return m;
			}).ToArray();

		/// <summary>
		/// A list with the name of each map in it
		/// </summary>
		public string[] Names => MapFiles.Select(selector: info => info.Name.Replace(MapExtedionWithDot, "")).ToArray();

		/// <summary>
		/// Expensive Operation: deserialises the map with the given name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Map Get(string name) {
			FileStream fs = Directory.GetFiles($"{name}{MapExtedionWithDot}")[0].OpenRead();
			Map        m  = (Map) serializer.ReadObject(fs);
			fs.Close();
			return m;
		}

		/// <summary>
		/// Adds a map to the repository
		/// </summary>
		/// <param name="m">the map to save</param>
		/// <param name="name">the name of the map</param>
		public void Add(Map m, string name) {
			FileStream fs = new FileStream(Path.Combine(Directory.FullName, $"{name}{MapExtedionWithDot}"),
										   FileMode.Create, FileAccess.Write);
			serializer.WriteObject(fs, m);
			fs.Close();
		}
	}
}