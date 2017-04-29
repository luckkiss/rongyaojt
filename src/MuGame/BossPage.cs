using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class BossPage
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly BossPage.<>c <>9 = new BossPage.<>c();

			public static Action<GameObject> <>9__25_0;

			public static Action<GameObject> <>9__25_2;

			public static Action<GameObject> <>9__25_3;

			public static Action<GameObject> <>9__25_5;

			public static Action<GameObject> <>9__25_6;

			public static Action<GameObject> <>9__25_8;

			internal void <Init>b__25_0(GameObject go)
			{
				BossPage.<>c__DisplayClass25_0 <>c__DisplayClass25_ = new BossPage.<>c__DisplayClass25_0();
				SXML sXML = XMLMgr.instance.GetSXML("worldboss", "");
				SXML node = sXML.GetNode("boss", "id==1");
				<>c__DisplayClass25_.mapId = node.getInt("target_map_id");
				<>c__DisplayClass25_.pos = new Vector3(node.getFloat("target_x"), 0f, node.getFloat("target_y"));
				InterfaceMgr.getInstance().open(InterfaceMgr.TRANSMIT_PANEL, (ArrayList)new TransmitData
				{
					mapId = <>c__DisplayClass25_.mapId,
					check_beforeShow = true,
					handle_customized_afterTransmit = new Action(<>c__DisplayClass25_.<Init>b__1),
					closeWinName = new string[]
					{
						InterfaceMgr.A3_ELITEMON
					}
				}, false);
			}

			internal void <Init>b__25_2(GameObject g)
			{
				flytxt.instance.fly("等级不足！", 0, default(Color), null);
			}

			internal void <Init>b__25_3(GameObject go)
			{
				BossPage.<>c__DisplayClass25_1 <>c__DisplayClass25_ = new BossPage.<>c__DisplayClass25_1();
				SXML sXML = XMLMgr.instance.GetSXML("worldboss", "");
				SXML node = sXML.GetNode("boss", "id==2");
				<>c__DisplayClass25_.mapId = node.getInt("target_map_id");
				<>c__DisplayClass25_.pos = new Vector3(node.getFloat("target_x"), 0f, node.getFloat("target_y"));
				InterfaceMgr.getInstance().open(InterfaceMgr.TRANSMIT_PANEL, (ArrayList)new TransmitData
				{
					mapId = <>c__DisplayClass25_.mapId,
					check_beforeShow = true,
					handle_customized_afterTransmit = new Action(<>c__DisplayClass25_.<Init>b__4),
					closeWinName = new string[]
					{
						InterfaceMgr.A3_ELITEMON
					}
				}, false);
			}

			internal void <Init>b__25_5(GameObject g)
			{
				flytxt.instance.fly("等级不足！", 0, default(Color), null);
			}

			internal void <Init>b__25_6(GameObject go)
			{
				BossPage.<>c__DisplayClass25_2 <>c__DisplayClass25_ = new BossPage.<>c__DisplayClass25_2();
				SXML sXML = XMLMgr.instance.GetSXML("worldboss", "");
				SXML node = sXML.GetNode("boss", "id==3");
				<>c__DisplayClass25_.mapId = node.getInt("target_map_id");
				<>c__DisplayClass25_.pos = new Vector3(node.getFloat("target_x"), 0f, node.getFloat("target_y"));
				InterfaceMgr.getInstance().open(InterfaceMgr.TRANSMIT_PANEL, (ArrayList)new TransmitData
				{
					mapId = <>c__DisplayClass25_.mapId,
					check_beforeShow = true,
					handle_customized_afterTransmit = new Action(<>c__DisplayClass25_.<Init>b__7),
					closeWinName = new string[]
					{
						InterfaceMgr.A3_ELITEMON
					}
				}, false);
			}

			internal void <Init>b__25_8(GameObject g)
			{
				flytxt.instance.fly("等级不足！", 0, default(Color), null);
			}
		}

		public GameObject prefabItemBossInfo;

		public GameObject goBossInfoScroll;

		public GameObject goBossRewardScroll;

		private GameObject curMonAvatar;

		private GameObject curMonScene;

		private GameObject camObj;

		public GameObject pboss1;

		public GameObject pboss2;

		public BaseButton btnBoss1_transmit;

		public BaseButton btnBoss2_transmit;

		public BaseButton btnBoss3_transmit;

		public Text textBoss1_RspnLftTm;

		public Text textBoss2_RspnLftTm;

		public Text textBoss3_RspnLftTm;

		public GameObject curSelected;

		public GameObject owner;

		private Dictionary<uint, List<uint>> dicReward;

		private Queue<GameObject> qGoReward;

		private Dictionary<uint, GameObject> dicBossItem;

		private static BossPage instance;

		public List<BaseButton> btnBoss;

		private uint? firstBossId;

		public static BossPage Instance
		{
			get
			{
				BossPage arg_15_0;
				if ((arg_15_0 = BossPage.instance) == null)
				{
					arg_15_0 = (BossPage.instance = new BossPage());
				}
				return arg_15_0;
			}
			set
			{
				BossPage.instance = value;
			}
		}

		public uint FirstBossId
		{
			get
			{
				bool flag = !this.firstBossId.HasValue;
				if (flag)
				{
					this.firstBossId = new uint?(new List<uint>(this.dicBossItem.Keys)[0]);
				}
				return this.firstBossId.Value;
			}
		}

		private BossPage()
		{
			BossPage.Instance = this;
			this.dicReward = new Dictionary<uint, List<uint>>();
			this.qGoReward = new Queue<GameObject>();
			this.dicBossItem = new Dictionary<uint, GameObject>();
			this.btnBoss = new List<BaseButton>();
			this.owner = A3_EliteMonster.Instance.transform.FindChild("con_page/container/bossPage").gameObject;
			this.prefabItemBossInfo = this.owner.transform.FindChild("Template/item_boss").gameObject;
			this.goBossInfoScroll = this.owner.transform.FindChild("scrollrect/scrollview").gameObject;
			this.goBossRewardScroll = this.owner.transform.FindChild("scrollmask_reward/scrollview").gameObject;
			this.Init();
			List<SXML> nodeList = XMLMgr.instance.GetSXML("worldboss", "").GetNodeList("boss", "");
			for (int i = 0; i < nodeList.Count; i++)
			{
				int @int = nodeList[i].getInt("id");
				uint monId = nodeList[i].getUint("boss_id");
				string name = "boss_" + @int;
				Transform transform = this.owner.transform.FindChild(name);
				bool flag = transform != null;
				if (flag)
				{
					this.dicBossItem.Add(monId, transform.gameObject);
					BaseButton baseButton = new BaseButton(transform.FindChild("btn"), 1, 1);
					baseButton.onClick = delegate(GameObject go)
					{
						GameObject expr_0C = this.curSelected;
						if (expr_0C != null)
						{
							expr_0C.SetActive(false);
						}
						this.curSelected = go.transform.FindChild("selected").gameObject;
						this.curSelected.SetActive(true);
						this.CreateModel(XMLMgr.instance.GetSXML("monsters.monsters", "id==" + monId).getString("elite_obj"), monId);
						A3_EliteMonster.Instance.CurrentSelectedMonsterId = monId;
						A3_EliteMonster.Instance.ShowRewardItemIcon(null);
					};
					this.btnBoss.Add(baseButton);
				}
				ModelBase<A3_EliteMonsterModel>.getInstance().LoadReward(monId);
			}
		}

		public void Init()
		{
			this.btnBoss1_transmit = new BaseButton(this.owner.transform.FindChild("boss_1/btn_transmit"), 1, 1);
			BaseButton arg_48_0 = this.btnBoss1_transmit;
			Action<GameObject> arg_48_1;
			if ((arg_48_1 = BossPage.<>c.<>9__25_0) == null)
			{
				arg_48_1 = (BossPage.<>c.<>9__25_0 = new Action<GameObject>(BossPage.<>c.<>9.<Init>b__25_0));
			}
			arg_48_0.onClick = arg_48_1;
			BaseButton arg_73_0 = this.btnBoss1_transmit;
			Action<GameObject> arg_73_1;
			if ((arg_73_1 = BossPage.<>c.<>9__25_2) == null)
			{
				arg_73_1 = (BossPage.<>c.<>9__25_2 = new Action<GameObject>(BossPage.<>c.<>9.<Init>b__25_2));
			}
			arg_73_0.onClickFalse = arg_73_1;
			this.btnBoss2_transmit = new BaseButton(this.owner.transform.FindChild("boss_2/btn_transmit"), 1, 1);
			BaseButton arg_C0_0 = this.btnBoss2_transmit;
			Action<GameObject> arg_C0_1;
			if ((arg_C0_1 = BossPage.<>c.<>9__25_3) == null)
			{
				arg_C0_1 = (BossPage.<>c.<>9__25_3 = new Action<GameObject>(BossPage.<>c.<>9.<Init>b__25_3));
			}
			arg_C0_0.onClick = arg_C0_1;
			BaseButton arg_EB_0 = this.btnBoss2_transmit;
			Action<GameObject> arg_EB_1;
			if ((arg_EB_1 = BossPage.<>c.<>9__25_5) == null)
			{
				arg_EB_1 = (BossPage.<>c.<>9__25_5 = new Action<GameObject>(BossPage.<>c.<>9.<Init>b__25_5));
			}
			arg_EB_0.onClickFalse = arg_EB_1;
			this.btnBoss3_transmit = new BaseButton(this.owner.transform.FindChild("boss_3/btn_transmit"), 1, 1);
			BaseButton arg_138_0 = this.btnBoss3_transmit;
			Action<GameObject> arg_138_1;
			if ((arg_138_1 = BossPage.<>c.<>9__25_6) == null)
			{
				arg_138_1 = (BossPage.<>c.<>9__25_6 = new Action<GameObject>(BossPage.<>c.<>9.<Init>b__25_6));
			}
			arg_138_0.onClick = arg_138_1;
			BaseButton arg_163_0 = this.btnBoss3_transmit;
			Action<GameObject> arg_163_1;
			if ((arg_163_1 = BossPage.<>c.<>9__25_8) == null)
			{
				arg_163_1 = (BossPage.<>c.<>9__25_8 = new Action<GameObject>(BossPage.<>c.<>9.<Init>b__25_8));
			}
			arg_163_0.onClickFalse = arg_163_1;
			new BaseButton(this.owner.transform.FindChild("boss_1/help"), 1, 1).onClick = new Action<GameObject>(this.Help1);
			new BaseButton(this.owner.transform.FindChild("boss_2/help"), 1, 1).onClick = new Action<GameObject>(this.Help2);
			new BaseButton(this.owner.transform.FindChild("boss_3/help"), 1, 1).onClick = new Action<GameObject>(this.Help3);
			new BaseButton(this.owner.transform.FindChild("pHelp/panel_help/bg/closeBtn"), 1, 1).onClick = new Action<GameObject>(this.CloseHelp);
			new BaseButton(this.owner.transform.FindChild("pHelp/panel_help/bg_0"), 1, 1).onClick = new Action<GameObject>(this.CloseHelp);
			this.textBoss1_RspnLftTm = this.owner.transform.FindChild("boss_1/time").GetComponent<Text>();
			this.textBoss2_RspnLftTm = this.owner.transform.FindChild("boss_2/time").GetComponent<Text>();
			this.textBoss3_RspnLftTm = this.owner.transform.FindChild("boss_3/time").GetComponent<Text>();
			this.pboss1 = this.owner.transform.FindChild("boss_1").gameObject;
			this.pboss2 = this.owner.transform.FindChild("boss_2").gameObject;
		}

		private void Help1(GameObject go)
		{
			string s = ModelBase<A3_EliteMonsterModel>.getInstance().s10;
			string s2 = ModelBase<A3_EliteMonsterModel>.getInstance().s11;
			string s3 = ModelBase<A3_EliteMonsterModel>.getInstance().s12;
			List<int> s4 = ModelBase<A3_EliteMonsterModel>.getInstance().s13;
			this.ShowHelp(s, s2, s3, s4);
		}

		private void Help2(GameObject go)
		{
			string s = ModelBase<A3_EliteMonsterModel>.getInstance().s20;
			string s2 = ModelBase<A3_EliteMonsterModel>.getInstance().s21;
			string s3 = ModelBase<A3_EliteMonsterModel>.getInstance().s22;
			List<int> s4 = ModelBase<A3_EliteMonsterModel>.getInstance().s23;
			this.ShowHelp(s, s2, s3, s4);
		}

		private void Help3(GameObject go)
		{
			string s = ModelBase<A3_EliteMonsterModel>.getInstance().s30;
			string s2 = ModelBase<A3_EliteMonsterModel>.getInstance().s31;
			string s3 = ModelBase<A3_EliteMonsterModel>.getInstance().s32;
			List<int> s4 = ModelBase<A3_EliteMonsterModel>.getInstance().s33;
			this.ShowHelp(s, s2, s3, s4);
		}

		private void ShowHelp(string tittle, string a, string b, List<int> ids)
		{
			Transform transform = this.owner.transform.FindChild("pHelp");
			Text component = transform.FindChild("panel_help/bg/descTxt1").GetComponent<Text>();
			Text component2 = transform.FindChild("panel_help/bg/descTxt2").GetComponent<Text>();
			component.text = "\t" + a;
			component2.text = "\t" + b;
			Transform transform2 = transform.FindChild("panel_help/bg/items/scroll/content");
			Transform[] componentsInChildren = transform2.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform3 = componentsInChildren[i];
				bool flag = transform3.parent == transform2;
				if (flag)
				{
					UnityEngine.Object.Destroy(transform3.gameObject);
				}
			}
			foreach (int current in ids)
			{
				a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)current);
				this.SetIcon(itemDataById, transform2, -1);
			}
			transform.gameObject.SetActive(true);
			GridLayoutGroup component3 = transform2.GetComponent<GridLayoutGroup>();
			RectTransform component4 = transform2.GetComponent<RectTransform>();
			component4.sizeDelta = new Vector2((component3.cellSize.y + component3.spacing.x) * (float)ids.Count, 0f);
			transform.FindChild("panel_help/bg/title_bg/title").GetComponent<Text>().text = tittle;
			component4.anchoredPosition3D = new Vector3(0f, component4.anchoredPosition3D.y, 0f);
		}

		private GameObject SetIcon(a3_ItemData data, Transform parent, int num = -1)
		{
			data.borderfile = "icon/itemborder/b039_0" + data.quality;
			data.item_type = 0;
			GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(data, true, num, 1f, false, -1, 0, false, false, false, -1, false, false);
			gameObject.transform.SetParent(parent, false);
			return gameObject;
		}

		public void OnShowed()
		{
			this.Refresh(null);
			this.CloseHelp(null);
			for (int i = 1; i < 25; i++)
			{
				this.GetNextTime(i);
			}
			BaseProxy<A3_ActiveProxy>.getInstance().addEventListener(EliteMonsterProxy.EVENT_BOSSOPSUCCESS, new Action<GameEvent>(this.RefreshTextRspnTime));
			bool flag = this.btnBoss.Count > 0;
			if (flag)
			{
				this.btnBoss[0].onClick(this.btnBoss[0].gameObject);
			}
			BaseProxy<A3_ActiveProxy>.getInstance().SendLoadActivies();
		}

		public void OnClosed()
		{
			BaseProxy<A3_ActiveProxy>.getInstance().removeEventListener(EliteMonsterProxy.EVENT_BOSSOPSUCCESS, new Action<GameEvent>(this.RefreshTextRspnTime));
		}

		private void RefreshTextRspnTime(GameEvent e = null)
		{
			TimeSpan timeSpan = new TimeSpan(0, 0, muNetCleint.instance.CurServerTimeStamp);
			int respawnTime = this.GetRespawnTime(ModelBase<A3_EliteMonsterModel>.getInstance().i11, ModelBase<A3_EliteMonsterModel>.getInstance().i12);
			bool flag = respawnTime >= 0;
			if (flag)
			{
				this.textBoss1_RspnLftTm.text = respawnTime + "点刷新";
			}
			respawnTime = this.GetRespawnTime(ModelBase<A3_EliteMonsterModel>.getInstance().i21, ModelBase<A3_EliteMonsterModel>.getInstance().i22);
			bool flag2 = respawnTime >= 0;
			if (flag2)
			{
				this.textBoss2_RspnLftTm.text = respawnTime + "点刷新";
			}
			respawnTime = this.GetRespawnTime(ModelBase<A3_EliteMonsterModel>.getInstance().i31, ModelBase<A3_EliteMonsterModel>.getInstance().i32);
			bool flag3 = respawnTime >= 0;
			if (flag3)
			{
				this.textBoss3_RspnLftTm.text = respawnTime + "点刷新";
			}
		}

		private int GetRespawnTime(int a, int b)
		{
			int num = a;
			DateTime nextTime = this.GetNextTime(0);
			int hour = nextTime.Hour;
			bool flag = hour >= a && hour < b;
			if (flag)
			{
				num = b;
			}
			bool flag2 = num - hour <= 1 && num - hour > 0;
			int result;
			if (flag2)
			{
				result = (int)(-1.0 * (this.GetNextTime(1) - nextTime).TotalSeconds);
			}
			else
			{
				result = num;
			}
			return result;
		}

		private DateTime GetNextTime(int h)
		{
			int num = muNetCleint.instance.CurServerTimeStamp / 3600 * 3600 + h * 60 * 60;
			bool flag = h == 0;
			if (flag)
			{
				num = muNetCleint.instance.CurServerTimeStamp + h * 60 * 60;
			}
			DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			long ticks = long.Parse(num + "0000000");
			TimeSpan value = new TimeSpan(ticks);
			return dateTime.Add(value);
		}

		private void Refresh(GameEvent e = null)
		{
			this.RefreshPanel(1);
			this.RefreshPanel(2);
			this.RefreshPanel(3);
			this.RefreshTextRspnTime(null);
		}

		private void RefreshPanel(int i)
		{
			SXML sXML = XMLMgr.instance.GetSXML("worldboss", "");
			SXML node = sXML.GetNode("boss", "id==" + i);
			string[] array = node.getString("level_limit").Split(new char[]
			{
				','
			});
			int num = int.Parse(array[0]);
			int num2 = int.Parse(array[1]);
			bool flag = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl > (ulong)((long)num) || ((ulong)ModelBase<PlayerModel>.getInstance().up_lvl == (ulong)((long)num) && (ulong)ModelBase<PlayerModel>.getInstance().lvl > (ulong)((long)num2));
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = i == 1;
				if (flag3)
				{
					this.btnBoss1_transmit.interactable = true;
				}
				else
				{
					bool flag4 = i == 2;
					if (flag4)
					{
						this.btnBoss2_transmit.interactable = true;
					}
					else
					{
						bool flag5 = i == 3;
						if (flag5)
						{
							this.btnBoss3_transmit.interactable = true;
						}
					}
				}
			}
			else
			{
				bool flag6 = i == 1;
				if (flag6)
				{
					this.btnBoss1_transmit.interactable = false;
				}
				else
				{
					bool flag7 = i == 2;
					if (flag7)
					{
						this.btnBoss2_transmit.interactable = false;
					}
					else
					{
						bool flag8 = i == 3;
						if (flag8)
						{
							this.btnBoss3_transmit.interactable = false;
						}
					}
				}
			}
		}

		public void Update()
		{
		}

		private void RefreshTime(GameEvent e)
		{
			Variant data = e.data;
			this.ChooseTime(ModelBase<A3_EliteMonsterModel>.getInstance().i11, ModelBase<A3_EliteMonsterModel>.getInstance().i12);
		}

		public void ChooseTime(int a, int b)
		{
			int respawnTime = this.GetRespawnTime(ModelBase<A3_EliteMonsterModel>.getInstance().i11, ModelBase<A3_EliteMonsterModel>.getInstance().i12);
			int respawnTime2 = this.GetRespawnTime(ModelBase<A3_EliteMonsterModel>.getInstance().i11 + 1, ModelBase<A3_EliteMonsterModel>.getInstance().i12 + 1);
			int hour = this.GetNextTime(0).Hour;
			bool flag = hour > a && hour < b;
			if (flag)
			{
				this.textBoss1_RspnLftTm.text = b + "点刷新";
				this.textBoss2_RspnLftTm.text = b + "点刷新";
				this.textBoss3_RspnLftTm.text = b + "点刷新";
			}
			else
			{
				bool flag2 = hour < a || hour > b;
				if (flag2)
				{
					this.textBoss1_RspnLftTm.text = a + "点刷新";
					this.textBoss2_RspnLftTm.text = a + "点刷新";
					this.textBoss3_RspnLftTm.text = a + "点刷新";
				}
			}
		}

		private void CloseHelp(GameObject go)
		{
			Transform transform = this.owner.transform.FindChild("pHelp");
			transform.gameObject.SetActive(false);
		}

		public void Clear()
		{
			for (int i = 0; i < this.qGoReward.Count; i++)
			{
				UnityEngine.Object.Destroy(this.qGoReward.Dequeue());
			}
		}

		public GameObject CreateModel(string objName, uint monId)
		{
			bool flag = this.curMonAvatar != null;
			if (flag)
			{
				UnityEngine.Object.DestroyImmediate(this.curMonAvatar);
				UnityEngine.Object.DestroyImmediate(this.camObj);
				UnityEngine.Object.DestroyImmediate(this.curMonScene);
			}
			GameObject gameObject = Resources.Load<GameObject>("monster/" + objName);
			GameObject original = Resources.Load<GameObject>("profession/avatar_ui/emonShow_scene");
			bool flag2 = gameObject == null;
			GameObject result;
			if (flag2)
			{
				Debug.LogError("monsters.xml:elite_obj字段配置错误");
				result = null;
			}
			else
			{
				float @float = XMLMgr.instance.GetSXML("monsters.monsters", "id==" + monId).getFloat("avatar_height");
				float float2 = XMLMgr.instance.GetSXML("monsters.monsters", "id==" + monId).getFloat("avatar_scale");
				this.curMonAvatar = (UnityEngine.Object.Instantiate(gameObject, new Vector3(-153.3f, @float, 0f), Quaternion.identity) as GameObject);
				this.curMonScene = UnityEngine.Object.Instantiate<GameObject>(original);
				Transform[] componentsInChildren = this.curMonAvatar.GetComponentsInChildren<Transform>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Transform transform = componentsInChildren[i];
					transform.gameObject.layer = EnumLayer.LM_FX;
				}
				Transform[] componentsInChildren2 = this.curMonScene.GetComponentsInChildren<Transform>();
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					Transform transform2 = componentsInChildren2[j];
					bool flag3 = transform2.name == "scene_ta";
					if (flag3)
					{
						transform2.gameObject.layer = EnumLayer.LM_ROLE_INVISIBLE;
					}
					else
					{
						transform2.gameObject.layer = EnumLayer.LM_FX;
					}
				}
				Transform transform3 = this.curMonAvatar.transform.FindChild("model");
				transform3.gameObject.AddComponent<Summon_Base_Event>();
				GameObject original2 = Resources.Load<GameObject>("profession/avatar_ui/scene_ui_camera_worldboss");
				this.camObj = UnityEngine.Object.Instantiate<GameObject>(original2);
				Camera componentInChildren = this.camObj.GetComponentInChildren<Camera>();
				bool flag4 = componentInChildren != null;
				if (flag4)
				{
					float orthographicSize = componentInChildren.orthographicSize * 1920f / 1080f * (float)Screen.height / (float)Screen.width;
					componentInChildren.orthographicSize = orthographicSize;
				}
				transform3.Rotate(Vector3.up, 180f);
				transform3.transform.localScale = ((float2 > 0f) ? float2 : 1f) * Vector3.one;
				Transform[] componentsInChildren3 = gameObject.GetComponentsInChildren<Transform>(true);
				for (int k = 0; k < componentsInChildren3.Length; k++)
				{
					Transform transform4 = componentsInChildren3[k];
					transform4.gameObject.layer = EnumLayer.LM_FX;
				}
				result = this.curMonAvatar;
			}
			return result;
		}

		public void HideOrShowModel(bool showOrHide = false)
		{
			bool flag = this.curMonAvatar != null && this.curMonAvatar.activeSelf != showOrHide;
			if (flag)
			{
				this.curMonAvatar.SetActive(showOrHide);
			}
			bool flag2 = this.camObj != null && this.camObj.activeSelf != showOrHide;
			if (flag2)
			{
				this.camObj.SetActive(showOrHide);
			}
		}

		public void DestroyModel()
		{
			bool flag = this.curMonAvatar != null;
			if (flag)
			{
				UnityEngine.Object.DestroyImmediate(this.curMonAvatar);
			}
			bool flag2 = this.camObj != null;
			if (flag2)
			{
				UnityEngine.Object.DestroyImmediate(this.camObj);
			}
			bool flag3 = this.curMonScene != null;
			if (flag3)
			{
				UnityEngine.Object.DestroyImmediate(this.curMonScene);
			}
		}

		public void InitModel(bool isThisPageShow = false)
		{
			bool flag = isThisPageShow && this.curMonAvatar == null;
			if (flag)
			{
				A3_EliteMonster.Instance.CurrentSelectedMonsterId = this.FirstBossId;
				this.CreateModel(XMLMgr.instance.GetSXML("monsters.monsters", "id==" + this.FirstBossId).getString("elite_obj"), this.FirstBossId);
			}
		}
	}
}
