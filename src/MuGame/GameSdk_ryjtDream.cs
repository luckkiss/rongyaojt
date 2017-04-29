using Cross;
using System;

namespace MuGame
{
	public class GameSdk_ryjtDream : GameSdk_base
	{
		public override void Pay(rechargeData data)
		{
			debug.Log("begin-pay");
			Variant variant = new Variant();
			debug.Log("serverId:" + Globle.curServerD.sid);
			variant["serverId"] = Globle.curServerD.sid;
			variant["serverName"] = Globle.curServerD.server_name;
			variant["serverDesc"] = Globle.curServerD.sid;
			variant["roleId"] = ModelBase<PlayerModel>.getInstance().cid;
			variant["roleName"] = ModelBase<PlayerModel>.getInstance().name;
			variant["productId"] = data.payid;
			debug.Log("rechargeData:" + ModelBase<RechargeModel>.getInstance().getRechargeDataById(data.id));
			debug.Log("name:" + data.name);
			variant["productName"] = data.name;
			variant["productPrice"] = data.golden;
			variant["productCount"] = 1;
			variant["productDesc"] = "description";
			variant["change_rate"] = 0;
			variant["roleLvl"] = ModelBase<PlayerModel>.getInstance().lvl;
			debug.Log("end-pay");
			string jstr = JsonManager.VariantToString(variant);
			AnyPlotformSDK.Call_Cmd("pay", "lanPay", jstr, true);
		}

		public override void record_quit()
		{
			bool flag = Globle.Lan != "zh_cn";
			if (!flag)
			{
				Variant variant = new Variant();
				variant["roleId"] = ModelBase<PlayerModel>.getInstance().cid;
				variant["roleName"] = ModelBase<PlayerModel>.getInstance().name;
				variant["roleLevel"] = ModelBase<PlayerModel>.getInstance().lvl;
				variant["roleGold"] = ModelBase<PlayerModel>.getInstance().gold;
				variant["roleleveluptime"] = "";
				variant["rolecreatetime"] = "";
				variant["rolevip"] = ModelBase<PlayerModel>.getInstance().vip;
				variant["roleYb"] = ModelBase<PlayerModel>.getInstance().money;
				variant["roleServerId"] = Globle.curServerD.sid;
				variant["roleServerName"] = Globle.curServerD.server_name;
				string text = JsonManager.VariantToString(variant);
				AnyPlotformSDK.Call_Cmd("exitPage", "lanRole", text, false);
				debug.Log("[record]quit:" + text);
			}
		}
	}
}
