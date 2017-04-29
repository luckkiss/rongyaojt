using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class SvrMapConfig : configParser
	{
		public Dictionary<uint, Variant> _mapConfs = new Dictionary<uint, Variant>();

		public static SvrMapConfig instance;

		public Variant simpleMapConfig;

		public Variant mapConfig;

		protected Variant _npc_in_map_id;

		public Dictionary<uint, Variant> mapConfs
		{
			get
			{
				return this._mapConfs;
			}
		}

		public SvrMapConfig(ClientConfig m) : base(m)
		{
			SvrMapConfig.instance = this;
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new SvrMapConfig(m as ClientConfig);
		}

		protected override void onData()
		{
			Variant variant = this.m_conf["m"];
			for (int i = 0; i < variant.Count; i++)
			{
				Variant variant2 = variant[i];
				uint @uint = variant2["id"]._uint;
				this._mapConfs[@uint] = variant2;
			}
		}

		protected void format_mission()
		{
		}

		public Variant getSingleMapConf(uint mapid)
		{
			bool flag = !this._mapConfs.ContainsKey(mapid);
			Variant result;
			if (flag)
			{
				Debug.LogWarning("找不到地图配置getSingleMapConf::mapid:" + mapid);
				result = null;
			}
			else
			{
				result = this._mapConfs[mapid];
			}
			return result;
		}

		public void prepareMapConfig(uint mapID, Action onFin, Action<uint, int, int> onProg)
		{
			bool flag = this.mapConfig[mapID];
			if (flag)
			{
				onFin();
			}
			else
			{
				Variant val = string.Concat(new object[]
				{
					this.m_conf["serverURL"],
					"?sid=",
					this.m_conf["serverID"],
					"&get_smap&id=",
					mapID,
					"&ver=",
					StringUtil.make_version(this.m_conf["configVers"]["mpver"])
				});
				IURLReq iURLReq = os.net.CreateURLReq(val);
				iURLReq.dataFormat = "binary";
				iURLReq.load(delegate(IURLReq r, object ret)
				{
					byte[] d = ret as byte[];
					ByteArray data = new ByteArray(d);
					bool flag2 = !this.on_map_data(data);
					if (flag2)
					{
						DebugTrace.add(Define.DebugTrace.DTT_ERR, "parser map id[" + mapID + "] config error");
					}
					onFin();
				}, delegate(IURLReq r, float progress)
				{
					onProg(mapID, (int)(progress * 100f), 100);
				}, delegate(IURLReq r, string msg)
				{
					DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new object[]
					{
						"load map id[",
						mapID,
						"] config error msg: ",
						msg
					}));
					onFin();
				});
			}
		}

		public Variant getMapConfig(uint mapID)
		{
			return this.mapConfig[mapID];
		}

		public bool on_map_data(ByteArray data)
		{
			data.uncompress();
			uint num = (uint)data.readUnsignedShort();
			Variant variant = new Variant();
			this.mapConfig[num] = variant;
			variant["param"] = new Variant();
			variant["param"]["id"] = num;
			variant["param"]["tile_size"] = data.readUnsignedShort();
			variant["param"]["width"] = data.readUnsignedShort();
			variant["param"]["height"] = data.readUnsignedShort();
			variant["param"]["tile_set"] = data.readUnsignedShort();
			variant["param"]["pk"] = data.readUnsignedByte();
			variant["param"]["name"] = StringUtil.read_NTSTR(data, "utf-8");
			uint num2 = (uint)data.readUnsignedShort();
			variant["param"]["links_count"] = num2;
			variant["Link"] = new Variant();
			uint num3;
			for (num3 = 0u; num3 < num2; num3 += 1u)
			{
				Variant variant2 = new Variant();
				variant2["goto"] = data.readUnsignedShort();
				variant2["x"] = data.readUnsignedShort();
				variant2["y"] = data.readUnsignedShort();
				variant2["to_x"] = data.readUnsignedShort();
				variant2["to_y"] = data.readUnsignedShort();
				variant["Link"]._arr.Add(variant2);
			}
			uint num4 = (uint)data.readUnsignedShort();
			variant["Npc"] = new Variant();
			for (num3 = 0u; num3 < num4; num3 += 1u)
			{
				Variant variant3 = new Variant();
				variant3["nid"] = data.readUnsignedShort();
				variant3["x"] = data.readShort();
				variant3["y"] = data.readShort();
				variant3["nr"] = data.readUnsignedShort();
				variant["Npc"]._arr.Add(variant3);
			}
			uint num5 = (uint)data.readUnsignedShort();
			variant["MapItems"] = new Variant();
			for (num3 = 0u; num3 < num5; num3 += 1u)
			{
				Variant variant4 = new Variant();
				variant4["iid"] = data.readUnsignedShort();
				variant4["x"] = data.readShort();
				variant4["y"] = data.readShort();
				variant4["r"] = data.readUnsignedByte();
				variant4["order"] = data.readUnsignedByte();
				variant4["zorder"] = data.readShort();
				variant4["blendMode"] = data.readUnsignedByte();
				variant["MapItems"]._arr.Add(variant4);
			}
			uint num6 = (uint)data.readUnsignedShort();
			variant["MapEffects"] = new Variant();
			for (num3 = 0u; num3 < num6; num3 += 1u)
			{
				Variant variant5 = new Variant();
				variant5["x"] = data.readShort();
				variant5["y"] = data.readShort();
				variant5["r"] = data.readUnsignedByte();
				variant5["order"] = data.readUnsignedByte();
				variant5["zorder"] = data.readShort();
				variant5["blendMode"] = data.readUnsignedByte();
				variant5["obj"] = StringUtil.read_NTSTR(data, "utf-8");
				variant["MapEffects"]._arr.Add(variant5);
			}
			uint num7 = (uint)data.readUnsignedShort();
			variant["MapMons"] = new Variant();
			for (num3 = 0u; num3 < num7; num3 += 1u)
			{
				Variant variant6 = new Variant();
				variant6["mid"] = data.readUnsignedShort();
				variant6["x"] = data.readShort();
				variant6["y"] = data.readShort();
				variant["MapMons"]._arr.Add(variant6);
			}
			ByteArray byteArray = new ByteArray();
			data.readBytes(byteArray, 0, data.length);
			variant["gridData"] = new Variant();
			num3 = 0u;
			while ((ulong)num3 < (ulong)((long)byteArray.length))
			{
				variant["gridData"]._arr.Add(byteArray.readUnsignedShort());
				num3 += 2u;
			}
			bool flag = variant["gridData"]["length"] < variant["param"]["width"] * variant["param"]["height"];
			if (flag)
			{
				DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new object[]
				{
					"map id[",
					num,
					"] width[",
					variant["param"]["width"],
					"] height[",
					variant["param"]["height"],
					"] grid data length[",
					variant["gridData"]["length"],
					"] err"
				}));
			}
			return true;
		}

		public bool on_maps_data(ByteArray data)
		{
			data.uncompress();
			uint num = (uint)data.readUnsignedShort();
			for (uint num2 = 0u; num2 < num; num2 += 1u)
			{
				Variant variant = new Variant();
				uint val = (uint)data.readUnsignedShort();
				this.simpleMapConfig[val] = variant;
				variant["name"] = StringUtil.read_NTSTR(data, "utf-8");
				uint num3 = (uint)data.readUnsignedShort();
				Variant variant2 = new Variant();
				for (uint num4 = 0u; num4 < num3; num4 += 1u)
				{
					uint val2 = (uint)data.readUnsignedShort();
					variant2._arr.Add(val2);
				}
				variant["links"] = variant2;
				uint num5 = (uint)data.readUnsignedShort();
				Variant variant3 = new Variant();
				Variant variant4 = new Variant();
				for (uint num4 = 0u; num4 < num5; num4 += 1u)
				{
					uint val3 = (uint)data.readUnsignedShort();
					int val4 = (int)data.readShort();
					int val5 = (int)data.readShort();
					variant3._arr.Add(val3);
					Variant variant5 = new Variant();
					variant5["id"] = val3;
					variant5["x"] = val4;
					variant5["y"] = val5;
					variant4[val3] = variant5;
				}
				variant["npcs"] = variant3;
				variant["npcinfo"] = variant4;
			}
			return true;
		}

		public uint get_npc_map_id(uint npc_id)
		{
			bool flag = this._npc_in_map_id == null;
			if (flag)
			{
				this._npc_in_map_id = new Variant();
				bool flag2 = this._mapConfs != null && this._mapConfs.Count > 0;
				if (flag2)
				{
					foreach (Variant current in this._mapConfs.Values)
					{
						uint @uint = current["id"]._uint;
						bool flag3 = !current.ContainsKey("n");
						if (!flag3)
						{
							Variant variant = current["n"];
							foreach (Variant current2 in variant._arr)
							{
								uint uint2 = current2["nid"]._uint;
								this._npc_in_map_id[uint2.ToString()] = @uint;
							}
						}
					}
				}
			}
			bool flag4 = this._npc_in_map_id.ContainsKey(npc_id.ToString());
			uint result;
			if (flag4)
			{
				result = this._npc_in_map_id[npc_id.ToString()];
			}
			else
			{
				GameTools.PrintError("get_npc_map_id npc[" + npc_id + "] not Exist!");
				result = 0u;
			}
			return result;
		}

		public Variant getMapConfigByMapid(int mapid)
		{
			return this.simpleMapConfig[mapid];
		}

		public Variant get_npc_info(uint npc_id)
		{
			Variant result;
			foreach (Variant current in this._mapConfs.Values)
			{
				bool flag = !current.ContainsKey("n");
				if (!flag)
				{
					Variant variant = current["n"];
					foreach (Variant current2 in variant._arr)
					{
						bool flag2 = current2["nid"]._uint == npc_id;
						if (flag2)
						{
							result = current2;
							return result;
						}
					}
				}
			}
			result = null;
			return result;
		}
	}
}
