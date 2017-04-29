using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class LGGDPVP : lgGDBase
	{
		private Variant _worshipData;

		private Variant _logs;

		private Action<Variant> _worshipCall;

		public LGGDPVP(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGGDPVP(m as gameManager);
		}

		public override void init()
		{
		}

		public void GetWorship()
		{
		}

		public void GetWorshipLog()
		{
		}

		public void ToWorship(uint wsid)
		{
		}

		public void OnWorshipRes(Variant data)
		{
		}

		public Variant GetWorshipInfo(Action<Variant> onfin = null)
		{
			bool flag = this._worshipData == null;
			if (flag)
			{
				bool flag2 = onfin != null;
				if (flag2)
				{
					this._worshipCall = onfin;
				}
				this.GetWorship();
			}
			else
			{
				bool flag3 = onfin != null;
				if (flag3)
				{
					onfin(this._worshipData);
				}
			}
			return this._worshipData;
		}
	}
}
