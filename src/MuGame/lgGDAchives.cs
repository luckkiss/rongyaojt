using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class lgGDAchives : lgGDBase
	{
		private InGameGeneralMsgs _genMsg;

		private Variant _achives;

		private LGIUINobility _uiAchive;

		private Variant _activeIds = "";

		private LGIUINobility uiAchive
		{
			get
			{
				bool flag = this._uiAchive == null;
				if (flag)
				{
					this._uiAchive = ((this.g_mgr.g_uiM as muUIClient).getLGUI("nobility") as LGIUINobility);
				}
				return this._uiAchive;
			}
		}

		public lgGDAchives(gameManager m) : base(m)
		{
			this._genMsg = (this.g_mgr.g_netM as muNetCleint).igGenMsg;
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new lgGDAchives(m as gameManager);
		}

		public override void init()
		{
		}

		public Variant GetAchives()
		{
			bool flag = this._achives == null;
			if (flag)
			{
				this._achives = "";
				this._genMsg.GetAchives();
			}
			return this._achives;
		}

		private void removeAchive(uint achid)
		{
			bool flag = this._achives && this._achives.Length > 0;
			if (flag)
			{
				for (int i = 0; i < this._achives.Length; i++)
				{
					Variant variant = this._achives[i];
					bool flag2 = variant["id"] == achid;
					if (flag2)
					{
						this._achives._arr.RemoveAt(i);
						break;
					}
				}
			}
		}

		public void ActiveAchive(uint achid)
		{
		}

		public void OnAddAchive(Variant data)
		{
			this.ActiveAchive(data["achive"]);
			bool flag = this._achives;
			if (flag)
			{
				Variant variant = new Variant();
				variant["id"] = data["achive"];
				bool flag2 = data.ContainsKey("tm");
				if (flag2)
				{
					variant["tm"] = data["tm"];
				}
				this._achives._arr.Add(variant);
				bool flag3 = Convert.ToBoolean(this.uiAchive);
				if (flag3)
				{
					this.uiAchive.AddAchives(variant);
				}
			}
		}

		public void OnGetAchiveRes(Variant data)
		{
			bool flag = data.ContainsKey("achives");
			if (flag)
			{
				this._achives = data["achives"];
				bool flag2 = Convert.ToBoolean(this.uiAchive);
				if (flag2)
				{
					this.uiAchive.AddAchives(this._achives);
				}
			}
			else
			{
				bool flag3 = data.ContainsKey("rmv_achive");
				if (flag3)
				{
					this.removeAchive(data["rmv_achive"]);
					bool flag4 = Convert.ToBoolean(this.uiAchive);
					if (flag4)
					{
						this.uiAchive.RmvAchive(data["rmv_achive"]);
					}
				}
			}
		}

		public void OnActAchiveChange(Variant data)
		{
			int num = data["iid"];
		}

		public uint GetFashionid(uint cid)
		{
			return this._activeIds[cid];
		}

		public void SetFashionid(uint cid, uint id)
		{
			uint num = this._activeIds[cid];
			bool flag = num == id;
			if (!flag)
			{
				this._activeIds[cid] = id;
				Variant fashionAchieves = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetFashionAchieves();
			}
		}

		public bool PosIsWearFashion(uint cid, uint pos, uint subtp = 0u)
		{
			bool flag = this._activeIds[cid];
			bool result;
			if (flag)
			{
				Variant fashionParts = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetFashionParts(this._activeIds[cid]._uint);
				bool flag2 = fashionParts;
				if (flag2)
				{
					foreach (Variant current in fashionParts._arr)
					{
						bool flag3 = (ulong)pos == (ulong)((long)int.Parse(current["pos"]));
						if (flag3)
						{
							bool flag4 = current["subtp"] && (ulong)subtp != (ulong)((long)int.Parse(current["subtp"]));
							if (flag4)
							{
								result = false;
								return result;
							}
							result = true;
							return result;
						}
					}
				}
			}
			result = false;
			return result;
		}

		public bool IsActAchid(uint achid)
		{
			return false;
		}
	}
}
