using GameFramework;
using MuGame.Qsmy.model;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_autoplay_pick : Window
	{
		private Toggle[] eqpToggles;

		private Toggle[] matToggles;

		private Toggle equip_Toggles;

		private Toggle pet_Toggles;

		private Toggle wing_Toggles;

		private Toggle summon_Toggles;

		private Toggle drugs_Toggles;

		private Toggle golds_Toggles;

		private Toggle other_Toggles;

		private AutoPlayModel apModel;

		public override void init()
		{
			this.apModel = ModelBase<AutoPlayModel>.getInstance();
			base.getEventTrigerByPath("ig_bg_bg").onClick = new EventTriggerListener.VoidDelegate(this.OnClose);
			BaseButton baseButton = new BaseButton(base.getTransformByPath("ok"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.OnOK);
			this.eqpToggles = new Toggle[5];
			for (int i = 0; i < 5; i++)
			{
				this.eqpToggles[i] = base.getComponentByPath<Toggle>("eqp" + i);
			}
			this.matToggles = new Toggle[5];
			for (int j = 0; j < 5; j++)
			{
				this.matToggles[j] = base.getComponentByPath<Toggle>("mat" + j);
			}
			this.equip_Toggles = base.getComponentByPath<Toggle>("eqp_cailiao");
			this.pet_Toggles = base.getComponentByPath<Toggle>("pet_cailiao");
			this.wing_Toggles = base.getComponentByPath<Toggle>("wing_cailiao");
			this.summon_Toggles = base.getComponentByPath<Toggle>("summon_cailiao");
			this.drugs_Toggles = base.getComponentByPath<Toggle>("drugs");
			this.golds_Toggles = base.getComponentByPath<Toggle>("golds");
			this.other_Toggles = base.getComponentByPath<Toggle>("other");
		}

		public override void onShowed()
		{
			int pickEqp = this.apModel.PickEqp;
			int pickMat = this.apModel.PickMat;
			for (int i = 0; i < 5; i++)
			{
				bool flag = (pickEqp & 1 << i) == 0;
				if (flag)
				{
					this.eqpToggles[i].isOn = false;
				}
				else
				{
					this.eqpToggles[i].isOn = true;
				}
				bool flag2 = (pickMat & 1 << i) == 0;
				if (flag2)
				{
					this.matToggles[i].isOn = false;
				}
				else
				{
					this.matToggles[i].isOn = true;
				}
			}
			this.equip_Toggles.isOn = (this.apModel.PickEqp_cailiao == 1);
			this.pet_Toggles.isOn = (this.apModel.PickPet_cailiao == 1);
			this.wing_Toggles.isOn = (this.apModel.PickWing_cailiao == 1);
			this.summon_Toggles.isOn = (this.apModel.PickSummon_cailiao == 1);
			this.drugs_Toggles.isOn = (this.apModel.PickDrugs == 1);
			this.golds_Toggles.isOn = (this.apModel.PickGold == 1);
			this.other_Toggles.isOn = (this.apModel.PickOther == 1);
		}

		private void OnClose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_AUTOPLAY_PICK);
		}

		private void OnOK(GameObject go)
		{
			this.apModel.PickEqp = 0;
			this.apModel.PickMat = 0;
			for (int i = 0; i < 5; i++)
			{
				bool isOn = this.eqpToggles[i].isOn;
				if (isOn)
				{
					this.apModel.PickEqp += 1 << i;
				}
				bool isOn2 = this.matToggles[i].isOn;
				if (isOn2)
				{
					this.apModel.PickMat += 1 << i;
				}
			}
			bool isOn3 = this.equip_Toggles.isOn;
			if (isOn3)
			{
				this.apModel.PickEqp_cailiao = 1;
			}
			else
			{
				this.apModel.PickEqp_cailiao = 0;
			}
			bool isOn4 = this.pet_Toggles.isOn;
			if (isOn4)
			{
				this.apModel.PickPet_cailiao = 1;
			}
			else
			{
				this.apModel.PickPet_cailiao = 0;
			}
			bool isOn5 = this.wing_Toggles.isOn;
			if (isOn5)
			{
				this.apModel.PickWing_cailiao = 1;
			}
			else
			{
				this.apModel.PickWing_cailiao = 0;
			}
			bool isOn6 = this.summon_Toggles.isOn;
			if (isOn6)
			{
				this.apModel.PickSummon_cailiao = 1;
			}
			else
			{
				this.apModel.PickSummon_cailiao = 0;
			}
			bool isOn7 = this.drugs_Toggles.isOn;
			if (isOn7)
			{
				this.apModel.PickDrugs = 1;
			}
			else
			{
				this.apModel.PickDrugs = 0;
			}
			bool isOn8 = this.golds_Toggles.isOn;
			if (isOn8)
			{
				this.apModel.PickGold = 1;
			}
			else
			{
				this.apModel.PickGold = 0;
			}
			bool isOn9 = this.other_Toggles.isOn;
			if (isOn9)
			{
				this.apModel.PickOther = 1;
			}
			else
			{
				this.apModel.PickOther = 0;
			}
			this.OnClose(null);
		}
	}
}
