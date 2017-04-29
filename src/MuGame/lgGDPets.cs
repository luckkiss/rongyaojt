using GameFramework;
using System;

namespace MuGame
{
	public class lgGDPets : lgGDBase
	{
		public lgGDPets(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new lgGDPets(m as gameManager);
		}

		public override void init()
		{
		}
	}
}
