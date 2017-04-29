using System;
using System.Collections.Generic;

namespace Cross
{
	public class GraphManager : IAppPlugin
	{
		protected static GraphManager _instance = null;

		protected Variant m_avaConf;

		protected Variant m_effConf;

		protected Variant m_mtrlConf;

		protected Variant m_sceneSlotConf;

		protected Variant m_taskConf;

		protected Dictionary<string, Variant> m_mapConf = new Dictionary<string, Variant>();

		protected Variant m_FileConfigs;

		protected Dictionary<string, GRWorld3D> m_worlds;

		protected Dictionary<string, GRShader> m_materials;

		protected Variant m_mapinfo = new Variant();

		protected bool m_compatibleMode;

		public string pluginName
		{
			get
			{
				return "graph";
			}
		}

		public static GraphManager singleton
		{
			get
			{
				return GraphManager._instance;
			}
		}

		public bool compatibleMode
		{
			get
			{
				return this.m_compatibleMode;
			}
		}

		public Variant MapInfo
		{
			get
			{
				return this.m_mapinfo;
			}
		}

		public GraphManager(bool compatibleMode = false)
		{
			bool flag = GraphManager._instance != null;
			if (flag)
			{
				throw new Exception();
			}
			GraphManager._instance = this;
			this.m_compatibleMode = compatibleMode;
			this.m_worlds = new Dictionary<string, GRWorld3D>();
			this.m_materials = new Dictionary<string, GRShader>();
		}

		public void onPreInit()
		{
		}

		public void onInit()
		{
			ConfigManager configManager = CrossApp.singleton.getPlugin("conf") as ConfigManager;
			configManager.regFormatFunc("effect", new Action<Variant>(this._formatEffectConf));
			configManager.regFormatFunc("avatar", new Action<Variant>(this._formatAvatarConf));
			configManager.regFormatFunc("map", new Action<Variant>(this._formatMapConf));
			configManager.regFormatFunc("material", new Action<Variant>(this._formatMaterialConf));
			configManager.regFormatFunc("sceneSlot", new Action<Variant>(this._formatSceneSlotConf));
			configManager.regFormatFunc("task", new Action<Variant>(this._formatTaskConf));
		}

		public void onPostInit()
		{
		}

		public GRWorld3D createWorld3D(string id)
		{
			bool flag = this.m_worlds.ContainsKey(id);
			GRWorld3D result;
			if (flag)
			{
				result = this.m_worlds[id];
			}
			else
			{
				GRWorld3D gRWorld3D = new GRWorld3D(id, this);
				this.m_worlds[id] = gRWorld3D;
				result = gRWorld3D;
			}
			return result;
		}

		public GRWorld3D getWorld3D(string id)
		{
			GRWorld3D result = null;
			bool flag = this.m_worlds.ContainsKey(id);
			if (flag)
			{
				result = this.m_worlds[id];
			}
			return result;
		}

