using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_teamPanel : BaseShejiao
	{
		public class itemTeamInfoPrefab
		{
			private GameObject prefab;

			private GameObject iconCarr_head;

			private GameObject iconCarr_zy;

			private Text txtCapatain;

			private Text txtLvl;

			private Text txtKnightage;

			private Text txtMap;

			private Text txtMembCount;

			private const string strMembCount = "{0}/{1}";

			public uint tid;

			public Transform root;

			public int limited_dj = 0;

			private int lvl;

			private int up_lvl;

			public itemTeamInfoPrefab(Transform trans)
			{
				this.Init(trans);
			}

			private void Init(Transform trans)
			{
				bool flag = this.prefab == null;
				if (flag)
				{
					this.prefab = UnityEngine.Object.Instantiate<GameObject>(trans.FindChild("itemPrefabs/itemTeamInfo").gameObject);
				}
				else
				{
					this.prefab = UnityEngine.Object.Instantiate<GameObject>(this.prefab);
				}
				this.root = this.prefab.transform;
				this.iconCarr_zy = this.root.FindChild("team_pre/2/zy").gameObject;
				this.iconCarr_head = this.root.FindChild("team_pre/1").gameObject;
				this.prefab.transform.SetParent(trans.FindChild("right/main/body/Panel/contains"), false);
				this.root.GetComponent<Toggle>().group = trans.FindChild("right/main/body/Panel/contains").GetComponent<ToggleGroup>();
				this.prefab.transform.localScale = Vector3.one;
				this.prefab.transform.SetAsLastSibling();
				bool flag2 = !this.prefab.activeSelf;
				if (flag2)
				{
					this.prefab.SetActive(true);
				}
				this.txtCapatain = this.prefab.transform.FindChild("team_pre/2/name").GetComponent<Text>();
				this.txtLvl = this.prefab.transform.FindChild("team_pre/2/dj").GetComponent<Text>();
				this.txtKnightage = this.prefab.transform.FindChild("txtKnightage").GetComponent<Text>();
				this.txtMap = this.prefab.transform.FindChild("team_pre/textMap").GetComponent<Text>();
				this.txtMembCount = this.prefab.transform.FindChild("txtMembCount").GetComponent<Text>();
				BaseButton baseButton = new BaseButton(this.prefab.transform.FindChild("btnWatch"), 1, 1);
				baseButton.onClick = new Action<GameObject>(this.onBtnWatchClick);
			}

			public int Set_Limited_dj(int i)
			{
				this.lvl = 0;
				this.up_lvl = 0;
				SXML sXML = XMLMgr.instance.GetSXML("func_open.team_lv_limit", "id==" + i);
				this.lvl = sXML.getInt("lv");
				this.up_lvl = sXML.getInt("zhuan");
				return this.up_lvl * 100 + this.lvl;
			}

			public void Set(ItemTeamData itd)
			{
				this.tid = itd.teamId;
				uint lv = ModelBase<PlayerModel>.getInstance().up_lvl * 100u + ModelBase<PlayerModel>.getInstance().lvl;
				this.txtCapatain.text = itd.name;
				this.txtLvl.text = string.Concat(new object[]
				{
					itd.zhuan,
					"转",
					itd.lvl.ToString(),
					"级"
				});
				this.txtKnightage.text = itd.knightage;
				bool flag = a3_teamPanel._instance.iconSpriteDic.ContainsKey(itd.carr);
				if (flag)
				{
					this.iconCarr_zy.transform.FindChild(string.Concat(itd.carr)).gameObject.SetActive(true);
					this.iconCarr_head.transform.FindChild(string.Concat(itd.carr)).gameObject.SetActive(true);
				}
				this.txtMap.text = ((SvrMapConfig.instance.getSingleMapConf(itd.mapId) == null) ? "" : SvrMapConfig.instance.getSingleMapConf(itd.mapId)["map_name"]._str);
				this.txtMembCount.text = string.Format("{0}/{1}", itd.curcnt, 5);
				bool flag2 = itd.members != null;
				if (flag2)
				{
					for (int i = 0; i < itd.members.Count; i++)
					{
						this.prefab.transform.FindChild(string.Concat(new object[]
						{
							"team_pre/3/zy/teamer/",
							i + 1,
							"/",
							itd.members[i]
						})).gameObject.SetActive(true);
					}
				}
				int num = 0;
				uint ltpid = itd.ltpid;
				switch (ltpid)
				{
				case 0u:
					num = 5;
					break;
				case 1u:
					num = 4;
					break;
				case 2u:
					num = 3;
					break;
				default:
					if (ltpid != 105u)
					{
						if (ltpid == 108u)
						{
							num = 1;
						}
					}
					else
					{
						num = 2;
					}
					break;
				}
				this.limited_dj = this.Set_Limited_dj(num);
				bool flag3 = this.limited_dj == 0;
				if (flag3)
				{
					this.prefab.transform.FindChild("team_pre/3/dj").GetComponent<Text>().text = "无等级限制";
				}
				else
				{
					this.prefab.transform.FindChild("team_pre/3/dj").GetComponent<Text>().text = string.Concat(new object[]
					{
						this.up_lvl,
						"转",
						this.lvl,
						"级"
					});
				}
				bool flag4 = itd.curcnt >= 5u;
				if (flag4)
				{
					this.prefab.transform.FindChild("team_pre/4/full").gameObject.SetActive(true);
					this.prefab.transform.FindChild("team_pre/4/apply").gameObject.SetActive(false);
					this.prefab.transform.FindChild("team_pre/4/applyed").gameObject.SetActive(false);
				}
				bool flag5 = BaseProxy<TeamProxy>.getInstance().MyTeamData == null && itd.curcnt < 5u;
				if (flag5)
				{
					this.prefab.transform.FindChild("team_pre/4/full").gameObject.SetActive(false);
					this.prefab.transform.FindChild("team_pre/4/apply").gameObject.SetActive(true);
					this.prefab.transform.FindChild("team_pre/4/applyed").gameObject.SetActive(false);
				}
				bool flag6 = itd.curcnt < 5u && !this.prefab.transform.FindChild("team_pre/4/applyed").gameObject.activeInHierarchy && (ulong)lv >= (ulong)((long)this.limited_dj);
				if (flag6)
				{
					this.prefab.transform.FindChild("team_pre/4/full").gameObject.SetActive(false);
					this.prefab.transform.FindChild("team_pre/4/apply").gameObject.SetActive(true);
					this.prefab.transform.FindChild("team_pre/4/applyed").gameObject.SetActive(false);
					new BaseButton(this.prefab.transform.FindChild("team_pre/4/apply"), 1, 1).onClick = delegate(GameObject oo)
					{
						bool flag7 = BaseProxy<TeamProxy>.getInstance().MyTeamData == null && (ulong)lv >= (ulong)((long)this.limited_dj);
						if (flag7)
						{
							BaseProxy<TeamProxy>.getInstance().SendApplyJoinTeam(itd.teamId);
							this.prefab.transform.FindChild("team_pre/4/apply").gameObject.SetActive(false);
							this.prefab.transform.FindChild("team_pre/4/applyed").gameObject.SetActive(true);
						}
						else
						{
							bool flag8 = BaseProxy<TeamProxy>.getInstance().MyTeamData != null && (ulong)lv >= (ulong)((long)this.limited_dj);
							if (flag8)
							{
								flytxt.instance.fly("请先退出当前队伍", 0, default(Color), null);
							}
							else
							{
								flytxt.instance.fly("未达到队伍等级要求！", 0, default(Color), null);
							}
						}
					};
				}
			}

			private void Set_apply(Transform go, ItemTeamData data)
			{
				uint num = ModelBase<PlayerModel>.getInstance().up_lvl * 100u + ModelBase<PlayerModel>.getInstance().lvl;
				bool flag = data.curcnt >= 5u;
				if (flag)
				{
					go.transform.FindChild("4/full").gameObject.SetActive(true);
					go.transform.FindChild("4/apply").gameObject.SetActive(false);
					go.transform.FindChild("4/applyed").gameObject.SetActive(false);
				}
				bool flag2 = data.curcnt < 5u && !go.transform.FindChild("4/applyed").gameObject.activeInHierarchy && (ulong)num >= (ulong)((long)this.limited_dj);
				if (flag2)
				{
					go.transform.FindChild("4/full").gameObject.SetActive(false);
					go.transform.FindChild("4/apply").gameObject.SetActive(true);
					go.transform.FindChild("4/applyed").gameObject.SetActive(false);
					new BaseButton(go.transform.FindChild("4/apply"), 1, 1).onClick = delegate(GameObject oo)
					{
						bool flag3 = BaseProxy<TeamProxy>.getInstance().MyTeamData == null;
						if (flag3)
						{
							BaseProxy<TeamProxy>.getInstance().SendApplyJoinTeam(data.teamId);
							go.transform.FindChild("4/apply").gameObject.SetActive(false);
							go.transform.FindChild("4/applyed").gameObject.SetActive(true);
						}
						else
						{
							flytxt.instance.fly("请先退出当前队伍", 0, default(Color), null);
						}
					};
				}
			}

			private void onBtnWatchClick(GameObject go)
			{
				worldmap.getmapid = false;
				BaseProxy<TeamProxy>.getInstance().SendWatchTeamInfo(this.tid);
				TeamProxy.WatchTeamId_limited = (uint)this.limited_dj;
			}
		}

		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_teamPanel.<>c <>9 = new a3_teamPanel.<>c();

			public static Action<GameObject> <>9__8_0;

			internal void <Init>b__8_0(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_SPEEDTEAM, null, false);
			}
		}

		public Dictionary<uint, a3_teamPanel.itemTeamInfoPrefab> itemTeamInfoPrefabDic;

		public static a3_teamPanel _instance;

		public Dictionary<uint, Sprite> iconSpriteDic;

		public uint begin_index = 0u;

		public uint end_index = 20u;

		public GameObject team_object;

		public int object_num;

		public a3_teamPanel(Transform trans) : base(trans)
		{
			this.Init();
		}

		public void Init()
		{
			a3_teamPanel._instance = this;
			this.itemTeamInfoPrefabDic = new Dictionary<uint, a3_teamPanel.itemTeamInfoPrefab>();
			BaseButton baseButton = new BaseButton(base.transform.FindChild("right/bottom/btnJoinTeam"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onBtnJoinTeamClick);
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("right/bottom/btnCreateTeam"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onBtnCreateClick);
			this.team_object = base.transform.FindChild("team_object").gameObject;
			this.team_object.SetActive(false);
			BaseButton baseButton3 = new BaseButton(base.transform.FindChild("team_object/btn_1"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.onbtn_1_Click);
			BaseButton baseButton4 = new BaseButton(base.transform.FindChild("team_object/btn_0"), 1, 1);
			baseButton4.onClick = new Action<GameObject>(this.onbtn_0_Click);
			BaseButton baseButton5 = new BaseButton(base.transform.FindChild("right/bottom/btnRefresh"), 1, 1);
			baseButton5.onClick = new Action<GameObject>(this.onBtnRefreshClick);
			BaseButton arg_149_0 = new BaseButton(base.transform.FindChild("right/bottom/speedteam"), 1, 1);
			Action<GameObject> arg_149_1;
			if ((arg_149_1 = a3_teamPanel.<>c.<>9__8_0) == null)
			{
				arg_149_1 = (a3_teamPanel.<>c.<>9__8_0 = new Action<GameObject>(a3_teamPanel.<>c.<>9.<Init>b__8_0));
			}
			arg_149_0.onClick = arg_149_1;
			Toggle component = base.transform.FindChild("right/main/body/showNearby").GetComponent<Toggle>();
			component.onValueChanged.AddListener(new UnityAction<bool>(this.onShowNearby));
			this.getProfessionSprite();
			BaseProxy<TeamProxy>.getInstance().SendGetMapTeam(this.begin_index, this.end_index);
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_CREATETEAM, new Action<GameEvent>(this.onCreateTeam));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_DISSOLVETEAM, new Action<GameEvent>(this.onDissolveTeam));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_TEAMLISTINFO, new Action<GameEvent>(this.onGetTeamListInfo));
		}

		private void onbtn_1_Click(GameObject go)
		{
			this.object_num = this.team_object.transform.FindChild("Dropdown").GetComponent<Dropdown>().value;
			bool flag = !ModelBase<A3_TeamModel>.getInstance().Limit_Change_Teammubiao(this.object_num);
			if (flag)
			{
				flytxt.instance.fly("您还没有开启该功能", 0, default(Color), null);
				this.team_object.transform.FindChild("Dropdown").GetComponent<Dropdown>().value = 0;
			}
			else
			{
				BaseProxy<TeamProxy>.getInstance().SendGetMapTeam(this.begin_index, this.end_index);
				BaseProxy<TeamProxy>.getInstance().SendCreateTeam(a3_currentTeamPanel._instance.change_v(this.object_num, true));
				this.team_object.SetActive(false);
				bool flag2 = a3_currentTeamPanel._instance != null;
				if (flag2)
				{
					a3_currentTeamPanel._instance.team_object.GetComponent<Dropdown>().value = this.object_num;
				}
			}
		}

		private void onbtn_0_Click(GameObject obj)
		{
			this.team_object.SetActive(false);
		}

		public override void onShowed()
		{
			base.onShowed();
			BaseProxy<TeamProxy>.getInstance().SendGetMapTeam(this.begin_index, this.end_index);
		}

		private void onShowNearby(bool b)
		{
			ItemTeamMemberData mapItemTeamData = BaseProxy<TeamProxy>.getInstance().mapItemTeamData;
			List<ItemTeamData> list = new List<ItemTeamData>();
			list.Clear();
			for (int i = 0; i < mapItemTeamData.itemTeamDataList.Count; i++)
			{
				bool flag = mapItemTeamData.itemTeamDataList[i].mapId != ModelBase<PlayerModel>.getInstance().mapid;
				if (flag)
				{
					list.Add(mapItemTeamData.itemTeamDataList[i]);
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				bool flag2 = this.itemTeamInfoPrefabDic.ContainsKey(list[j].teamId);
				if (flag2)
				{
					this.itemTeamInfoPrefabDic[list[j].teamId].root.gameObject.SetActive(!b);
				}
			}
		}

		private void onBtnJoinTeamClick(GameObject go)
		{
			foreach (KeyValuePair<uint, a3_teamPanel.itemTeamInfoPrefab> current in this.itemTeamInfoPrefabDic)
			{
				bool isOn = current.Value.root.GetComponent<Toggle>().isOn;
				bool flag = isOn;
				if (flag)
				{
					BaseProxy<TeamProxy>.getInstance().SendApplyJoinTeam(current.Key);
					break;
				}
			}
		}

		private void onBtnCreateClick(GameObject go)
		{
			this.team_object.SetActive(true);
			this.team_object.transform.FindChild("Dropdown").GetComponent<Dropdown>().value = 0;
		}

		private void onBtnRefreshClick(GameObject go)
		{
			BaseProxy<TeamProxy>.getInstance().SendGetMapTeam(this.begin_index, this.end_index);
		}

		private void getProfessionSprite()
		{
			this.iconSpriteDic = new Dictionary<uint, Sprite>();
			for (int i = 2; i <= 5; i++)
			{
				this.iconSpriteDic.Add((uint)i, Resources.Load<Sprite>(this.getProfession(i)));
			}
		}

		private string getProfession(int profession)
		{
			string result = string.Empty;
			switch (profession)
			{
			case 2:
				result = "icon/job_icon/h2";
				break;
			case 3:
				result = "icon/job_icon/h3";
				break;
			case 4:
				result = "icon/job_icon/h4";
				break;
			case 5:
				result = "icon/job_icon/h5";
				break;
			}
			return result;
		}

		private void onCreateTeam(GameEvent e)
		{
			Variant data = e.data;
			uint num = data["teamid"];
			bool flag = !this.itemTeamInfoPrefabDic.ContainsKey(num);
			if (flag)
			{
				ItemTeamData itemTeamData = new ItemTeamData();
				itemTeamData.name = ModelBase<PlayerModel>.getInstance().name;
				itemTeamData.lvl = ModelBase<PlayerModel>.getInstance().lvl;
				itemTeamData.knightage = ModelBase<PlayerModel>.getInstance().clanid.ToString();
				itemTeamData.mapId = ModelBase<PlayerModel>.getInstance().mapid;
				itemTeamData.MembCount = 1;
				itemTeamData.cid = ModelBase<PlayerModel>.getInstance().cid;
				itemTeamData.zhuan = ModelBase<PlayerModel>.getInstance().up_lvl;
				itemTeamData.combpt = ModelBase<PlayerModel>.getInstance().combpt;
				itemTeamData.teamId = num;
				itemTeamData.isCaptain = true;
				a3_teamPanel.itemTeamInfoPrefab itemTeamInfoPrefab = new a3_teamPanel.itemTeamInfoPrefab(base.transform);
				itemTeamInfoPrefab.Set(itemTeamData);
				this.itemTeamInfoPrefabDic[num] = itemTeamInfoPrefab;
				base.gameObject.SetActive(false);
			}
		}

		private void onGetTeamListInfo(GameEvent e)
		{
			ItemTeamMemberData mapItemTeamData = BaseProxy<TeamProxy>.getInstance().mapItemTeamData;
			List<uint> list = new List<uint>();
			list.Clear();
			for (int i = 0; i < mapItemTeamData.itemTeamDataList.Count; i++)
			{
				list.Add(mapItemTeamData.itemTeamDataList[i].teamId);
			}
			Dictionary<uint, a3_teamPanel.itemTeamInfoPrefab> dictionary = new Dictionary<uint, a3_teamPanel.itemTeamInfoPrefab>();
			dictionary.Clear();
			foreach (KeyValuePair<uint, a3_teamPanel.itemTeamInfoPrefab> current in this.itemTeamInfoPrefabDic)
			{
				bool flag = !list.Contains(current.Key);
				if (flag)
				{
					dictionary[current.Key] = current.Value;
				}
			}
			foreach (KeyValuePair<uint, a3_teamPanel.itemTeamInfoPrefab> current2 in dictionary)
			{
				UnityEngine.Object.Destroy(this.itemTeamInfoPrefabDic[current2.Key].root.gameObject);
				this.itemTeamInfoPrefabDic.Remove(current2.Key);
			}
			uint totalCount = mapItemTeamData.totalCount;
			uint idxBegin = mapItemTeamData.idxBegin;
			foreach (ItemTeamData current3 in mapItemTeamData.itemTeamDataList)
			{
				bool flag2 = this.itemTeamInfoPrefabDic.ContainsKey(current3.teamId);
				if (flag2)
				{
					this.itemTeamInfoPrefabDic[current3.teamId].Set(current3);
				}
				else
				{
					a3_teamPanel.itemTeamInfoPrefab itemTeamInfoPrefab = new a3_teamPanel.itemTeamInfoPrefab(base.transform);
					itemTeamInfoPrefab.Set(current3);
					this.itemTeamInfoPrefabDic[current3.teamId] = itemTeamInfoPrefab;
				}
			}
		}

		private void onDissolveTeam(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data.ContainsKey("teamid");
			if (flag)
			{
				uint key = data["teamid"];
				bool flag2 = this.itemTeamInfoPrefabDic.ContainsKey(key);
				if (flag2)
				{
					GameObject gameObject = this.itemTeamInfoPrefabDic[key].root.gameObject;
					UnityEngine.Object.Destroy(gameObject);
					this.itemTeamInfoPrefabDic.Remove(key);
				}
			}
		}
	}
}
