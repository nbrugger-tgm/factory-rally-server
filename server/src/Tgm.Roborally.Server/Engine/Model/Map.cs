using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Tgm.Roborally.Server.Models;

namespace Tgm.Roborally.Server.Engine {
	[DataContract]
	public class Map {
		private GameLogic _game;

		[DataMember] private Tile[] _tiles;

		public Map(int columnCount = 10, int rowCount = 10) {
			Height = rowCount;
			Width  = columnCount;
			_tiles = InitEmpty(columnCount, rowCount);
		}

		[field: DataMember] public int Height { get; private set; }

		[field: DataMember] public int Width { get; private set; }

		public MapInfo Info => new MapInfo(this);

		public void Assign(GameLogic logic) {
			_game = logic;
		}

		public Position PrioCorePos {
			get {
				for (int y = 0; y < Width; y++) {
					for (int x = 0; x < Height; x++) {
						if (this[x, y].Type == TileType.PrioCore)
							return new Position(x, y);
					}
				}

				return null;
			}
		}


		/// <summary>
		/// Set/Get the Tile at the specific position defined by X and Y cordinates
		/// </summary>
		/// <param name="x">the x cordinate (starting at 0)</param>
		/// <param name="y">the y cordinate (starting at 0)</param>
		/// <exception cref="ArgumentException">When trying to set an illegal tile (eg. 2 prio cores on one map)</exception>
		public Tile this[int x, int y] {
			get => _tiles[x + y * Width];
			set {
				if (value.Type == TileType.PrioCore && PrioCoreCount > 0)
					throw new ArgumentException("Only one Prio Core per Map allowed");
				_tiles[x + y * Width] = value;
			}
		}

		public int PrioCoreCount {
			get {
				int count = 0;
				for (int c = 0; c < Height; c++) {
					for (int r = 0; r < Width; r++) {
						if (this[c, r].Type == TileType.PrioCore)
							count++;
					}
				}

				return count;
			}
		}

		private static Tile[] InitEmpty(int columnCount, int rowCount) {
			Tile[] tiles = new Tile[columnCount * rowCount];
			for (int c = 0; c < columnCount; c++) {
				for (int r = 0; r < rowCount; r++) tiles[r * columnCount + c] = new Tile();
			}

			return tiles;
		}


		/// <summary>
		/// Caculates wich fields are empty (no entities on top) and sets the regarding property
		/// </summary>
		public void CalculateEmpty() {
			if (_game == null)
				return;
			ImmutableList<(int X, int Y)> occupied = _game.Entitys.List
														  .Select(selector: e => e.Location)
														  .Select(selector: p => (p.X, p.Y))
														  .ToImmutableList();
			for (int c = 0; c < Width; c++) {
				for (int r = 0; r < Height; r++) this[c, r].Empty = !occupied.Contains((c, r));
			}
		}

		public bool AddColumn(int index) {
			if (index < 0 || index > Height)
				return false;

			Width++;
			Tile[,] tiles = new Tile[Width, Height];
			for (int c = 0; c < Width; c++) {
				for (int r = 0; r < Height; r++) {
					if (c < index)
						tiles[c, r] = this[c, r];
					else if (c > index)
						tiles[c, r] = this[c - 1, r];
					else
						tiles[c, r] = new Tile();
				}
			}

			SetDeepTiles(tiles);

			return true;
		}

		public bool RemoveColumn(int index) {
			if (index < 0 || index >= Height)
				return false;

			Width--;
			Tile[,] tiles = new Tile[Width, Height];
			for (int c = 0; c < Width; c++) {
				for (int r = 0; r < Height; r++) {
					if (c < index)
						tiles[c, r] = this[c, r];
					else if (c > index)
						tiles[c, r] = this[c + 1, r];
					else
						tiles[c, r] = new Tile();
				}
			}

			SetDeepTiles(tiles);

			return true;
		}

		private void SetDeepTiles(Tile[,] tiles) {
			Height = tiles.GetLength(1);
			Width  = tiles.GetLength(0);
			_tiles = new Tile[Height * Width];
			for (int y = 0; y < Height; y++) {
				for (int x = 0; x < Width; x++) {
					this[x, y] = tiles[x, y];
				}
			}
		}

