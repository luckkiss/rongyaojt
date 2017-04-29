using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class GameSdk_quick : GameSdk_base
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
			variant["productyb"] = data.golden_value;
			debug.Log("end-pay");
			string jstr = JsonManager.VariantToString(variant);
			AnyPlotformSDK.Call_Cmd("pay", "lanPay", jstr, true);
		}

		public override void record_createRole(Variant data)
		{
			int val = data["cid"];
			string text = data["name"];
			bool flag = text.Length > 2;
			if (flag)
			{
				text = text.Remove(text.Length - 2);
			}
			uint up_lv = data["zhua"];
			uint lv = data["lvl"];
			int val2 = data["carr"];
			Variant variant = new Variant();
			variant["roleId"] = val;
			variant["roleName"] = text;
			variant["roleLevel"] = this.getlv(up_lv, lv);
			variant["roleGold"] = 0;
			variant["roleYb"] = 0;
			variant["roleCreateTime"] = NetClient.instance.CurServerTimeStamp;
			variant["roleServerId"] = Globle.curServerD.sid;
			variant["roleServerName"] = Globle.curServerD.server_name;
			variant["rolevip"] = 0;
			variant["rolePartyName"] = "";
			variant["rolePartyId"] = "";
			variant["rolePower"] = 0;
			variant["rolePartyRoleId"] = "";
			variant["rolePartyRoleName"] = "";
			variant["roleProfessionId"] = val2;
			variant["roleProfession"] = "";
			variant["roleFriendlist"] = "";
			string text2 = JsonManager.VariantToString(variant);
			AnyPlotformSDK.Call_Cmd("createRole", "lanRole", text2, false);
			debug.Log("[record]createRole:" + text2);
		}

		public string getlv(uint up_lv, uint lv)
		{
			bool flag = lv < 10u;
			string str;
			if (flag)
			{
				str = "00" + lv.ToString();
			}
			else
			{
				bool flag2 = lv >= 100u;
				if (flag2)
				{
					str = lv.ToString();
				}
				else
				{
					str = "0" + lv.ToString();
				}
			}
			return (up_lv + 1u).ToString() + str;
		}

		public override void record_login()
		{
			Variant variant = new Variant();
			variant["roleId"] = ModelBase<PlayerModel>.getInstance().cid;
			variant["roleName"] = ModelBase<PlayerModel>.getInstance().name;
			variant["roleLevel"] = this.getlv(ModelBase<PlayerModel>.getInstance().up_lvl, ModelBase<PlayerModel>.getInstance().lvl);
			variant["roleGold"] = ModelBase<PlayerModel>.getInstance().money;
			variant["roleYb"] = ModelBase<PlayerModel>.getInstance().gold;
			variant["roleCreateTime"] = ModelBase<PlayerModel>.getInstance().crttm;
			variant["roleServerId"] = Globle.curServerD.sid;
			variant["roleServerName"] = Globle.curServerD.server_name;
			variant["rolevip"] = ModelBase<PlayerModel>.getInstance().vip;
			variant["rolePartyName"] = "";
			variant["rolePartyId"] = ModelBase<PlayerModel>.getInstance().clanid;
			variant["rolePower"] = ModelBase<PlayerModel>.getInstance().combpt;
			variant["rolePartyRoleId"] = "";
			variant["rolePartyRoleName"] = "";
			variant["roleProfessionId"] = ModelBase<PlayerModel>.getInstance().profession;
			variant["roleProfession"] = "";
			variant["roleFriendlist"] = "";
			string text = JsonManager.VariantToString(variant);
			AnyPlotformSDK.Call_Cmd("enterGame", "lanRole", text, false);
			debug.Log("[record]login:" + text);
		}

		public override void record_LvlUp()
		{
			Variant variant = new Variant();
			variant["roleId"] = ModelBase<PlayerModel>.getInstance().cid;
			variant["roleName"] = ModelBase<PlayerModel>.getInstance().name;
			variant["roleLevel"] = this.getlv(ModelBase<PlayerModel>.getInstance().up_lvl, ModelBase<PlayerModel>.getInstance().lvl);
			variant["roleGold"] = ModelBase<PlayerModel>.getInstance().money;
			variant["roleYb"] = ModelBase<PlayerModel>.getInstance().gold;
			variant["roleCreateTime"] = ModelBase<PlayerModel>.getInstance().crttm;
			variant["roleServerId"] = Globle.curServerD.sid;
			variant["roleServerName"] = Globle.curServerD.server_name;
			variant["rolevip"] = ModelBase<PlayerModel>.getInstance().vip;
			variant["rolePartyName"] = "";
			variant["rolePartyId"] = ModelBase<PlayerModel>.getInstance().clanid;
			variant["rolePower"] = ModelBase<PlayerModel>.getInstance().combpt;
			variant["rolePartyRoleId"] = "";
			variant["rolePartyRoleName"] = "";
			variant["roleProfessionId"] = ModelBase<PlayerModel>.getInstance().profession;
			variant["roleProfession"] = "";
			variant["roleFriendlist"] = "";
			string text = JsonManager.VariantToString(variant);
			AnyPlotformSDK.Call_Cmd("roleUpgrade", "lanRole", text, false);
			debug.Log("[record]LvlUp:" + text);
		}

		public override void record_quit()
		{
			Variant variant = new Variant();
			variant["roleId"] = ModelBase<PlayerModel>.getInstance().cid;
			variant["roleName"] = ModelBase<PlayerModel>.getInstance().name;
			variant["roleLevel"] = this.getlv(ModelBase<PlayerModel>.getInstance().up_lvl, ModelBase<PlayerModel>.getInstance().lvl);
			variant["roleGold"] = ModelBase<PlayerModel>.getInstance().money;
			variant["roleYb"] = ModelBase<PlayerModel>.getInstance().gold;
			variant["roleCreateTime"] = ModelBase<PlayerModel>.getInstance().crttm;
			variant["roleServerId"] = Globle.curServerD.sid;
			variant["roleServerName"] = Globle.curServerD.server_name;
			variant["rolevip"] = ModelBase<PlayerModel>.getInstance().vip;
			variant["rolePartyName"] = "";
			variant["rolePartyId"] = ModelBase<PlayerModel>.getInstance().clanid;
			variant["rolePower"] = ModelBase<PlayerModel>.getInstance().combpt;
			variant["rolePartyRoleId"] = "";
			variant["rolePartyRoleName"] = "";
			variant["roleProfessionId"] = ModelBase<PlayerModel>.getInstance().profession;
			variant["roleProfession"] = "";
			variant["roleFriendlist"] = "";
			string text = JsonManager.VariantToString(variant);
			AnyPlotformSDK.Call_Cmd("exitPage", "lanRole", text, false);
			debug.Log("[record]exitPage:" + text);
		}
	}
}
