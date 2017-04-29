using Cross;
using System;

namespace MuGame
{
	internal class _graphChaSprite
	{
		protected bool _disposed = false;

		public Variant userdata;

		public virtual float width
		{
			get
			{
				return 0f;
			}
		}

		public virtual float height
		{
			get
			{
				return 0f;
			}
		}

		public virtual IUIBaseControl dispObj
		{
			get
			{
				return null;
			}
		}

		public virtual void initShowInfo(Variant data, Action<_graphChaSprite> cb)
		{
		}

		public virtual void update(float tmSlice)
		{
		}

		public virtual void dispose()
		{
			this._disposed = true;
		}
	}
}
