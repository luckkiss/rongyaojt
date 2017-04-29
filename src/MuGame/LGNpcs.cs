using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class LGNpcs : lgGDBase, IObjectPlugin
	{
		public static LGNpcs instance;

		private Dictionary<string, LGAvatarNpc> _npcs;

		public LGNpcs(gameManager m) : base(m)
		{
			LGNpcs.instance = this;
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGNpcs(m as gameManager);
		}

		public LGAvatarNpc getNpc(int npcid)
		{
			bool flag = this._npcs == null || !this._npcs.ContainsKey(npcid.ToString());
			LGAvatarNpc result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this._npcs[npcid.ToString()];
			}
			return result;
		}

		public Dictionary<string, LGAvatarNpc> getNpcs()
		{
			return this._npcs;
		}

		public override void init()
		{
			this._npcs = new Dictionary<string, LGAvatarNpc>();
		}

		private void clear()
		{
			foreach (LGAvatarNpc current in this._npcs.Values)
			{
				current.dispose();
			}
			this._npcs.Clear();
		}

		public void onMapchg()
		{
			this.clear();
			this.createMapNpcs();
		}

		private void createMapNpcs()
		{
			bool s_bStandaloneScene = SelfRole.s_bStandaloneScene;
			if (!s_bStandaloneScene)
			{
				joinWorldInfo joinWorldInfo = this.g_mgr.g_netM.getObject("DATA_JOIN_WORLD") as joinWorldInfo;
				Variant mainPlayerInfo = joinWorldInfo.mainPlayerInfo;
				uint mapid = joinWorldInfo.mapid;
				Variant singleMapConf = (this.g_mgr.g_gameConfM.getObject("SvrMap") as SvrMapConfig).getSingleMapConf(mapid);
				bool flag = singleMapConf == null || !singleMapConf.ContainsKey("n");
				if (!flag)
				{
					Variant variant = singleMapConf["n"];
					foreach (Variant current in variant._arr)
					{
						int npcid = int.Parse(current["nid"]);
						Variant variant2 = (this.g_mgr.g_gameConfM.getObject("SvrNPC") as SvrNPCConfig).get_npc_data(npcid);
						bool flag2 = variant2 == null;
						if (!flag2)
						{
							LGAvatarNpc lGAvatarNpc = new LGAvatarNpc(this.g_mgr);
							this._npcs[npcid.ToString()] = lGAvatarNpc;
							bool flag3 = variant2.ContainsKey("defdid");
							if (flag3)
							{
								lGAvatarNpc.dialogId = variant2["defdid"];
							}
							Variant variant3 = new Variant();
							variant3["x"] = (float)current["x"]._int / 1.666f;
							variant3["y"] = (float)current["y"]._int / 1.666f;
							variant3["nid"] = current["nid"];
							variant3["name"] = variant2["name"];
							variant3["octOri"] = current["r"];
							lGAvatarNpc.initData(variant3);
							lGAvatarNpc.init();
						}
					}
				}
			}
		}
	}
}
