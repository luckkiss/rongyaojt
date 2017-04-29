using System;

namespace MuGame
{
	internal class GridST
	{
		public float x = 0f;

		public float y = 0f;

		public int G = 0;

		public int H = 0;

		public int F = 0;

		public GridST parent = null;

		public bool isCorner = false;

		public bool isEnd = false;

		public uint tile_idx = 0u;
	}
}
