using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class A3_WingProxy : BaseProxy<A3_WingProxy>
	{
		public const uint ON_LEVEL_EXP_CHANGE = 0u;

		public const uint ON_STAGE_CHANGE = 1u;

		public const uint ON_LEVEL_AUTO_UPGRADE = 2u;

		public const uint ON_SHOW_CHANGE = 3u;

		public const uint ON_STAGE_DIFT = 4u;

		public A3_WingProxy()
		{
			this.addProxyListener(89u, new Action<Variant>(this.OnWings));
		}

		public void GetWings()
		{
			debug.Log("send_89_op_1_");
			Variant variant = new Variant();
			variant["op"] = 1;
			this.sendRPC(89u, variant);
		}

		public void SendUpgradeLevel(bool use_yb = false)
		{
			debug.Log("send_89_op_2_useyb_" + use_yb.ToString());
			Variant variant = new Variant();
			variant["op"] = 2;
			variant["use_yb"] = use_yb;
			this.sendRPC(89u, variant);
		}

		public void SendUpgradeStage(int item_num)
		{
			debug.Log("send_89_op_3_item_num_" + item_num);
			Variant variant = new Variant();
			variant["op"] = 3;
			variant["item_num"] = item_num;
			this.sendRPC(89u, variant);
		}

		public void SendAutoUpgradeLevel()
		{
			debug.Log("send_89_op_4");
			Variant variant = new Variant();
			variant["op"] = 4;
			this.sendRPC(89u, variant);
		}

		public void SetAuotUse()
		{
		}

		public void SendShowStage(int showStage)
		{
			debug.Log("send_89_op_5_showStage" + showStage);
			Variant variant = new Variant();
			variant["op"] = 5;
			variant["show_stage"] = showStage;
			this.sendRPC(89u, variant);
		}

		public void OnWings(Variant data)
		{
			debug.Log("wing::" + data.dump());
			int res = data["res"];
			switch (res)
			{
			case 1:
				ModelBase<A3_WingModel>.getInstance().InitWingInfo(data);
				break;
			case 2:
				ModelBase<A3_WingModel>.getInstance().SetLevelExp(data);
				base.dispatchEvent(GameEvent.Create(0u, this, data, false));
				break;
			case 3:
			{
				ModelBase<A3_WingModel>.getInstance().SetStageInfo(data);
				bool stageUp = ModelBase<A3_WingModel>.getInstance().stageUp;
				if (stageUp)
				{
					base.dispatchEvent(GameEvent.Create(1u, this, data, false));
					ModelBase<A3_WingModel>.getInstance().SetShowStage(data);
					base.dispatchEvent(GameEvent.Create(3u, this, data, false));
				}
				else
				{
					base.dispatchEvent(GameEvent.Create(4u, this, data, false));
				}
				break;
			}
			case 4:
				ModelBase<A3_WingModel>.getInstance().SetLevelExp(data);
				base.dispatchEvent(GameEvent.Create(2u, this, data, false));
				break;
			case 5:
				ModelBase<A3_WingModel>.getInstance().SetShowStage(data);
				base.dispatchEvent(GameEvent.Create(3u, this, data, false));
				break;
			default:
				Globle.err_output(res);
				break;
			}
		}
	}
}
