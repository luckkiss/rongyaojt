using Cross;
using GameFramework;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class relive : Window
	{
		private float curCd;

		private processStruct process;

		private Text txt;

		private bool hasSend = false;

		public override void init()
		{
			this.txt = base.getComponentByPath<Text>("txt");
			this.process = new processStruct(new Action<float>(this.onUpdate), "relive", false, false);
		}

		public override void onShowed()
		{
			base.getComponentByPath<Button>("bt").onClick.AddListener(new UnityAction(this.onClick));
			this.curCd = Time.time + 30f;
			(CrossApp.singleton.getPlugin("processManager") as processManager).addProcess(this.process, false);
			base.onShowed();
			this.hasSend = false;
			this.refresh();
		}

		private void refresh()
		{
			int num = (int)(this.curCd - Time.time);
			bool flag = num <= 0 && !this.hasSend;
			if (flag)
			{
				this.hasSend = true;
				BaseProxy<MapProxy>.getInstance().sendRespawn(false);
			}
			else
			{
				this.txt.text = string.Concat(num);
			}
		}

		private void onUpdate(float s)
		{
			this.refresh();
		}

		public override void onClosed()
		{
			base.getComponentByPath<Button>("bt").onClick.RemoveListener(new UnityAction(this.onClick));
			(CrossApp.singleton.getPlugin("processManager") as processManager).addProcess(this.process, false);
			base.onClosed();
		}

		private void onClick()
		{
			bool flag = this.hasSend;
			if (!flag)
			{
				BaseProxy<MapProxy>.getInstance().sendRespawn(true);
				this.hasSend = true;
			}
		}
	}
}
