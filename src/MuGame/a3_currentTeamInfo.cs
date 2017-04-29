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
	internal class a3_currentTeamInfo : FloatUi
	{
		private class ItemCurrentTeamInfo
		{
			private Image iconHead;

			private Image img_state;

			private Image img_captain;

			private Text txtName;

			private Slider sldBlood;

			private GameObject job;

			private Image sldImgBackGround;

			private Image sldImgForeGround;

			private float[] PercentLow100 = new float[]
			{
				0.156862751f,
				1f,
				0f,
				1f
			};

			private float[] PercentLow90 = new float[]
			{
				1f,
				0.929411769f,
				0f,
				1f
			};

			private float[] PercentLow50 = new float[]
			{
				1f,
				0f,
				0.145098045f,
				1f
			};

			public Transform root;

			public ItemCurrentTeamInfo(Transform trans)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(trans.gameObject);
				this.root = gameObject.transform;
				this.root.SetParent(a3_currentTeamInfo.mInstance.mContant);
				this.root.gameObject.SetActive(true);
				this.root.localScale = Vector3.one;
				this.iconHead = this.root.FindChild("icon").GetComponent<Image>();
				this.job = this.root.FindChild("head").gameObject;
				this.img_state = this.iconHead.transform.FindChild("state").GetComponent<Image>();
				this.img_captain = this.root.FindChild("iconCaptain").GetComponent<Image>();
				this.txtName = this.root.FindChild("txtName").GetComponent<Text>();
				this.sldBlood = this.root.FindChild("Slider").GetComponent<Slider>();
				this.sldImgBackGround = this.sldBlood.transform.FindChild("Background").GetComponent<Image>();
				this.sldImgForeGround = this.sldBlood.transform.FindChild("Fill Area/Fill").GetComponent<Image>();
			}

			public void SetInfo(ItemTeamData itd)
			{
				this.sethead(itd.carr);
				this.SetSldImgBackGround(itd.hp);
				this.SetSldImgForeGround(itd.hp, itd.maxHp);
				this.SetOnLine(itd.online);
				this.SetCaptainSign(itd.isCaptain);
			}

			private void SetSldImgBackGround(uint hp)
			{
				bool flag = hp <= 0u;
				if (flag)
				{
					this.sldImgBackGround.fillCenter = false;
					this.sldBlood.value = 0f;
				}
				else
				{
					this.sldImgBackGround.fillCenter = true;
				}
			}

			private void SetSldImgForeGround(uint hp, uint maxHp)
			{
				float num = hp / maxHp;
				this.sldBlood.value = float.Parse(num.ToString("F2"));
				bool flag = hp <= 0f;
				if (flag)
				{
					this.SetIconHead(true);
				}
				bool flag2 = hp / maxHp < 0.5f;
				if (flag2)
				{
					this.sldImgForeGround.color = new Color(this.PercentLow50[0], this.PercentLow50[1], this.PercentLow50[2], this.PercentLow50[3]);
				}
				else
				{
					bool flag3 = hp / maxHp < 0.9f;
					if (flag3)
					{
						this.sldImgForeGround.color = new Color(this.PercentLow90[0], this.PercentLow90[1], this.PercentLow90[2], this.PercentLow90[3]);
					}
					else
					{
						bool flag4 = hp / maxHp <= 1f;
						if (flag4)
						{
							this.sldImgForeGround.color = new Color(this.PercentLow100[0], this.PercentLow100[1], this.PercentLow100[2], this.PercentLow100[3]);
						}
					}
				}
			}

			public void SetTxtName(string nameStr, bool isDead = false)
			{
				this.txtName.text = nameStr;
			}

			private void SetIconHead(bool isDead = false)
			{
				if (isDead)
				{
					this.iconHead.material = a3_currentTeamInfo.mInstance.mMaterialGrey;
					this.img_state.sprite = a3_currentTeamInfo.mInstance.deadSprite;
				}
				else
				{
					this.iconHead.material = null;
				}
			}

			private void SetSlider(uint hp, uint maxHp)
			{
				bool flag = hp > 0u;
				if (flag)
				{
					this.sldBlood.maxValue = maxHp;
					this.sldBlood.minValue = hp;
				}
				else
				{
					this.sldBlood.value = 0f;
				}
			}

			public void SetOnLine(bool online)
			{
				if (online)
				{
					this.sldImgForeGround.material = null;
					this.sldImgBackGround.material = null;
					this.iconHead.material = null;
				}
				else
				{
					this.sldImgForeGround.material = a3_currentTeamInfo.mInstance.mMaterialGrey;
					this.sldImgBackGround.material = a3_currentTeamInfo.mInstance.mMaterialGrey;
					this.iconHead.material = a3_currentTeamInfo.mInstance.mMaterialGrey;
				}
			}

			public void SetCaptainSign(bool b)
			{
				this.img_captain.gameObject.SetActive(b);
			}

			internal void sethead(uint carr)
			{
				this.job.transform.FindChild("job2").gameObject.SetActive(false);
				this.job.transform.FindChild("job3").gameObject.SetActive(false);
				this.job.transform.FindChild("job5").gameObject.SetActive(false);
				this.job.transform.FindChild("job" + carr).gameObject.SetActive(true);
			}
		}

		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_currentTeamInfo.<>c <>9 = new a3_currentTeamInfo.<>c();

			public static Action<GameObject> <>9__10_0;

			internal void <init>b__10_0(GameObject go)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(2);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_SHEJIAO, arrayList, false);
			}
		}

		public static a3_currentTeamInfo mInstance;

		private Transform root;

		private Transform itemCurrentInfoPrefab;

		private Dictionary<uint, a3_currentTeamInfo.ItemCurrentTeamInfo> m_ItemCurrentTeamInfoDic;

		public Transform mContant;

		public Material mMaterialGrey;

		public Sprite leaveOnLineSprite;

		public Sprite deadSprite;

		private Transform friend;

		public int job;

		public override void init()
		{
			a3_currentTeamInfo.mInstance = this;
			this.m_ItemCurrentTeamInfoDic = new Dictionary<uint, a3_currentTeamInfo.ItemCurrentTeamInfo>();
			this.friend = base.getTransformByPath("contant/friend/friend");
			this.root = base.transform;
			this.mMaterialGrey = Resources.Load<Material>("uifx/uiGray");
			this.mContant = base.transform.FindChild("contant");
			this.itemCurrentInfoPrefab = base.transform.FindChild("temp/itemMemberInfo");
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_SYNCTEAMBLOOD, new Action<GameEvent>(this.onSyncTeamBlood));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_LEAVETEAM, new Action<GameEvent>(this.onLeaveTeam));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_DISSOLVETEAM, new Action<GameEvent>(this.onDissolveTeam));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_NOTICEHAVEMEMBERLEAVE, new Action<GameEvent>(this.onNoticeHaveMemberLeave));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_AFFIRMINVITE, new Action<GameEvent>(this.onAffirmInvite));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_KICKOUT, new Action<GameEvent>(this.onNoticeHaveMemberLeave));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_NEWMEMBERJOIN, new Action<GameEvent>(this.onNewMemberJoin));
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_NOTICEONLINESTATECHANGE, new Action<GameEvent>(this.onNoticeOnlineStateChange));
			this.leaveOnLineSprite = Resources.Load<Sprite>("icon/comm/icon_notice");
			this.deadSprite = Resources.Load<Sprite>("icon/job_icon/boss");
			BaseButton arg_19C_0 = new BaseButton(base.getTransformByPath("contant/friend/friend/go"), 1, 1);
			Action<GameObject> arg_19C_1;
			if ((arg_19C_1 = a3_currentTeamInfo.<>c.<>9__10_0) == null)
			{
				arg_19C_1 = (a3_currentTeamInfo.<>c.<>9__10_0 = new Action<GameObject>(a3_currentTeamInfo.<>c.<>9.<init>b__10_0));
			}
			arg_19C_0.onClick = arg_19C_1;
		}

		public override void onShowed()
		{
			bool flag = this.uiData != null;
			if (flag)
			{
				Transform parent = (Transform)this.uiData[0];
				base.transform.SetParent(parent, false);
				base.transform.localScale = Vector3.one;
				base.transform.localPosition = Vector3.one;
				base.InvokeRepeating("SendSyncTeamBlood", 0f, 3f);
				bool flag2 = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
				if (flag2)
				{
					for (int i = 0; i < BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count; i++)
					{
						uint cid = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList[i].cid;
						bool flag3 = cid == ModelBase<PlayerModel>.getInstance().cid;
						if (!flag3)
						{
							bool flag4 = this.m_ItemCurrentTeamInfoDic != null && !this.m_ItemCurrentTeamInfoDic.ContainsKey(cid);
							if (flag4)
							{
								a3_currentTeamInfo.ItemCurrentTeamInfo itemCurrentTeamInfo = new a3_currentTeamInfo.ItemCurrentTeamInfo(this.itemCurrentInfoPrefab);
								string name = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList[i].name;
								itemCurrentTeamInfo.SetTxtName(name, false);
								this.m_ItemCurrentTeamInfoDic.Add(cid, itemCurrentTeamInfo);
							}
						}
					}
					bool flag5 = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count < 5;
					if (flag5)
					{
						base.getGameObjectByPath("contant/friend").SetActive(true);
						base.getTransformByPath("contant/friend").SetAsLastSibling();
					}
					else
					{
						base.getGameObjectByPath("contant/friend").SetActive(false);
					}
				}
			}
		}

		private void SendSyncTeamBlood()
		{
			BaseProxy<TeamProxy>.getInstance().SendSyncTeamBlood();
		}

		public override void onClosed()
		{
			base.CancelInvoke("SendSyncTeamBlood");
		}

		private void onSyncTeamBlood(GameEvent e)
		{
			Variant data = e.data;
			List<Variant> arr = data["infos"]._arr;
			for (int i = 0; i < arr.Count; i++)
			{
				uint num = arr[i]["cid"];
				uint maxHp = arr[i]["max_hp"];
				uint hp = arr[i]["hp"];
				ItemTeamData itemTeamData = new ItemTeamData();
				itemTeamData.cid = num;
				itemTeamData.hp = hp;
				itemTeamData.maxHp = maxHp;
				for (int j = 0; j < BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count; j++)
				{
					bool flag = num == BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList[j].cid;
					if (flag)
					{
						itemTeamData.carr = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList[j].carr;
						itemTeamData.isCaptain = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList[j].isCaptain;
						itemTeamData.online = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList[j].online;
						break;
					}
				}
				bool flag2 = this.m_ItemCurrentTeamInfoDic.ContainsKey(num);
				if (flag2)
				{
					this.m_ItemCurrentTeamInfoDic[num].SetInfo(itemTeamData);
				}
				else
				{
					a3_currentTeamInfo.ItemCurrentTeamInfo itemCurrentTeamInfo = new a3_currentTeamInfo.ItemCurrentTeamInfo(this.itemCurrentInfoPrefab);
					itemCurrentTeamInfo.SetInfo(itemTeamData);
					this.m_ItemCurrentTeamInfoDic.Add(num, itemCurrentTeamInfo);
				}
			}
		}

		private void onLeaveTeam(GameEvent e)
		{
			this.DistroyItemCurrentTeamInfo();
		}

		private void onDissolveTeam(GameEvent e)
		{
			this.DistroyItemCurrentTeamInfo();
		}

		private void onNoticeHaveMemberLeave(GameEvent e)
		{
			Variant data = e.data;
			uint key = data["cid"];
			bool flag = this.m_ItemCurrentTeamInfoDic.ContainsKey(key);
			if (flag)
			{
				UnityEngine.Object.Destroy(this.m_ItemCurrentTeamInfoDic[key].root.gameObject);
				this.m_ItemCurrentTeamInfoDic.Remove(key);
			}
		}

		private void onAffirmInvite(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data == null;
			if (!flag)
			{
				bool flag2 = data.ContainsKey("cid");
				if (flag2)
				{
					uint num = data["cid"];
					List<ItemTeamData> itemTeamDataList = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList;
					for (int i = 0; i < itemTeamDataList.Count; i++)
					{
						bool flag3 = !this.m_ItemCurrentTeamInfoDic.ContainsKey(num) && num != ModelBase<PlayerModel>.getInstance().cid;
						if (flag3)
						{
							a3_currentTeamInfo.ItemCurrentTeamInfo value = new a3_currentTeamInfo.ItemCurrentTeamInfo(this.itemCurrentInfoPrefab);
							this.m_ItemCurrentTeamInfoDic.Add(num, value);
						}
					}
				}
				bool flag4 = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count < 5;
				if (flag4)
				{
					base.getGameObjectByPath("contant/friend").SetActive(true);
				}
				else
				{
					base.getGameObjectByPath("contant/friend").SetActive(false);
				}
				base.getTransformByPath("contant/friend").SetAsLastSibling();
			}
		}

		private void onNewMemberJoin(GameEvent e)
		{
			Variant data = e.data;
			uint key = data["cid"];
			string nameStr = data["name"];
			uint carr = data["carr"];
			bool flag = !this.m_ItemCurrentTeamInfoDic.ContainsKey(key);
			if (flag)
			{
				a3_currentTeamInfo.ItemCurrentTeamInfo itemCurrentTeamInfo = new a3_currentTeamInfo.ItemCurrentTeamInfo(this.itemCurrentInfoPrefab);
				itemCurrentTeamInfo.SetTxtName(nameStr, false);
				itemCurrentTeamInfo.sethead(carr);
				this.m_ItemCurrentTeamInfoDic.Add(key, itemCurrentTeamInfo);
			}
			else
			{
				this.m_ItemCurrentTeamInfoDic[key].sethead(carr);
				this.m_ItemCurrentTeamInfoDic[key].SetTxtName(nameStr, false);
			}
			bool flag2 = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList.Count < 5;
			if (flag2)
			{
				base.getGameObjectByPath("contant/friend").SetActive(true);
			}
			else
			{
				base.getGameObjectByPath("contant/friend").SetActive(false);
			}
			base.getTransformByPath("contant/friend").SetAsLastSibling();
		}

		private void onNoticeOnlineStateChange(GameEvent e)
		{
			Variant data = e.data;
			uint key = data["cid"];
			bool onLine = data["online"];
			bool flag = this.m_ItemCurrentTeamInfoDic.ContainsKey(key);
			if (flag)
			{
				this.m_ItemCurrentTeamInfoDic[key].SetOnLine(onLine);
			}
		}

		private void DistroyItemCurrentTeamInfo()
		{
			base.CancelInvoke("SendSyncTeamBlood");
			foreach (KeyValuePair<uint, a3_currentTeamInfo.ItemCurrentTeamInfo> current in this.m_ItemCurrentTeamInfoDic)
			{
				UnityEngine.Object.Destroy(current.Value.root.gameObject);
			}
			this.m_ItemCurrentTeamInfoDic.Clear();
		}
	}
}
