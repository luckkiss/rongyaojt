using GameFramework;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_eqpInherit : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_eqpInherit.<>c <>9 = new a3_eqpInherit.<>c();

			public static Action<GameObject> <>9__2_1;

			internal void <init>b__2_1(GameObject go)
			{
				ModelBase<a3_EquipModel>.getInstance().Attchange_wite = false;
				a3_attChange.instans.runTxt(null);
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQPINHERIT);
			}
		}

		private Transform eqpIcon1 = null;

		private Transform eqpIcon2 = null;

		private uint eqp1_id = 0u;

		private uint eqp2_id = 0u;

		public override void init()
		{
			this.eqpIcon1 = base.transform.FindChild("eqp1");
			this.eqpIcon2 = base.transform.FindChild("eqp2");
			new BaseButton(base.transform.FindChild("yes"), 1, 1).onClick = delegate(GameObject go)
			{
				BaseProxy<EquipProxy>.getInstance().sendInherit(this.eqp1_id, this.eqp2_id, 1, false);
			};
			BaseButton arg_8C_0 = new BaseButton(base.transform.FindChild("no"), 1, 1);
			Action<GameObject> arg_8C_1;
			if ((arg_8C_1 = a3_eqpInherit.<>c.<>9__2_1) == null)
			{
				arg_8C_1 = (a3_eqpInherit.<>c.<>9__2_1 = new Action<GameObject>(a3_eqpInherit.<>c.<>9.<init>b__2_1));
			}
			arg_8C_0.onClick = arg_8C_1;
		}

		public override void onShowed()
		{
			bool flag = this.uiData != null && this.uiData.Count > 1;
			if (flag)
			{
				this.eqp1_id = (uint)this.uiData[0];
				this.eqp2_id = (uint)this.uiData[1];
				bool flag2 = this.eqp2_id == this.eqp1_id;
				if (flag2)
				{
					InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQPINHERIT);
					return;
				}
			}
			base.transform.SetAsLastSibling();
			BaseProxy<EquipProxy>.getInstance().addEventListener(EquipProxy.EVENT_EQUIP_INHERIT, new Action<GameEvent>(this.onEquipInherit));
			this.SetShow();
		}

		private void SetShow()
		{
			this.clearicon();
			GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(ModelBase<a3_EquipModel>.getInstance().getEquipByAll(this.eqp1_id), false, -1, 1f, false);
			gameObject.transform.SetParent(this.eqpIcon1, false);
			gameObject.transform.FindChild("iconborder/ismark").gameObject.SetActive(false);
			GameObject gameObject2 = IconImageMgr.getInstance().createA3ItemIcon(ModelBase<a3_EquipModel>.getInstance().getEquipByAll(this.eqp2_id), false, -1, 1f, false);
			gameObject2.transform.SetParent(this.eqpIcon2, false);
			gameObject2.transform.FindChild("iconborder/ismark").gameObject.SetActive(false);
			SXML sXML = XMLMgr.instance.GetSXML("item.inheritance", "equip_stage==" + ModelBase<a3_EquipModel>.getInstance().getEquipByAll(this.eqp1_id).equipdata.stage);
			base.transform.FindChild("money").GetComponent<Text>().text = sXML.getString("money");
		}

		private void onEquipInherit(GameEvent e)
		{
			bool flag = a3_bag.isshow;
			if (flag)
			{
				a3_bag.isshow.refOneEquipIcon(this.eqp2_id);
			}
			flytxt.instance.fly("传承成功！！！", 0, default(Color), null);
			this.eqp1_id = 0u;
			this.eqp2_id = 0u;
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_EQPINHERIT);
			ModelBase<a3_EquipModel>.getInstance().Attchange_wite = false;
			a3_attChange.instans.runTxt(null);
		}

		private void clearicon()
		{
			for (int i = 0; i < this.eqpIcon1.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.eqpIcon1.GetChild(i).gameObject);
			}
			for (int j = 0; j < this.eqpIcon2.childCount; j++)
			{
				UnityEngine.Object.Destroy(this.eqpIcon2.GetChild(j).gameObject);
			}
		}

		public override void onClosed()
		{
			BaseProxy<EquipProxy>.getInstance().removeEventListener(EquipProxy.EVENT_EQUIP_INHERIT, new Action<GameEvent>(this.onEquipInherit));
			ModelBase<a3_EquipModel>.getInstance().Attchange_wite = false;
		}
	}
}
