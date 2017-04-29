using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_active_godlight : Window
	{
		private Text time_txt;

		private Text go_txt;

		private int map_x;

		private int map_y;

		private int map_id;

		public override void init()
		{
			BaseButton baseButton = new BaseButton(base.transform.FindChild("btn_close"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onclose);
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("text_go"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onGo);
			this.time_txt = base.transform.FindChild("time").GetComponent<Text>();
			this.go_txt = base.transform.FindChild("text_go").GetComponent<Text>();
		}

		public override void onShowed()
		{
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
			this.map_x = 0;
			this.map_y = 0;
			this.map_id = 1;
			string str = "";
			SXML sXML = XMLMgr.instance.GetSXML("god_light", "");
			List<SXML> nodeList = sXML.GetNodeList("player_info", "");
			bool flag = nodeList != null;
			if (flag)
			{
				foreach (SXML current in nodeList)
				{
					bool flag2 = (long)current.getInt("zhuan") < (long)((ulong)ModelBase<PlayerModel>.getInstance().up_lvl);
					if (!flag2)
					{
						bool flag3 = (long)current.getInt("zhuan") == (long)((ulong)ModelBase<PlayerModel>.getInstance().up_lvl);
						if (!flag3)
						{
							break;
						}
						bool flag4 = (long)current.getInt("lv") > (long)((ulong)ModelBase<PlayerModel>.getInstance().lvl);
						if (flag4)
						{
							break;
						}
					}
					str = current.getString("map_name");
					this.map_id = current.getInt("map_id");
					this.map_x = current.getInt("map_x");
					this.map_y = current.getInt("map_y");
				}
			}
			this.go_txt.text = "推荐前往 " + str + " 挂机>>";
		}

		public override void onClosed()
		{
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
		}

		private void onclose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_ACTIVE_GODLIGHT);
		}

		private void onGo(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_ACTIVE_GODLIGHT);
			SelfRole.moveto(this.map_id, new Vector3((float)this.map_x, (float)this.map_y, 0f), null, 0.3f);
		}

		private void Update()
		{
			bool flag = Globle.formatTime((int)a3_liteMinimap.instance.active_leftTm, true) == "00:00:00";
			if (flag)
			{
				IconAddLightMgr.getInstance().showOrHideFire("Light_btnCseth", false);
				bool flag2 = DateTime.Now.Hour >= 20 || DateTime.Now.Hour <= 12;
				if (flag2)
				{
					this.time_txt.text = "下次将在12:00开启";
				}
				else
				{
					this.time_txt.text = "下次将在19:00开启";
				}
			}
			else
			{
				IconAddLightMgr.getInstance().showOrHideFire("Light_btnCseth", false);
				this.time_txt.text = Globle.formatTime((int)a3_liteMinimap.instance.active_leftTm, true);
			}
		}
	}
}
