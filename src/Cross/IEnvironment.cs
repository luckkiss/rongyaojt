using System;
using System.Collections.Generic;

namespace Cross
{
	public interface IEnvironment
	{
		bool displayfog
		{
			get;
			set;
		}

		float fogden
		{
			get;
			set;
		}

		Vec4 fogcolor
		{
			get;
			set;
		}

		string fogmode
		{
			get;
			set;
		}

		float strdistance
		{
			get;
			set;
		}

		float enddistance
		{
			get;
			set;
		}

		Vec4 ambcolor
		{
			get;
			set;
		}

		List<string> lightmap
		{
			get;
			set;
		}

		string lightprobes
		{
			get;
			set;
		}

		string skybox
		{
			get;
			set;
		}
	}
}
