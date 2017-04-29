using System;

namespace Cross
{
	public class ClassIterator
	{
		protected Variant _iterator;

		public Variant iterator
		{
			get
			{
				return this._iterator;
			}
			set
			{
				this._iterator = value;
			}
		}
	}
}
