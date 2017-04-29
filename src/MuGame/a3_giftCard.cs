using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_giftCard : Window
	{
		public static a3_giftCard instance;

		private GiftCardData giftdata;

		public override void init()
		{
			a3_giftCard.instance = this;
			BaseButton baseButton = new BaseButton(base.transform.FindChild("btn_do"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onBtnClose);
		}

		public override void onShowed()
		{
			base.onShowed();
			bool flag = this.uiData != null && this.uiData.Count > 0;
			if (flag)
			{
				this.giftdata = (GiftCardData)this.uiData[0];
			}
			bool flag2 = this.giftdata == null;
			if (!flag2)
			{
				base.transform.FindChild("name").GetComponent<Text>().text = this.giftdata.cardType.name;
				base.transform.FindChild("desc").GetComponent<Text>().text = this.giftdata.cardType.desc;
				Transform transform = base.transform.FindChild("itemMask/items");
				for (int i = 0; i < transform.childCount; i++)
				{
					UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
				}
				foreach (BaseItemData current in this.giftdata.cardType.lItem)
				{
					GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(uint.Parse(current.id), true, current.num, 1f, false, -1, 0, false, false, false, false);
					gameObject.transform.FindChild("bicon").gameObject.SetActive(true);
					gameObject.transform.SetParent(transform, false);
				}
			}
		}

		private void onBtnClose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_GIFTCARD);
		}
	}
}
