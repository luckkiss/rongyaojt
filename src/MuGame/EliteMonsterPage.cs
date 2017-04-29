using Cross;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class EliteMonsterPage
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly EliteMonsterPage.<>c <>9 = new EliteMonsterPage.<>c();

			public static Action <>9__20_2;

			public static Action <>9__20_3;

			internal void <RefreshMonInfo>b__20_2()
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_ELITEMON);
			}

			internal void <RefreshMonInfo>b__20_3()
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_ELITEMON);
			}
		}

		public GameObject prefabItemMonInfo;

		public GameObject goMonInfoScroll;

		public GameObject goRewardScroll;

		private GameObject curMonAvatar;

		private GameObject curMonScene;

		private GameObject camObj;

		public GameObject curSelected;

		private Dictionary<uint, List<uint>> dicReward;

		private Dictionary<uint, KeyValuePair<Text, uint>> dicTimerText;

		private Dictionary<uint, bool> dicGoAvailable;

		private Dictionary<uint, GameObject> dicEMonItem;

		private Queue<GameObject> qGoReward;

		public GameObject owner;

		private static EliteMonsterPage instance;

		public static EliteMonsterPage Instance
		{
			get
			{
				EliteMonsterPage arg_15_0;
				if ((arg_15_0 = EliteMonsterPage.instance) == null)
				{
					arg_15_0 = (EliteMonsterPage.instance = new EliteMonsterPage());
				}
				return arg_15_0;
			}
			set
			{
				EliteMonsterPage.instance = value;
			}
		}

		private EliteMonsterPage()
		{
			EliteMonsterPage.Instance = this;
			this.dicReward = new Dictionary<uint, List<uint>>();
			this.qGoReward = new Queue<GameObject>();
			this.dicTimerText = new Dictionary<uint, KeyValuePair<Text, uint>>();
			this.dicGoAvailable = new Dictionary<uint, bool>();
			this.dicEMonItem = new Dictionary<uint, GameObject>();
			this.owner = A3_EliteMonster.Instance.transform.FindChild("con_page/container/monPage/").gameObject;
			this.prefabItemMonInfo = this.owner.transform.FindChild("Template/item_elitemon").gameObject;
			this.goMonInfoScroll = this.owner.transform.FindChild("scrollrect/scrollview").gameObject;
		}

		private bool CheckGoAvaiable(uint monId)
		{
			int num = 0;
			int num2 = 0;
			Variant singleMapConf = SvrMapConfig.instance.getSingleMapConf((uint)ModelBase<A3_EliteMonsterModel>.getInstance().dicEMonInfo[monId].mapId);
			bool flag = singleMapConf.ContainsKey("lv_up");
			if (flag)
			{
				num = singleMapConf["lv_up"]._int;
			}
			bool flag2 = singleMapConf.ContainsKey("lv");
			if (flag2)
			{
				num2 = singleMapConf["lv"]._int;
			}
			return (long)num < (long)((ulong)ModelBase<PlayerModel>.getInstance().up_lvl) || ((long)num == (long)((ulong)ModelBase<PlayerModel>.getInstance().up_lvl) && (long)num2 < (long)((ulong)ModelBase<PlayerModel>.getInstance().lvl));
		}

		private bool CheckGoAvaiable(uint monId, ref int? lv_up, ref int? lv)
		{
			Variant singleMapConf = SvrMapConfig.instance.getSingleMapConf((uint)ModelBase<A3_EliteMonsterModel>.getInstance().dicEMonInfo[monId].mapId);
			bool flag = singleMapConf.ContainsKey("lv_up");
			if (flag)
			{
				lv_up = new int?(singleMapConf["lv_up"]._int);
			}
			bool flag2 = singleMapConf.ContainsKey("lv");
			if (flag2)
			{
				lv = new int?(singleMapConf["lv"]._int);
			}
			int? num = lv_up;
			bool arg_169_0;
			if (!((num.HasValue ? new long?((long)num.GetValueOrDefault()) : null) < (long)((ulong)ModelBase<PlayerModel>.getInstance().up_lvl)))
			{
				num = lv_up;
				if ((num.HasValue ? new long?((long)num.GetValueOrDefault()) : null) == (long)((ulong)ModelBase<PlayerModel>.getInstance().up_lvl))
				{
					num = lv;
					arg_169_0 = ((num.HasValue ? new long?((long)num.GetValueOrDefault()) : null) < (long)((ulong)ModelBase<PlayerModel>.getInstance().lvl));
				}
				else
				{
					arg_169_0 = false;
				}
			}
			else
			{
				arg_169_0 = true;
			}
			return arg_169_0;
		}

		public void RefreshMonInfo(uint monId, uint sec = 0u)
		{
			bool flag = !this.dicTimerText.ContainsKey(monId);
			if (flag)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabItemMonInfo);
				gameObject.transform.SetParent(this.goMonInfoScroll.transform, false);
				this.dicTimerText.Add(monId, new KeyValuePair<Text, uint>(gameObject.transform.FindChild("info_leftTm/Time").GetComponent<Text>(), sec));
				this.dicEMonItem.Add(monId, gameObject);
			}
			else
			{
				this.dicTimerText[monId] = new KeyValuePair<Text, uint>(this.dicTimerText[monId].Key, sec);
			}
			this.dicTimerText[monId].Key.text = this.GetTimeValueBySec(this.dicTimerText[monId].Value);
			this.dicTimerText[monId].Key.transform.parent.parent.FindChild("text_nameMon").GetComponent<Text>().text = XMLMgr.instance.GetSXML("monsters.monsters", "id==" + monId).getString("name");
			this.dicTimerText[monId].Key.transform.parent.parent.FindChild("text_lvGo").GetComponent<Text>().text = string.Concat(new object[]
			{
				ModelBase<A3_EliteMonsterModel>.getInstance().dicEMonInfo[monId].upLv,
				"转",
				ModelBase<A3_EliteMonsterModel>.getInstance().dicEMonInfo[monId].lv,
				"级"
			});
			GameObject gameObject2 = this.dicTimerText[monId].Key.transform.parent.parent.FindChild("btn_go").gameObject;
			int? num = null;
			int? num2 = null;
			bool flag2 = this.CheckGoAvaiable(monId, ref num, ref num2);
			bool flag3 = this.dicGoAvailable.ContainsKey(monId);
			if (flag3)
			{
				this.dicGoAvailable[monId] = flag2;
			}
			else
			{
				this.dicGoAvailable.Add(monId, flag2);
			}
			Transform trans;
			(trans = gameObject2.transform.parent.FindChild("lock")).gameObject.SetActive(!this.dicGoAvailable[monId]);
			new BaseButton(trans, 1, 1).onClick = delegate(GameObject go)
			{
				bool flag9 = !this.dicGoAvailable[monId];
				if (flag9)
				{
					flytxt.instance.fly("等级不足,无法挑战", 0, default(Color), null);
				}
			};
			Text component;
			(component = gameObject2.transform.parent.FindChild("lock/openText").GetComponent<Text>()).text = string.Concat(new object[]
			{
				num,
				"转",
				num2,
				"级解锁"
			});
			bool flag4 = component != null;
			if (flag4)
			{
				component.gameObject.SetActive(!flag2);
			}
			this.dicTimerText[monId].Key.transform.parent.gameObject.SetActive(flag2);
			gameObject2.gameObject.SetActive(flag2);
			new BaseButton(gameObject2.transform, 1, 1).onClick = delegate(GameObject go)
			{
				EliteMonsterInfo eliteMonsterInfo = ModelBase<A3_EliteMonsterModel>.getInstance().dicEMonInfo[monId];
				bool flag9 = eliteMonsterInfo.mapId == (int)ModelBase<PlayerModel>.getInstance().mapid;
				if (flag9)
				{
					SelfRole.fsm.Stop();
					SelfRole.WalkToMap(eliteMonsterInfo.mapId, new Vector3(eliteMonsterInfo.pos.x / 1.666f, 0f, eliteMonsterInfo.pos.y / 1.666f), null, 0.3f);
					InterfaceMgr.getInstance().close(InterfaceMgr.A3_ELITEMON);
				}
				else
				{
					InterfaceMgr arg_12C_0 = InterfaceMgr.getInstance();
					string arg_12C_1 = InterfaceMgr.TRANSMIT_PANEL;
					TransmitData expr_9D = new TransmitData();
					Action arg_BD_1;
					if ((arg_BD_1 = EliteMonsterPage.<>c.<>9__20_2) == null)
					{
						arg_BD_1 = (EliteMonsterPage.<>c.<>9__20_2 = new Action(EliteMonsterPage.<>c.<>9.<RefreshMonInfo>b__20_2));
					}
					expr_9D.after_clickBtnWalk = arg_BD_1;
					Action arg_E2_1;
					if ((arg_E2_1 = EliteMonsterPage.<>c.<>9__20_3) == null)
					{
						arg_E2_1 = (EliteMonsterPage.<>c.<>9__20_3 = new Action(EliteMonsterPage.<>c.<>9.<RefreshMonInfo>b__20_3));
					}
					expr_9D.after_clickBtnTransmit = arg_E2_1;
					expr_9D.targetPosition = new Vector3(eliteMonsterInfo.pos.x / 1.666f, 0f, eliteMonsterInfo.pos.y / 1.666f);
					expr_9D.mapId = eliteMonsterInfo.mapId;
					arg_12C_0.open(arg_12C_1, (ArrayList)expr_9D, false);
				}
			};
			GameObject gameObject3 = this.dicTimerText[monId].Key.transform.parent.parent.FindChild("btn_show").gameObject;
			new BaseButton(gameObject3.transform, 1, 1).onClick = delegate(GameObject go)
			{
				GameObject expr_0C = this.curSelected;
				if (expr_0C != null)
				{
					expr_0C.SetActive(false);
				}
				this.curSelected = go.transform.parent.FindChild("bg/selected").gameObject;
				this.curSelected.SetActive(true);
				A3_EliteMonster.Instance.CurrentSelectedMonsterId = monId;
				this.CreateModel(XMLMgr.instance.GetSXML("monsters.monsters", "id==" + monId).getString("elite_obj"), monId);
				this.ShowReward(monId);
			};
			bool flag5 = (ulong)sec > (ulong)((long)muNetCleint.instance.CurServerTimeStamp);
			if (flag5)
			{
				bool flag6 = this.CheckGoAvaiable(monId);
				if (flag6)
				{
					A3_EliteMonster.Instance.TurnOnEliteMonTimer();
					this.dicTimerText[monId].Key.transform.parent.gameObject.SetActive(true);
					gameObject2.transform.parent.FindChild("lock/openText").gameObject.SetActive(false);
					gameObject2.SetActive(false);
				}
			}
			else
			{
				bool flag7 = this.CheckGoAvaiable(monId);
				if (flag7)
				{
					this.dicTimerText[monId].Key.transform.parent.gameObject.SetActive(false);
					gameObject2.transform.parent.FindChild("lock/openText").gameObject.SetActive(false);
					gameObject2.SetActive(true);
				}
			}
			List<uint> sortedMonInfoIdList = ModelBase<A3_EliteMonsterModel>.getInstance().GetSortedMonInfoIdList();
			for (int i = 0; i < sortedMonInfoIdList.Count; i++)
			{
				bool flag8 = this.dicEMonItem.ContainsKey(sortedMonInfoIdList[i]);
				if (flag8)
				{
					this.dicEMonItem[sortedMonInfoIdList[i]].transform.SetAsLastSibling();
				}
			}
		}

		public void ShowLeftTime()
		{
			int i = 0;
			List<uint> list = new List<uint>(this.dicTimerText.Keys);
			while (i < list.Count)
			{
				bool flag = (this.dicTimerText[list[i]].Key.text = this.GetTimeValueBySec(this.dicTimerText[list[i]].Value)).Equals("");
				if (flag)
				{
					this.dicTimerText[list[i]].Key.transform.parent.parent.FindChild("btn_go").gameObject.SetActive(this.CheckGoAvaiable(list[i]));
					this.dicTimerText[list[i]].Key.transform.parent.gameObject.SetActive(false);
				}
				i++;
			}
		}

		private string GetTimeValueBySec(uint sec)
		{
			long num = (long)muNetCleint.instance.CurServerTimeStamp;
			long num2 = (long)((ulong)sec - (ulong)num);
			bool flag = num2 > 0L;
			string result;
			if (flag)
			{
				result = string.Format("{0:D2}:{1:D2}:{2:D2}", num2 / 3600L, num2 % 3600L / 60L, num2 % 60L);
			}
			else
			{
				result = "";
			}
			return result;
		}

		public void AddReward(uint monId, uint tpid)
		{
			bool flag = !this.dicReward.ContainsKey(monId);
			if (flag)
			{
				this.dicReward.Add(monId, new List<uint>());
			}
			this.dicReward[monId].Add(tpid);
		}

		public void ShowReward(uint monId)
		{
			A3_EliteMonster.Instance.ShowRewardItemIcon(new uint?(monId));
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
			float @float = XMLMgr.instance.GetSXML("monsters.monsters", "id==" + monId).getFloat("avatar_height");
			float float2 = XMLMgr.instance.GetSXML("monsters.monsters", "id==" + monId).getFloat("avatar_scale");
			bool flag2 = gameObject == null;
			GameObject result;
			if (flag2)
			{
				Debug.LogError("monsters.xml:elite_obj字段配置错误");
				result = null;
			}
			else
			{
				this.curMonAvatar = (UnityEngine.Object.Instantiate(gameObject, new Vector3(-153.4f, @float, 0f), Quaternion.identity) as GameObject);
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
				GameObject original2 = Resources.Load<GameObject>("profession/avatar_ui/scene_ui_camera_emon");
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
				result = this.curMonAvatar;
			}
			return result;
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
			bool flag3 = this.curMonScene != null && !showOrHide;
			if (flag3)
			{
				UnityEngine.Object.DestroyImmediate(this.curMonScene);
			}
		}
	}
}
