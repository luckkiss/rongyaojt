using GameFramework;
using MuGame.Qsmy.model;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_autoplay_eqp : Window
	{
		private Toggle[] eqpToggles;

		private Toggle sellToggle;

		private Toggle recyleToggle;

		private AutoPlayModel apModel;

		public override void init()
		{
			this.apModel = ModelBase<AutoPlayModel>.getInstance();
			base.getEventTrigerByPath("ig_bg_bg").onClick = new EventTriggerListener.VoidDelegate(this.OnClose);
			this.eqpToggles = new Toggle[5];
			for (int i = 0; i < 5; i++)
			{
				this.eqpToggles[i] = base.getComponentByPath<Toggle>("eqp" + i);
			}
			this.sellToggle = base.getComponentByPath<Toggle>("sell");
			this.sellToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnSellChange));
			this.recyleToggle = base.getComponentByPath<Toggle>("recyle");
			this.recyleToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnRecyleChange));
			BaseButton baseButton = new BaseButton(base.getTransformByPath("ok"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.OnOK);
		}

		public override void onShowed()
		{
			int eqpProc = this.apModel.EqpProc;
			for (int i = 0; i < 5; i++)
			{
				bool flag = (eqpProc & 1 << i) == 0;
				if (flag)
				{
					this.eqpToggles[i].isOn = false;
				}
				else
				{
					this.eqpToggles[i].isOn = true;
				}
			}
			this.sellToggle.isOn = false;
			this.recyleToggle.isOn = false;
			switch (this.apModel.EqpType)
			{
			case 1:
				this.sellToggle.isOn = true;
				break;
			case 2:
				this.recyleToggle.isOn = true;
				break;
			}
		}

		private void OnClose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_AUTOPLAY_EQP);
		}

		private void OnSellChange(bool v)
		{
			if (v)
			{
				this.recyleToggle.isOn = false;
			}
		}

		private void OnRecyleChange(bool v)
		{
			if (v)
			{
				this.sellToggle.isOn = false;
			}
		}

		private void OnOK(GameObject go)
		{
			this.apModel.EqpProc = 0;
			for (int i = 0; i < 5; i++)
			{
				bool isOn = this.eqpToggles[i].isOn;
				if (isOn)
				{
					this.apModel.EqpProc += 1 << i;
				}
			}
			bool isOn2 = this.sellToggle.isOn;
			if (isOn2)
			{
				this.apModel.EqpType = 1;
			}
			else
			{
				bool isOn3 = this.recyleToggle.isOn;
				if (isOn3)
				{
					this.apModel.EqpType = 2;
				}
				else
				{
					this.apModel.EqpType = 0;
				}
			}
			this.OnClose(null);
		}
	}
}
