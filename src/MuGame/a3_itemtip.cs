using GameFramework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_itemtip : Window
	{
		private uint curid;

		private a3_BagItemData item_data;

		private Scrollbar num_bar;

		private int cur_num;

		private bool is_put_in = false;

		private equip_tip_type tiptype = equip_tip_type.Bag_tip;

		private Transform bodyNum;

		public static string closeWin = null;

		private float recycle_price;

		private bool needEvent = true;

		private bool togo = false;

		public override void init()
		{
			BaseButton baseButton = new BaseButton(base.transform.FindChild("touch"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onclose);
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("info/use"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.ondo);
			BaseButton baseButton3 = new BaseButton(base.transform.FindChild("info/sell"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.onsell);
			BaseButton baseButton4 = new BaseButton(base.transform.FindChild("info/bodyNum/btn_add"), 1, 1);
			baseButton4.onClick = new Action<GameObject>(this.onadd);
			BaseButton baseButton5 = new BaseButton(base.transform.FindChild("info/bodyNum/btn_reduce"), 1, 1);
			baseButton5.onClick = new Action<GameObject>(this.onreduce);
			BaseButton baseButton6 = new BaseButton(base.transform.FindChild("info/put"), 1, 1);
			baseButton6.onClick = new Action<GameObject>(this.onput);
			BaseButton baseButton7 = new BaseButton(base.transform.FindChild("info/bodyNum/max"), 1, 1);
			baseButton7.onClick = new Action<GameObject>(this.onmax);
			BaseButton baseButton8 = new BaseButton(base.transform.FindChild("info/bodyNum/min"), 1, 1);
			baseButton8.onClick = new Action<GameObject>(this.onmin);
			this.num_bar = base.transform.FindChild("info/bodyNum/Scrollbar").GetComponent<Scrollbar>();
			this.num_bar.onValueChanged.AddListener(new UnityAction<float>(this.onNumChange));
			this.recycle_price = XMLMgr.instance.GetSXMLList("npc_shop.float_change", "")[0].getFloat("recycle_price") / 100f;
			this.bodyNum = base.transform.FindChild("info/bodyNum");
		}

		private void onmin(GameObject obj)
		{
			this.cur_num = 1;
			this.num_bar.value = (float)this.cur_num / (float)this.item_data.num;
			base.transform.FindChild("info/bodyNum/donum").GetComponent<Text>().text = this.cur_num.ToString();
			bool flag = ModelBase<A3_NPCShopModel>.getInstance().all_float.ContainsKey(this.item_data.tpid);
			if (flag)
			{
				int num = (int)(ModelBase<A3_NPCShopModel>.getInstance().all_float[this.item_data.tpid] * this.recycle_price);
				base.transform.FindChild("info/bodyNum/value").GetComponent<Text>().text = (num * this.cur_num).ToString();
			}
			else
			{
				base.transform.FindChild("info/bodyNum/value").GetComponent<Text>().text = (this.item_data.confdata.value * this.cur_num).ToString();
			}
			this.needEvent = false;
		}

		private void onmax(GameObject obj)
		{
			this.cur_num = this.item_data.num;
			this.num_bar.value = (float)this.cur_num / (float)this.item_data.num;
			base.transform.FindChild("info/bodyNum/donum").GetComponent<Text>().text = this.cur_num.ToString();
			bool flag = ModelBase<A3_NPCShopModel>.getInstance().all_float.ContainsKey(this.item_data.tpid);
			if (flag)
			{
				int num = (int)(ModelBase<A3_NPCShopModel>.getInstance().all_float[this.item_data.tpid] * this.recycle_price);
				base.transform.FindChild("info/bodyNum/value").GetComponent<Text>().text = (num * this.cur_num).ToString();
			}
			else
			{
				base.transform.FindChild("info/bodyNum/value").GetComponent<Text>().text = (this.item_data.confdata.value * this.cur_num).ToString();
			}
			this.needEvent = false;
		}

		public override void onShowed()
		{
			this.tiptype = equip_tip_type.Bag_tip;
			base.transform.SetAsLastSibling();
			bool flag = this.uiData == null;
			if (!flag)
			{
				bool flag2 = this.uiData.Count != 0;
				if (flag2)
				{
					this.item_data = (a3_BagItemData)this.uiData[0];
					this.curid = this.item_data.id;
					this.tiptype = (equip_tip_type)this.uiData[1];
				}
				base.transform.FindChild("info/use").gameObject.SetActive(false);
				base.transform.FindChild("info/sell").gameObject.SetActive(false);
				base.transform.FindChild("info/put").gameObject.SetActive(false);
				this.bodyNum.gameObject.SetActive(true);
				bool flag3 = this.tiptype == equip_tip_type.HouseOut_tip;
				if (flag3)
				{
					this.is_put_in = false;
					base.transform.FindChild("info/put/Text").GetComponent<Text>().text = "取出";
					base.transform.FindChild("info/put").gameObject.SetActive(true);
				}
				else
				{
					bool flag4 = this.tiptype == equip_tip_type.HouseIn_tip;
					if (flag4)
					{
						this.is_put_in = true;
						base.transform.FindChild("info/put/Text").GetComponent<Text>().text = "放入";
						base.transform.FindChild("info/put").gameObject.SetActive(true);
					}
					else
					{
						bool flag5 = this.tiptype == equip_tip_type.Bag_tip;
						if (flag5)
						{
							base.transform.FindChild("info/sell").gameObject.SetActive(true);
							base.transform.FindChild("info/use").gameObject.SetActive(true);
						}
						else
						{
							bool flag6 = this.tiptype == equip_tip_type.Chat_tip;
							if (flag6)
							{
								this.bodyNum.gameObject.SetActive(false);
							}
						}
					}
				}
				this.initItemInfo();
			}
		}

		public override void onClosed()
		{
			bool flag = !this.togo && a3_itemtip.closeWin != null;
			if (flag)
			{
				a3_itemtip.closeWin = null;
			}
		}

		private void initItemInfo()
		{
			Transform transform = base.transform.FindChild("info");
			transform.FindChild("name").GetComponent<Text>().text = this.item_data.confdata.item_name;
			transform.FindChild("name").GetComponent<Text>().color = Globle.getColorByQuality(this.item_data.confdata.quality);
			transform.FindChild("desc").GetComponent<Text>().text = StringUtils.formatText(this.item_data.confdata.desc);
			bool flag = this.item_data.confdata.use_limit > 0;
			if (flag)
			{
				transform.FindChild("lv").GetComponent<Text>().text = string.Concat(new object[]
				{
					this.item_data.confdata.use_limit,
					"转",
					this.item_data.confdata.use_lv,
					"级"
				});
			}
			else
			{
				transform.FindChild("lv").GetComponent<Text>().text = "无限制";
			}
			Transform transform2 = transform.FindChild("icon");
			bool flag2 = transform2.childCount > 0;
			if (flag2)
			{
				UnityEngine.Object.Destroy(transform2.GetChild(0).gameObject);
			}
			GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(this.item_data, false, -1, 1f, false);
			gameObject.transform.SetParent(transform2, false);
			this.num_bar.value = 0f;
			this.cur_num = 1;
			bool flag3 = this.item_data.confdata.use_type > 0;
			if (flag3)
			{
				bool flag4 = this.item_data.confdata.use_type != 21;
				if (flag4)
				{
					base.transform.FindChild("info/use").GetComponent<Button>().interactable = true;
				}
				else
				{
					bool flag5 = this.item_data.confdata.use_sum_require <= ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(this.item_data.confdata.tpid);
					if (flag5)
					{
						base.transform.FindChild("info/use").GetComponent<Button>().interactable = true;
					}
					else
					{
						base.transform.FindChild("info/use").GetComponent<Button>().interactable = false;
					}
				}
			}
			else
			{
				base.transform.FindChild("info/use").GetComponent<Button>().interactable = false;
			}
			this.onNumChange(0f);
		}

		private void onNumChange(float rate)
		{
			bool flag = !this.needEvent;
			if (flag)
			{
				this.needEvent = true;
			}
			else
			{
				this.cur_num = (int)Math.Floor((double)(rate * (float)this.item_data.num));
				bool flag2 = this.cur_num == 0;
				if (flag2)
				{
					this.cur_num = 1;
				}
				base.transform.FindChild("info/bodyNum/donum").GetComponent<Text>().text = this.cur_num.ToString();
				bool flag3 = ModelBase<A3_NPCShopModel>.getInstance().all_float.ContainsKey(this.item_data.tpid);
				if (flag3)
				{
					int num = (int)(ModelBase<A3_NPCShopModel>.getInstance().all_float[this.item_data.tpid] * this.recycle_price);
					base.transform.FindChild("info/bodyNum/value").GetComponent<Text>().text = (num * this.cur_num).ToString();
				}
				else
				{
					base.transform.FindChild("info/bodyNum/value").GetComponent<Text>().text = (this.item_data.confdata.value * this.cur_num).ToString();
				}
			}
		}

		private void onclose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_ITEMTIP);
		}

		private void onadd(GameObject go)
		{
			this.cur_num++;
			bool flag = this.cur_num >= this.item_data.num;
			if (flag)
			{
				this.cur_num = this.item_data.num;
			}
			this.num_bar.value = (float)this.cur_num / (float)this.item_data.num;
			base.transform.FindChild("info/bodyNum/donum").GetComponent<Text>().text = this.cur_num.ToString();
			bool flag2 = ModelBase<A3_NPCShopModel>.getInstance().all_float.ContainsKey(this.item_data.tpid);
			if (flag2)
			{
				int num = (int)(ModelBase<A3_NPCShopModel>.getInstance().all_float[this.item_data.tpid] * this.recycle_price);
				base.transform.FindChild("info/bodyNum/value").GetComponent<Text>().text = (num * this.cur_num).ToString();
			}
			else
			{
				base.transform.FindChild("info/bodyNum/value").GetComponent<Text>().text = (this.item_data.confdata.value * this.cur_num).ToString();
			}
			this.needEvent = false;
		}

		private void onreduce(GameObject go)
		{
			this.cur_num--;
			bool flag = this.cur_num < 1;
			if (flag)
			{
				this.cur_num = 1;
			}
			this.num_bar.value = (float)this.cur_num / (float)this.item_data.num;
			base.transform.FindChild("info/bodyNum/donum").GetComponent<Text>().text = this.cur_num.ToString();
			bool flag2 = ModelBase<A3_NPCShopModel>.getInstance().all_float.ContainsKey(this.item_data.tpid);
			if (flag2)
			{
				int num = (int)(ModelBase<A3_NPCShopModel>.getInstance().all_float[this.item_data.tpid] * this.recycle_price);
				base.transform.FindChild("info/bodyNum/value").GetComponent<Text>().text = (num * this.cur_num).ToString();
			}
			else
			{
				base.transform.FindChild("info/bodyNum/value").GetComponent<Text>().text = (this.item_data.confdata.value * this.cur_num).ToString();
			}
			this.needEvent = false;
		}

		private void onput(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_ITEMTIP);
			bool flag = this.is_put_in;
			if (flag)
			{
				BaseProxy<BagProxy>.getInstance().sendRoomItems(true, this.curid, this.cur_num);
			}
			else
			{
				BaseProxy<BagProxy>.getInstance().sendRoomItems(false, this.curid, this.cur_num);
			}
		}

		private void onsell(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_ITEMTIP);
			BaseProxy<BagProxy>.getInstance().sendSellItems(this.curid, this.cur_num);
		}

		private void ondo(GameObject go)
		{
			bool flag = this.item_data.confdata.use_type == 21;
			if (flag)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(5);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_SUMMON, arrayList, false);
				bool flag2 = a3_itemtip.closeWin != null;
				if (flag2)
				{
					InterfaceMgr.getInstance().close(a3_itemtip.closeWin);
					a3_itemtip.closeWin = null;
					this.togo = true;
				}
			}
			else
			{
				bool flag3 = this.item_data.confdata.use_type == 18;
				if (flag3)
				{
					bool flag4 = !FunctionOpenMgr.instance.Check(FunctionOpenMgr.SUMMON_MONSTER, false);
					if (flag4)
					{
						flytxt.instance.fly("完成1转51级主线后开启召唤兽", 0, default(Color), null);
						return;
					}
					ArrayList arrayList2 = new ArrayList();
					arrayList2.Add(4);
					InterfaceMgr.getInstance().open(InterfaceMgr.A3_SUMMON, arrayList2, false);
					bool flag5 = a3_itemtip.closeWin != null;
					if (flag5)
					{
						InterfaceMgr.getInstance().close(a3_itemtip.closeWin);
						a3_itemtip.closeWin = null;
						this.togo = true;
					}
				}
				else
				{
					BaseProxy<BagProxy>.getInstance().sendUseItems(this.curid, this.cur_num);
				}
			}
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_ITEMTIP);
		}
	}
}
