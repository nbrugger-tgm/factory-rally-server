using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Managers {
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

		public Map BlankMap {
			get {
				Map m = new Map();
				m[0, 0] = new Tile();
				return m;
			}
		}


		public string RepoPath {
			set => Directory = new DirectoryInfo(value);
			get => Directory.FullName;
		}

		private FileInfo[] MapFiles => Directory.GetFiles($"*{MapExtedionWithDot}").ToArray();

		public Map[] Maps =>
			MapFiles.Select(selector: file => {
				FileStream fs = file.OpenRead();
				Map        m  = (Map) serializer.ReadObject(fs);
				fs.Close();
				return m;
			}).ToArray();

		public string[] names => MapFiles.Select(selector: info => info.Name.Replace(MapExtedionWithDot, "")).ToArray();
		public string[] Names => MapFiles.Select(selector: info => info.Name.Replace(MapExtedionWithDot, "")).ToArray();

		public Map Get(string name) {
			FileStream fs = Directory.GetFiles($"{name}{MapExtedionWithDot}")[0].OpenRead();
			Map        m  = (Map) serializer.ReadObject(fs);
			fs.Close();
			return m;
		}

		public void Add(Map m, string name) {
			FileStream fs = new FileStream(Path.Combine(Directory.FullName, $"{name}{MapExtedionWithDot}"),
										   FileMode.Create, FileAccess.Write);
			serializer.WriteObject(fs, m);
			fs.Close();
		}
	}
}