		public bool AddRow(int index) {
			if (index < 0 || index > Width)
				return false;

			Height++;
			Tile[,] tiles = new Tile[Width, Height];
			for (int c = 0; c < Width; c++) {
				for (int r = 0; r < Height; r++) {
					if (r < index)
						tiles[c, r] = this[c, r];
					else if (r > index)
						tiles[c, r] = this[c, r - 1];
					else
						tiles[c, r] = new Tile();
				}
			}

			SetDeepTiles(tiles);

			return true;
		}

		public bool RemoveRow(int index) {
			if (index < 0 || index >= Width)
				return false;

			Height--;
			Tile[,] tiles = new Tile[Width, Height];
			for (int c = 0; c < Width; c++) {
				for (int r = 0; r < Height; r++) {
					if (r < index)
						tiles[c, r] = this[c, r];
					else if (r > index)
						tiles[c, r] = this[c, r + 1];
				}
			}

			SetDeepTiles(tiles);

			return true;
		}

		public bool SwitchTiles(int x1, int y1, int x2, int y2) {
			if (Width <= x1 || Height <= y1 || Width <= x2 || Height <= y2)
				return false;

			Tile tile1 = this[x1, y1];
			Tile tile2 = this[x2, y2];

			this[x1, y1] = tile2;
			this[x2, y2] = tile1;

			return true;
		}

		public bool SwitchColumns(int column1, int column2) {
			if (Width <= column1 || Width <= column2)
				return false;

			for (int r = 0; r < Height; r++) SwitchTiles(column1, r, column2, r);

			return true;
		}

		public bool SwitchRows(int row1, int row2) {
			if (Height <= row1 || Height <= row2)
				return false;

			for (int c = 0; c < Width; c++) SwitchTiles(c, row1, c, row2);

			return true;
		}


		public bool IsValid() {
			// Checks if all Tiles are set
			for (int c = 0; c < Width; c++) {
				for (int r = 0; r < Height; r++) {
					if (this[c, r] == null)
						return false;
				}
			}

			// Checks if there is only one priority Beacon
			if (PrioCoreCount != 1)
				return false;

			return true;
		}

		public string ToMapString() {
			string mapString = "";
			for (int r = 0; r < Height + 1; r++) {
				for (int c = 0; c < Width + 1; c++) {
					if (r == Height) {
						if (c == 0)
							mapString += "╚═══";
						else if (c == Width)
							mapString += "╝\n";
						else
							mapString += "╧═══";
					}
					else {
						if (r == 0 && c == 0)
							mapString += "╔═══";
						else if (r == 0 && c == Width)
							mapString += "╗\n";
						else if (c == 0)
							mapString += "╟═══";
						else if (c == Width)
							mapString += "╢\n";
						else if (r == 0)
							mapString += "╤═══";
						else
							mapString += "╬═══";
					}
				}

				for (int c = 0; c < Width; c++) {
					if (r == Height) {
					}
					else {
						string value = ((int) this[c, r].Type).ToString();
						mapString += "║";
						mapString += " ";
						mapString += value;
						mapString += new string(' ', 2 - value.Length);
						if (c == Width - 1)
							mapString += "║\n";
					}
				}
			}

			return mapString;
		}

		public bool IsWithin(Position newPos) => newPos.X < Height && newPos.Y < Width;
	}

	// SOURCE: Stackoverflow
	public static class ArrayExt {
		public static T[] GetRow<T>(this T[,] array, int row) {
			if (!typeof(T).IsPrimitive)
				throw new InvalidOperationException("Not supported for managed types.");

			if (array == null)
				throw new ArgumentNullException("array");

			int cols   = array.GetUpperBound(1) + 1;
			T[] result = new T[cols];

			int size;

			if (typeof(T) == typeof(bool))
				size = 1;
			else if (typeof(T) == typeof(char))
				size = 2;
			else
				size = Marshal.SizeOf<T>();

			Buffer.BlockCopy(array, row * cols * size, result, 0, cols * size);

			return result;
		}
	}
}
