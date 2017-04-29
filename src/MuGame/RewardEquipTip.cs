using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class RewardEquipTip
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly RewardEquipTip.<>c <>9 = new RewardEquipTip.<>c();

			public static Action<GameObject> <>9__13_0;

			internal void ctor>b__13_0(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_MINITIP);
			}
		}

		public GameObject owner;

		public GameObject itemIcon;

		private Text textItemName;

		private Text textTipDesc;

		private Text textCarrLimit;

		private Text textBaseHead;

		private Text textBaseAttr;

		private Text textAddAttr;

		private Text textExtraDesc1;

		private Text textExtraDesc2;

		private Text textExtraDesc3;

		private List<Text> listTextExtraDesc;

		private List<GameObject> listGoBgImg;

		public RewardEquipTip(GameObject owner)
		{
			this.owner = owner;
			this.listGoBgImg = new List<GameObject>();
			this.listTextExtraDesc = new List<Text>();
			Transform transform = owner.transform.FindChild("bgImg");
			int i = 0;
			int num = (transform != null) ? transform.childCount : 0;
			while (i < num)
			{
				this.listGoBgImg.Add(transform.GetChild(i).gameObject);
				i++;
			}
			this.itemIcon = owner.transform.FindChild("text_bg/iconbg/icon").gameObject;
			this.textItemName = owner.transform.FindChild("text_bg/nameBg/itemName").GetComponent<Text>();
			this.textTipDesc = owner.transform.FindChild("text_bg/nameBg/tipDesc/dispText").GetComponent<Text>();
			this.textCarrLimit = owner.transform.FindChild("text_bg/nameBg/carrReq/dispText").GetComponent<Text>();
			this.textBaseHead = owner.transform.FindChild("text_bg/textBase/baseDisc").GetComponent<Text>();
			this.textBaseAttr = owner.transform.FindChild("text_bg/textBase/dispText").GetComponent<Text>();
			this.textAddAttr = owner.transform.FindChild("text_bg/textAddBase/dispText").GetComponent<Text>();
			this.listTextExtraDesc.Add(this.textExtraDesc1 = owner.transform.FindChild("text_bg/textTip1").GetComponent<Text>());
			this.listTextExtraDesc.Add(this.textExtraDesc2 = owner.transform.FindChild("text_bg/textTip2").GetComponent<Text>());
			this.listTextExtraDesc.Add(this.textExtraDesc3 = owner.transform.FindChild("text_bg/textTip3").GetComponent<Text>());
			BaseButton arg_1E2_0 = new BaseButton(owner.transform.FindChild("close_btn"), 1, 1);
			Action<GameObject> arg_1E2_1;
			if ((arg_1E2_1 = RewardEquipTip.<>c.<>9__13_0) == null)
			{
				arg_1E2_1 = (RewardEquipTip.<>c.<>9__13_0 = new Action<GameObject>(RewardEquipTip.<>c.<>9.<.ctor>b__13_0));
			}
			arg_1E2_0.onClick = arg_1E2_1;
		}

		public void ShowBgImgByQuality(int quality, int beginWith = 0)
		{
			bool flag = this.listGoBgImg.Count < quality - beginWith || quality < beginWith;
			if (!flag)
			{
				for (int i = 0; i < this.listGoBgImg.Count; i++)
				{
					GameObject expr_2F = this.listGoBgImg[i];
					if (expr_2F != null)
					{
						expr_2F.SetActive(false);
					}
				}
				GameObject expr_60 = this.listGoBgImg[quality - beginWith];
				if (expr_60 != null)
				{
					expr_60.SetActive(true);
				}
			}
		}

		public void ShowEquipTip(uint itemId)
		{
			bool flag = this.itemIcon.transform.childCount > 0;
			if (flag)
			{
				for (int i = this.itemIcon.transform.childCount - 1; i >= 0; i--)
				{
					UnityEngine.Object.Destroy(this.itemIcon.transform.GetChild(i).gameObject);
				}
			}
			this.textBaseHead.gameObject.SetActive(false);
			IconImageMgr.getInstance().createA3ItemIcon(itemId, false, -1, 1f, false, -1, 0, false, false, true, false).transform.SetParent(this.itemIcon.transform, false);
			SXML itemXml = ModelBase<a3_BagModel>.getInstance().getItemXml((int)itemId);
			this.textItemName.text = itemXml.getString("item_name");
			switch (itemXml.getInt("job_limit"))
			{
			case -1:
			case 0:
			case 1:
			case 4:
				IL_FC:
				this.textCarrLimit.text = "无限制";
				goto IL_148;
			case 2:
				this.textCarrLimit.text = "战士";
				goto IL_148;
			case 3:
				this.textCarrLimit.text = "法师";
				goto IL_148;
			case 5:
				this.textCarrLimit.text = "刺客";
				goto IL_148;
			}
			goto IL_FC;
			IL_148:
			int @int = itemXml.getInt("att_type");
			SXML sXML = XMLMgr.instance.GetSXML("item.stage", "stage_level==0");
			SXML node = sXML.GetNode("stage_info", "itemid==" + itemId);
			bool flag2 = node != null;
			if (flag2)
			{
				int num = @int;
				if (num != 5)
				{
					int int2 = node.getInt("basic_att");
					this.textBaseAttr.text = string.Format("{0}:{1}", Globle.getAttrNameById(@int), int2);
				}
				else
				{
					string[] array = node.getString("basic_att").Split(new char[]
					{
						','
					});
					bool flag3 = array.Length == 1;
					int num3;
					int num2;
					if (flag3)
					{
						num2 = (num3 = int.Parse(array[0]));
					}
					else
					{
						bool flag4 = array.Length == 2;
						if (!flag4)
						{
							return;
						}
						num2 = int.Parse(array[0]);
						num3 = int.Parse(array[1]);
					}
					this.textBaseAttr.text = string.Format("攻击:{0}-{1}", num2, num3);
				}
			}
			this.textAddAttr.text = Globle.getAttrNameById(itemXml.getInt("att_type"));
			SXML node2 = itemXml.GetNode("default_tip", "");
			bool flag5 = node2 == null;
			if (flag5)
			{
				Debug.LogError("未配置default_tip");
			}
			else
			{
				this.textTipDesc.text = node2.getString("equip_desc");
				List<SXML> nodeList = node2.GetNodeList("random_tip", "");
				for (int j = 0; j < nodeList.Count; j++)
				{
					bool flag6 = j >= this.listTextExtraDesc.Count;
					if (flag6)
					{
						break;
					}
					this.listTextExtraDesc[j].text = nodeList[j].getString("tip");
				}
				this.ShowBgImgByQuality(itemXml.getInt("quality"), 1);
			}
		}

		public void ShowXMLCustomizedEquipTip(uint itemId, RewardDescText? rewardDescText)
		{
			bool flag = !rewardDescText.HasValue;
			if (!flag)
			{
				bool flag2 = this.itemIcon.transform.childCount > 0;
				if (flag2)
				{
					for (int i = this.itemIcon.transform.childCount - 1; i >= 0; i--)
					{
						UnityEngine.Object.Destroy(this.itemIcon.transform.GetChild(i).gameObject);
					}
				}
				IconImageMgr.getInstance().createA3ItemIcon(itemId, false, -1, 1f, false, -1, 0, false, false, true, false).transform.SetParent(this.itemIcon.transform, false);
				RewardDescText value = rewardDescText.Value;
				this.textItemName.text = value.strItemName;
				this.textTipDesc.text = value.strTipDesc;
				this.textCarrLimit.text = value.strCarrLimit;
				this.textBaseHead.gameObject.SetActive(true);
				this.textBaseAttr.text = value.strBaseAttr;
				this.textAddAttr.text = value.strAddAttr;
				this.textExtraDesc1.text = value.strExtraDesc1;
				this.textExtraDesc2.text = value.strExtraDesc2;
				this.textExtraDesc3.text = value.strExtraDesc3;
			}
		}
	}
}
