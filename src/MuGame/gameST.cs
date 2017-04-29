using Cross;
using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	public class gameST
	{
		public static bool _bUntestPlot = true;

		public static Action<int> REQ_PLOT_RES = null;

		public static Action REQ_PLAY_PLOT = null;

		public static Action REQ_STOP_PLOT = null;

		public static Action<GameObject, float> REQ_SET_FAST_BLOOM = null;

		public static Action<GameObject[], PLOT_CHARRES_TYPE, int, string[]> REV_CHARRES_LINKER = new Action<GameObject[], PLOT_CHARRES_TYPE, int, string[]>(GRMap.REV_CHARRES_LINKER);

		public static Action<GameObject[], string> REV_FXRES_LINKER = new Action<GameObject[], string>(GRMap.REV_FXRES_LINKER);

		public static Action<GameObject, int> REV_SOUNDRES_LINKER = new Action<GameObject, int>(GRMap.REV_SOUNDRES_LINKER);

		public static Action<string> REV_ZIMU_TEXT = new Action<string>(GRMap.REV_ZIMU_TEXT);

		public static Action<string> REV_PLOT_UI = new Action<string>(GRMap.REV_PLOT_UI);

		public static Action REV_RES_LIST_OK;

		public static Action REV_PLOT_PLAY_OVER;

		public static GameObject SHADOW_PREFAB;

		public static int HIT_Main_Color_nameID = -1;

		public static int HIT_Rim_Color_nameID = -1;

		public static int HIT_Rim_Width_nameID = -1;

		public static Material BORN_MTL;

		public static Material DEAD_MTL;

		public static Material CHAR_MTL;

		public static int DEAD_MT_AMOUNT = -1;

		public static int BORN_MT_AMOUNT = -1;

		public static int MTL_Main_Tex = -1;

		public static int MTL_Dead_Tex = -1;

		public static int MTL_Born_Tex = -1;

		protected GRWorld3D m_world;

		protected GRCharacter3D m_char;

		protected gameMain main;

		public gameMain m
		{
			get
			{
				return this.main;
			}
		}

		protected void _debugAvatar()
		{
			new CrossApp(true);
			ConfigManager confMgr = CrossApp.singleton.getPlugin("conf") as ConfigManager;
			Action<Variant> <>9__1;
			confMgr.loadExtendConfig("gconf/avatar", delegate(Variant v)
			{
				GraphManager.singleton._formatAvatarConf(v);
				ConfigManager arg_37_0 = confMgr;
				string arg_37_1 = "gconf/effect";
				Action<Variant> arg_37_2;
				if ((arg_37_2 = <>9__1) == null)
				{
					arg_37_2 = (<>9__1 = delegate(Variant vv)
					{
						GraphManager.singleton._formatEffectConf(vv);
						confMgr.loadExtendConfig("gconf/material", new Action<Variant>(this.<_debugAvatar>b__27_2));
					});
				}
				arg_37_0.loadExtendConfig(arg_37_1, arg_37_2);
			});
		}

		public void init(Variant parma)
		{
			gameST.loadBaseData();
			bool flag = this.main == null;
			if (flag)
			{
				this.main = new MuMain();
			}
			this.main.init(parma);
		}

		public static void loadBaseData()
		{
			gameST.SHADOW_PREFAB = Resources.Load<GameObject>("shadow/shadow");
			bool flag = gameST.SHADOW_PREFAB == null;
			if (flag)
			{
				gameST.SHADOW_PREFAB = new GameObject();
			}
			gameST.HIT_Main_Color_nameID = Shader.PropertyToID("_Color");
			gameST.HIT_Rim_Color_nameID = Shader.PropertyToID("_RimColor");
			gameST.HIT_Rim_Width_nameID = Shader.PropertyToID("_RimWidth");
			gameST.MTL_Main_Tex = Shader.PropertyToID("_MainTex");
			gameST.MTL_Dead_Tex = Shader.PropertyToID("_MainTex");
			gameST.MTL_Born_Tex = Shader.PropertyToID("_MainTex");
			gameST.BORN_MTL = U3DAPI.U3DResLoad<Material>("mtl/born_mtl");
			gameST.DEAD_MTL = U3DAPI.U3DResLoad<Material>("mtl/dead_mtl");
			gameST.DEAD_MT_AMOUNT = Shader.PropertyToID("_Amount");
			gameST.BORN_MT_AMOUNT = Shader.PropertyToID("_Amount");
			gameST.CHAR_MTL = U3DAPI.U3DResLoad<Material>("mtl/char_mtl");
			EnumMaterial.EMT_EQUIP_L = U3DAPI.U3DResLoad<Material>("mtl/equip_low");
			EnumMaterial.EMT_EQUIP_H = U3DAPI.U3DResLoad<Material>("mtl/equip_high");
			EnumMaterial.EMT_SKILL_HIDE = U3DAPI.U3DResLoad<Material>("mtl/skill_hide");
		}

		public void init(string server_config_url, string server_ip, uint server_id, uint port, uint uid, uint clnt, string token, string mainConfig)
		{
			LGPlatInfo.inst.firstAnalysisPoint(server_id, uid);
			gameST.loadBaseData();
			DebugTrace.Printf(os.sys.windowWidth + "," + os.sys.windowHeight, new string[0]);
			Variant variant = new Variant();
			variant["server_config_url"] = server_config_url;
			variant["server_id"] = server_id;
			variant["mainConfig"] = mainConfig;
			variant["outgamevar"] = new Variant();
			variant["outgamevar"]["server_ip"] = server_ip;
			variant["outgamevar"]["server_port"] = port;
			variant["outgamevar"]["uid"] = uid;
			variant["outgamevar"]["token"] = token;
			variant["outgamevar"]["clnt"] = clnt;
			debug.Log("初始化进来.............0000000000000000");
			bool flag = this.main == null;
			if (flag)
			{
				this.main = new MuMain();
			}
			this.main.init(variant);
			debug.Log("初始化进来.............1111111111111111");
		}

		private void test()
		{
			Variant variant = new Variant(1);
			Variant variant2 = new Variant(1);
			Variant variant3 = new Variant();
			Variant variant4 = new Variant();
			variant3["x"] = variant;
			variant4["b"] = variant2;
			bool flag = variant == variant2;
			if (flag)
			{
				DebugTrace.Printf("x", new string[0]);
			}
			bool flag2 = variant3["x"] == variant4["b"];
			if (flag2)
			{
				DebugTrace.Printf("x", new string[0]);
			}
		}
	}
}
