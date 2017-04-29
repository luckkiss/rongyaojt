using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_active_findbtu : a3BaseActive
	{
		private bool open = false;

		public static a3_active_findbtu instans;

		private Text Time;

		private Transform itemViewCon;

		private GameObject tip;

		public GameObject monobj;

		private GameObject Camobj;

		private List<int> mapid = new List<int>();

		private int runTime = 0;

		public a3_active_findbtu(Window win, string pathStr) : base(win, pathStr)
		{
		}

		public override void init()
		{
			new BaseButton(base.transform.FindChild("b/help"), 1, 1).onClick = delegate(GameObject go)
			{
				base.transform.FindChild("b/tishi").gameObject.SetActive(true);
			};
			new BaseButton(base.transform.FindChild("b/tishi/tach"), 1, 1).onClick = (new BaseButton(base.transform.FindChild("b/tishi/close"), 1, 1).onClick = delegate(GameObject go)
			{
				base.transform.FindChild("b/tishi").gameObject.SetActive(false);
			});
			this.tip = base.transform.FindChild("close_desc").gameObject;
			this.itemViewCon = base.transform.FindChild("a/body/itemView/content");
			this.Time = base.transform.FindChild("a/time").GetComponent<Text>();
			this.setrewards();
			base.getEventTrigerByPath("b/avatar_touch").onDrag = new EventTriggerListener.VectorDelegate(this.OnDrag);
		}

		public override void onShowed()
		{
			a3_active_findbtu.instans = this;
			base.transform.FindChild("b/tishi").gameObject.SetActive(false);
			BaseProxy<FindBestoProxy>.getInstance().addEventListener(FindBestoProxy.EVENT_INFO, new Action<GameEvent>(this.oninfo));
			BaseProxy<FindBestoProxy>.getInstance().getinfo();
			this.tip.SetActive(false);
		}

		public override void onClose()
		{
			a3_active_findbtu.instans = null;
			this.runTime = 0;
			BaseProxy<FindBestoProxy>.getInstance().removeEventListener(FindBestoProxy.EVENT_INFO, new Action<GameEvent>(this.oninfo));
			this.SetDisposeAvatar();
		}

		private void OnDrag(GameObject go, Vector2 delta)
		{
			bool flag = this.monobj != null;
			if (flag)
			{
				this.monobj.transform.Rotate(Vector3.up, -delta.x);
			}
		}

		private void SetCreateAvatar()
		{
			this.SetDisposeAvatar();
			GameObject original = Resources.Load<GameObject>("monster/" + 10066);
			this.monobj = (UnityEngine.Object.Instantiate(original, new Vector3(-153.48f, 0.48f, 0f), Quaternion.identity) as GameObject);
			Transform[] componentsInChildren = this.monobj.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.layer = EnumLayer.LM_FX;
			}
			Transform transform2 = this.monobj.transform.FindChild("model");
			Animator component = transform2.GetComponent<Animator>();
			component.cullingMode = AnimatorCullingMode.AlwaysAnimate;
			transform2.gameObject.AddComponent<Summon_Base_Event>();
			GameObject original2 = Resources.Load<GameObject>("profession/avatar_ui/roleinfo_ui_camera");
			this.Camobj = UnityEngine.Object.Instantiate<GameObject>(original2);
			Camera componentInChildren = this.Camobj.GetComponentInChildren<Camera>();
			transform2.Rotate(Vector3.up, 180f);
			transform2.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
			bool flag = componentInChildren != null;
			if (flag)
			{
				float orthographicSize = componentInChildren.orthographicSize * 1920f / 1080f * (float)Screen.height / (float)Screen.width;
				componentInChildren.orthographicSize = orthographicSize;
			}
		}

		private void SetDisposeAvatar()
		{
			bool flag = this.monobj != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.monobj);
				UnityEngine.Object.Destroy(this.Camobj);
			}
		}

		private void oninfo(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data.ContainsKey("count_down");
			if (flag)
			{
				this.runTime = data["count_down"];
			}
			bool flag2 = data.ContainsKey("mapid") && data["mapid"].Count > 0;
			if (flag2)
			{
				this.open = true;
			}
			else
			{
				this.open = false;
			}
			bool flag3 = this.open;
			if (flag3)
			{
				this.mapid.Clear();
				for (int i = 0; i < data["mapid"].Count; i++)
				{
					this.mapid.Add(data["mapid"][i]);
				}
				base.transform.FindChild("b").gameObject.SetActive(true);
				base.transform.FindChild("a").gameObject.SetActive(false);
				this.SetCreateAvatar();
				this.setBtns();
			}
			else
			{
				base.transform.FindChild("b").gameObject.SetActive(false);
				base.transform.FindChild("a").gameObject.SetActive(true);
				this.SetDisposeAvatar();
				bool flag4 = this.runTime > 0;
				if (flag4)
				{
					this.Time.gameObject.SetActive(true);
					base.transform.FindChild("a/no_open").gameObject.SetActive(false);
					a3_active expr_19B = a3_active.onshow;
					if (expr_19B != null)
					{
						expr_19B.Runtimer_bt(this.runTime);
					}
				}
				else
				{
					this.Time.gameObject.SetActive(false);
					base.transform.FindChild("a/no_open").gameObject.SetActive(true);
				}
			}
		}

		public void showtime(int time)
		{
			TimeSpan timeSpan = new TimeSpan(0, 0, time);
			bool flag = timeSpan.Minutes >= 10 && timeSpan.Seconds >= 10;
			if (flag)
			{
				this.Time.text = string.Concat(new object[]
				{
					timeSpan.Hours,
					":",
					timeSpan.Minutes,
					":",
					timeSpan.Seconds
				});
			}
			else
			{
				bool flag2 = timeSpan.Minutes < 10 && timeSpan.Seconds >= 10;
				if (flag2)
				{
					this.Time.text = string.Concat(new object[]
					{
						timeSpan.Hours,
						":0",
						timeSpan.Minutes,
						":",
						timeSpan.Seconds
					});
				}
				else
				{
					bool flag3 = timeSpan.Minutes >= 10 && timeSpan.Seconds < 10;
					if (flag3)
					{
						this.Time.text = string.Concat(new object[]
						{
							timeSpan.Hours,
							":",
							timeSpan.Minutes,
							":0",
							timeSpan.Seconds
						});
					}
					else
					{
						bool flag4 = timeSpan.Minutes < 10 && timeSpan.Seconds < 10;
						if (flag4)
						{
							this.Time.text = string.Concat(new object[]
							{
								timeSpan.Hours,
								":0",
								timeSpan.Minutes,
								":0",
								timeSpan.Seconds
							});
						}
					}
				}
			}
		}

		private void setBtns()
		{
			for (int i = 0; i < base.transform.FindChild("b/btns").childCount; i++)
			{
				Variant singleMapConf = SvrMapConfig.instance.getSingleMapConf((uint)this.mapid[i]);
				string text = singleMapConf.ContainsKey("map_name") ? singleMapConf["map_name"]._str : "--";
				base.transform.FindChild("b/btns").GetChild(i).FindChild("name").GetComponent<Text>().text = text;
				base.transform.FindChild("b/btns").GetChild(i).FindChild("lvl").GetComponent<Text>().text = singleMapConf["lv_up"] + "转" + singleMapConf["lv"] + "级";
				int num = singleMapConf.ContainsKey("icon") ? singleMapConf["icon"]._int : 1;
				base.transform.FindChild("b/btns").GetChild(i).GetComponent<Image>().sprite = (Resources.Load("icon/treasure/" + num, typeof(Sprite)) as Sprite);
				bool canfly = false;
				int point = singleMapConf.ContainsKey("point_id") ? singleMapConf["point_id"]._int : 101;
				bool flag = ModelBase<PlayerModel>.getInstance().up_lvl > singleMapConf["lv_up"];
				if (flag)
				{
					canfly = true;
				}
				else
				{
					bool flag2 = ModelBase<PlayerModel>.getInstance().up_lvl == singleMapConf["lv_up"];
					if (flag2)
					{
						bool flag3 = ModelBase<PlayerModel>.getInstance().lvl >= singleMapConf["lv"];
						if (flag3)
						{
							canfly = true;
						}
						else
						{
							canfly = false;
						}
					}
					else
					{
						canfly = false;
					}
				}
				new BaseButton(base.transform.FindChild("b/btns").GetChild(i).FindChild("go"), 1, 1).onClick = delegate(GameObject go)
				{
					bool canfly = canfly;
					if (canfly)
					{
						worldmapsubwin.id = point;
						InterfaceMgr.getInstance().open(InterfaceMgr.WORLD_MAP_SUB, null, false);
					}
					else
					{
						flytxt.instance.fly("等级不够，无法前往目的地", 0, default(Color), null);
					}
				};
			}
		}

		private void setrewards()
		{
			GameObject gameObject = base.transform.FindChild("a/body/itemView/item").gameObject;
			SXML sXML = XMLMgr.instance.GetSXML("treasure_reward", "");
			List<SXML> nodeList = sXML.GetNodeList("reward", "");
			for (int i = 0; i < nodeList.Count; i++)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				gameObject2.SetActive(true);
				gameObject2.transform.SetParent(this.itemViewCon, false);
				gameObject2.transform.FindChild("count").GetComponent<Text>().text = nodeList[i].getInt("cost").ToString();
				int id = nodeList[i].getInt("item_id");
				GameObject gameObject3 = gameObject2.transform.FindChild("icon").gameObject;
				GameObject gameObject4 = IconImageMgr.getInstance().createA3ItemIcon(ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)id), false, -1, 0.8f, false, -1, 0, false, false, false, -1, false, false);
				gameObject4.transform.SetParent(gameObject3.transform, false);
				new BaseButton(gameObject3.transform, 1, 1).onClick = delegate(GameObject go)
				{
					this.tip.SetActive(true);
					SXML sXML2 = XMLMgr.instance.GetSXML("item.item", "id==" + id);
					this.tip.transform.FindChild("text_bg/name/namebg").GetComponent<Text>().text = sXML2.getString("item_name");
					this.tip.transform.FindChild("text_bg/name/namebg").GetComponent<Text>().color = Globle.getColorByQuality(sXML2.getInt("quality"));
					bool flag = sXML2.getInt("use_limit") == 0;
					if (flag)
					{
						this.tip.transform.FindChild("text_bg/name/dengji").GetComponent<Text>().text = "无限制";
					}
					else
					{
						this.tip.transform.FindChild("text_bg/name/dengji").GetComponent<Text>().text = sXML2.getString("use_limit") + "转";
					}
					this.tip.transform.FindChild("text_bg/text").GetComponent<Text>().text = StringUtils.formatText(sXML2.getString("desc"));
					this.tip.transform.FindChild("text_bg/iconbg/icon").GetComponent<Image>().sprite = (Resources.Load("icon/item/" + sXML2.getInt("icon_file"), typeof(Sprite)) as Sprite);
					new BaseButton(this.tip.transform.FindChild("close_btn"), 1, 1).onClick = new Action<GameObject>(this.<setrewards>b__19_1);
				};
			}
		}
	}
}
