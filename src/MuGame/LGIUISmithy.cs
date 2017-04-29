using Cross;
using System;

namespace MuGame
{
	public interface LGIUISmithy
	{
		void ForgeBack(int type, bool succ);

		void DecompSucc();

		void DecompFail();

		void AppraisalSucc(Variant data);
	}
}
