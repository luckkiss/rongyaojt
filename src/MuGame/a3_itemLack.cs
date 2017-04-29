using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_itemLack : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_itemLack.<>c <>9 = new a3_itemLack.<>c();

			public static Action<GameObject> <>9__6_0;

			internal void <init>b__6_0(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_ITEMLACK);
			}
		}

		private a3_ItemData item_data;

		private GameObject toget;

		private Transform Btncon;

		public string closewindow = null;

		public static a3_itemLack intans;

		private GameObject avatarobj = null;

		private bool ToWin = false;

		public override void init()
		{
			a3_itemLack.intans = this;
			this.toget = base.transform.FindChild("toGet").gameObject;
			this.Btncon = base.transform.FindChild("toGet/scrollview/con");
			BaseButton arg_6E_0 = new BaseButton(base.transform.FindChild("close"), 1, 1);
			Action<GameObject> arg_6E_1;
			if ((arg_6E_1 = a3_itemLack.<>c.<>9__6_0) == null)
			{
				arg_6E_1 = (a3_itemLack.<>c.<>9__6_0 = new Action<GameObject>(a3_itemLack.<>c.<>9.<init>b__6_0));
			}
			arg_6E_0.onClick = arg_6E_1;
			new BaseButton(base.transform.FindChild("Get"), 1, 1).onClick = delegate(GameObject go)
			{
				this.toget.SetActive(true);
				this.onShowGet();
			};
		}

		public override void onShowed()
		{
			this.ToWin = false;
			base.transform.SetAsLastSibling();
			this.toget.SetActive(false);
			bool flag = this.uiData == null;
			if (!flag)
			{
				bool flag2 = this.uiData.Count != 0;
				if (flag2)
				{
					this.item_data = (a3_ItemData)this.uiData[0];
					bool flag3 = this.uiData.Count > 1;
					if (flag3)
					{
						this.closewindow = (string)this.uiData[1];
					}
					bool flag4 = this.uiData.Count > 2;
					if (flag4)
					{
						this.avatarobj = (GameObject)this.uiData[2];
					}
				}
				Transform transform = base.transform.FindChild("info");
				Transform transform2 = transform.FindChild("icon");
				bool flag5 = transform2.childCount > 0;
				if (flag5)
				{
					UnityEngine.Object.Destroy(transform2.GetChild(0).gameObject);
				}
				GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(this.item_data, false, -1, 1f, false, -1, 0, false, false, false, -1, false, false);
				gameObject.transform.SetParent(transform2, false);
				transform.FindChild("name").GetComponent<Text>().text = this.item_data.item_name;
				transform.FindChild("name").GetComponent<Text>().color = Globle.getColorByQuality(this.item_data.quality);
				transform.FindChild("desc").GetComponent<Text>().text = StringUtils.formatText(this.item_data.desc);
				bool flag6 = this.avatarobj != null;
				if (flag6)
				{
					this.avatarobj.SetActive(false);
				}
			}
		}

		public override void onClosed()
		{
			bool flag = !this.ToWin;
			if (flag)
			{
				bool flag2 = this.closewindow != null && this.closewindow != "";
				if (flag2)
				{
					this.closewindow = null;
				}
			}
			bool flag3 = this.avatarobj != null;
			if (flag3)
			{
				this.avatarobj.SetActive(true);
				this.avatarobj = null;
			}
		}

		private void closeObj()
		{
		}

		private void onShowGet()
		{
			for (int i = 0; i < this.Btncon.childCount; i++)
			{
				this.Btncon.GetChild(i).gameObject.SetActive(false);
			}
			SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + this.item_data.tpid);
			List<SXML> nodeList = sXML.GetNodeList("drop_info", "");
			bool flag = nodeList == null || nodeList.Count <= 0;
			if (!flag)
			{
				foreach (SXML current in nodeList)
				{
					int @int = current.getInt("drop_type");
					switch (@int)
					{
					case 1:
					{
						bool flag2 = this.closewindow == InterfaceMgr.SHOP_A3;
						if (!flag2)
						{
							this.Btncon.FindChild("1").gameObject.SetActive(true);
							int shopid = current.getInt("id");
							new BaseButton(this.Btncon.FindChild("1"), 1, 1).onClick = delegate(GameObject go)
							{
								InterfaceMgr.getInstance().close(this.closewindow);
								ArrayList arrayList = new ArrayList();
								arrayList.Add(shopid);
								InterfaceMgr.getInstance().open(InterfaceMgr.SHOP_A3, arrayList, false);
								this.ToWin = true;
								InterfaceMgr.getInstance().close(InterfaceMgr.A3_ITEMLACK);
							};
						}
						break;
					}
					case 2:
					{
						bool flag3 = this.closewindow == InterfaceMgr.A3_VIP;
						if (!flag3)
						{
							this.Btncon.FindChild("2").gameObject.SetActive(true);
							new BaseButton(this.Btncon.FindChild("2"), 1, 1).onClick = delegate(GameObject go)
							{
								InterfaceMgr.getInstance().close(this.closewindow);
								ArrayList arrayList = new ArrayList();
								arrayList.Add(1);
								InterfaceMgr.getInstance().open(InterfaceMgr.A3_VIP, arrayList, false);
								this.ToWin = true;
								InterfaceMgr.getInstance().close(InterfaceMgr.A3_ITEMLACK);
							};
						}
						break;
					}
					case 3:
					{
						bool flag4 = this.closewindow == InterfaceMgr.A3_COUNTERPART;
						if (!flag4)
						{
							this.Btncon.FindChild("3").gameObject.SetActive(true);
							new BaseButton(this.Btncon.FindChild("3"), 1, 1).onClick = delegate(GameObject go)
							{
								InterfaceMgr.getInstance().close(this.closewindow);
								InterfaceMgr.getInstance().open(InterfaceMgr.A3_COUNTERPART, null, false);
								this.ToWin = true;
								InterfaceMgr.getInstance().close(InterfaceMgr.A3_ITEMLACK);
							};
						}
						break;
					}
					case 4:
					{
						bool flag5 = this.closewindow == InterfaceMgr.A3_BAG;
						if (!flag5)
						{
							this.Btncon.FindChild("4").gameObject.SetActive(true);
							new BaseButton(this.Btncon.FindChild("4"), 1, 1).onClick = delegate(GameObject go)
							{
								InterfaceMgr.getInstance().close(this.closewindow);
								InterfaceMgr.getInstance().open(InterfaceMgr.A3_BAG, null, false);
								this.ToWin = true;
								InterfaceMgr.getInstance().close(InterfaceMgr.A3_ITEMLACK);
							};
						}
						break;
					}
					case 5:
					{
						bool flag6 = this.closewindow == InterfaceMgr.A3_FIRESTRECHARGEAWARD;
						if (!flag6)
						{
							this.Btncon.FindChild("5").gameObject.SetActive(true);
							new BaseButton(this.Btncon.FindChild("5"), 1, 1).onClick = delegate(GameObject go)
							{
								InterfaceMgr.getInstance().close(this.closewindow);
								InterfaceMgr.getInstance().open(InterfaceMgr.A3_FIRESTRECHARGEAWARD, null, false);
								this.ToWin = true;
								InterfaceMgr.getInstance().close(InterfaceMgr.A3_ITEMLACK);
							};
						}
						break;
					}
					case 6:
					{
						bool flag7 = !FunctionOpenMgr.instance.Check(FunctionOpenMgr.GLOBA_BOSS, false);
						if (!flag7)
						{
							bool flag8 = this.closewindow == InterfaceMgr.A3_ELITEMON;
							if (!flag8)
							{
								this.Btncon.FindChild("6").gameObject.SetActive(true);
								new BaseButton(this.Btncon.FindChild("6"), 1, 1).onClick = delegate(GameObject go)
								{
									InterfaceMgr.getInstance().close(this.closewindow);
									ArrayList arrayList = new ArrayList();
									arrayList.Add(ELITE_MONSTER_PAGE_IDX.BOSSPAGE);
									InterfaceMgr.getInstance().open(InterfaceMgr.A3_ELITEMON, arrayList, false);
									this.ToWin = true;
									InterfaceMgr.getInstance().close(InterfaceMgr.A3_ITEMLACK);
								};
							}
						}
						break;
					}
					case 7:
					{
						bool flag9 = this.closewindow == InterfaceMgr.SHOP_A3;
						if (!flag9)
						{
							this.Btncon.FindChild("7").gameObject.SetActive(true);
							int shopid1 = current.getInt("id");
							new BaseButton(this.Btncon.FindChild("7"), 1, 1).onClick = delegate(GameObject go)
							{
								InterfaceMgr.getInstance().close(this.closewindow);
								ArrayList arrayList = new ArrayList();
								arrayList.Add(shopid1);
								InterfaceMgr.getInstance().open(InterfaceMgr.SHOP_A3, arrayList, false);
								this.ToWin = true;
								InterfaceMgr.getInstance().close(InterfaceMgr.A3_ITEMLACK);
							};
						}
						break;
					}
					case 8:
					{
						bool flag10 = this.closewindow == InterfaceMgr.A3_TASK;
						if (!flag10)
						{
							this.Btncon.FindChild("8").gameObject.SetActive(true);
							new BaseButton(this.Btncon.FindChild("8"), 1, 1).onClick = delegate(GameObject go)
							{
								InterfaceMgr.getInstance().close(this.closewindow);
								a3_task.openwin = 5;
								InterfaceMgr.getInstance().open(InterfaceMgr.A3_TASK, null, false);
								this.ToWin = true;
								InterfaceMgr.getInstance().close(InterfaceMgr.A3_ITEMLACK);
							};
						}
						break;
					}
					case 9:
					{
						bool flag11 = this.closewindow == InterfaceMgr.A3_SHEJIAO;
						if (!flag11)
						{
							this.Btncon.FindChild("9").gameObject.SetActive(true);
							new BaseButton(this.Btncon.FindChild("9"), 1, 1).onClick = delegate(GameObject go)
							{
								InterfaceMgr.getInstance().close(this.closewindow);
								ArrayList arrayList = new ArrayList();
								arrayList.Add(0);
								arrayList.Add(2);
								InterfaceMgr.getInstance().open(InterfaceMgr.A3_SHEJIAO, arrayList, false);
								this.ToWin = true;
								InterfaceMgr.getInstance().close(InterfaceMgr.A3_ITEMLACK);
							};
						}
						break;
					}
					}
				}
			}
		}
	}
}
