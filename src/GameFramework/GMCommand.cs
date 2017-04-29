using Cross;
using System;

namespace GameFramework
{
	public class GMCommand
	{
		private static GMCommand m_gmcommand;

		private NetClient _netM;

		public static GMCommand inst
		{
			get
			{
				bool flag = GMCommand.m_gmcommand == null;
				if (flag)
				{
					GMCommand.m_gmcommand = new GMCommand();
				}
				return GMCommand.m_gmcommand;
			}
		}

		public void setNetManager(NetClient m)
		{
			this._netM = m;
		}

		private void sendTPKG(uint cmd, Variant parm)
		{
			this._netM.sendTpkg(cmd, parm);
		}

		public void get_item(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			int num = param.IndexOf(" ");
			string s = param.Substring(0, num);
			string s2 = param.Substring(num + 1);
			variant2["tpid"] = int.Parse(s);
			variant2["cnt"] = int.Parse(s2);
			variant["cmd"] = "get_item";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void go_to(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			int num = param.IndexOf(" ");
			string s = param.Substring(0, num);
			int num2 = param.IndexOf(" ", num + 1);
			string s2 = param.Substring(num + 1, num2 - num - 1);
			string s3 = param.Substring(num2 + 1);
			variant2["mpid"] = int.Parse(s);
			variant2["x"] = int.Parse(s2);
			variant2["y"] = int.Parse(s3);
			variant["cmd"] = "go_to";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void send_sys_mail(string param)
		{
			Variant parm = new Variant();
			int num = param.IndexOf(" ");
			string text = param.Substring(0, num);
			string text2 = param.Substring(num + 1);
			this.sendTPKG(50u, parm);
		}

		public void acp_mis(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			variant2["id"] = int.Parse(param);
			variant["cmd"] = "acp_mis";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void fin_mis(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			variant2["id"] = int.Parse(param);
			variant["cmd"] = "fin_mis";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void speed(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			variant2["spd"] = int.Parse(param);
			variant["cmd"] = "speed";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void learn_skil(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			variant2["sid"] = int.Parse(param);
			variant["cmd"] = "learn_skil";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void active_meri(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			variant2["mid"] = int.Parse(param);
			variant["cmd"] = "active_meri";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void active_acup(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			int num = param.IndexOf(" ");
			string s = param.Substring(0, num);
			string s2 = param.Substring(num + 1);
			variant2["mid"] = int.Parse(s);
			variant2["aid"] = int.Parse(s2);
			variant["cmd"] = "active_acup";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void acup_up(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			int num = param.IndexOf(" ");
			string s = param.Substring(0, num);
			string s2 = param.Substring(num + 1);
			variant2["mid"] = int.Parse(s);
			variant2["mid"] = int.Parse(s2);
			variant["cmd"] = "acup_up";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void kick(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			variant2["name"] = int.Parse(param);
			variant["cmd"] = "kick";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void moveto(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			variant2["name"] = int.Parse(param);
			variant["cmd"] = "moveto";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void dragpl(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			variant2["name"] = int.Parse(param);
			variant["cmd"] = "dragpl";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void forb_talk(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			int num = param.IndexOf(" ");
			string val = param.Substring(0, num);
			string s = param.Substring(num + 1);
			variant2["name"] = val;
			variant2["tm"] = int.Parse(s);
			variant["cmd"] = "forb_talk";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void seal_cha(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			int num = param.IndexOf(" ");
			string val = param.Substring(0, num);
			string s = param.Substring(num + 1);
			variant2["name"] = val;
			variant2["tm"] = int.Parse(s);
			variant["cmd"] = "seal_cha";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void hide(string param)
		{
			Variant variant = new Variant();
			Variant value = new Variant();
			variant["cmd"] = "hide";
			variant["par"] = value;
			this.sendTPKG(50u, variant);
		}

		public void add_npc(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			int num = param.IndexOf(" ");
			string s = param.Substring(0, num);
			int num2 = param.IndexOf(" ", num + 1);
			string s2 = param.Substring(num + 1, num2 - num - 1);
			string s3 = param.Substring(num2 + 1);
			variant2["nid"] = int.Parse(s);
			variant2["r"] = int.Parse(s2);
			variant2["tm"] = int.Parse(s3);
			variant["cmd"] = "add_npc";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void add_mon(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			int num = param.IndexOf(" ");
			string s = param.Substring(0, num);
			int num2 = param.IndexOf(" ", num + 1);
			string s2 = param.Substring(num + 1, num2 - num - 1);
			int num3 = param.IndexOf(" ", num2 + 1);
			string s3 = param.Substring(num2 + 1, num3 - num2 - 1);
			int num4 = param.IndexOf(" ", num3 + 1);
			string s4 = param.Substring(num3 + 1, num4 - num3 - 1);
			string s5 = param.Substring(num4);
			variant2["mid"] = int.Parse(s);
			variant2["sdid"] = int.Parse(s2);
			variant2["r_x"] = int.Parse(s3);
			variant2["r_y"] = int.Parse(s4);
			variant2["stm"] = int.Parse(s5);
			variant["cmd"] = "add_mon";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void broadcast(string param)
		{
			Variant variant = new Variant();
			variant["tp"] = 6;
			variant["msg"] = param;
			this.sendTPKG(160u, variant);
		}

		public void kill_mon(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			int num = param.IndexOf(" ");
			string s = param.Substring(0, num);
			int num2 = param.IndexOf(" ", num + 1);
			string s2 = param.Substring(num + 1, num2 - num - 1);
			string s3 = param.Substring(num2 + 1);
			variant2["mid"] = int.Parse(s);
			variant2["cnt"] = int.Parse(s2);
			variant2["tm"] = int.Parse(s3);
			variant["cmd"] = "kill_mon";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void mod_pet_exp(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			variant2["exp"] = int.Parse(param);
			variant["cmd"] = "mod_pet_exp";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void set_ol_tms(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			int num = param.IndexOf(" ");
			string s = param.Substring(0, num);
			int num2 = param.IndexOf(" ", num + 1);
			string s2 = param.Substring(num + 1, num2 - num - 1);
			string s3 = param.Substring(num2 + 1);
			variant2["ldol"] = int.Parse(s) * 3600;
			variant2["lwol"] = int.Parse(s2) * 3600;
			variant2["ofla"] = int.Parse(s3) * 3600;
			variant["cmd"] = "set_ol_tms";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void set_vip(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			variant2["vip"] = int.Parse(param);
			variant["cmd"] = "set_vip";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void get_wh(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			variant2["tpid"] = int.Parse(param);
			variant["cmd"] = "get_wh";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void get_pet(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			variant2["tpid"] = int.Parse(param);
			variant["cmd"] = "get_pet";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void get_chpawd(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			variant2["id"] = int.Parse(param);
			variant["cmd"] = "get_chpawd";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void get_achive(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			int num = param.IndexOf(" ");
			string s = param.Substring(0, num);
			string s2 = param.Substring(num + 1);
			variant2["id"] = int.Parse(s);
			variant2["tm"] = int.Parse(s2);
			variant["cmd"] = "get_achive";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void del_achive(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			int num = param.IndexOf(" ");
			bool flag = num >= 0;
			if (flag)
			{
				string s = param.Substring(0, num);
				string s2 = param.Substring(num + 1);
				variant2["id"] = int.Parse(s);
				variant2["cid"] = int.Parse(s2);
				variant["cmd"] = "del_achive";
				variant["par"] = variant2;
				this.sendTPKG(50u, variant);
			}
			else
			{
				variant2["id"] = int.Parse(param);
				variant["cmd"] = "del_achive";
				variant["par"] = variant2;
				this.sendTPKG(50u, variant);
			}
		}

		public void set_fin_lvl(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			int num = param.IndexOf(" ");
			string s = param.Substring(0, num);
			int num2 = param.IndexOf(" ", num + 1);
			string s2 = param.Substring(num + 1, num2 - num - 1);
			string s3 = param.Substring(num2 + 1);
			variant2["tpid"] = int.Parse(s);
			variant2["diff"] = int.Parse(s2);
			variant2["tm"] = int.Parse(s3);
			variant["cmd"] = "set_fin_lvl";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void set_carrlvl(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			variant2["lvl"] = int.Parse(param);
			variant["cmd"] = "set_carrlvl";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}

		public void clr_cd(string param)
		{
			Variant variant = new Variant();
			Variant variant2 = new Variant();
			variant2["cid"] = int.Parse(param);
			variant["cmd"] = "clr_cd";
			variant["par"] = variant2;
			this.sendTPKG(50u, variant);
		}
	}
}
