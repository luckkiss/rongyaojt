using Cross;
using GameFramework;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class confirmtext : Window
	{
		public static Variant v;

		private Text txtInfo1;

		private Text txtInfo2;

		private Text txtDesc;

		private Text placeholder;

		private InputField input;

		public static void showDeleChar(Variant charinfo)
		{
			confirmtext.v = charinfo;
			InterfaceMgr.getInstance().open(InterfaceMgr.CONFIRM_TEXT, null, false);
		}

		public override void init()
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.FLYTXT, null, false);
			InterfaceMgr.openByLua("flytxt", null);
			this.txtInfo1 = base.getComponentByPath<Text>("info1");
			this.txtInfo2 = base.getComponentByPath<Text>("info2");
			this.txtDesc = base.getComponentByPath<Text>("desc");
			this.input = base.getComponentByPath<InputField>("input");
			this.placeholder = base.getComponentByPath<Text>("input/Placeholder");
			base.getComponentByPath<Button>("y").onClick.AddListener(new UnityAction(this.onYClick));
			base.getComponentByPath<Button>("n").onClick.AddListener(new UnityAction(this.onNClick));
		}

		public override void onShowed()
		{
			this.input.text = "";
			this.placeholder.text = "";
			string strJob = Globle.getStrJob(confirmtext.v["carr"]);
			string text = confirmtext.v["lvl"]._int.ToString();
			string text2 = confirmtext.v["name"];
			string text3 = confirmtext.v["zhua"]._int.ToString();
			this.txtInfo1.text = ContMgr.getCont("comm_delechar_info1", new string[]
			{
				text2
			});
			this.txtInfo2.text = ContMgr.getCont("comm_delechar_info2", new string[]
			{
				strJob,
				text3,
				text
			});
			this.txtDesc.text = ContMgr.getCont("comm_dele", null);
		}

		private void onYClick()
		{
			bool flag = this.input.text.ToLower() == "delete";
			if (flag)
			{
				UIClient.instance.dispatchEvent(GameEvent.Create(4032u, this, GameTools.createGroup(new Variant[]
				{
					"cid",
					confirmtext.v["cid"]
				}), false));
				InterfaceMgr.getInstance().close(InterfaceMgr.CONFIRM_TEXT);
			}
			else
			{
				flytxt.instance.fly(ContMgr.getCont("comm_inputerror", null), 0, default(Color), null);
			}
		}

		private void onNClick()
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.CONFIRM_TEXT);
		}
	}
}
