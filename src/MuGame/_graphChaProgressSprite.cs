using Cross;
using System;

namespace MuGame
{
	internal class _graphChaProgressSprite : _graphChaSprite
	{
		private IUIProgressBar progressBar;

		public float maxNum
		{
			get
			{
				return this.progressBar.maxNum;
			}
			set
			{
				this.progressBar.maxNum = value;
			}
		}

		public float num
		{
			get
			{
				return this.progressBar.value;
			}
			set
			{
				this.progressBar.value = value;
			}
		}

		public override IUIBaseControl dispObj
		{
			get
			{
				return this.progressBar;
			}
		}

		public override void initShowInfo(Variant data, Action<_graphChaSprite> cb)
		{
			bool flag = data.ContainsKey("width");
			if (flag)
			{
				this.progressBar.width = data["width"]._float;
			}
			bool flag2 = data.ContainsKey("height");
			if (flag2)
			{
				this.progressBar.height = data["height"]._float;
			}
			bool flag3 = data.ContainsKey("res");
			if (flag3)
			{
				this.progressBar.file = data["res"];
				this.progressBar.createUIObject(null);
			}
			bool flag4 = cb != null;
			if (flag4)
			{
				cb(this);
			}
		}

		public override void dispose()
		{
			base.dispose();
			bool flag = this.progressBar != null;
			if (flag)
			{
				this.progressBar.dispose();
				this.progressBar = null;
			}
		}
	}
}
