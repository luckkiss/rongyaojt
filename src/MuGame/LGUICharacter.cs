using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class LGUICharacter : BaseLGUI
	{
		protected Variant _selfDetailInfo;

		protected uint _radioIdx = 0u;

		private bool _initShow = false;

		protected int _curIndex = -1;

		private Variant tData = null;

		private Variant adddata = null;

		private Variant rmvdata = null;

		public LGUICharacter(muUIClient m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGUICharacter(m as muUIClient);
		}

		public override void init()
		{
		}

		protected override void _regEventListener()
		{
		}

		protected override void _unRegEventListener()
		{
		}

		protected override void onOpen(Variant data)
		{
			base.onOpen(data);
		}

		public void DoubleClkEquipTile(GameEvent e)
		{
			Variant data = e.data;
			this.change_eqp(data["data"]["id"]._uint);
		}

		protected void change_eqp(uint id)
		{
		}

		public void SelfDetailInfoChange(Variant info, string type)
		{
			using (List<Variant>.Enumerator enumerator = info._arr.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string key = enumerator.Current;
					this._selfDetailInfo[key] = info[key];
				}
			}
			Variant variant = new Variant();
			variant["radioIdx"] = this._radioIdx;
			bool flag = this.shouldSetChaInfo(variant);
			if (flag)
			{
				bool flag2 = type == "hp" || type == "mp" || type == "dp" || type == "pk";
				if (flag2)
				{
					Variant variant2 = new Variant();
					variant2["type"] = type;
					variant2["info"] = info;
					base.dispatchEvent(GameEvent.Create(5001u, this, variant2, false));
				}
			}
		}

		public bool shouldSetChaInfo(Variant data)
		{
			bool flag = !base.isOpen;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = data != null;
				if (flag2)
				{
					bool flag3 = data.ContainsKey("radioIdx");
					if (flag3)
					{
						bool flag4 = data["radioIdx"]._int > 1;
						if (flag4)
						{
							result = false;
							return result;
						}
					}
					bool flag5 = (long)data["radioIdx"]._int != (long)((ulong)this._radioIdx);
					if (flag5)
					{
						result = false;
						return result;
					}
				}
				result = true;
			}
			return result;
		}

		public void SelfAddEquip(Variant eqps)
		{
			bool initShow = this._initShow;
			if (initShow)
			{
				base.dispatchEvent(GameEvent.Create(5002u, this, eqps, false));
			}
			bool flag = this.shouldSetChaInfo(this.adddata);
			if (flag)
			{
				this.setEqpData(eqps, true);
			}
			this.addSelfEqp(eqps);
		}

		private void addSelfEqp(Variant eqps)
		{
			bool isOpen = base.isOpen;
			if (isOpen)
			{
				this.showWingPos();
			}
		}

		protected void showWingPos()
		{
		}

		public void SelfRmvEquip(Variant eqps)
		{
			bool initShow = this._initShow;
			if (initShow)
			{
				base.dispatchEvent(GameEvent.Create(5003u, this, eqps, false));
			}
			bool flag = this.shouldSetChaInfo(this.rmvdata);
			if (flag)
			{
				this.setEqpData(eqps, false);
			}
			this.selfRemoveEquip(eqps);
		}

		private void selfRemoveEquip(Variant eqps)
		{
			bool isOpen = base.isOpen;
			if (isOpen)
			{
				bool flag = this._curIndex == 3;
				if (flag)
				{
					foreach (Variant current in eqps._arr)
					{
						bool flag2 = current["id"]._int == this.tData["id"]._int;
						if (flag2)
						{
							bool flag3 = this.tData["compose"] != null;
							if (flag3)
							{
							}
							break;
						}
					}
				}
				this.showWingPos();
			}
		}

		protected void setEqpData(Variant eqps, bool isAdd)
		{
			foreach (Variant current in eqps._arr)
			{
				Variant variant = (this.g_mgr.g_gameConfM as muCLientConfig).svrItemConf.get_item_conf(current["tpid"]._uint);
				bool flag = variant != null;
				if (flag)
				{
					uint @uint = variant["conf"]["pos"]._uint;
				}
			}
		}
	}
}
