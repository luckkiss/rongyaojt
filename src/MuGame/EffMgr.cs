using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class EffMgr
	{
		public static EffMgr instance = new EffMgr();

		private List<EffItm> lEff = new List<EffItm>();

		public static Dictionary<int, GameObject> dRuneEff = new Dictionary<int, GameObject>();

		private static SXML runeXml;

		private TickItem tick;

		public static GameObject getRuneEff(int tuneid)
		{
			bool flag = EffMgr.runeXml == null;
			if (flag)
			{
				EffMgr.runeXml = XMLMgr.instance.GetSXML("rune", "");
			}
			bool flag2 = !EffMgr.dRuneEff.ContainsKey(tuneid);
			GameObject result;
			if (flag2)
			{
				SXML node = EffMgr.runeXml.GetNode("rune", "id==" + tuneid);
				bool flag3 = node == null;
				if (flag3)
				{
					result = null;
					return result;
				}
				string @string = node.getString("eff");
				bool flag4 = @string == "null";
				if (flag4)
				{
					EffMgr.dRuneEff[tuneid] = null;
				}
				else
				{
					EffMgr.dRuneEff[tuneid] = Resources.Load<GameObject>("FX/comFX/fuwenFX/" + @string);
				}
			}
			result = EffMgr.dRuneEff[tuneid];
			return result;
		}

		public EffMgr()
		{
			this.tick = new TickItem(new Action<float>(this.onUpdate));
			TickMgr.instance.addTick(this.tick);
		}

		public void addEff(BaseRole frm, BaseRole to, GameObject eff, float sec)
		{
			EffItm effItm = new EffItm();
			effItm.to = to;
			effItm.frm = frm;
			effItm.eff = eff;
			effItm.sec = sec;
			eff.transform.SetParent(U3DAPI.FX_POOL_TF, false);
			this.lEff.Add(effItm);
		}

		public void removeEff(EffItm itm)
		{
			this.lEff.Remove(itm);
			itm.dispose();
		}

		private void onUpdate(float s)
		{
			List<EffItm> list = null;
			foreach (EffItm current in this.lEff)
			{
				current.update(s);
				bool flag = current.sec < 0f;
				if (flag)
				{
					bool flag2 = list == null;
					if (flag2)
					{
						list = new List<EffItm>();
					}
					list.Add(current);
				}
			}
			bool flag3 = list != null;
			if (flag3)
			{
				foreach (EffItm current2 in list)
				{
					this.removeEff(current2);
				}
			}
		}
	}
}
