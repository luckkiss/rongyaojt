using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class a3_equipup : FloatUi
	{
		public static a3_equipup instance;

		private Transform con;

		private GameObject item;

		public uint nowShow;

		public override void init()
		{
			a3_equipup.instance = this;
			this.showUse();
		}

		public override void onShowed()
		{
			base.onShowed();
		}

		public void showUse()
		{
			bool flag = ModelBase<a3_BagModel>.getInstance().newshow_item.Count > 0;
			if (flag)
			{
				base.gameObject.SetActive(true);
				using (Dictionary<uint, a3_BagItemData>.KeyCollection.Enumerator enumerator = ModelBase<a3_BagModel>.getInstance().newshow_item.Keys.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						uint current = enumerator.Current;
						this.refreshShow(ModelBase<a3_BagModel>.getInstance().newshow_item[current]);
						this.nowShow = current;
					}
				}
			}
			else
			{
				bool flag2 = ModelBase<a3_BagModel>.getInstance().newshow_item.Count <= 0 && ModelBase<a3_BagModel>.getInstance().neweqp.Count > 0;
				if (flag2)
				{
					base.gameObject.SetActive(true);
					using (Dictionary<uint, a3_BagItemData>.KeyCollection.Enumerator enumerator2 = ModelBase<a3_BagModel>.getInstance().neweqp.Keys.GetEnumerator())
					{
						if (enumerator2.MoveNext())
						{
							uint current2 = enumerator2.Current;
							this.refreshShow(ModelBase<a3_BagModel>.getInstance().neweqp[current2]);
							this.nowShow = current2;
						}
					}
				}
				else
				{
					bool flag3 = ModelBase<a3_BagModel>.getInstance().newshow_item.Count <= 0 && ModelBase<a3_BagModel>.getInstance().neweqp.Count <= 0;
					if (flag3)
					{
						base.gameObject.SetActive(false);
					}
				}
			}
		}

		private void refreshShow(a3_BagItemData data)
		{
			Transform transform = base.transform.FindChild("bg/icon");
			bool flag = transform.childCount > 0;
			if (flag)
			{
				for (int i = 0; i < transform.childCount; i++)
				{
					UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
				}
			}
			GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(data, false, -1, 1f, false);
			gameObject.transform.SetParent(transform, false);
			new BaseButton(base.transform.FindChild("bg/do"), 1, 1).onClick = delegate(GameObject go)
			{
				bool isEquip = data.isEquip;
				if (isEquip)
				{
					BaseProxy<EquipProxy>.getInstance().sendChangeEquip(data.id);
				}
				else
				{
					bool flag2 = data.confdata.use_type == 13 || data.confdata.use_type == 20;
					if (flag2)
					{
						ModelBase<a3_BagModel>.getInstance().useItemByTpid(data.confdata.tpid, 1);
					}
				}
			};
			new BaseButton(base.transform.FindChild("bg/close"), 1, 1).onClick = delegate(GameObject go)
			{
				bool isEquip = data.isEquip;
				if (isEquip)
				{
					ModelBase<a3_BagModel>.getInstance().neweqp.Remove(data.id);
				}
				else
				{
					bool flag2 = data.confdata.use_type == 13 || data.confdata.use_type == 20;
					if (flag2)
					{
						ModelBase<a3_BagModel>.getInstance().newshow_item.Remove(data.id);
					}
				}
				this.showUse();
			};
		}

		private void Update()
		{
		}

		private void timeGo()
		{
			base.gameObject.SetActive(false);
		}
	}
}