		public bool deleteWorld3D(string id)
		{
			bool flag = this.m_worlds.ContainsKey(id);
			bool result;
			if (flag)
			{
				this.m_worlds.Remove(id);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public void onFin()
		{
		}

		public void onResize(int a, int b)
		{
		}

		public void onPreRender(float tmSlice)
		{
		}

		public void onRender(float tmSlice)
		{
		}

		public void onPostRender(float tmSlice)
		{
		}

		public void onPreProcess(float tmSlice)
		{
		}

		public void onProcess(float tmSlice)
		{
		}

		public void onPostProcess(float tmSlice)
		{
		}

		protected void _loadMapConfigs(List<Variant> toLoadVec)
		{
			bool flag = toLoadVec.Count <= 0;
			if (!flag)
			{
				Variant variant = toLoadVec[toLoadVec.Count - 1];
				toLoadVec.RemoveAt(toLoadVec.Count - 1);
				string str = variant["file"]._str;
				string str2 = variant["id"]._str;
				this._loadMapConfig(str, str2);
				this._loadMapConfigs(toLoadVec);
			}
		}

		protected void _loadMapConfig(string url, string id)
		{
			ConfigManager configManager = CrossApp.singleton.getPlugin("conf") as ConfigManager;
			configManager.loadExtendConfig(url, delegate(Variant conf)
			{
				this.m_mapConf[id] = conf;
			});
		}

		public void _formatMapConf(Variant conf)
		{
			this.m_FileConfigs = new Variant();
			bool flag = conf.ContainsKey("map");
			if (flag)
			{
				for (int i = 0; i < conf["map"].Count; i++)
				{
					Variant variant = conf["map"][i];
					this.m_mapinfo._arr.Add(variant);
					Variant value = variant.clone();
					this.m_FileConfigs[variant["file"]._str] = value;
				}
			}
			List<Variant> list = new List<Variant>();
			foreach (Variant current in this.m_FileConfigs.Values)
			{
				list.Add(current);
			}
			this._loadMapConfigs(list);
		}

		public Variant getMapConf(string id)
		{
			bool flag = this.m_mapConf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = !this.m_mapConf.ContainsKey(id);
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = this.m_mapConf[id];
				}
			}
			return result;
		}

		public Variant getMaterialConf(string id)
		{
			bool flag = this.m_mtrlConf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_mtrlConf["mtrl"][id];
			}
			return result;
		}

		protected void _formatSceneSlotConf(Variant conf)
		{
			bool flag = conf.ContainsKey("slots");
			if (flag)
			{
				conf["slots"] = conf["slots"].convertToDct("id");
			}
			else
			{
				conf["slots"] = new Variant();
			}
			this.m_sceneSlotConf = conf;
		}

		protected void _formatTaskConf(Variant conf)
		{
			bool flag = conf.ContainsKey("Tasks");
			if (flag)
			{
				conf["Tasks"] = conf["Tasks"].convertToDct("id");
			}
			else
			{
				conf["Tasks"] = new Variant();
			}
			this.m_sceneSlotConf = conf;
		}

		public void _formatEffectConf(Variant conf)
		{
			bool flag = conf.ContainsKey("eff");
			if (flag)
			{
				conf["eff"] = conf["eff"].convertToDct("id");
			}
			else
			{
				conf["eff"] = new Variant();
			}
			bool flag2 = conf.ContainsKey("chaEff");
			if (flag2)
			{
				conf["chaEff"] = conf["chaEff"].convertToDct("id");
			}
			else
			{
				conf["chaEff"] = new Variant();
			}
			bool flag3 = conf.ContainsKey("entEff");
			if (flag3)
			{
				conf["entEff"] = conf["entEff"].convertToDct("id");
			}
			else
			{
				conf["entEff"] = new Variant();
			}
			bool flag4 = conf.ContainsKey("knifeEff");
			if (flag4)
			{
				conf["knifeEff"] = conf["knifeEff"].convertToDct("id");
			}
			else
			{
				conf["knifeEff"] = new Variant();
			}
			this.m_effConf = conf;
		}

