using System;
using UnityEngine;

namespace MuGame
{
	public interface INameObj
	{
		string roleName
		{
			get;
			set;
		}

		int curhp
		{
			get;
			set;
		}

		int maxHp
		{
			get;
			set;
		}

		int title_id
		{
			get;
			set;
		}

		bool isactive
		{
			get;
			set;
		}

		int rednm
		{
			get;
			set;
		}

		uint hidbacktime
		{
			get;
			set;
		}

		Vector3 getHeadPos();
	}
}
