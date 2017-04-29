using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class LGNpc : LGAvatar
	{
		private uint _npcid;

		public uint npcid
		{
			get
			{
				return this._npcid;
			}
			set
			{
				this._npcid = value;
			}
		}

		public override Variant viewInfo
		{
			get
			{
				return null;
			}
		}

		public LGNpc(gameManager m) : base(m)
		{
		}
	}
}
