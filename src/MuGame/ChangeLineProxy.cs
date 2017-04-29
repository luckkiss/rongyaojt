using Cross;
using System;

namespace MuGame
{
	internal class ChangeLineProxy : BaseProxy<ChangeLineProxy>
	{
		public int mSelectedLine = 1;

		public ChangeLineProxy()
		{
			this.addProxyListener(7u, new Action<Variant>(this.onChangeLine));
		}

		private void onChangeLine(Variant data)
		{
			int num = data["res"];
			bool flag = num < 0;
			if (flag)
			{
				Globle.err_output(num);
			}
			else
			{
				Variant variant = new Variant();
			}
		}

		public void sendLineProxy(uint index = 1u)
		{
			Variant variant = new Variant();
			variant["idx"] = index;
			this.sendTPKG(7u, variant);
		}
	}
}
