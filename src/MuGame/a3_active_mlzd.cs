using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_active_mlzd : a3BaseActive
	{
		private RectTransform parView;

		private Transform lvlView;

		private RectTransform top;

		private RectTransform down;

		private GameObject isthis;

		private int diff_lvl = 0;

		private int alllvl = 0;

		private GameObject tip;

		public static a3_active_mlzd instans;

		private float parView_pos_x;

		private BaseButton enterbtn;

		private ScrollRect rect;

		private RectTransform rect_Content;

		private float contentCellSize;

		private GameObject isthisClon = null;

		private int minlvl = 1;

		private int maxlvl = 1;

		public int CountForOne = 9;

		private float startV = -1f;

		private float endV = -1f;

		private bool Load = false;

		private bool toup = false;

		private bool todown = false;

		private Dictionary<int, GameObject> lvlobj = new Dictionary<int, GameObject>();

		private int tasklvl = 0;

		private int lastlvl = 0;

		private int Pastlvl = 0;

		private int thisonelvPos = 0;

		private GameObject friendinfo = null;

		public a3_active_mlzd(Window win, string pathStr) : base(win, pathStr)
		{
		}

		public override void init()
		{
			this.parView = base.transform.FindChild("scrollview/con").GetComponent<RectTransform>();
			this.parView.pivot = new Vector2(0f, 1f);
			this.parView_pos_x = this.parView.anchoredPosition.x;
			this.rect = base.transform.FindChild("scrollview").GetComponent<ScrollRect>();
			this.lvlView = this.parView.FindChild("lvlCon");
			this.top = this.parView.FindChild("top").GetComponent<RectTransform>();
			this.down = this.parView.FindChild("down").GetComponent<RectTransform>();
			this.isthis = base.transform.FindChild("scrollview/this").gameObject;
			this.tip = base.transform.parent.parent.FindChild("tip").gameObject;
			this.rect.onValueChanged.AddListener(delegate(Vector2 <arg0>)
			{
				this.onChange();
			});
			EventTriggerListener.Get(this.rect.gameObject).onDragEnd = new EventTriggerListener.VectorDelegate(this.Dragend);
			EventTriggerListener.Get(this.rect.gameObject).onDragIn = new EventTriggerListener.VoidDelegate(this.BeginDrag);
			this.rect_Content = this.parView.GetComponent<RectTransform>();
			this.enterbtn = new BaseButton(base.getTransformByPath("enter"), 1, 1);
			this.enterbtn.onClick = delegate(GameObject go)
			{
				bool flag = this.diff_lvl <= ModelBase<A3_ActiveModel>.getInstance().nowlvl + 1;
				if (flag)
				{
					Debug.Log("Enter");
					Variant variant2 = new Variant();
					variant2["mapid"] = 3338;
					variant2["npcid"] = 0;
					variant2["ltpid"] = 104;
					variant2["diff_lvl"] = this.diff_lvl;
					BaseProxy<LevelProxy>.getInstance().sendCreate_lvl(variant2);
				}
				else
				{
					flytxt.instance.fly("完成前一层的挑战！", 0, default(Color), null);
				}
			};
			this.contentCellSize = this.lvlView.GetComponent<GridLayoutGroup>().cellSize.y + this.lvlView.GetComponent<GridLayoutGroup>().spacing.y;
			Variant variant = SvrLevelConfig.instacne.get_level_data(104u);
			this.maxlvl = variant["all"][0]["level"];
		}

		private void onChange()
		{
			bool flag = (double)this.rect.verticalNormalizedPosition > 0.8;
			if (flag)
			{
				this.toup = true;
				this.todown = false;
			}
			else
			{
				bool flag2 = (double)this.rect.verticalNormalizedPosition < 0.2;
				if (flag2)
				{
					this.todown = true;
					this.toup = false;
				}
				else
				{
					this.todown = false;
					this.toup = false;
				}
			}
		}

		private void Dragend(GameObject go, Vector2 delta)
		{
			this.Load = true;
			this.endV = this.rect.verticalNormalizedPosition;
			bool flag = this.endV - this.startV > 0f && this.toup;
			if (flag)
			{
				this.addAndRec_Obj(true);
			}
			else
			{
				bool flag2 = this.endV - this.startV < 0f && this.todown;
				if (flag2)
				{
					this.addAndRec_Obj(false);
				}
			}
		}

		private void BeginDrag(GameObject go)
		{
			this.startV = this.rect.verticalNormalizedPosition;
		}

		public override void onShowed()
		{
			a3_active_mlzd.instans = this;
			this.tasklvl = ModelBase<A3_ActiveModel>.getInstance().nowlvl + 1;
			bool flag = this.tasklvl > this.maxlvl;
			if (flag)
			{
				this.tasklvl = this.maxlvl;
			}
			this.tip.SetActive(false);
			base.getTransformByPath("cue/limit").GetComponent<Text>().text = "副本限制： <color=#00ff00>0/1</color> （每天0点重置）";
			this.RefreshLeftTimes();
			this.SetView();
		}

		public new virtual void onClose()
		{
			a3_active_mlzd.instans = null;
		}

		private void RefreshLeftTimes()
		{
			Variant variant = SvrLevelConfig.instacne.get_level_data(104u);
			int num = variant["daily_cnt"];
			int num2 = 0;
			bool flag = MapModel.getInstance().dFbDta.ContainsKey(104);
			if (flag)
			{
				num2 = Mathf.Min(MapModel.getInstance().dFbDta[104].cycleCount, num);
			}
			base.getTransformByPath("cue/limit").GetComponent<Text>().text = string.Concat(new object[]
			{
				"副本限制： <color=#00ff00>",
				num - num2,
				"/",
				num,
				"</color> （每天0点重置）"
			});
			bool flag2 = num - num2 <= 0;
			if (flag2)
			{
				this.enterbtn.interactable = false;
			}
			else
			{
				this.enterbtn.interactable = true;
			}
		}

		private void SetView()
		{
			this.lvlobj.Clear();
			for (int i = 0; i < this.lvlView.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.lvlView.GetChild(i).gameObject);
			}
			GameObject gameObject = base.transform.FindChild("scrollview/lvlitem").gameObject;
			int pos = this.GetPos(this.tasklvl);
			int num = pos * this.CountForOne;
			int num2 = num - this.CountForOne + 1;
			this.lastlvl = num + 1;
			this.Pastlvl = num2 - 1;
			int num3 = 0;
			for (int j = num2; j <= num; j++)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				num3++;
				gameObject2.SetActive(true);
				gameObject2.transform.SetParent(this.lvlView.transform, false);
				gameObject2.name = j.ToString();
				gameObject2.transform.SetAsFirstSibling();
				gameObject2.transform.FindChild("namebg/name").GetComponent<Text>().text = "第" + j + "层";
				this.setFriend(gameObject2, j);
				bool flag = j == this.tasklvl;
				if (flag)
				{
					this.locklvl(gameObject2, j);
					this.thisonelvPos = num3;
				}
				this.lvlobj[j] = gameObject2;
				this.setlockAndClick(gameObject2, j);
			}
			this.setSize();
			this.setNowPos();
		}

		private void setSize()
		{
			int pos = this.GetPos(this.tasklvl);
			float y = this.lvlView.GetComponent<GridLayoutGroup>().cellSize.y;
			float y2 = this.lvlView.GetComponent<GridLayoutGroup>().spacing.y;
			float num = (float)this.lvlobj.Count * y + (float)(this.lvlView.childCount - 1) * y2;
			bool flag = pos == this.GetPos(this.minlvl);
			float y3;
			if (flag)
			{
				this.top.gameObject.SetActive(false);
				this.down.gameObject.SetActive(true);
				y3 = this.down.offsetMin.y - this.down.offsetMax.y * -1f + num;
				this.lvlView.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.lvlView.GetComponent<RectTransform>().anchoredPosition.x, 0f);
			}
			else
			{
				bool flag2 = pos == this.GetPos(this.maxlvl);
				if (flag2)
				{
					this.top.gameObject.SetActive(true);
					this.down.gameObject.SetActive(false);
					y3 = this.top.offsetMin.y * -1f - this.top.offsetMax.y + num;
					this.lvlView.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.lvlView.GetComponent<RectTransform>().anchoredPosition.x, -(this.top.offsetMin.y * -1f - this.top.offsetMax.y));
				}
				else
				{
					this.top.gameObject.SetActive(false);
					this.down.gameObject.SetActive(false);
					y3 = num;
					this.lvlView.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.lvlView.GetComponent<RectTransform>().anchoredPosition.x, 0f);
				}
			}
			Vector2 sizeDelta = new Vector2(this.parView.sizeDelta.x, y3);
			this.parView.sizeDelta = sizeDelta;
		}

		private void setNowPos()
		{
			int pos = this.GetPos(this.tasklvl);
			bool flag = pos == this.GetPos(this.minlvl);
			if (flag)
			{
				bool flag2 = this.thisonelvPos >= 1 && this.thisonelvPos <= 4;
				if (flag2)
				{
					this.parView.pivot = Vector2.zero;
					RectTransform arg_80_0 = this.parView;
					RectTransform arg_78_0 = this.parView;
					Vector2 vector = new Vector2(0f, 0f);
					arg_78_0.anchorMin = vector;
					arg_80_0.anchorMax = vector;
				}
				else
				{
					this.parView.pivot = new Vector2(0f, 1f);
					RectTransform arg_CC_0 = this.parView;
					RectTransform arg_C4_0 = this.parView;
					Vector2 vector = new Vector2(0f, 1f);
					arg_C4_0.anchorMin = vector;
					arg_CC_0.anchorMax = vector;
				}
			}
			else
			{
				bool flag3 = pos == this.GetPos(this.maxlvl);
				if (flag3)
				{
					bool flag4 = this.thisonelvPos >= 1 && this.thisonelvPos <= 5;
					if (flag4)
					{
						this.parView.pivot = Vector2.zero;
						RectTransform arg_149_0 = this.parView;
						RectTransform arg_141_0 = this.parView;
						Vector2 vector = new Vector2(0f, 0f);
						arg_141_0.anchorMin = vector;
						arg_149_0.anchorMax = vector;
					}
					else
					{
						this.parView.pivot = new Vector2(0f, 1f);
						RectTransform arg_195_0 = this.parView;
						RectTransform arg_18D_0 = this.parView;
						Vector2 vector = new Vector2(0f, 1f);
						arg_18D_0.anchorMin = vector;
						arg_195_0.anchorMax = vector;
					}
				}
				else
				{
					bool flag5 = this.thisonelvPos >= 1 && this.thisonelvPos <= 4;
					if (flag5)
					{
						this.parView.pivot = Vector2.zero;
						RectTransform arg_1FA_0 = this.parView;
						RectTransform arg_1F2_0 = this.parView;
						Vector2 vector = new Vector2(0f, 0f);
						arg_1F2_0.anchorMin = vector;
						arg_1FA_0.anchorMax = vector;
					}
					else
					{
						this.parView.pivot = new Vector2(0f, 1f);
						RectTransform arg_246_0 = this.parView;
						RectTransform arg_23E_0 = this.parView;
						Vector2 vector = new Vector2(0f, 1f);
						arg_23E_0.anchorMin = vector;
						arg_246_0.anchorMax = vector;
					}
				}
			}
			this.parView.anchoredPosition = new Vector2(this.parView_pos_x, 0f);
		}

		private void addAndRec_Obj(bool up)
		{
			bool flag = up && this.Load;
			if (flag)
			{
				int pos = this.GetPos(this.lastlvl);
				bool flag2 = pos > this.GetPos(this.maxlvl);
				if (flag2)
				{
					this.Load = false;
					return;
				}
				GameObject gameObject = base.transform.FindChild("scrollview/lvlitem").gameObject;
				int num = pos * this.CountForOne;
				int num2 = num - this.CountForOne + 1;
				this.lastlvl = num + 1;
				for (int i = num2; i <= num; i++)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					gameObject2.SetActive(true);
					gameObject2.transform.SetParent(this.lvlView.transform, false);
					gameObject2.name = i.ToString();
					gameObject2.transform.SetAsFirstSibling();
					gameObject2.transform.FindChild("namebg/name").GetComponent<Text>().text = "第" + i + "层";
					this.setFriend(gameObject2, i);
					bool flag3 = i == this.diff_lvl;
					if (flag3)
					{
						bool flag4 = this.isthisClon != null;
						if (flag4)
						{
							UnityEngine.Object.Destroy(this.isthisClon);
						}
						this.isthisClon = UnityEngine.Object.Instantiate<GameObject>(this.isthis);
						this.isthisClon.transform.localPosition = Vector3.zero;
						this.isthisClon.SetActive(true);
						bool activeSelf = gameObject2.transform.FindChild("fplayer").gameObject.activeSelf;
						if (activeSelf)
						{
							gameObject2.transform.FindChild("fplayer").gameObject.SetActive(false);
							this.friendinfo = gameObject2.transform.FindChild("fplayer").gameObject;
						}
						this.isthisClon.transform.SetParent(gameObject2.transform, false);
					}
					this.lvlobj[i] = gameObject2;
					this.setlockAndClick(gameObject2, i);
				}
				Vector2 offsetMax;
				offsetMax.x = this.rect_Content.offsetMax.x;
				bool flag5 = pos == this.GetPos(this.maxlvl);
				if (flag5)
				{
					this.top.gameObject.SetActive(true);
					offsetMax.y = this.rect_Content.offsetMax.y + this.contentCellSize * (float)this.CountForOne + (this.top.offsetMin.y * -1f - this.top.offsetMax.y);
					this.lvlView.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.lvlView.GetComponent<RectTransform>().anchoredPosition.x, -(this.top.offsetMin.y * -1f - this.top.offsetMax.y));
				}
				else
				{
					this.top.gameObject.SetActive(false);
					offsetMax.y = this.rect_Content.offsetMax.y + this.contentCellSize * (float)this.CountForOne;
					this.lvlView.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.lvlView.GetComponent<RectTransform>().anchoredPosition.x, 0f);
				}
				this.rect_Content.offsetMax = offsetMax;
				int num3 = pos - 2;
				bool flag6 = num3 < this.GetPos(this.minlvl);
				if (flag6)
				{
					this.Load = false;
					return;
				}
				int num4 = num3 * this.CountForOne;
				int num5 = num4 - this.CountForOne + 1;
				int num6 = 0;
				this.Pastlvl = num4;
				for (int j = num5; j <= num4; j++)
				{
					bool flag7 = this.lvlobj.ContainsKey(j);
					if (flag7)
					{
						UnityEngine.Object.Destroy(this.lvlobj[j]);
						this.lvlobj.Remove(j);
						num6++;
					}
				}
				bool flag8 = num6 > 0;
				if (flag8)
				{
					Vector2 offsetMin;
					offsetMin.x = this.rect_Content.offsetMin.x;
					bool flag9 = num3 == this.GetPos(this.minlvl);
					if (flag9)
					{
						this.down.gameObject.SetActive(false);
						offsetMin.y = this.rect_Content.offsetMin.y + this.contentCellSize * (float)num6 + (this.down.offsetMin.y - this.down.offsetMax.y * -1f);
					}
					else
					{
						offsetMin.y = this.rect_Content.offsetMin.y + this.contentCellSize * (float)num6;
					}
					this.rect_Content.offsetMin = offsetMin;
				}
			}
			else
			{
				int pos2 = this.GetPos(this.Pastlvl);
				bool flag10 = pos2 < this.GetPos(this.minlvl);
				if (flag10)
				{
					this.Load = false;
					return;
				}
				GameObject gameObject3 = base.transform.FindChild("scrollview/lvlitem").gameObject;
				int num7 = pos2 * this.CountForOne;
				int num8 = num7 - this.CountForOne + 1;
				this.Pastlvl = num8 - 1;
				for (int k = num7; k >= num8; k--)
				{
					GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(gameObject3);
					gameObject4.SetActive(true);
					gameObject4.transform.SetParent(this.lvlView.transform, false);
					gameObject4.name = k.ToString();
					gameObject4.transform.SetAsLastSibling();
					gameObject4.transform.FindChild("namebg/name").GetComponent<Text>().text = "第" + k + "层";
					this.lvlobj[k] = gameObject4;
					this.setFriend(gameObject4, k);
					bool flag11 = k == this.diff_lvl;
					if (flag11)
					{
						bool flag12 = this.isthisClon != null;
						if (flag12)
						{
							UnityEngine.Object.Destroy(this.isthisClon);
						}
						this.isthisClon = UnityEngine.Object.Instantiate<GameObject>(this.isthis);
						this.isthisClon.transform.localPosition = Vector3.zero;
						this.isthisClon.SetActive(true);
						bool activeSelf2 = gameObject4.transform.FindChild("fplayer").gameObject.activeSelf;
						if (activeSelf2)
						{
							gameObject4.transform.FindChild("fplayer").gameObject.SetActive(false);
							this.friendinfo = gameObject4.transform.FindChild("fplayer").gameObject;
						}
						this.isthisClon.transform.SetParent(gameObject4.transform, false);
					}
					this.setlockAndClick(gameObject4, k);
				}
				Vector2 offsetMin2;
				offsetMin2.x = this.rect_Content.offsetMin.x;
				bool flag13 = pos2 == this.GetPos(this.minlvl);
				if (flag13)
				{
					this.down.gameObject.SetActive(true);
					offsetMin2.y = this.rect_Content.offsetMin.y - this.contentCellSize * (float)this.CountForOne - (this.down.offsetMin.y - this.down.offsetMax.y * -1f);
				}
				else
				{
					this.down.gameObject.SetActive(false);
					offsetMin2.y = this.rect_Content.offsetMin.y - this.contentCellSize * (float)this.CountForOne;
				}
				this.rect_Content.offsetMin = offsetMin2;
				int num9 = pos2 + 2;
				bool flag14 = num9 > this.GetPos(this.maxlvl);
				if (flag14)
				{
					this.Load = false;
					return;
				}
				int num10 = num9 * this.CountForOne;
				int num11 = num10 - this.CountForOne + 1;
				int num12 = 0;
				this.lastlvl = num10;
				for (int l = num11; l <= num10; l++)
				{
					bool flag15 = this.lvlobj.ContainsKey(l);
					if (flag15)
					{
						UnityEngine.Object.Destroy(this.lvlobj[l]);
						this.lvlobj.Remove(l);
						num12++;
					}
				}
				bool flag16 = num12 > 0;
				if (flag16)
				{
					Vector2 offsetMax2;
					offsetMax2.x = this.rect_Content.offsetMax.x;
					bool flag17 = num9 == this.GetPos(this.maxlvl);
					if (flag17)
					{
						this.top.gameObject.SetActive(false);
						offsetMax2.y = this.rect_Content.offsetMax.y - this.contentCellSize * (float)num12 - (this.top.offsetMin.y * -1f - this.top.offsetMax.y);
						this.lvlView.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.lvlView.GetComponent<RectTransform>().anchoredPosition.x, 0f);
					}
					else
					{
						offsetMax2.y = this.rect_Content.offsetMax.y - this.contentCellSize * (float)num12;
					}
					this.rect_Content.offsetMax = offsetMax2;
				}
			}
			this.Load = false;
		}

		private void addClick(GameObject go, int lvl)
		{
			new BaseButton(go.transform, 1, 1).onClick = delegate(GameObject oo)
			{
				this.locklvl(oo, lvl);
			};
		}

		private void locklvl(GameObject go, int lvl)
		{
			this.diff_lvl = lvl;
			bool flag = this.isthisClon != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.isthisClon);
			}
			this.isthisClon = UnityEngine.Object.Instantiate<GameObject>(this.isthis);
			this.isthisClon.transform.localPosition = Vector3.zero;
			this.isthisClon.SetActive(true);
			this.isthisClon.transform.SetParent(go.transform, false);
			this.setInfo_One(lvl);
			bool flag2 = this.friendinfo != null && !this.friendinfo.activeSelf;
			if (flag2)
			{
				this.friendinfo.SetActive(true);
			}
			bool activeSelf = go.transform.FindChild("fplayer").gameObject.activeSelf;
			if (activeSelf)
			{
				go.transform.FindChild("fplayer").gameObject.SetActive(false);
				this.friendinfo = go.transform.FindChild("fplayer").gameObject;
			}
		}

		private void setlockAndClick(GameObject go, int lvl)
		{
			bool flag = lvl <= this.tasklvl;
			if (flag)
			{
				this.addClick(go, lvl);
				go.transform.FindChild("namebg").gameObject.SetActive(true);
				go.transform.FindChild("lock").gameObject.SetActive(false);
			}
			else
			{
				go.transform.FindChild("namebg").gameObject.SetActive(false);
				go.transform.FindChild("lock").gameObject.SetActive(true);
			}
		}

		private void setlock()
		{
			foreach (int current in this.lvlobj.Keys)
			{
				bool flag = current <= ModelBase<A3_ActiveModel>.getInstance().nowlvl + 1;
				if (flag)
				{
					this.lvlobj[current].transform.FindChild("namebg").gameObject.SetActive(true);
					this.lvlobj[current].transform.FindChild("lock").gameObject.SetActive(false);
				}
				else
				{
					this.lvlobj[current].transform.FindChild("namebg").gameObject.SetActive(false);
					this.lvlobj[current].transform.FindChild("lock").gameObject.SetActive(true);
				}
			}
		}

		private void setInfo_One(int lvl)
		{
			Variant variant = SvrLevelConfig.instacne.get_level_data(104u);
			for (int i = 0; i < variant["info"].Count; i++)
			{
				string[] array = variant["info"][i]["lvl"].Split(new char[]
				{
					','
				});
				int num = int.Parse(array[0]);
				int num2 = int.Parse(array[1]);
				bool flag = lvl >= num && lvl <= num2;
				if (flag)
				{
					string text = variant["info"][i]["des"];
					base.transform.FindChild("info/context").GetComponent<Text>().text = text.Replace("{0}", lvl.ToString());
					base.transform.FindChild("cue/info").GetComponent<Text>().text = variant["info"][i]["des2"];
					a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(variant["info"][i]["item"]);
					for (int j = 0; j < base.transform.FindChild("cue/icon").childCount; j++)
					{
						UnityEngine.Object.Destroy(base.transform.FindChild("cue/icon").GetChild(j).gameObject);
					}
					IconImageMgr.getInstance().createA3ItemIcon(itemDataById, false, -1, 1f, false, -1, 0, false, false, false, -1, false, false).transform.SetParent(base.transform.FindChild("cue/icon"), false);
					uint id = uint.Parse(variant["info"][i]["item"]);
					new BaseButton(base.transform.FindChild("cue/icon"), 1, 1).onClick = delegate(GameObject go)
					{
						this.tip.SetActive(true);
						a3_ItemData itemDataById2 = ModelBase<a3_BagModel>.getInstance().getItemDataById(id);
						this.tip.transform.FindChild("text_bg/name/namebg").GetComponent<Text>().text = itemDataById2.item_name;
						this.tip.transform.FindChild("text_bg/name/namebg").GetComponent<Text>().color = Globle.getColorByQuality(itemDataById2.quality);
						bool flag2 = itemDataById2.use_limit <= 0;
						if (flag2)
						{
							this.tip.transform.FindChild("text_bg/name/dengji").GetComponent<Text>().text = "无限制";
						}
						else
						{
							this.tip.transform.FindChild("text_bg/name/dengji").GetComponent<Text>().text = itemDataById2.use_limit + "转";
						}
						this.tip.transform.FindChild("text_bg/text").GetComponent<Text>().text = StringUtils.formatText(itemDataById2.desc);
						this.tip.transform.FindChild("text_bg/iconbg/icon").GetComponent<Image>().sprite = (Resources.Load(itemDataById2.file, typeof(Sprite)) as Sprite);
						new BaseButton(this.tip.transform.FindChild("close_btn"), 1, 1).onClick = new Action<GameObject>(this.<setInfo_One>b__45_1);
					};
					break;
				}
			}
		}

		private void setFriend(GameObject go, int lvl)
		{
			uint num = 0u;
			foreach (itemFriendData current in BaseProxy<FriendProxy>.getInstance().FriendDataList.Values)
			{
				bool flag = current.mlzd_lv == lvl;
				if (flag)
				{
					bool flag2 = num > 0u;
					if (flag2)
					{
						bool flag3 = current.zhuan > BaseProxy<FriendProxy>.getInstance().FriendDataList[num].zhuan;
						if (flag3)
						{
							num = current.cid;
						}
						else
						{
							bool flag4 = current.zhuan == BaseProxy<FriendProxy>.getInstance().FriendDataList[num].zhuan;
							if (flag4)
							{
								bool flag5 = current.lvl > BaseProxy<FriendProxy>.getInstance().FriendDataList[num].lvl;
								if (flag5)
								{
									num = current.cid;
								}
							}
						}
					}
					else
					{
						num = current.cid;
					}
				}
			}
			bool flag6 = num > 0u;
			if (flag6)
			{
				go.transform.FindChild("fplayer").gameObject.SetActive(true);
				go.transform.FindChild("fplayer").GetComponent<Image>().sprite = (Resources.Load("icon/npctask/" + BaseProxy<FriendProxy>.getInstance().FriendDataList[num].carr, typeof(Sprite)) as Sprite);
				go.transform.FindChild("fplayer/name").GetComponent<Text>().text = BaseProxy<FriendProxy>.getInstance().FriendDataList[num].name;
			}
			else
			{
				go.transform.FindChild("fplayer").gameObject.SetActive(false);
			}
		}

		private void setInfo(int lvl)
		{
			Variant variant = SvrLevelConfig.instacne.get_level_data(104u);
			base.transform.FindChild("info/context").GetComponent<Text>().text = variant["info"][lvl - 1]["des"];
			base.transform.FindChild("cue/info").GetComponent<Text>().text = variant["info"][lvl - 1]["des2"];
			a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(variant["info"][lvl - 1]["item"]);
			for (int i = 0; i < base.transform.FindChild("cue/icon").childCount; i++)
			{
				UnityEngine.Object.Destroy(base.transform.FindChild("cue/icon").GetChild(i).gameObject);
			}
			IconImageMgr.getInstance().createA3ItemIcon(itemDataById, false, -1, 1f, false, -1, 0, false, false, false, -1, false, false).transform.SetParent(base.transform.FindChild("cue/icon"), false);
			uint id = uint.Parse(variant["info"][lvl - 1]["item"]);
			new BaseButton(base.transform.FindChild("cue/icon"), 1, 1).onClick = delegate(GameObject go)
			{
				this.tip.SetActive(true);
				a3_ItemData itemDataById2 = ModelBase<a3_BagModel>.getInstance().getItemDataById(id);
				this.tip.transform.FindChild("text_bg/name/namebg").GetComponent<Text>().text = itemDataById2.item_name;
				this.tip.transform.FindChild("text_bg/name/namebg").GetComponent<Text>().color = Globle.getColorByQuality(itemDataById2.quality);
				bool flag = itemDataById2.use_limit <= 0;
				if (flag)
				{
					this.tip.transform.FindChild("text_bg/name/dengji").GetComponent<Text>().text = "无限制";
				}
				else
				{
					this.tip.transform.FindChild("text_bg/name/dengji").GetComponent<Text>().text = itemDataById2.use_limit + "转";
				}
				this.tip.transform.FindChild("text_bg/text").GetComponent<Text>().text = StringUtils.formatText(itemDataById2.desc);
				this.tip.transform.FindChild("text_bg/iconbg/icon").GetComponent<Image>().sprite = (Resources.Load(itemDataById2.file, typeof(Sprite)) as Sprite);
				new BaseButton(this.tip.transform.FindChild("close_btn"), 1, 1).onClick = new Action<GameObject>(this.<setInfo>b__47_1);
			};
		}

		private void onshowlvl(int lvl)
		{
			bool flag = lvl >= this.lvlobj.Count;
			if (flag)
			{
				lvl = this.lvlobj.Count;
			}
			this.diff_lvl = lvl;
			this.isthis.gameObject.SetActive(true);
			this.isthis.transform.SetParent(this.lvlobj[lvl].transform, false);
			float y = 0f;
			float y2 = this.lvlView.GetComponent<GridLayoutGroup>().cellSize.y;
			switch (lvl)
			{
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
				y = 464f;
				break;
			case 6:
			case 7:
			case 8:
				y = 18f;
				break;
			}
			this.parView.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.parView.GetComponent<RectTransform>().anchoredPosition.x, y);
			this.setInfo(lvl);
		}

		private int GetPos(int lvl)
		{
			bool flag = lvl % this.CountForOne > 0;
			int result;
			if (flag)
			{
				result = lvl / this.CountForOne + 1;
			}
			else
			{
				result = lvl / this.CountForOne;
			}
			return result;
		}
	}
}
