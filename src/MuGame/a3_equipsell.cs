using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_equipsell : Window
	{
		private GameObject quick_image;

		private bool isquicklyChoose = false;

		private GameObject contain_left;

		private GameObject contain;

		private GameObject grid;

		private GameObject prompt;

		private Toggle white;

		private Toggle green;

		private Toggle blue;

		private Toggle purple;

		private Dictionary<uint, a3_BagItemData> dic_right = new Dictionary<uint, a3_BagItemData>();

		private Dictionary<uint, GameObject> dic_rightObj = new Dictionary<uint, GameObject>();

		private Dictionary<uint, a3_BagItemData> dic_left = new Dictionary<uint, a3_BagItemData>();

		private Dictionary<uint, GameObject> dic_leftObj = new Dictionary<uint, GameObject>();

		private Dictionary<uint, a3_BagItemData> dic_change = new Dictionary<uint, a3_BagItemData>();

		private Text mojing;

		private Text shengguanghuiji;

		private Text mifageli;

		private int mojing_num;

		private int shengguanghuiji_num;

		private int mifageli_num;

		private ScrollControler scrollControer;

		private ScrollControler scrollcontroers;

		public static a3_equipsell _instance;

		private int right_count = 0;

		private int left_count = 0;

		private Dictionary<int, int> Itemnum = new Dictionary<int, int>();

		private List<uint> dic_leftAllid = new List<uint>();

		private bool ishanve = false;

		public override void init()
		{
			a3_equipsell._instance = this;
			this.scrollControer = new ScrollControler();
			ScrollRect component = base.transform.FindChild("panel_right/bg/Scroll_rect").GetComponent<ScrollRect>();
			this.scrollControer.create(component, 4);
			this.scrollcontroers = new ScrollControler();
			ScrollRect component2 = base.transform.FindChild("panel_left/bg/Scrollrect").GetComponent<ScrollRect>();
			this.scrollcontroers.create(component2, 4);
			this.contain = base.transform.FindChild("panel_right/bg/Scroll_rect/contain").gameObject;
			this.grid = base.transform.FindChild("panel_right/bg/Scroll_rect/grid").gameObject;
			this.contain_left = base.transform.FindChild("panel_left/bg/Scrollrect/contain").gameObject;
			this.white = base.getComponentByPath<Toggle>("panel_left/panel/Toggle_white");
			this.mojing = base.getComponentByPath<Text>("panel_left/panel/mojing/num");
			this.shengguanghuiji = base.getComponentByPath<Text>("panel_left/panel/shenguang/num");
			this.mifageli = base.getComponentByPath<Text>("panel_left/panel/mifa/num");
			this.prompt = base.getGameObjectByPath("prompt");
			this.quick_image = base.getGameObjectByPath("panel_right/bg/Image");
			this.white.onValueChanged.AddListener(delegate(bool ison)
			{
				if (ison)
				{
					this.EquipsSureSell(0u, 1);
				}
				else
				{
					this.EquipsNoSell(0u, 1);
				}
			});
			this.green = base.getComponentByPath<Toggle>("panel_left/panel/Toggle_green");
			this.green.onValueChanged.AddListener(delegate(bool ison)
			{
				if (ison)
				{
					this.EquipsSureSell(0u, 2);
				}
				else
				{
					this.EquipsNoSell(0u, 2);
				}
			});
			this.blue = base.getComponentByPath<Toggle>("panel_left/panel/Toggle_blue");
			this.blue.onValueChanged.AddListener(delegate(bool ison)
			{
				if (ison)
				{
					this.EquipsSureSell(0u, 3);
				}
				else
				{
					this.EquipsNoSell(0u, 3);
				}
			});
			this.purple = base.getComponentByPath<Toggle>("panel_left/panel/Toggle_puple");
			this.purple.onValueChanged.AddListener(delegate(bool ison)
			{
				if (ison)
				{
					this.EquipsSureSell(0u, 4);
				}
				else
				{
					this.EquipsNoSell(0u, 4);
				}
			});
			BaseButton baseButton = new BaseButton(base.transform.FindChild("btn_close"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onclose);
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("panel_right/Button"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onQuicklyChoose);
			BaseButton baseButton3 = new BaseButton(base.transform.FindChild("panel_left/Button"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.onDecompose);
			BaseButton baseButton4 = new BaseButton(this.prompt.transform.FindChild("Button"), 1, 1);
			baseButton4.onClick = new Action<GameObject>(this.Sendproxy);
			BaseButton baseButton5 = new BaseButton(this.prompt.transform.FindChild("btn_close"), 1, 1);
			baseButton5.onClick = delegate(GameObject go)
			{
				this.prompt.SetActive(false);
			};
		}

		public override void onShowed()
		{
			this.showInfos();
			this.CreateGrids();
			this.CreateEquipIcon();
		}

		public override void onClosed()
		{
			this.DestroyGrids();
			this.removeAllinfos();
		}

		private void CreateGrids()
		{
			int num = ModelBase<a3_BagModel>.getInstance().getUnEquips().Keys.Count;
			bool flag = num <= 30;
			if (flag)
			{
				this.AddGrids(30);
			}
			else
			{
				int num2 = 5 - num % 5;
				num += num2;
				this.AddGrids(num);
			}
		}

		private void AddGrids(int num)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.grid);
				gameObject.SetActive(true);
				gameObject.transform.SetParent(this.contain.transform, false);
			}
			float x = this.contain.GetComponent<RectTransform>().sizeDelta.x;
			float y = (float)((double)((float)(num / 5) * this.grid.GetComponent<RectTransform>().sizeDelta.y) + 2.35 * (double)(num / 5 - 1));
			this.contain.GetComponent<RectTransform>().sizeDelta = new Vector2(x, y);
		}

		private void DestroyGrids()
		{
			for (int i = 0; i < this.contain.transform.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.contain.transform.GetChild(i).gameObject);
			}
			for (int j = 0; j < this.contain_left.transform.childCount; j++)
			{
				bool flag = this.contain_left.transform.GetChild(j).childCount > 0;
				if (flag)
				{
					UnityEngine.Object.Destroy(this.contain_left.transform.GetChild(j).GetChild(0).gameObject);
				}
			}
		}

		private void CreateEquipIcon()
		{
			foreach (uint current in ModelBase<a3_BagModel>.getInstance().getUnEquips().Keys)
			{
				GameObject icon = IconImageMgr.getInstance().createA3ItemIcon(ModelBase<a3_BagModel>.getInstance().getUnEquips()[current], true, -1, 1f, false);
				icon.transform.SetParent(this.contain.transform.GetChild(this.right_count).transform, false);
				icon.name = ModelBase<a3_BagModel>.getInstance().getUnEquips()[current].id.ToString();
				this.right_count++;
				BaseButton baseButton = new BaseButton(icon.transform, 1, 1);
				baseButton.onClick = delegate(GameObject go)
				{
					this.onEquipClickShowTips(icon, uint.Parse(icon.name), false);
				};
				this.dic_rightObj[ModelBase<a3_BagModel>.getInstance().getUnEquips()[current].id] = icon;
				this.dic_right[ModelBase<a3_BagModel>.getInstance().getUnEquips()[current].id] = ModelBase<a3_BagModel>.getInstance().getUnEquips()[current];
			}
		}

		private void onEquipClickShowTips(GameObject go, uint id, bool isselltrue)
		{
			bool flag = this.isquicklyChoose;
			if (flag)
			{
				bool flag2 = !isselltrue;
				if (flag2)
				{
					this.EquipsSureSell(id, 0);
				}
				else
				{
					this.EquipsNoSell(id, 0);
				}
			}
			else if (isselltrue)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(ModelBase<a3_BagModel>.getInstance().getUnEquips()[id]);
				arrayList.Add(equip_tip_type.SellOut_tip);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIPTIP, arrayList, false);
			}
			else
			{
				ArrayList arrayList2 = new ArrayList();
				arrayList2.Add(ModelBase<a3_BagModel>.getInstance().getUnEquips()[id]);
				arrayList2.Add(equip_tip_type.SellIn_tip);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIPTIP, arrayList2, false);
			}
		}

		public void EquipsSureSell(uint id = 0u, int quality = 0)
		{
			bool flag = quality == 0;
			if (flag)
			{
				bool flag2 = this.dic_rightObj.ContainsKey(id);
				if (flag2)
				{
					bool flag3 = this.dic_left.Keys.Count < 20;
					if (flag3)
					{
						this.dic_left[id] = ModelBase<a3_BagModel>.getInstance().getUnEquips()[id];
						this.dic_change[id] = ModelBase<a3_BagModel>.getInstance().getUnEquips()[id];
						this.showItemNum(ModelBase<a3_BagModel>.getInstance().getUnEquips()[id].tpid, true);
						this.DestroyObj(id, true, this.dic_right, this.dic_rightObj, this.contain);
						this.ShowObj(this.dic_left, this.dic_leftObj, this.contain_left);
					}
					else
					{
						flytxt.instance.fly("格子已满！", 1, default(Color), null);
					}
				}
			}
			else
			{
				bool flag4 = this.dic_rightObj.Keys.Count > 0;
				if (flag4)
				{
					foreach (uint current in this.dic_rightObj.Keys)
					{
						uint tpid = ModelBase<a3_BagModel>.getInstance().getUnEquips()[current].tpid;
						bool flag5 = ModelBase<a3_BagModel>.getInstance().getItemDataById(tpid).quality == quality;
						if (flag5)
						{
							bool flag6 = this.dic_left.Keys.Count < 20;
							if (!flag6)
							{
								flytxt.instance.fly("格子已满！", 1, default(Color), null);
								break;
							}
							bool flag7 = this.dic_leftObj.ContainsKey(current);
							if (flag7)
							{
								UnityEngine.Object.Destroy(this.dic_leftObj[current]);
								this.dic_leftObj.Remove(current);
							}
							this.dic_left[current] = ModelBase<a3_BagModel>.getInstance().getUnEquips()[current];
							this.dic_change[current] = ModelBase<a3_BagModel>.getInstance().getUnEquips()[current];
							this.showItemNum(ModelBase<a3_BagModel>.getInstance().getUnEquips()[current].tpid, true);
							this.DestroyObj(current, false, this.dic_right, this.dic_rightObj, this.contain);
							this.ShowObj(this.dic_left, this.dic_leftObj, this.contain_left);
						}
					}
					this.Refresh(this.dic_left, this.dic_right, this.dic_rightObj);
				}
			}
		}

		public void EquipsNoSell(uint id = 0u, int quality = 0)
		{
			bool flag = quality == 0;
			if (flag)
			{
				bool flag2 = this.dic_leftObj.ContainsKey(id);
				if (flag2)
				{
					this.dic_right[id] = ModelBase<a3_BagModel>.getInstance().getUnEquips()[id];
					this.dic_change[id] = ModelBase<a3_BagModel>.getInstance().getUnEquips()[id];
					this.DestroyObj(id, true, this.dic_left, this.dic_leftObj, this.contain_left);
					this.ShowObj(this.dic_right, this.dic_rightObj, this.contain);
				}
			}
			else
			{
				bool flag3 = this.dic_leftObj.Keys.Count > 0;
				if (flag3)
				{
					foreach (uint current in this.dic_leftObj.Keys)
					{
						uint tpid = ModelBase<a3_BagModel>.getInstance().getUnEquips()[current].tpid;
						bool flag4 = ModelBase<a3_BagModel>.getInstance().getItemDataById(tpid).quality == quality;
						if (flag4)
						{
							bool flag5 = this.dic_rightObj.ContainsKey(current);
							if (flag5)
							{
								UnityEngine.Object.Destroy(this.dic_rightObj[current]);
								this.dic_rightObj.Remove(current);
							}
							this.dic_right[current] = ModelBase<a3_BagModel>.getInstance().getUnEquips()[current];
							this.dic_change[current] = ModelBase<a3_BagModel>.getInstance().getUnEquips()[current];
							this.DestroyObj(current, false, this.dic_left, this.dic_leftObj, this.contain_left);
							this.ShowObj(this.dic_right, this.dic_rightObj, this.contain);
						}
					}
					this.Refresh(this.dic_right, this.dic_left, this.dic_leftObj);
				}
			}
		}

		private void DestroyObj(uint id, bool isOneRemove, Dictionary<uint, a3_BagItemData> dic = null, Dictionary<uint, GameObject> dic_obj = null, GameObject contain = null)
		{
			GameObject gameObject = dic_obj[id].transform.parent.gameObject;
			UnityEngine.Object.Destroy(gameObject);
			if (isOneRemove)
			{
				dic_obj.Remove(id);
				dic.Remove(id);
			}
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.grid);
			gameObject2.SetActive(true);
			gameObject2.transform.SetParent(contain.transform, false);
			gameObject2.transform.SetSiblingIndex(dic_obj.Count + 1);
			bool flag = dic == this.dic_left;
			if (flag)
			{
				this.showItemNum(ModelBase<a3_BagModel>.getInstance().getUnEquips()[id].tpid, false);
				this.left_count--;
			}
			else
			{
				this.right_count--;
			}
		}

		private void ShowObj(Dictionary<uint, a3_BagItemData> dic, Dictionary<uint, GameObject> dic_obj, GameObject contain)
		{
			foreach (uint current in this.dic_change.Keys)
			{
				GameObject icon = IconImageMgr.getInstance().createA3ItemIcon(dic[current], true, -1, 1f, false);
				bool flag = dic == this.dic_left;
				if (flag)
				{
					icon.transform.SetParent(contain.transform.GetChild(this.left_count).transform, false);
					this.left_count++;
				}
				else
				{
					icon.transform.SetParent(contain.transform.GetChild(this.right_count).transform, false);
					this.right_count++;
				}
				icon.name = dic[current].id.ToString();
				BaseButton baseButton = new BaseButton(icon.transform, 1, 1);
				baseButton.onClick = delegate(GameObject go)
				{
					bool flag2 = dic == this.dic_left;
					if (flag2)
					{
						this.onEquipClickShowTips(icon, uint.Parse(icon.name), true);
					}
					else
					{
						this.onEquipClickShowTips(icon, uint.Parse(icon.name), false);
					}
				};
				dic_obj[current] = icon;
			}
			this.dic_change.Clear();
		}

		private void Refresh(Dictionary<uint, a3_BagItemData> dic, Dictionary<uint, a3_BagItemData> refresh_dic, Dictionary<uint, GameObject> refresh_dicobj)
		{
			foreach (uint current in dic.Keys)
			{
				bool flag = refresh_dicobj.ContainsKey(current);
				if (flag)
				{
					refresh_dicobj.Remove(current);
				}
			}
			foreach (uint current2 in dic.Keys)
			{
				bool flag2 = refresh_dic.ContainsKey(current2);
				if (flag2)
				{
					refresh_dic.Remove(current2);
				}
			}
		}

		private void showItemNum(uint tpid, bool add)
		{
			MonoBehaviour.print("左边字典的个数：" + this.dic_left.Keys.Count);
			SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + tpid);
			List<SXML> nodeList = sXML.GetNodeList("decompose", "");
			foreach (SXML current in nodeList)
			{
				sellItems sellItems = new sellItems();
				sellItems.item_id = current.getInt("item");
				sellItems.item_num = current.getInt("num");
				this.Itemnum[sellItems.item_id] = sellItems.item_num;
			}
			foreach (int current2 in this.Itemnum.Keys)
			{
				switch (current2)
				{
				case 1540:
					if (add)
					{
						this.mojing_num += this.Itemnum[current2];
					}
					else
					{
						this.mojing_num -= this.Itemnum[current2];
					}
					this.mojing.text = this.mojing_num.ToString();
					break;
				case 1541:
					if (add)
					{
						this.shengguanghuiji_num += this.Itemnum[current2];
					}
					else
					{
						this.shengguanghuiji_num -= this.Itemnum[current2];
					}
					this.shengguanghuiji.text = this.shengguanghuiji_num.ToString();
					break;
				case 1542:
					if (add)
					{
						this.mifageli_num += this.Itemnum[current2];
					}
					else
					{
						this.mifageli_num -= this.Itemnum[current2];
					}
					this.mifageli.text = this.mifageli_num.ToString();
					break;
				}
			}
		}

		public void refresh()
		{
			for (int i = 0; i < this.dic_leftAllid.Count; i++)
			{
				GameObject gameObject = this.dic_leftObj[this.dic_leftAllid[i]].transform.parent.gameObject;
				UnityEngine.Object.Destroy(gameObject);
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.grid);
				gameObject2.SetActive(true);
				gameObject2.transform.SetParent(this.contain_left.transform, false);
				gameObject2.transform.SetSiblingIndex(this.dic_leftObj.Count + 1);
			}
			this.dic_left.Clear();
			this.dic_leftObj.Clear();
			this.dic_leftAllid.Clear();
			this.left_count = 0;
			this.mojing_num = 0;
			this.shengguanghuiji_num = 0;
			this.mifageli_num = 0;
			this.mojing.text = string.Concat(0);
			this.shengguanghuiji.text = string.Concat(0);
			this.mifageli.text = string.Concat(0);
			this.purple.isOn = false;
			this.blue.isOn = false;
			this.green.isOn = false;
			this.white.isOn = false;
		}

		private void onDecompose(GameObject go)
		{
			this.dic_leftAllid.Clear();
			bool flag = this.dic_left.Keys.Count > 0;
			if (flag)
			{
				foreach (uint current in this.dic_left.Keys)
				{
					this.dic_leftAllid.Add(this.dic_left[current].id);
				}
				bool flag2 = this.haveYullowEquip();
				if (flag2)
				{
					this.prompt.SetActive(true);
				}
				else
				{
					this.Sendproxy(go);
				}
			}
			else
			{
				flytxt.instance.fly("请添加装备！", 1, default(Color), null);
			}
		}

		private void Sendproxy(GameObject go)
		{
			BaseProxy<EquipProxy>.getInstance().sendsell(this.dic_leftAllid);
			bool activeSelf = this.prompt.activeSelf;
			if (activeSelf)
			{
				this.prompt.SetActive(false);
			}
		}

		private bool haveYullowEquip()
		{
			for (int i = 0; i < this.dic_leftAllid.Count; i++)
			{
				uint tpid = ModelBase<a3_BagModel>.getInstance().getUnEquips()[this.dic_leftAllid[i]].tpid;
				bool flag = ModelBase<a3_BagModel>.getInstance().getItemDataById(tpid).quality == 5;
				if (flag)
				{
					this.ishanve = true;
					break;
				}
				this.ishanve = false;
			}
			return this.ishanve;
		}

		private void onclose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQUIPSELL);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_BAG, null, false);
		}

		private void onQuicklyChoose(GameObject go)
		{
			bool flag = !this.isquicklyChoose;
			if (flag)
			{
				this.quick_image.SetActive(true);
				base.getComponentByPath<Text>("panel_right/Button/Text").text = "单个选择装备";
				this.isquicklyChoose = true;
			}
			else
			{
				this.quick_image.SetActive(false);
				base.getComponentByPath<Text>("panel_right/Button/Text").text = "快速选择装备";
				this.isquicklyChoose = false;
			}
		}

		private void showInfos()
		{
			this.contain.transform.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 1f;
			this.purple.isOn = false;
			this.blue.isOn = false;
			this.green.isOn = false;
			this.white.isOn = false;
			this.isquicklyChoose = false;
			base.getComponentByPath<Text>("panel_right/Button/Text").text = "快速选择装备";
		}

		private void removeAllinfos()
		{
			this.dic_left.Clear();
			this.dic_right.Clear();
			this.dic_leftObj.Clear();
			this.dic_rightObj.Clear();
			this.dic_change.Clear();
			this.Itemnum.Clear();
			this.left_count = 0;
			this.right_count = 0;
			this.mojing_num = 0;
			this.shengguanghuiji_num = 0;
			this.mifageli_num = 0;
			this.mojing.text = string.Concat(0);
			this.shengguanghuiji.text = string.Concat(0);
			this.mifageli.text = string.Concat(0);
		}
	}
}
