using Cross;
using System;

namespace MuGame
{
	internal class _graphChaAniSprite : _graphChaSprite
	{
		private IUIImageBox aniBox;

		private int _spaceTm = 0;

		private int _playTm = 0;

		public override float width
		{
			get
			{
				bool flag = this.aniBox != null;
				float width;
				if (flag)
				{
					width = this.aniBox.width;
				}
				else
				{
					width = base.width;
				}
				return width;
			}
		}

		public override float height
		{
			get
			{
				bool flag = this.aniBox != null;
				float height;
				if (flag)
				{
					height = this.aniBox.height;
				}
				else
				{
					height = base.height;
				}
				return height;
			}
		}

		public override IUIBaseControl dispObj
		{
			get
			{
				return this.aniBox;
			}
		}

		public override void initShowInfo(Variant data, Action<_graphChaSprite> cb)
		{
			bool flag = data != null;
			if (flag)
			{
				bool flag2 = data.ContainsKey("sptm") && data["sptm"]._int > 0;
				if (flag2)
				{
					this._spaceTm = data["sptm"]._int;
				}
				else
				{
					this._spaceTm = 0;
				}
				bool flag3 = data.ContainsKey("playtm") && data["playtm"]._int > 0;
				if (flag3)
				{
					this._playTm = data["playtm"]._int;
				}
				else
				{
					this._playTm = 0;
				}
				bool flag4 = data.ContainsKey("width");
				if (flag4)
				{
					this.aniBox.width = data["width"]._float;
				}
				bool flag5 = data.ContainsKey("height");
				if (flag5)
				{
					this.aniBox.height = data["height"]._float;
				}
			}
			bool flag6 = this.aniBox.file != data["res"]._str;
			if (flag6)
			{
				this.aniBox.loadImgFun(delegate(IUIImageBox imgbox)
				{
					bool flag9 = this.aniBox.imageBmp != null;
					if (flag9)
					{
						this.aniBox.imageBmp.loop = true;
					}
				});
				this.aniBox.file = data["res"]._str;
			}
			else
			{
				bool flag7 = this.aniBox.imageBmp != null;
				if (flag7)
				{
					this.aniBox.imageBmp.loop = true;
					this.aniBox.imageBmp.frame = 0;
				}
			}
			bool flag8 = cb != null;
			if (flag8)
			{
				cb(this);
			}
		}

		public override void update(float tmSlice)
		{
			base.update(tmSlice);
		}

		public override void dispose()
		{
			base.dispose();
			bool flag = this.aniBox != null;
			if (flag)
			{
				this.aniBox.dispose();
				this.aniBox = null;
			}
		}
	}
}
