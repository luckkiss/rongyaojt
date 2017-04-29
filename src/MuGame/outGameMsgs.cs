using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class outGameMsgs : MsgProcduresBase
	{
		public static outGameMsgs instance;

		public outGameMsgs(IClientBase m) : base(m)
		{
			outGameMsgs.instance = this;
		}

		public static outGameMsgs create(IClientBase m)
		{
			return new outGameMsgs(m);
		}

		public override void init()
		{
			SessionFuncMgr.instance.addFunc(5u, new Action<Variant>(this.onCreat), false);
			SessionFuncMgr.instance.addFunc(4u, new Action<Variant>(this.oDelChar), false);
			SessionFuncMgr.instance.addFunc(51u, new Action<Variant>(this.onShowOutSvr), false);
			SessionFuncMgr.instance.addFunc(20u, new Action<Variant>(this.onLoginLine), false);
		}

		public void selectCha(uint cid)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			base.sendTpkg(3u, variant);
		}

		public void deleteCha(uint cid)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			base.sendTpkg(4u, variant);
		}

		public void onCreat(Variant data)
		{
			SceneCamera.m_isFirstLogin = true;
			NetClient.instance.dispatchEvent(GameEvent.Create(2005u, this, data, false));
		}

		public void oDelChar(Variant data)
		{
			NetClient.instance.dispatchEvent(GameEvent.Create(2004u, this, data, false));
		}

		public void onLoginLine(Variant data)
		{
			debug.Log("排队登入信息==" + data.dump());
			bool flag = login.instance != null;
			if (flag)
			{
				int @int = data._int;
				string cont = ContMgr.getCont("sever_list1", new List<string>
				{
					@int.ToString()
				});
				bool flag2 = @int > 1000;
				string cont2;
				if (flag2)
				{
					cont2 = ContMgr.getCont("sever_list2", null);
				}
				else
				{
					double num = Math.Ceiling((double)@int / 50.0);
					cont2 = ContMgr.getCont("sever_list3", new List<string>
					{
						num.ToString()
					});
				}
				login.instance.setWaitingTxt(cont + cont2);
			}
		}

		public void onShowOutSvr(Variant data)
		{
			bool flag = broadcasting.instance != null;
			if (flag)
			{
				broadcasting.instance.addMsg(data["msg"]);
			}
			data["tp"] = 6;
			bool flag2 = a3_chatroom._instance != null;
			if (flag2)
			{
				a3_chatroom._instance.otherSays(data);
			}
		}

		public void createCha(string name, uint carr, uint sex)
		{
			Variant variant = new Variant();
			variant["name"] = name;
			variant["carr"] = carr;
			variant["sex"] = sex;
			base.sendTpkg(5u, variant);
		}

		public void getWorldCount()
		{
			base.sendTpkg(6u, null);
		}
	}
}