		public Variant getEffectConf(string id)
		{
			bool flag = this.m_effConf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = !this.m_effConf.ContainsKey("eff");
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = this.m_effConf["eff"][id];
				}
			}
			return result;
		}

		public Variant getEffectKnifeLightConf(string id)
		{
			bool flag = this.m_effConf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = !this.m_effConf.ContainsKey("knifeEff");
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = this.m_effConf["knifeEff"][id];
				}
			}
			return result;
		}

		public Variant getChaEffectConf(string id)
		{
			bool flag = this.m_effConf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = !this.m_effConf.ContainsKey("chaEff");
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = this.m_effConf["chaEff"][id];
				}
			}
			return result;
		}

		public Variant getEntEffectConf(string id)
		{
			bool flag = !this.m_effConf.ContainsKey("entEff");
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_effConf["entEff"][id];
			}
			return result;
		}

		public Variant SceneSlotConf(string id)
		{
			bool flag = this.m_sceneSlotConf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = !this.m_sceneSlotConf.ContainsKey("slots") || !this.m_sceneSlotConf["slots"].ContainsKey(id);
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = this.m_sceneSlotConf["slots"][id]["slot"];
				}
			}
			return result;
		}

		public Variant TaskConf(string id)
		{
			bool flag = this.m_sceneSlotConf == null || !this.m_taskConf.ContainsKey("Tasks") || !this.m_sceneSlotConf["Tasks"].ContainsKey(id);
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_sceneSlotConf["Tasks"][id]["Task"];
			}
			return result;
		}

		protected void _formatAvatarConfNew(Variant conf)
		{
			bool flag = conf.ContainsKey("chaEff");
			if (flag)
			{
				conf["chaEff"] = conf["chaEff"].convertToDct("id");
			}
			else
			{
				conf["chaEff"] = new Variant();
			}
			bool flag2 = conf.ContainsKey("chaAni");
			if (flag2)
			{
				conf["chaAni"] = conf["chaAni"].convertToDct("id");
			}
			else
			{
				conf["chaAni"] = new Variant();
			}
			bool flag3 = conf.ContainsKey("cha");
			if (flag3)
			{
				conf["cha"] = conf["cha"].convertToDct("id");
				foreach (Variant current in conf["cha"].Values)
				{
					bool flag4 = current.ContainsKey("ani");
					if (flag4)
					{
						Variant variant = conf["chaAni"][current["ani"]._str];
						bool flag5 = variant != null;
						if (flag5)
						{
							current["ani"] = variant["ani"];
							current["defani"] = variant["defAni"];
						}
						else
						{
							current["ani"] = new Variant();
							current["ani"]["ani"] = new Variant();
							current["ani"]["ani"].setToArray();
						}
					}
					bool flag6 = current.ContainsKey("eff");
					if (flag6)
					{
						Variant variant2 = conf["chaEff"][current["eff"]._str];
						bool flag7 = variant2 != null;
						if (flag7)
						{
							current["eff"] = variant2["eff"];
						}
						else
						{
							current["eff"] = new Variant();
							current["eff"]["eff"] = new Variant();
							current["eff"]["eff"].setToArray();
						}
					}
				}
			}
			else
			{
				conf["cha"] = new Variant();
			}
			bool flag8 = conf.ContainsKey("entEff");
			if (flag8)
			{
				conf["entEff"] = conf["entEff"].convertToDct("id");
			}
			else
			{
				conf["entEff"] = new Variant();
			}
			bool flag9 = conf.ContainsKey("entAni");
			if (flag9)
			{
				conf["entAni"] = conf["entAni"].convertToDct("id");
			}
			else
			{
				conf["entAni"] = new Variant();
			}
			bool flag10 = conf.ContainsKey("ent");
			if (flag10)
			{
				conf["ent"] = conf["ent"].convertToDct("id");
				foreach (Variant current2 in conf["ent"].Values)
				{
					bool flag11 = current2.ContainsKey("ani");
					if (flag11)
					{
						Variant variant3 = conf["entAni"][current2["ani"]._str];
						bool flag12 = variant3 != null;
						if (flag12)
						{
							current2["ani"] = variant3["ani"];
							current2["defAni"] = variant3["defAni"];
						}
						else
						{
							current2["ani"] = new Variant();
							current2["ani"]["ani"] = new Variant();
							current2["ani"]["ani"].setToArray();
						}
					}
					bool flag13 = current2.ContainsKey("eff");
					if (flag13)
					{
						Variant variant4 = conf["entEff"][current2["eff"]._str];
						bool flag14 = variant4 != null;
						if (flag14)
						{
							current2["eff"] = variant4["eff"];
						}
						else
						{
							current2["eff"] = new Variant();
							current2["eff"]["eff"] = new Variant();
							current2["eff"]["eff"].setToArray();
						}
					}
				}
			}
			else
			{
				conf["ent"] = new Variant();
			}
			bool flag15 = conf.ContainsKey("bossEff");
			if (flag15)
			{
				conf["bossEff"] = conf["bossEff"].convertToDct("id");
			}
			else
			{
				conf["bossEff"] = new Variant();
			}
			bool flag16 = conf.ContainsKey("bossAni");
			if (flag16)
			{
				conf["bossAni"] = conf["bossAni"].convertToDct("id");
			}
			else
			{
				conf["bossAni"] = new Variant();
			}
			bool flag17 = conf.ContainsKey("boss");
			if (flag17)
			{
				conf["boss"] = conf["boss"].convertToDct("id");
				foreach (Variant current3 in conf["boss"].Values)
				{
					bool flag18 = current3.ContainsKey("ani");
					if (flag18)
					{
						Variant variant5 = conf["bossAni"][current3["ani"]._str];
						bool flag19 = variant5 != null;
						if (flag19)
						{
							current3["ani"] = variant5["ani"];
							current3["defAni"] = variant5["defAni"];
						}
						else
						{
							current3["ani"] = new Variant();
							current3["ani"]["ani"] = new Variant();
							current3["ani"]["ani"].setToArray();
						}
					}
					bool flag20 = current3.ContainsKey("eff");
					if (flag20)
					{
						Variant variant6 = conf["bossEff"][current3["eff"]._str];
						bool flag21 = variant6 != null;
						if (flag21)
						{
							current3["eff"] = variant6["eff"];
						}
						else
						{
							current3["eff"] = new Variant();
							current3["eff"]["eff"] = new Variant();
							current3["eff"]["eff"].setToArray();
						}
					}
				}
			}
			else
			{
				conf["boss"] = new Variant();
			}
			bool flag22 = conf.ContainsKey("mountEff");
			if (flag22)
			{
				conf["mountEff"] = conf["mountEff"].convertToDct("id");
			}
			else
			{
				conf["mountEff"] = new Variant();
			}
			bool flag23 = conf.ContainsKey("mountsAni");
			if (flag23)
			{
				conf["mountsAni"] = conf["mountsAni"].convertToDct("id");
			}
			else
			{
				conf["mountsAni"] = new Variant();
			}
			bool flag24 = conf.ContainsKey("mounts");
			if (flag24)
			{
				conf["mounts"] = conf["mounts"].convertToDct("id");
				foreach (Variant current4 in conf["mounts"].Values)
				{
					bool flag25 = current4.ContainsKey("ani");
					if (flag25)
					{
						Variant variant7 = conf["mountsAni"][current4["ani"]._str];
						bool flag26 = variant7 != null;
						if (flag26)
						{
							current4["ani"] = variant7["ani"];
							current4["defAni"] = variant7["defAni"];
						}
						else
						{
							current4["ani"] = new Variant();
							current4["ani"]["ani"] = new Variant();
							current4["ani"]["ani"].setToArray();
						}
					}
					bool flag27 = current4.ContainsKey("eff");
					if (flag27)
					{
						Variant variant8 = conf["mountEff"][current4["eff"]._str];
						bool flag28 = variant8 != null;
						if (flag28)
						{
							current4["eff"] = variant8["eff"];
						}
						else
						{
							current4["eff"] = new Variant();
							current4["eff"]["eff"] = new Variant();
							current4["eff"]["eff"].setToArray();
						}
					}
				}
			}
			else
			{
				conf["mounts"] = new Variant();
			}
			bool flag29 = conf.ContainsKey("uiAni");
			if (flag29)
			{
				conf["uiAni"] = conf["uiAni"].convertToDct("id");
			}
			bool flag30 = conf.ContainsKey("uijt");
			if (flag30)
			{
				conf["uijt"] = conf["uijt"].convertToDct("id");
			}
			bool flag31 = conf.ContainsKey("uiyingzi");
			if (flag31)
			{
				conf["uiyingzi"] = conf["uiyingzi"].convertToDct("id");
			}
			bool flag32 = conf.ContainsKey("uicha");
			if (flag32)
			{
				conf["uicha"] = conf["uicha"].convertToDct("id");
				foreach (Variant current5 in conf["uicha"].Values)
				{
					bool flag33 = current5.ContainsKey("ani");
					if (flag33)
					{
						Variant variant9 = conf["uiAni"][current5["ani"]._str];
						bool flag34 = variant9 != null;
						if (flag34)
						{
							current5["ani"] = variant9["ani"];
							current5["defAni"] = variant9["defAni"];
						}
						else
						{
							current5["ani"] = new Variant();
							current5["ani"]["ani"] = new Variant();
							current5["ani"]["ani"].setToArray();
						}
					}
				}
			}
			else
			{
				conf["uicha"] = new Variant();
			}
			bool flag35 = conf.ContainsKey("npcEff");
			if (flag35)
			{
				conf["npcEff"] = conf["npcEff"].convertToDct("id");
			}
			else
			{
				conf["npcEff"] = new Variant();
			}
			bool flag36 = conf.ContainsKey("npcAni");
			if (flag36)
			{
				conf["npcAni"] = conf["npcAni"].convertToDct("id");
			}
			else
			{
				conf["npcAni"] = new Variant();
			}
			bool flag37 = conf.ContainsKey("npc");
			if (flag37)
			{
				conf["npc"] = conf["npc"].convertToDct("id");
				foreach (Variant current6 in conf["npc"].Values)
				{
					bool flag38 = current6.ContainsKey("ani");
					if (flag38)
					{
						Variant variant10 = conf["npcAni"][current6["ani"]._str];
						bool flag39 = variant10 != null;
						if (flag39)
						{
							current6["ani"] = variant10["ani"];
							current6["defAni"] = variant10["defAni"];
						}
						else
						{
							current6["ani"] = new Variant();
							current6["ani"]["ani"] = new Variant();
							current6["ani"]["ani"].setToArray();
						}
					}
					bool flag40 = current6.ContainsKey("eff");
					if (flag40)
					{
						Variant variant11 = conf["npcEff"][current6["eff"]._str];
						bool flag41 = variant11 != null;
						if (flag41)
						{
							current6["eff"] = variant11["eff"];
						}
						else
						{
							current6["eff"] = new Variant();
							current6["eff"]["eff"] = new Variant();
							current6["eff"]["eff"].setToArray();
						}
					}
				}
			}
			else
			{
				conf["npc"] = new Variant();
			}
			bool flag42 = conf.ContainsKey("chaAvatar");
			if (flag42)
			{
				conf["chaAvatar"] = conf["chaAvatar"].convertToDct("id");
				foreach (Variant current7 in conf["chaAvatar"].Values)
				{
					current7["ava"] = current7["ava"].convertToDct("id");
				}
			}
			else
			{
				conf["chaAvatar"] = new Variant();
			}
			bool flag43 = conf.ContainsKey("avapart");
			if (flag43)
			{
				conf["avapart"] = conf["avapart"].convertToDct("part");
			}
			else
			{
				conf["avapart"] = new Variant();
			}
			this.m_avaConf = conf;
		}

		protected void _formatAvatarConfOld(Variant conf)
		{
			bool flag = conf.ContainsKey("chaAni");
			if (flag)
			{
				conf["chaAni"] = conf["chaAni"].convertToDct("id");
				foreach (Variant current in conf["chaAni"].Values)
				{
					bool flag2 = !current.ContainsKey("ani");
					if (flag2)
					{
						current["ani"] = new Variant();
						current["ani"].setToDct();
					}
					else
					{
						current["ani"] = current["ani"].convertToDct("name");
						Variant variant = current["ani"];
						foreach (Variant current2 in variant.Values)
						{
							bool flag3 = variant.ContainsKey("preload") && variant["preload"]._bool && variant.ContainsKey("file");
							if (flag3)
							{
								IAsset asset = os.asset.getAsset<IAssetSkAnimation>(variant["file"]._str);
								asset.autoDisposeTime = -1f;
								asset.load();
							}
						}
					}
				}
			}
			else
			{
				conf["chaAni"] = new Variant();
				conf["chaAni"].setToDct();
			}
			bool flag4 = conf.ContainsKey("cha");
			if (flag4)
			{
				for (int i = 0; i < conf["cha"].Count; i++)
				{
					Variant variant2 = conf["cha"][i];
					bool flag5 = variant2.ContainsKey("preload") && variant2["preload"]._bool && variant2.ContainsKey("file");
					if (flag5)
					{
						IAsset asset2 = os.asset.getAsset<IAssetSkAniMesh>(variant2["file"]._str);
						asset2.autoDisposeTime = -1f;
						asset2.load();
					}
					bool flag6 = variant2.ContainsKey("ani");
					if (flag6)
					{
						Variant variant3 = conf["chaAni"][variant2["ani"]._str];
						bool flag7 = variant3 != null;
						if (flag7)
						{
							variant2["ani"] = variant3["ani"];
							variant2["defani"] = variant3["defani"];
							bool flag8 = variant3.ContainsKey("defori");
							if (flag8)
							{
								variant2["defori"] = variant3["defori"];
							}
						}
						else
						{
							DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new string[]
							{
								"cha config[",
								variant2["id"],
								"] ani[",
								variant2["ani"],
								"] config not exist"
							}));
							variant2["ani"] = new Variant();
							variant2["ani"]["ani"] = new Variant();
							variant2["ani"]["ani"].setToArray();
						}
					}
				}
				conf["cha"] = conf["cha"].convertToDct("id");
			}
			else
			{
				conf["cha"] = new Variant();
				conf["cha"].setToDct();
			}
			bool flag9 = conf.ContainsKey("ent");
			if (flag9)
			{
				conf["ent"] = conf["ent"].convertToDct("id");
			}
			else
			{
				conf["ent"] = new Variant();
				conf["ent"].setToDct();
			}
			bool flag10 = conf.ContainsKey("chaAvatar");
			if (flag10)
			{
				Variant variant4 = conf["chaAvatar"];
				for (int j = 0; j < variant4.Count; j++)
				{
					Variant variant5 = variant4[j];
					variant5["ava"] = variant5["ava"].convertToDct("id");
				}
				Variant variant6 = new Variant();
				for (int k = 0; k < variant4.Count; k++)
				{
					Variant variant7 = variant4[k];
					bool flag11 = -1 != variant7["chaid"]._str.IndexOf(",");
					string[] array;
					if (flag11)
					{
						array = variant7["chaid"]._str.Split(new char[]
						{
							','
						});
					}
					else
					{
						array = new string[]
						{
							variant7["chaid"]._str
						};
					}
					string[] array2 = array;
					for (int l = 0; l < array2.Length; l++)
					{
						string key = array2[l];
						variant6[key] = variant7;
					}
				}
				conf["chaAvatar"] = variant6;
			}
			else
			{
				conf["chaAvatar"] = new Variant();
				conf["chaAvatar"].setToDct();
			}
			bool flag12 = conf.ContainsKey("avapart");
			if (flag12)
			{
				conf["avapart"] = conf["avapart"].convertToDct("part");
			}
			else
			{
				conf["avapart"] = new Variant();
				conf["chaAvatar"].setToDct();
			}
			bool flag13 = conf.ContainsKey("cbapart");
			if (flag13)
			{
				conf["cbapart"] = conf["cbapart"].convertToDct("id");
			}
			else
			{
				conf["cbapart"] = new Variant();
				conf["cbapart"].setToDct();
			}
			bool flag14 = conf.ContainsKey("shadow");
			if (flag14)
			{
				conf["shadow"] = conf["shadow"].convertToDct("id");
			}
			else
			{
				conf["shadow"] = new Variant();
				conf["shadow"].setToDct();
			}
			this.m_avaConf = conf;
		}

		public void _formatAvatarConf(Variant conf)
		{
			bool compatibleMode = this.m_compatibleMode;
			if (compatibleMode)
			{
				this._formatAvatarConfOld(conf);
			}
			else
			{
				this._formatAvatarConfNew(conf);
			}
		}

		public void _formatMaterialConf(Variant conf)
		{
			bool flag = conf.ContainsKey("mtrl");
			if (flag)
			{
				conf["mtrl"] = conf["mtrl"].convertToDct("id");
			}
			else
			{
				conf["mtrl"] = new Variant();
				conf["mtrl"].setToDct();
			}
			this.m_mtrlConf = conf;
		}

		public Variant getAvatarPartConf(string partid)
		{
			return this.m_avaConf["avapart"][partid];
		}

		public Variant getCharacterConf(string chaid)
		{
			bool flag = !this.m_avaConf["cha"].ContainsKey(chaid);
			Variant result;
			if (flag)
			{
				DebugTrace.add(Define.DebugTrace.DTT_ERR, "Could not find character config: [chaid=" + chaid + "]");
				result = null;
			}
			else
			{
				result = this.m_avaConf["cha"][chaid];
			}
			return result;
		}

		public Variant getFriendConf(string frdid)
		{
			bool flag = !this.m_avaConf["npc"].ContainsKey(frdid);
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_avaConf["npc"][frdid];
			}
			return result;
		}

		public Variant getFriendAniConf(string frdid)
		{
			bool flag = !this.m_avaConf["npcAni"].ContainsKey(frdid);
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_avaConf["npcAni"][frdid]["anitime"];
			}
			return result;
		}

		public Variant getEntityConf(string entid)
		{
			bool flag = !this.m_avaConf["ent"].ContainsKey(entid);
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_avaConf["ent"][entid];
			}
			return result;
		}

		public Variant getBossConf(string bossid)
		{
			bool flag = !this.m_avaConf["boss"].ContainsKey(bossid);
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_avaConf["boss"][bossid];
			}
			return result;
		}

		public Variant getMountConf(string mountsid)
		{
			bool flag = !this.m_avaConf["mounts"].ContainsKey(mountsid);
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_avaConf["mounts"][mountsid];
			}
			return result;
		}

		public Variant getNPCConf(string npcid)
		{
			bool flag = !this.m_avaConf["npc"].ContainsKey(npcid);
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_avaConf["npc"][npcid];
			}
			return result;
		}

		public Variant getConf(string type, string id)
		{
			bool flag = !this.m_avaConf[type].ContainsKey(id);
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_avaConf[type][id];
			}
			return result;
		}

		public Variant getAvatarConf(string chaid, string avaID)
		{
			bool flag = !this.m_avaConf["chaAvatar"].ContainsKey(chaid);
			Variant result;
			if (flag)
			{
				DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new string[]
				{
					"Could not find avarta config: [chaid=",
					chaid,
					"] [avaid=",
					avaID,
					"]"
				}));
				result = null;
			}
			else
			{
				Variant variant = this.m_avaConf["chaAvatar"][chaid];
				bool flag2 = !variant["ava"].ContainsKey(avaID);
				if (flag2)
				{
					DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new string[]
					{
						"Could not find avarta config: [chaid=",
						chaid,
						"] [avaid=",
						avaID,
						"]"
					}));
					result = null;
				}
				else
				{
					result = variant["ava"][avaID];
				}
			}
			return result;
		}

		public Variant getAvatarConf(string chaid)
		{
			bool flag = !this.m_avaConf["chaAvatar"].ContainsKey(chaid);
			Variant result;
			if (flag)
			{
				DebugTrace.add(Define.DebugTrace.DTT_ERR, "Could not find avarta config: [chaid=" + chaid + "]");
				result = null;
			}
			else
			{
				result = this.m_avaConf["chaAvatar"][chaid];
			}
			return result;
		}

		public Variant getAllCharacterConf()
		{
			bool flag = !this.m_avaConf.ContainsKey("cha");
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_avaConf["cha"];
			}
			return result;
		}

		public Variant getCharPartConf(string partid)
		{
			return this.m_avaConf["chaAvatar"][partid];
		}

		public Variant getAllMaterialConf()
		{
			bool flag = this.m_mtrlConf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_mtrlConf["mtrl"];
			}
			return result;
		}
	}
}
