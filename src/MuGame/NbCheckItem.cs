using System;

namespace MuGame
{
	public class NbCheckItem
	{
		public NbCheckItems.boolDelegate _chekAction;

		public string[] _pram;

		public NbCheckItem(NbCheckItems.boolDelegate chekAction, string[] pram)
		{
			this._chekAction = chekAction;
			this._pram = pram;
		}

		public bool doit()
		{
			return this._chekAction(this._pram);
		}
	}
}
