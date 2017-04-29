using Cross;
using System;

namespace MuGame
{
	internal class _graphChaTxtSprite : _graphChaSprite
	{
		protected IUIText _textBitmap;

		protected Variant _normalFmt = null;

		public override IUIBaseControl dispObj
		{
			get
			{
				return this._textBitmap;
			}
		}

		public override float width
		{
			get
			{
				bool flag = this._textBitmap != null;
				float width;
				if (flag)
				{
					width = this._textBitmap.width;
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
				bool flag = this._textBitmap != null;
				float height;
				if (flag)
				{
					height = this._textBitmap.height;
				}
				else
				{
					height = base.height;
				}
				return height;
			}
		}

		public override void initShowInfo(Variant data, Action<_graphChaSprite> cb)
		{
			this._normalFmt = data["fmt"];
			bool flag = this._normalFmt != null;
			if (flag)
			{
				this._textBitmap.text = data["text"];
				bool flag2 = this._normalFmt.ContainsKey("color");
				if (flag2)
				{
					this._textBitmap.color = this._normalFmt["color"];
				}
				bool flag3 = this._normalFmt.ContainsKey("size");
				if (flag3)
				{
					this._textBitmap.fontSize = this._normalFmt["size"]._int;
				}
			}
			Style2D style2D = data["style"]._val as Style2D;
			bool flag4 = style2D != null;
			if (flag4)
			{
				this._textBitmap.align = style2D;
			}
			this._textBitmap.visible = (!data.ContainsKey("visible") || data["visible"]._bool);
			bool flag5 = cb != null;
			if (flag5)
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
			bool flag = this._textBitmap != null;
			if (flag)
			{
				this._textBitmap.dispose();
				this._textBitmap = null;
			}
		}

		public void setText(string str)
		{
			this._textBitmap.text = str;
		}
	}
}
