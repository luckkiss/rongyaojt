using Cross;
using System;
using System.Collections.Generic;

namespace GameFramework
{
	internal class AttAniManager
	{
		private List<AttAni> _attAnis = new List<AttAni>();

		protected UIClient g_mgr;

		public static AttAniManager singleton;

		public AttAniManager(UIClient m)
		{
			AttAniManager.singleton = this;
			this.g_mgr = m;
		}

		public void AddAttAni(AttAni ani)
		{
			bool flag = ani == null;
			if (!flag)
			{
				bool flag2 = this._attAnis.IndexOf(ani) >= 0;
				if (flag2)
				{
					DebugTrace.add(Define.DebugTrace.DTT_ERR, "AddAttAni ani already exist!");
				}
				else
				{
					this._attAnis.Add(ani);
					ani.Start((float)this.g_mgr.g_netM.CurServerTimeStampMS);
				}
			}
		}

		public void RemoveAttAni(AttAni ani)
		{
			int num = this._attAnis.IndexOf(ani);
			bool flag = num >= 0;
			if (flag)
			{
				this._attAnis.RemoveAt(num);
			}
		}

		public void process(float tmSlice)
		{
			bool flag = this._attAnis.Count == 0;
			if (!flag)
			{
				float currTm = (float)this.g_mgr.g_netM.CurServerTimeStampMS;
				for (int i = 0; i < this._attAnis.Count; i++)
				{
					AttAni attAni = this._attAnis[i];
					bool flag2 = attAni != null;
					if (flag2)
					{
						attAni.Update(currTm);
					}
				}
			}
		}
	}
}
