using System;

namespace Cross
{
	public interface IGRAvatarPart
	{
		string partId
		{
			get;
		}

		string attachTo
		{
			get;
		}

		string mountTo
		{
			get;
		}

		void load(Variant conf);

		void dispose();
	}
}
