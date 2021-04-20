using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Managers {
	/// <summary>
	/// Manages the global map repository. Singelton
	/// </summary>
	public class MapManager {
		public static  MapManager Instance => _i;
		private static MapManager _i;

		public static void createInstance(string path) {
			_i = new MapManager(path);
		}

		private static readonly JsonSerializer serializer = new JsonSerializer();

		private const string MAP_EXTENSION = "json";

		public MapManager(string basepath) {
			RepoPath = basepath;
			if (Names.Length == 0) Add(BlankMap, "BLANK");
		}

		private string MapExtedionWithDot => $".{MAP_EXTENSION}";

		private DirectoryInfo Directory { get; set; }

		/// <summary>
		/// An empty 10x10 map
		/// </summary>
		public Map BlankMap {
			get {
				Map m = new Map();
				m.CalculateEmpty();
				return m;
			}
		}


		/// <summary>
		/// The path of the repository as string
		/// </summary>
		public string RepoPath {
			set {
				Directory = new DirectoryInfo(value);
				CreateDir();
			}
			get => Directory.FullName;
		}

		private IEnumerable<FileInfo> MapFiles => Directory.GetFiles($"*{MapExtedionWithDot}").ToArray();

		/// <summary>
		/// The list of all maps
		/// </summary>
		public Map[] Maps =>
			MapFiles.Select(selector: file => {
				FileStream     fs     = file.OpenRead();
				JsonTextReader reader = new JsonTextReader(new StreamReader(fs));
				Map            m      = serializer.Deserialize<Map>(reader);
				reader.Close();
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
			FileInfo info = Directory.GetFiles($"{name}{MapExtedionWithDot}")[0];
			if (!info.Exists)
				return null;
			FileStream     fs     = info.OpenRead();
			JsonTextReader reader = new JsonTextReader(new StreamReader(fs));
			Map            m      = (Map) serializer.Deserialize(reader);
			reader.Close();
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
			JsonTextWriter writer = new JsonTextWriter(new StreamWriter(fs));
			serializer.Serialize(writer, m);
			writer.Flush();
			writer.Close();
		}

		private void CreateDir() {
			while (!Directory.Exists) {
				DirectoryInfo dir = Directory;
				while (!dir.Parent.Exists) {
					dir = dir.Parent;
				}

				dir.Create();
			}
		}
	}
}