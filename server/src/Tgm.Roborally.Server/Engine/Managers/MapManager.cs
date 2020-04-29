using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Win32.SafeHandles;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine.Managers
{
	public class MapManager
	{
		public static readonly MapManager Instance = new MapManager();

		private DirectoryInfo _directory =
			new DirectoryInfo(@"D:\Users\Nils\Desktop\Schule\ITP\robot-rally\game-controller\maps\");

		public string MapExtendion = "rmap";
		public string MapExtedionWithDot => $".{MapExtendion}";
		public DirectoryInfo Directory => _directory;

		private static DataContractSerializer serializer = new DataContractSerializer(typeof(Map));

		public MapManager()
		{
			if (names.Length == 0)
			{
				Add(BlankMap,"BLANK");
			}
		}

		public Map BlankMap
		{
			get
			{
				Map m = new Map(10, 10);
				m[0, 0] = new Tile();
				return m;
			}
		}


		public string RepoPath
		{
			set => _directory = new DirectoryInfo(value);
			get => _directory.FullName;
		}

		private FileInfo[] MapFiles => _directory.GetFiles($"*{MapExtedionWithDot}").ToArray();

		public Map[] Maps
		{
			get
			{
				return MapFiles.Select(file =>
				{
					FileStream fs = file.OpenRead();
					Map m =  (Map) serializer.ReadObject(fs);
					fs.Close();
					return m;
				}).ToArray();
			}
		}

		public string[] names => MapFiles.Select(info => info.Name.Replace(MapExtedionWithDot, "")).ToArray();
		public Map Get(string name)
		{
			FileStream fs = _directory.GetFiles($"{name}{MapExtedionWithDot}")[0].OpenRead();
			Map m = (Map) serializer.ReadObject(fs);
			fs.Close();
			return m;
		}

		public void Add(Map m,string name)
		{
			FileStream fs = new FileStream(Path.Combine(_directory.FullName,$"{name}{MapExtedionWithDot}"),FileMode.Create,FileAccess.Write);
			serializer.WriteObject(fs,m);
			fs.Close();
		}
	}
}