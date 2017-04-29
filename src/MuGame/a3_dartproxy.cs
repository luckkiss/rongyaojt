using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class a3_dartproxy : BaseProxy<a3_dartproxy>
	{
		public const int EVENT_GETINFO = 1;

		public const int LETSGO = 2;

		public const int EVENT_AWARDNUM = 3;

		public const int DARTHPNOW = 4;

		public bool gotoDart = true;

		public bool isme = false;

		public bool show2 = false;

		public bool dartHave = false;

		public bool canOpenDart = true;

		private int per;

		private bool showOne = false;

		public a3_dartproxy()
		{
			this.addProxyListener(181u, new Action<Variant>(this.dartinfo));
		}

		public void sendDartGo()
		{
			Variant variant = new Variant();
			variant["op"] = 1;
			this.sendRPC(181u, variant);
		}

		public void sendDartStart(uint line)
		{
			Variant variant = new Variant();
			variant["op"] = 2;
			variant["line"] = line;
			this.sendRPC(181u, variant);
		}

		private void dartinfo(Variant data)
		{
			debug.Log("镖车信息:" + data.dump());
			int num = data["res"];
			bool flag = num < 0;
			if (flag)
			{
				Globle.err_output(num);
			}
			else
			{
				switch (num)
				{
				case 1:
					this.info(data);
					break;
				case 2:
					this.wannaGo(data);
					break;
				case 3:
					this.itemNum(data);
					break;
				case 4:
					this.dartHP(data);
					break;
				}
			}
		}

		private void info(Variant data)
		{
			base.dispatchEvent(GameEvent.Create(1u, this, data, false));
			bool flag = data["line"] > 0;
			if (flag)
			{
				ModelBase<A3_dartModel>.getInstance().init(data["line"]);
			}
			bool flag2 = data["finish"];
			if (flag2)
			{
				a3_liteMinimap expr_5D = a3_liteMinimap.instance;
				if (expr_5D != null)
				{
					expr_5D.getGameObjectByPath("goonDart").SetActive(false);
				}
				this.dartHave = false;
			}
			this.canOpenDart = !data["finish"];
		}

		private void wannaGo(Variant data)
		{
			bool flag = data["line"] > 0;
			if (flag)
			{
				ModelBase<A3_dartModel>.getInstance().init(data["line"]);
			}
			bool flag2 = !this.isme;
			if (flag2)
			{
				flytxt.instance.fly("活动已开启", 0, default(Color), null);
				bool flag3 = GRMap.instance.m_nCurMapID >= 3333;
				if (!flag3)
				{
					this.show2 = true;
					InterfaceMgr.getInstance().open(InterfaceMgr.A3_LEGION_DART, null, false);
					a3_legion_dart expr_97 = a3_legion_dart.instance;
					if (expr_97 != null)
					{
						expr_97.getGameObjectByPath("candodart").SetActive(false);
					}
					a3_legion_dart expr_B3 = a3_legion_dart.instance;
					if (expr_B3 != null)
					{
						expr_B3.getGameObjectByPath("cantdart").SetActive(true);
					}
				}
			}
		}

		private void itemNum(Variant data)
		{
			List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("item.item", "id==" + ModelBase<A3_dartModel>.getInstance().item_id);
			string @string = sXMLList[0].getString("item_name");
			this.dartHave = false;
			MsgBoxMgr.getInstance().showDartGetAwd(ContMgr.getCont("clan_11", new string[]
			{
				data["item_num"]
			}) + @string, ModelBase<A3_dartModel>.getInstance().item_id, data["item_num"], this.per * 1000);
		}

		private void yesHandle()
		{
		}

		private void dartHP(Variant data)
		{
			this.per = data["hp_per"]._int;
			base.dispatchEvent(GameEvent.Create(4u, this, data, false));
			bool flag = !this.gotoDart;
			if (!flag)
			{
				bool flag2 = data.ContainsKey("x");
				if (flag2)
				{
					this.dartHave = true;
					float x = data["x"]._float / 53.3f;
					float z = data["y"]._float / 53.3f;
					bool flag3 = SelfRole._inst != null;
					if (flag3)
					{
						SelfRole.WalkToMap(data["map_id"], new Vector3(x, 2f, z), null, 0.3f);
					}
				}
				bool flag4 = data["hp_per"]._int <= 20 && !this.showOne;
				if (flag4)
				{
					flytxt.instance.fly(ContMgr.getCont("clan_9", null), 0, default(Color), null);
					this.showOne = true;
				}
			}
		}
	}
}
