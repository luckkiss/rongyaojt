using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class lgGDDmis : lgGDBase
	{
		private Variant _dmisData = new Variant();

		public const int IS_PROG = 1;

		public const int IS_FIN = 2;

		private InGameDmisMsgs _dmisMsg
		{
			get
			{
				return (this.g_mgr.g_netM as muNetCleint).igDmisMsgs;
			}
		}

		private LGIUIWelfare uiDmis
		{
			get
			{
				return this.g_mgr.g_uiM.getLGUI("welfare") as LGIUIWelfare;
			}
		}

		public lgGDDmis(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new lgGDDmis(m as gameManager);
		}

		public override void init()
		{
		}

		private void onDmisRes(GameEvent e)
		{
			Variant data = e.data;
			this.DmisRes(data);
		}

		public void GetDmis()
		{
			Variant variant = new Variant();
			variant["tp"] = 1;
			this._dmisMsg.GetDmis(variant);
		}

		public void GetDmisPrize(int id)
		{
			Variant variant = new Variant();
			variant["tp"] = 2;
			variant["awdid"] = id;
			this._dmisMsg.GetDmis(variant);
		}

		public Variant GetDmisData()
		{
			return this._dmisData;
		}

		public int GetDmisPoint()
		{
			return this._dmisData.ContainsKey("pt") ? this._dmisData["pt"]._int : 0;
		}

		public bool IsGetPrize(int id)
		{
			bool result;
			using (List<Variant>.Enumerator enumerator = this._dmisData["gotawd"]._arr.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int num = enumerator.Current;
					bool flag = num == id;
					if (flag)
					{
						result = true;
						return result;
					}
				}
			}
			result = false;
			return result;
		}

		public void DmisRes(Variant data)
		{
			uint @uint = data["tp"]._uint;
			bool flag = 1u == @uint;
			if (flag)
			{
				bool flag2 = false;
				Variant variant = this._dmisData["prog"];
				bool flag3 = variant;
				if (flag3)
				{
					foreach (Variant current in variant._arr)
					{
						bool flag4 = current["id"] == data["id"];
						if (flag4)
						{
							current["cnt"] = data["cnt"];
							flag2 = true;
							break;
						}
					}
					bool flag5 = !flag2;
					if (flag5)
					{
						Variant variant2 = new Variant();
						variant2["id"] = data["id"];
						variant2["cnt"] = data["cnt"];
						this._dmisData["prog"]._arr.Add(variant2);
					}
					this._dmisData["pt"] = data["pt"];
					bool flag6 = this.uiDmis != null;
					if (flag6)
					{
						this.uiDmis.DmisChange(1, data);
					}
				}
			}
			else
			{
				bool flag7 = 2u == @uint;
				if (flag7)
				{
					Variant variant3 = this._dmisData["acp_ids"];
					Variant variant4 = this._dmisData["fin_ids"];
					Variant variant5 = this._dmisData["prog"];
					bool flag8 = variant3 && variant4;
					if (flag8)
					{
						int num = variant3["indexOf"][data["id"]];
						bool flag9 = num > -1;
						if (flag9)
						{
							variant3._arr.RemoveAt(num);
						}
						variant4._arr.Add(data["id"]);
						bool flag10 = variant5 && variant5.Length > 0;
						if (flag10)
						{
							for (int i = 0; i < variant5.Length; i++)
							{
								bool flag11 = data["id"] == variant5[i]["id"];
								if (flag11)
								{
									variant5._arr.RemoveAt(i);
									break;
								}
							}
						}
						this._dmisData["pt"] = data["pt"];
						bool flag12 = this.uiDmis != null;
						if (flag12)
						{
							this.uiDmis.DmisChange(2, data);
						}
					}
				}
				else
				{
					bool flag13 = 3u == @uint;
					if (flag13)
					{
						this._dmisData = new Variant();
						foreach (string current2 in data.Keys)
						{
							this._dmisData[current2] = data[current2];
						}
						bool flag14 = this.uiDmis != null;
						if (flag14)
						{
							this.uiDmis.SetDmisInfo(data);
						}
					}
					else
					{
						bool flag15 = 4u == @uint;
						if (flag15)
						{
							Variant variant6 = this._dmisData["gotawd"];
							bool flag16 = variant6;
							if (flag16)
							{
								variant6._arr.Add(data["awdid"]);
							}
							bool flag17 = this.uiDmis != null;
							if (flag17)
							{
								this.uiDmis.GetDmisRes(data["awdid"]);
							}
						}
						else
						{
							bool flag18 = 5u == @uint;
							if (flag18)
							{
								bool flag19 = this._dmisData["awd_ids"];
								if (flag19)
								{
									this._dmisData["awd_ids"]._arr.Add(data["id"]);
								}
							}
						}
					}
				}
			}
		}
	}
}
