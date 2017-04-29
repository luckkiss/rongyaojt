using GameFramework;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class RewardItemTip
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly RewardItemTip.<>c <>9 = new RewardItemTip.<>c();

			public static Action<GameObject> <>9__4_0;

			internal void ctor>b__4_0(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_MINITIP);
			}
		}

		public GameObject owner;

		public GameObject itemIcon;

		public Text textDesc;

		public Text textName;

		public RewardItemTip(GameObject owner)
		{
			this.owner = owner;
			this.itemIcon = owner.transform.FindChild("text_bg/iconbg/icon").gameObject;
			this.textDesc = owner.transform.FindChild("text_bg/text").GetComponent<Text>();
			this.textName = owner.transform.FindChild("text_bg/nameBg/itemName").GetComponent<Text>();
			BaseButton arg_96_0 = new BaseButton(owner.transform.FindChild("close_btn"), 1, 1);
			Action<GameObject> arg_96_1;
			if ((arg_96_1 = RewardItemTip.<>c.<>9__4_0) == null)
			{
				arg_96_1 = (RewardItemTip.<>c.<>9__4_0 = new Action<GameObject>(RewardItemTip.<>c.<>9.<.ctor>b__4_0));
			}
			arg_96_0.onClick = arg_96_1;
		}

		public void ShowItemTip(uint itemId)
		{
			bool flag = this.itemIcon.transform.childCount > 0;
			if (flag)
			{
				for (int i = this.itemIcon.transform.childCount - 1; i >= 0; i--)
				{
					UnityEngine.Object.Destroy(this.itemIcon.transform.GetChild(i).gameObject);
				}
			}
			IconImageMgr.getInstance().createA3ItemIcon(itemId, false, -1, 1f, false, -1, 0, false, false, true, false).transform.SetParent(this.itemIcon.transform, false);
			SXML itemXml = ModelBase<a3_BagModel>.getInstance().getItemXml((int)itemId);
			this.textDesc.text = StringUtils.formatText(itemXml.getString("desc"));
			this.textName.text = itemXml.getString("item_name");
		}
	}
}
