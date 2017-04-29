using Cross;
using System;

namespace MuGame
{
	internal class ShowStruct
	{
		protected IUIBaseControl _disp;

		protected IUIContainer _parent;

		protected Variant _userdata;

		protected bool _isadd = false;

		public Variant userdata
		{
			get
			{
				return this._userdata;
			}
			set
			{
				this._userdata = value;
			}
		}

		public IUIBaseControl disp
		{
			get
			{
				return this._disp;
			}
		}

		public virtual bool Update()
		{
			return false;
		}
	}
}
