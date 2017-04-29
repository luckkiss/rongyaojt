using System;

namespace GameFramework
{
	public class LGAvatarBase : lgGDBase
	{
		public static int ROLE_TYPE_ROLE = 0;

		public static int ROLE_TYPE_MONSTER = 1;

		public static int ROLE_TYPE_PLAYER = 2;

		public static int ROLE_TYPE_USER = 3;

		public static int ROLE_TYPE_HERO = 4;

		protected int _roletype = LGAvatarBase.ROLE_TYPE_ROLE;

		protected float _ori = 0f;

		public virtual int roleType
		{
			get
			{
				return this._roletype;
			}
		}

		public virtual float x
		{
			get;
			set;
		}

		public virtual float y
		{
			get;
			set;
		}

		public virtual float lg_ori_angle
		{
			get;
			set;
		}

		public virtual uint octOri
		{
			get
			{
				return (uint)(((double)this._ori + 1.5707963267948966) / 0.78539816339744828);
			}
			set
			{
				this._ori = (float)(value * 0.78539816339744828 - 1.5707963267948966);
			}
		}

		public LGAvatarBase(gameManager m) : base(m)
		{
		}
	}
}
