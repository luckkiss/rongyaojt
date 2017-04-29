using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class A3_FindBesto : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly A3_FindBesto.<>c <>9 = new A3_FindBesto.<>c();

			public static Action<GameObject> <>9__5_0;

			public static Action<GameObject> <>9__5_2;

			internal void <init>b__5_0(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_FINDBESTO);
			}

			internal void <init>b__5_2(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_FINDBESTO);
			}
		}

		private Transform itemViewCon;

		private GameObject isthis;

		private GameObject tip;

		private GameObject tishi;

		public static A3_FindBesto instan;

		private bool open = false;

		public int count = -1;

		public override void init()
		{
			A3_FindBesto.instan = this;
			this.tip = base.transform.FindChild("close_desc").gameObject;
			this.tishi = base.transform.FindChild("tishi").gameObject;
			BaseButton arg_73_0 = new BaseButton(base.transform.FindChild("close"), 1, 1);
			Action<GameObject> arg_73_1;
			if ((arg_73_1 = A3_FindBesto.<>c.<>9__5_0) == null)
			{
				arg_73_1 = (A3_FindBesto.<>c.<>9__5_0 = new Action<GameObject>(A3_FindBesto.<>c.<>9.<init>b__5_0));
			}
			arg_73_0.onClick = arg_73_1;
			new BaseButton(base.transform.FindChild("do"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag = this.count >= 0;
				if (flag)
				{
					this.tishi.SetActive(true);
				}
				else
				{
					flytxt.instance.fly("未选择兑换物品!", 0, default(Color), null);
				}
			};
			BaseButton arg_D8_0 = new BaseButton(base.transform.FindChild("bg/close_bg"), 1, 1);
			Action<GameObject> arg_D8_1;
			if ((arg_D8_1 = A3_FindBesto.<>c.<>9__5_2) == null)
			{
				arg_D8_1 = (A3_FindBesto.<>c.<>9__5_2 = new Action<GameObject>(A3_FindBesto.<>c.<>9.<init>b__5_2));
			}
			arg_D8_0.onClick = arg_D8_1;
			new BaseButton(this.tishi.transform.FindChild("yes"), 1, 1).onClick = delegate(GameObject go)
			{
				BaseProxy<FindBestoProxy>.getInstance().sendMap(this.count);
				this.tishi.SetActive(false);
			};
			new BaseButton(this.tishi.transform.FindChild("no"), 1, 1).onClick = delegate(GameObject go)
			{
				this.tishi.SetActive(false);
			};
			this.itemViewCon = base.transform.FindChild("body/itemView/content");
			this.isthis = base.transform.FindChild("body/itemView/this").gameObject;
			this.intoUI();
		}

		public override void onShowed()
		{
			this.count = -1;
			this.isthis.gameObject.SetActive(false);
			this.tishi.SetActive(false);
			this.refreCount();
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
		}

		public override void onClosed()
		{
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
		}

		public void refreCount()
		{
			base.transform.FindChild("count").GetComponent<Text>().text = ModelBase<PlayerModel>.getInstance().treasure_num.ToString();
		}

		public void intoUI()
		{
			GameObject gameObject = base.transform.FindChild("body/itemView/item").gameObject;
			SXML sXML = XMLMgr.instance.GetSXML("treasure_reward", "");
			List<SXML> nodeList = sXML.GetNodeList("reward", "");
			for (int i = 0; i < nodeList.Count; i++)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				gameObject2.SetActive(true);
				gameObject2.transform.SetParent(this.itemViewCon, false);
				gameObject2.transform.FindChild("name").GetComponent<Text>().text = nodeList[i].getString("name");
				gameObject2.transform.FindChild("count").GetComponent<Text>().text = "x" + nodeList[i].getInt("cost").ToString();
				gameObject2.transform.FindChild("num").GetComponent<Text>().text = nodeList[i].getInt("nums").ToString();
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
					new BaseButton(this.tip.transform.FindChild("close_btn"), 1, 1).onClick = new Action<GameObject>(this.<intoUI>b__11_1);
				};
				gameObject2.name = nodeList[i].getInt("id").ToString();
				new BaseButton(gameObject2.transform, 1, 1).onClick = delegate(GameObject go)
				{
					this.count = int.Parse(go.name);
					this.isthis.gameObject.SetActive(true);
					this.isthis.transform.SetParent(go.transform);
					this.isthis.transform.localPosition = Vector2.zero;
				};
			}
		}
	}
}
