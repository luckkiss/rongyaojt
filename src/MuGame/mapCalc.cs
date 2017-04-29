using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class mapCalc
	{
		private LGMap _lgmap;

		private Variant _curMapConf;

		private short[] _grd;

		private Dictionary<int, uint> _CollideInfoEx = new Dictionary<int, uint>();

		private Variant _BlockZones = new Variant();

		private Dictionary<int, uint> _BlockZoneInfo = new Dictionary<int, uint>();

		private Dictionary<uint, int> visited = new Dictionary<uint, int>();

		private SvrMapConfig svrMapConfig
		{
			get
			{
				return this._lgmap.g_mgr.g_gameConfM.getObject("SvrMap") as SvrMapConfig;
			}
		}

		public mapCalc(LGMap m)
		{
			this._lgmap = m;
		}

		public void changeMap(Variant mpconf, short[] grd)
		{
			this._curMapConf = mpconf;
			this._grd = grd;
			this._clearCurMap();
			this._initCurMap();
		}

		protected void _initCurMap()
		{
			this._initCollide();
		}

		protected void _clearCurMap()
		{
			this._CollideInfoEx.Clear();
			this._BlockZones = new Variant();
			this._BlockZoneInfo.Clear();
		}

		private void _calcBlockZoneInfo()
		{
			this._BlockZoneInfo.Clear();
			foreach (Variant current in this._BlockZones.Values)
			{
				for (int i = current["left"]; i < current["right"]; i++)
				{
					for (int j = current["top"]; j < current["bottom"]; j++)
					{
						this._BlockZoneInfo[j << 9 | (i & 511)] = 1u;
					}
				}
			}
		}

		public void addBlockZone(Array data)
		{
			foreach (Variant variant in data)
			{
				this._BlockZones[variant["id"]] = variant;
			}
			this._calcBlockZoneInfo();
		}

		public void clearBlockZone()
		{
			this._BlockZones = new Variant();
			this._calcBlockZoneInfo();
		}

		private int _get_tile_id(int idx)
		{
			bool flag = idx >= this._grd.Length || idx < 0;
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				result = (int)this._grd[idx];
			}
			return result;
		}

		private void _initCollide()
		{
			bool flag = this._curMapConf == null;
			if (!flag)
			{
				int num = (int)((long)this._curMapConf["param"]["width"]._int / 32L);
				int num2 = (int)((long)this._curMapConf["param"]["height"]._int / 32L);
				for (int i = 0; i < num; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						int idx = j * num + i;
						int num3 = this._get_tile_id(idx);
						bool flag2 = num3 < 0;
						if (!flag2)
						{
							uint num4 = (uint)(num3 >> 12);
							this._CollideInfoEx[j << 9 | (i & 511)] = (num4 & 1u);
						}
					}
				}
			}
		}

		public bool IsWalkAble(float gx, float gy)
		{
			bool flag = this._curMapConf == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				int num = (int)((long)this._curMapConf["param"]["width"]._int / 32L);
				int num2 = (int)((long)this._curMapConf["param"]["height"]._int / 32L);
				bool flag2 = gx < 0f || gx >= (float)num || gy < 0f || gy >= (float)num2;
				if (flag2)
				{
					result = false;
				}
				else
				{
					int key = (int)gy << 9 | ((int)gx & 511);
					int num3 = 0;
					bool flag3 = this._CollideInfoEx.ContainsKey(key);
					if (flag3)
					{
						num3 = (int)this._CollideInfoEx[key];
					}
					bool flag4 = num3 == 1;
					if (flag4)
					{
						result = false;
					}
					else
					{
						bool flag5 = this._BlockZoneInfo.ContainsKey(key) && this._BlockZoneInfo[key] > 0u;
						result = !flag5;
					}
				}
			}
			return result;
		}

		protected Vec2 _getWalkabelEnd(Vec2 start, Vec2 end)
		{
			int num = (int)start.x - (int)end.x;
			int num2 = (int)start.y - (int)end.y;
			int num3 = Math.Max(Math.Abs(num), Math.Abs(num2));
			Vec2 result = null;
			uint num4 = 0u;
			while ((ulong)num4 < (ulong)((long)num3))
			{
				int num5 = (int)(end.x + (float)((long)num * (long)((ulong)num4 / (ulong)((long)num3))));
				int num6 = (int)(end.y + (float)((long)num2 * (long)((ulong)num4 / (ulong)((long)num3))));
				Vec2 vec = new Vec2((float)num5, (float)num6);
				bool flag = this.IsWalkAble((float)((int)vec.x), (float)((int)vec.y));
				if (flag)
				{
					result = vec;
					break;
				}
				num4 += 1u;
			}
			return result;
		}

		private int _OpenGrids_compfunc(GridST g1, GridST g2)
		{
			bool flag = g1.F < g2.F;
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				bool flag2 = g1.F == g2.F;
				if (flag2)
				{
					bool flag3 = g1.H < g2.H;
					if (flag3)
					{
						result = -1;
						return result;
					}
				}
				result = 1;
			}
			return result;
		}

		public List<GridST> findPath(Vec2 start, Vec2 end)
		{
			bool flag = !this.IsWalkAble((float)((int)end.x), (float)((int)end.y));
			List<GridST> result;
			if (flag)
			{
				end = this._getWalkabelEnd(start, end);
				bool flag2 = end == null;
				if (flag2)
				{
					GameTools.PrintNotice("end point not walkable!");
					result = null;
					return result;
				}
			}
			bool flag3 = start.x == end.x && start.y == end.y;
			if (flag3)
			{
				result = new List<GridST>();
			}
			else
			{
				int num = (int)((long)this._curMapConf["param"]["width"]._int / 32L);
				int num2 = (int)((long)this._curMapConf["param"]["height"]._int / 32L);
				List<GridST> list = new List<GridST>();
				Dictionary<uint, GridST> dictionary = new Dictionary<uint, GridST>();
				Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
				list.Add(new GridST
				{
					x = (float)((int)start.x),
					y = (float)((int)start.y),
					parent = null,
					G = 0,
					H = 0,
					F = 0,
					isCorner = false,
					tile_idx = 0u
				});
				GridST gridST = null;
				while (list.Count > 0)
				{
					GridST gridST2 = ArrayUtil.pop_priority_queue<GridST>(list, new ArrayUtil.CompareFunction<GridST>(this._OpenGrids_compfunc));
					uint key = (uint)((int)gridST2.y << 9 | ((int)gridST2.x & 511));
					dictionary2[(int)key] = 1;
					for (int i = 0; i < 3; i++)
					{
						int num3 = (int)(gridST2.y + (float)i - 1f);
						bool flag4 = num3 < 0 || num3 >= num2;
						if (!flag4)
						{
							uint num4 = (uint)((uint)num3 << 9);
							for (int j = 0; j < 3; j++)
							{
								int num5 = (int)(gridST2.x + (float)j - 1f);
								bool flag5 = num5 < 0 || num5 >= num;
								if (!flag5)
								{
									uint num6 = num4 | (uint)(num5 & 511);
									bool flag6 = dictionary2.ContainsKey((int)num6);
									if (!flag6)
									{
										int key2 = (int)num6;
										bool flag7 = this._CollideInfoEx[key2] == 1u || (this._BlockZoneInfo.ContainsKey((int)num6) && this._BlockZoneInfo[(int)num6] > 0u);
										if (flag7)
										{
											dictionary2[(int)num6] = 1;
										}
										else
										{
											int num7 = gridST2.G + 1;
											bool isCorner = false;
											bool flag8 = gridST2.x != (float)num5 && gridST2.y != (float)num3;
											if (flag8)
											{
												num7 = gridST2.G + 2;
												isCorner = true;
											}
											int num8 = (int)(Math.Abs((float)num3 - end.y) + Math.Abs((float)num5 - end.x));
											int num9 = num7 + num8;
											bool flag9 = dictionary.ContainsKey(num6);
											if (flag9)
											{
												GridST gridST3 = dictionary[num6];
												bool flag10 = num9 < gridST3.F || (num9 == gridST3.F && num8 < gridST3.H);
												if (flag10)
												{
													gridST3.F = num9;
													gridST3.H = num8;
													gridST3.G = num7;
													gridST3.parent = gridST2;
													gridST3.isCorner = isCorner;
													gridST3.tile_idx = num6;
												}
											}
											else
											{
												dictionary[num6] = new GridST
												{
													x = (float)num5,
													y = (float)num3,
													parent = gridST2,
													G = num7,
													H = num8,
													F = num9,
													isCorner = isCorner,
													tile_idx = num6
												};
												ArrayUtil.push_priority_queue<GridST>(list, dictionary[num6], new ArrayUtil.CompareFunction<GridST>(this._OpenGrids_compfunc));
												bool flag11 = (float)num5 == end.x && (float)num3 == end.y;
												if (flag11)
												{
													gridST = dictionary[num6];
													break;
												}
											}
										}
									}
								}
							}
							bool flag12 = gridST != null;
							if (flag12)
							{
								break;
							}
						}
					}
					bool flag13 = gridST != null;
					if (flag13)
					{
						break;
					}
				}
				bool flag14 = gridST == null;
				if (flag14)
				{
					result = null;
				}
				else
				{
					List<GridST> list2 = new List<GridST>();
					while (gridST != null)
					{
						list2.Add(new GridST
						{
							x = gridST.x,
							y = gridST.y,
							parent = gridST.parent,
							G = gridST.G,
							H = gridST.H,
							F = gridST.F,
							isCorner = gridST.isCorner,
							tile_idx = gridST.tile_idx
						});
						gridST = gridST.parent;
					}
					result = list2;
				}
			}
			return result;
		}

		private int getDirection(int fx, int fy, int tx, int ty)
		{
			int num = tx - fx;
			int num2 = ty - fy;
			int result = 0;
			bool flag = num2 == 0;
			if (flag)
			{
				result = ((num > 0) ? 1 : 4);
			}
			else
			{
				bool flag2 = num == 0;
				if (flag2)
				{
					result = ((num2 > 0) ? 2 : 8);
				}
				else
				{
					bool flag3 = num > 0 && num2 > 0;
					if (flag3)
					{
						result = 16;
					}
					else
					{
						bool flag4 = num > 0 && num2 < 0;
						if (flag4)
						{
							result = 128;
						}
						else
						{
							bool flag5 = num < 0 && num2 > 0;
							if (flag5)
							{
								result = 32;
							}
							else
							{
								bool flag6 = num < 0 && num2 < 0;
								if (flag6)
								{
									result = 64;
								}
							}
						}
					}
				}
			}
			return result;
		}

		public Vec2 FindCanMoveGridPosNearBy(Vec2 grid, float minRad, float rangeRad, uint minR = 2u, uint range = 3u, int maxCount = 5)
		{
			Vec2 vec = new Vec2(grid.x, grid.y);
			bool flag = this._curMapConf != null;
			if (flag)
			{
				int @int = this._curMapConf["param"]["width"]._int32;
				int int2 = this._curMapConf["param"]["height"]._int32;
				int i = maxCount;
				Random random = new Random();
				while (i > 0)
				{
					double num = (double)minRad + random.NextDouble() * (double)rangeRad;
					double num2 = minR + random.NextDouble() * range;
					double num3 = num2 * Math.Cos(num);
					int num4 = (int)((double)grid.x + num3);
					bool flag2 = num3 < 0.0;
					if (flag2)
					{
						num4++;
					}
					num3 = num2 * Math.Sin(num);
					int num5 = (int)((double)grid.y - num3);
					bool flag3 = num3 > 0.0;
					if (flag3)
					{
						num5++;
					}
					bool flag4 = num4 >= 0 && num5 >= 0 && num4 < @int && num5 < int2;
					if (flag4)
					{
						bool flag5 = this._CollideInfoEx[num5 << 9 | (num4 & 511)] == 0u && !this._BlockZoneInfo.ContainsKey(num5 << 9 | (num4 & 511));
						if (flag5)
						{
							vec.x = (float)num4;
							vec.y = (float)num5;
							break;
						}
					}
					i--;
				}
			}
			return vec;
		}

		public List<uint> get_map_path(uint curMapId, uint dest_map_id)
		{
			List<uint> list = new List<uint>();
			bool flag = curMapId == dest_map_id;
			List<uint> result;
			if (flag)
			{
				list.Add(dest_map_id);
				result = list;
			}
			else
			{
				this.DFSTraverse(curMapId, dest_map_id);
				uint num = dest_map_id;
				while (true)
				{
					list.Add(num);
					Variant variant = this.svrMapConfig.mapConfs[num];
					bool flag2 = variant == null || !variant.ContainsKey("parent") || variant["parent"]._uint == 0u;
					if (flag2)
					{
						break;
					}
					num = variant["parent"]._uint;
				}
				result = list;
			}
			return result;
		}

		private void DFSTraverse(uint start_map_id, uint dest_map_id)
		{
			foreach (uint current in this.svrMapConfig.mapConfs.Keys)
			{
				this.visited[current] = 0;
			}
			this.DFS(start_map_id, start_map_id, dest_map_id, 1, 0u);
		}

		private void DFS(uint origin_map, uint start_map_id, uint dest_map_id, int length, uint parent_id = 0u)
		{
			this.visited[start_map_id] = length;
			Variant variant = this.svrMapConfig.mapConfs[start_map_id];
			variant["parent"] = parent_id;
			bool flag = start_map_id == dest_map_id;
			if (!flag)
			{
				Variant variant2 = variant["l"];
				List<uint> list = new List<uint>();
				foreach (Variant current in variant2._arr)
				{
					list.Add(current["gto"]._uint);
				}
				bool flag2 = start_map_id == origin_map;
				if (flag2)
				{
					bool flag3 = this._lgmap.tmpLinks != null && this._lgmap.tmpLinks.Count > 0;
					if (flag3)
					{
						foreach (Variant current2 in this._lgmap.tmpLinks.Values)
						{
							list.Add(current2["data"]["goto"]._uint);
						}
					}
				}
				for (int i = 0; i < list.Count; i++)
				{
					uint num = list[i];
					bool flag4 = !this.visited.ContainsKey(num);
					if (!flag4)
					{
						bool flag5 = this.visited[num] == 0 || this.visited[num] > length + 1;
						if (flag5)
						{
							this.DFS(origin_map, num, dest_map_id, length + 1, start_map_id);
						}
					}
				}
			}
		}
	}
}
