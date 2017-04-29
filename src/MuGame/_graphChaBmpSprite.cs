using Cross;
using System;

namespace MuGame
{
	internal class _graphChaBmpSprite : _graphChaSprite
	{
		private IUIImageBox bmp;

		public override IUIBaseControl dispObj
		{
			get
			{
				return this.bmp;
			}
		}

		public override void initShowInfo(Variant data, Action<_graphChaSprite> cb)
		{
			bool flag = data.ContainsKey("width");
			if (flag)
			{
				this.bmp.width = data["width"]._float;
			}
			bool flag2 = data.ContainsKey("height");
			if (flag2)
			{
				this.bmp.height = data["height"]._float;
			}
			bool flag3 = data.ContainsKey("file");
			if (flag3)
			{
				this.bmp.file = data["file"];
			}
			else
			{
				bool flag4 = data.ContainsKey("res");
				if (flag4)
				{
					this.bmp.file = data["res"];
				}
			}
			this.bmp.createUIObject(null);
			bool flag5 = cb != null;
			if (flag5)
			{
				cb(this);
			}
		}

		public override void dispose()
		{
			base.dispose();
			bool flag = this.bmp != null;
			if (flag)
			{
				this.bmp.dispose();
				this.bmp = null;
			}
		}
	}
}
