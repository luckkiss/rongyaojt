using Cross;
using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	public class charsInfo : LGDataBase
	{
		public int g_gm = 0;

		public bool g_safe = false;

		private Variant _chas;

		public charsInfo(muNetCleint m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new charsInfo(m as muNetCleint);
		}

		public override void init()
		{
			base.g_mgr.addEventListener(3021u, new Action<GameEvent>(this.onLogin));
			base.g_mgr.addEventListener(2004u, new Action<GameEvent>(this.onDeleteChar));
			base.g_mgr.addEventListener(2005u, new Action<GameEvent>(this.onCreatChar));
		}

		private void onCreatChar(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data["res"]._int > 0;
			if (flag)
			{
				this._chas._arr.Add(data["cha"]);
			}
			base.dispatchEvent(GameEvent.Create(4031u, this, data, false));
		}

		private void onDeleteChar(GameEvent e)
		{
			bool flag = e.data["res"] < 0;
			if (flag)
			{
				Globle.err_output(e.data["res"]);
			}
			else
			{
				flytxt.instance.fly(ContMgr.getCont("role_delete", null), 0, default(Color), null);
			}
			Variant data = e.data;
			bool flag2 = data["res"]._int > 0;
			if (flag2)
			{
				uint @uint = data["cid"]._uint;
				for (int i = 0; i < this._chas.Count; i++)
				{
					bool flag3 = this._chas[i].ContainsKey("cid") && this._chas[i]["cid"]._uint == @uint;
					if (flag3)
					{
						this._chas._arr.RemoveAt(i);
					}
				}
				base.dispatchEvent(GameEvent.Create(4032u, this, data, false));
			}
		}

		private void onSelectChar(Variant data)
		{
		}

		private void onClose(Variant data)
		{
		}

		private void onConnectFailed(Variant data)
		{
		}

		public Variant getChas()
		{
			return this._chas;
		}

		private void onLogin(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data["res"]._int < 0;
			if (!flag)
			{
				this._chas = data["chas"];
				base.dispatchEvent(GameEvent.Create(3021u, this, null, false));
			}
		}
	}
}
