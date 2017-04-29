using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_active_forchest : a3BaseActive
	{
		public static a3_active_forchest instance;

		public bool box_ing;

		private BaseButton goToMap;

		private GameObject help;

		private string[] time1;

		private string[] time2;

		private GameObject pre;

		private Transform contain;

		public a3_active_forchest(Window win, string pathStr) : base(win, pathStr)
		{
		}

		public override void init()
		{
			this.pre = base.transform.FindChild("bg2/items/Image").gameObject;
			this.contain = base.transform.FindChild("bg2/items/scroll/contain");
			this.help = base.transform.FindChild("help").gameObject;
			new BaseButton(base.transform.FindChild("bg2/hp"), 1, 1).onClick = delegate(GameObject go)
			{
				this.help.SetActive(true);
			};
			new BaseButton(base.transform.FindChild("help/panel_help/bg/closeBtn"), 1, 1).onClick = delegate(GameObject go)
			{
				this.help.SetActive(false);
			};
			XMLMgr expr_A4 = XMLMgr.instance;
			SXML sXML = (expr_A4 != null) ? expr_A4.GetSXML("box.box", "id==" + 1) : null;
			this.time1 = sXML.getString("time1").Split(new char[]
			{
				','
			});
			this.time2 = sXML.getString("time2").Split(new char[]
			{
				','
			});
			new BaseButton(base.transform.FindChild("bg2/btn_transmit"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag = !this.box_ing;
				if (flag)
				{
					flytxt.instance.fly("未到活动开启时间！", 0, default(Color), null);
				}
				else
				{
					a3_active.instance.map_light = true;
					InterfaceMgr.getInstance().worldmap = true;
					InterfaceMgr.getInstance().open(InterfaceMgr.WORLD_MAP, null, false).winItem.transform.SetAsLastSibling();
				}
			};
		}

		public override void onShowed()
		{
			this.destorycontain();
			this.image_show();
			int hour = DateTime.Now.Hour;
			int minute = DateTime.Now.Minute;
			bool flag = (float)hour + (float)minute / 60f > (float)int.Parse(this.time1[0]) + (float)(int.Parse(this.time1[1]) - 15) / 60f && (float)hour + (float)minute / 60f <= (float)int.Parse(this.time1[0]) + (float)(int.Parse(this.time1[1]) + 25) / 60f;
			if (flag)
			{
				this.box_ing = true;
			}
			else
			{
				bool flag2 = (float)hour + (float)minute / 60f > (float)int.Parse(this.time2[0]) + (float)(int.Parse(this.time2[1]) - 15) / 60f && (float)hour + (float)minute / 60f <= (float)int.Parse(this.time2[0]) + (float)(int.Parse(this.time2[1]) + 25) / 60f;
				if (flag2)
				{
					this.box_ing = true;
				}
				else
				{
					this.box_ing = false;
				}
			}
		}

		public void image_show()
		{
			List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("box.award", "");
			for (int i = 0; i < sXMLList.Count; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pre);
				gameObject.SetActive(true);
				gameObject.transform.SetParent(this.contain, false);
				int cc = sXMLList[i].getInt("item_id");
				int quality = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)cc).quality;
				gameObject.transform.FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/itemborder/b039_0" + quality);
				gameObject.transform.FindChild("icon").GetComponent<Image>().sprite = (Resources.Load("icon/item/" + cc, typeof(Sprite)) as Sprite);
				BaseButton baseButton = new BaseButton(gameObject.transform, 1, 1);
				baseButton.onClick = delegate(GameObject goo)
				{
					ArrayList arrayList = new ArrayList();
					arrayList.Add((uint)cc);
					arrayList.Add(1);
					InterfaceMgr.getInstance().open(InterfaceMgr.A3_MINITIP, arrayList, false);
				};
			}
		}

		public void destorycontain()
		{
			bool flag = this.contain.transform.childCount > 0;
			if (flag)
			{
				for (int i = this.contain.transform.childCount - 1; i >= 0; i--)
				{
					UnityEngine.Object.Destroy(this.contain.transform.GetChild(i).gameObject);
				}
			}
		}
	}
}
