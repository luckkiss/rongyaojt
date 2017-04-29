using Cross;
using System;

namespace MuGame
{
	public interface LGIUIMainuiAttach
	{
		void CloseTargetInfo();

		void UpdateTargetInfo();

		void TargetStateChange();

		void SetLoginAwd();

		void OpenCombptUp(Variant data = null);

		void SetCurSelectName(string name, int cid);
	}
}
