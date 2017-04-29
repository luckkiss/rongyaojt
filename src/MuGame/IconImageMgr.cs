using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class IconImageMgr
	{
		private static IconImageMgr _instance;

		private void init()
		{
		}

		public GameObject createA3ItemIcon(a3_BagItemData data, bool istouch = false, int num = -1, float scale = 1f, bool tip = false)
		{
			bool isUpEquip = false;
			bool isEquip = data.isEquip;
			if (isEquip)
			{
				bool flag = ModelBase<a3_EquipModel>.getInstance().getEquips().ContainsKey(data.id);
				if (!flag)
				{
					bool flag2 = ModelBase<a3_EquipModel>.getInstance().getEquipsByType().ContainsKey(data.confdata.equip_type) && ModelBase<a3_EquipModel>.getInstance().checkisSelfEquip(data.confdata);
					if (flag2)
					{
						a3_BagItemData a3_BagItemData = ModelBase<a3_EquipModel>.getInstance().getEquipsByType()[data.confdata.equip_type];
						bool flag3 = data.equipdata.combpt > a3_BagItemData.equipdata.combpt;
						if (flag3)
						{
							isUpEquip = true;
						}
					}
					else
					{
						isUpEquip = true;
					}
				}
			}
			return this.createA3ItemIcon(data.confdata, istouch, num, scale, tip, data.equipdata.stage, data.equipdata.blessing_lv, data.isNew, isUpEquip, data.ismark, data.equipdata.attribute, false, false);
		}

		public GameObject createA3ItemIcon(uint itemid, bool istouch = false, int num = -1, float scale = 1f, bool tip = false, int stage = -1, int blessing_lv = 0, bool isNew = false, bool isUpEquip = false, bool ignoreLimit = false, bool isicon = false)
		{
			a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(itemid);
			return this.createA3ItemIcon(itemDataById, istouch, num, scale, tip, stage, blessing_lv, isNew, isUpEquip, false, -1, ignoreLimit, isicon);
		}

		public GameObject createA3ItemIconTip(uint itemid, bool istouch = false, int num = -1, float scale = 1f, bool tip = false, int stage = -1, int blessing_lv = 0, bool isNew = false, bool isUpEquip = false, bool isMark = false)
		{
			a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(itemid);
			GameObject original = Resources.Load("prefab/iconimageTip") as GameObject;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
			Image component = gameObject.transform.FindChild("icon").GetComponent<Image>();
			component.sprite = (Resources.Load(itemDataById.file, typeof(Sprite)) as Sprite);
			Image component2 = gameObject.transform.FindChild("iconbor").GetComponent<Image>();
			component2.sprite = (Resources.Load(itemDataById.borderfile, typeof(Sprite)) as Sprite);
			Text component3 = gameObject.transform.FindChild("num").GetComponent<Text>();
			if (istouch)
			{
				gameObject.transform.GetComponent<Button>().enabled = true;
			}
			else
			{
				gameObject.transform.GetComponent<Button>().enabled = false;
			}
			bool flag = num != -1;
			if (flag)
			{
				component3.text = num.ToString();
				component3.gameObject.SetActive(true);
			}
			else
			{
				component3.gameObject.SetActive(false);
			}
			bool flag2 = itemDataById.item_type == 2;
			if (flag2)
			{
				bool flag3 = !ModelBase<a3_EquipModel>.getInstance().checkisSelfEquip(itemDataById);
				if (flag3)
				{
					gameObject.transform.FindChild("iconborder/equip_self").gameObject.SetActive(true);
				}
				else
				{
					bool flag4 = !ModelBase<a3_EquipModel>.getInstance().checkCanEquip(itemDataById, stage, blessing_lv);
					if (flag4)
					{
						gameObject.transform.FindChild("iconborder/equip_canequip").gameObject.SetActive(true);
					}
					if (isUpEquip)
					{
						gameObject.transform.FindChild("iconborder/is_upequip").gameObject.SetActive(true);
					}
				}
			}
			if (isNew)
			{
				gameObject.transform.FindChild("iconborder/is_new").gameObject.SetActive(true);
			}
			if (isMark)
			{
				gameObject.transform.FindChild("iconborder/ismark").gameObject.SetActive(true);
			}
			else
			{
				gameObject.transform.FindChild("iconborder/ismark").gameObject.SetActive(false);
			}
			gameObject.name = "icon";
			gameObject.transform.localScale = new Vector3(scale, scale, 1f);
			return gameObject;
		}

		public GameObject createA3ItemIcon(a3_ItemData item, bool istouch = false, int num = -1, float scale = 1f, bool tip = false, int stage = -1, int blessing_lv = 0, bool isNew = false, bool isUpEquip = false, bool isMark = false, int shuxing = -1, bool ignoreAllEquipTip = false, bool isicon = false)
		{
			GameObject original = Resources.Load("prefab/iconimage") as GameObject;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
			Image component = gameObject.transform.FindChild("icon").GetComponent<Image>();
			component.sprite = (Resources.Load(item.file, typeof(Sprite)) as Sprite);
			Image component2 = gameObject.transform.FindChild("iconbor").GetComponent<Image>();
			component2.sprite = (Resources.Load(item.borderfile, typeof(Sprite)) as Sprite);
			Text component3 = gameObject.transform.FindChild("num").GetComponent<Text>();
			gameObject.transform.FindChild("shuxing").gameObject.SetActive(false);
			if (isicon)
			{
				Image component4 = gameObject.transform.FindChild("bicon").GetComponent<Image>();
				component4.gameObject.SetActive(true);
			}
			if (istouch)
			{
				gameObject.transform.GetComponent<Button>().enabled = true;
			}
			else
			{
				gameObject.transform.GetComponent<Button>().enabled = false;
			}
			bool flag = num != -1;
			if (flag)
			{
				component3.text = num.ToString();
				component3.gameObject.SetActive(true);
			}
			else
			{
				component3.gameObject.SetActive(false);
			}
			bool flag2 = item.item_type == 2;
			if (flag2)
			{
				bool flag3 = !ignoreAllEquipTip;
				if (flag3)
				{
					bool flag4 = !ModelBase<a3_EquipModel>.getInstance().checkisSelfEquip(item);
					if (flag4)
					{
						gameObject.transform.FindChild("iconborder/equip_self").gameObject.SetActive(true);
					}
					else
					{
						bool flag5 = !ModelBase<a3_EquipModel>.getInstance().checkCanEquip(item, stage, blessing_lv);
						if (flag5)
						{
							gameObject.transform.FindChild("iconborder/equip_canequip").gameObject.SetActive(true);
						}
						if (isUpEquip)
						{
							gameObject.transform.FindChild("iconborder/is_upequip").gameObject.SetActive(true);
						}
					}
					bool flag6 = shuxing > 0;
					if (flag6)
					{
						string path = "icon/shuxing/" + shuxing;
						gameObject.transform.FindChild("shuxing").gameObject.SetActive(true);
						gameObject.transform.FindChild("shuxing").GetComponent<Image>().sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
					}
					else
					{
						gameObject.transform.FindChild("shuxing").gameObject.SetActive(false);
					}
				}
			}
			if (isNew)
			{
				gameObject.transform.FindChild("iconborder/is_new").gameObject.SetActive(true);
			}
			if (isMark)
			{
				gameObject.transform.FindChild("iconborder/ismark").gameObject.SetActive(true);
			}
			else
			{
				gameObject.transform.FindChild("iconborder/ismark").gameObject.SetActive(false);
			}
			gameObject.name = "icon";
			gameObject.transform.localScale = new Vector3(scale, scale, 1f);
			return gameObject;
		}

		public GameObject createA3EquipIcon(a3_BagItemData data, float scale = 1f, bool istouch = false)
		{
			return this.createA3EquipIcon(data.confdata, data.equipdata.stage, data.equipdata.blessing_lv, scale, istouch);
		}

		public void refreshA3EquipIcon_byType(GameObject root, a3_BagItemData data, EQUIP_SHOW_TYPE show_type = EQUIP_SHOW_TYPE.SHOW_COMMON)
		{
			Text component = root.transform.FindChild("inlvl").GetComponent<Text>();
			component.color = new Color(1f, 0.7f, 0f);
			Text component2 = root.transform.FindChild("num").GetComponent<Text>();
			component2.gameObject.SetActive(false);
			Transform transform = root.transform.FindChild("stars");
			switch (data.equipdata.stage)
			{
			}
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).gameObject.SetActive(false);
			}
			for (int j = 0; j < data.equipdata.stage; j++)
			{
				transform.GetChild(j).gameObject.SetActive(true);
			}
			switch (show_type)
			{
			case EQUIP_SHOW_TYPE.SHOW_INTENSIFY:
				transform.gameObject.SetActive(false);
				component.gameObject.SetActive(true);
				component.text = "+" + data.equipdata.intensify_lv;
				break;
			case EQUIP_SHOW_TYPE.SHOW_ADDLV:
				transform.gameObject.SetActive(false);
				component.gameObject.SetActive(true);
				component.text = "追" + data.equipdata.add_level;
				break;
			case EQUIP_SHOW_TYPE.SHOW_STAGE:
				component.gameObject.SetActive(false);
				transform.gameObject.SetActive(true);
				break;
			case EQUIP_SHOW_TYPE.SHOW_INTENSIFYANDSTAGE:
				transform.gameObject.SetActive(true);
				component.gameObject.SetActive(true);
				component.text = "+" + data.equipdata.intensify_lv;
				break;
			default:
				transform.gameObject.SetActive(false);
				component.gameObject.SetActive(false);
				break;
			}
		}

		public GameObject createA3EquipIcon(a3_ItemData data, int stage = -1, int blessing_lv = 0, float scale = 1f, bool istouch = false)
		{
			string path = "icon/equip/" + data.tpid;
			string path2 = "icon/itemborder/b039_0" + data.quality;
			GameObject original = Resources.Load("prefab/iconimage") as GameObject;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
			Image component = gameObject.transform.FindChild("icon").GetComponent<Image>();
			component.sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
			Image component2 = gameObject.transform.FindChild("iconbor").GetComponent<Image>();
			Transform transform = gameObject.transform.FindChild("wk");
			component2.sprite = (Resources.Load(path2, typeof(Sprite)) as Sprite);
			Text component3 = gameObject.transform.FindChild("num").GetComponent<Text>();
			component3.gameObject.SetActive(false);
			gameObject.transform.GetComponent<Button>().enabled = false;
			gameObject.transform.localScale = new Vector3(scale, scale, 1f);
			gameObject.name = "icon";
			if (istouch)
			{
				gameObject.transform.GetComponent<Button>().enabled = true;
			}
			else
			{
				gameObject.transform.GetComponent<Button>().enabled = false;
			}
			bool flag = !ModelBase<a3_EquipModel>.getInstance().checkisSelfEquip(data);
			if (flag)
			{
				gameObject.transform.FindChild("iconborder/equip_self").gameObject.SetActive(true);
			}
			else
			{
				bool flag2 = !ModelBase<a3_EquipModel>.getInstance().checkCanEquip(data, stage, blessing_lv);
				if (flag2)
				{
					gameObject.transform.FindChild("iconborder/equip_canequip").gameObject.SetActive(true);
				}
			}
			component.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(80f, 160f);
			component2.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(80f, 160f);
			transform.GetComponent<RectTransform>().sizeDelta = new Vector2(80f, 160f);
			gameObject.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(92f, 166f);
			return gameObject;
		}

		public GameObject createItemIcon4Lottery(a3_ItemData item, bool istouch = false, int num = -1, bool isicon = false, float scale = 1f, bool tip = false)
		{
			GameObject original = Resources.Load("prefab/iconimage") as GameObject;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
			Image component = gameObject.GetComponent<Image>();
			component.enabled = false;
			Image component2 = gameObject.transform.FindChild("icon").GetComponent<Image>();
			component2.sprite = (Resources.Load(item.file, typeof(Sprite)) as Sprite);
			Image component3 = gameObject.transform.FindChild("qicon").GetComponent<Image>();
			component3.sprite = (Resources.Load(item.borderfile, typeof(Sprite)) as Sprite);
			component3.gameObject.SetActive(true);
			if (isicon)
			{
				Image component4 = gameObject.transform.FindChild("bicon").GetComponent<Image>();
				component4.gameObject.SetActive(true);
			}
			Transform transform = gameObject.transform.FindChild("iconbor");
			transform.gameObject.SetActive(false);
			Text component5 = gameObject.transform.FindChild("num").GetComponent<Text>();
			component5.enabled = false;
			if (istouch)
			{
				gameObject.transform.GetComponent<Button>().enabled = true;
			}
			else
			{
				gameObject.transform.GetComponent<Button>().enabled = false;
			}
			bool flag = num != -1;
			if (flag)
			{
				component5.text = num.ToString();
				component5.gameObject.SetActive(true);
			}
			else
			{
				component5.gameObject.SetActive(false);
			}
			gameObject.name = "icon";
			gameObject.transform.localScale = new Vector3(scale, scale, 1f);
			return gameObject;
		}

		public GameObject createLotteryInfo(itemLotteryAwardInfoData data, bool isTouch = false, int num = -1, float scale = 1f)
		{
			GameObject original = Resources.Load("prefab/lotteryItemAwardInfo") as GameObject;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
			Text component = gameObject.transform.FindChild("txt_info").GetComponent<Text>();
			a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(data.tpid);
			component.text = string.Format("<color=#ff0000>{0}</color> <color=#ffffff>获得了</color> {1}", data.name, a3_lottery.mInstance.GetLotteryItemNameColor(itemDataById.item_name, itemDataById.quality));
			gameObject.transform.localScale = new Vector3(scale, scale, 1f);
			return gameObject;
		}

		public GameObject creatEeverydayWelfareItemIcon(a3_ItemData item, bool istouch = false, int dayCount = -1, int num = -1, float scale = 1f, bool tip = false, int stage = -1, int blessing_lv = 0, bool isNew = false, bool isUpEquip = false)
		{
			GameObject original = Resources.Load("prefab/everydayWelfare") as GameObject;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
			Image component = gameObject.transform.FindChild("icon").GetComponent<Image>();
			component.sprite = (Resources.Load(item.file, typeof(Sprite)) as Sprite);
			Image component2 = gameObject.transform.FindChild("iconborder").GetComponent<Image>();
			component2.sprite = (Resources.Load(item.borderfile, typeof(Sprite)) as Sprite);
			Text component3 = gameObject.transform.FindChild("num").GetComponent<Text>();
			if (istouch)
			{
				gameObject.transform.GetComponent<Button>().enabled = true;
			}
			else
			{
				gameObject.transform.GetComponent<Button>().enabled = false;
			}
			bool flag = num != -1;
			if (flag)
			{
				component3.text = num.ToString();
				component3.gameObject.SetActive(true);
			}
			else
			{
				component3.gameObject.SetActive(false);
			}
			bool flag2 = item.item_type == 2;
			if (flag2)
			{
				bool flag3 = !ModelBase<a3_EquipModel>.getInstance().checkisSelfEquip(item);
				if (flag3)
				{
					gameObject.transform.FindChild("iconborder/equip_self").gameObject.SetActive(true);
				}
				else
				{
					bool flag4 = !ModelBase<a3_EquipModel>.getInstance().checkCanEquip(item, stage, blessing_lv);
					if (flag4)
					{
						gameObject.transform.FindChild("iconborder/equip_canequip").gameObject.SetActive(true);
					}
					if (isUpEquip)
					{
						gameObject.transform.FindChild("iconborder/is_upequip").gameObject.SetActive(true);
					}
				}
			}
			if (isNew)
			{
				gameObject.transform.FindChild("iconborder/is_new").gameObject.SetActive(true);
			}
			gameObject.name = "icon";
			gameObject.transform.localScale = new Vector3(scale, scale, 1f);
			return gameObject;
		}

		public GameObject creatItemAwardCenterIcon(a3_ItemData item)
		{
			GameObject original = Resources.Load("prefab/itemAwardCenter") as GameObject;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
			Image component = gameObject.transform.FindChild("icon").GetComponent<Image>();
			component.sprite = (Resources.Load(item.file, typeof(Sprite)) as Sprite);
			Text component2 = gameObject.transform.FindChild("txtInfor").GetComponent<Text>();
			gameObject.name = "icon";
			return gameObject;
		}

		public GameObject createEquipIcon(EquipConf data, float scale = 1f, bool istouch = false)
		{
			string path = "icon/equip/" + data.tpid;
			string path2 = "icon/itemborder/b039_0" + data.quality;
			GameObject original = Resources.Load("prefab/iconimage") as GameObject;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
			Image component = gameObject.transform.FindChild("icon").GetComponent<Image>();
			component.sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
			Image component2 = gameObject.transform.FindChild("iconbor").GetComponent<Image>();
			component2.sprite = (Resources.Load(path2, typeof(Sprite)) as Sprite);
			Text component3 = gameObject.transform.FindChild("num").GetComponent<Text>();
			component3.gameObject.SetActive(false);
			gameObject.transform.GetComponent<Button>().enabled = false;
			gameObject.transform.localScale = new Vector3(scale, scale, 1f);
			gameObject.name = "icon";
			if (istouch)
			{
				gameObject.transform.GetComponent<Button>().enabled = true;
			}
			else
			{
				gameObject.transform.GetComponent<Button>().enabled = false;
			}
			return gameObject;
		}

		public GameObject createMoneyIcon(string type, float scale = 1f, int num = -1)
		{
			string path = "icon/comm/" + type;
			string path2 = "icon/itemborder/b039_02";
			GameObject original = Resources.Load("prefab/iconimage") as GameObject;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
			Image component = gameObject.transform.FindChild("icon").GetComponent<Image>();
			component.sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
			Image component2 = gameObject.transform.FindChild("iconbor").GetComponent<Image>();
			component2.sprite = (Resources.Load(path2, typeof(Sprite)) as Sprite);
			Text component3 = gameObject.transform.FindChild("num").GetComponent<Text>();
			bool flag = num != -1;
			if (flag)
			{
				component3.text = num.ToString();
				component3.gameObject.SetActive(true);
			}
			else
			{
				component3.gameObject.SetActive(false);
			}
			gameObject.transform.GetComponent<Button>().enabled = false;
			gameObject.transform.localScale = new Vector3(scale, scale, 1f);
			return gameObject;
		}

		public static IconImageMgr getInstance()
		{
			bool flag = IconImageMgr._instance == null;
			if (flag)
			{
				IconImageMgr._instance = new IconImageMgr();
				IconImageMgr._instance.init();
			}
			return IconImageMgr._instance;
		}
	}
}
