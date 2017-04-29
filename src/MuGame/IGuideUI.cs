using Cross;
using System;

namespace MuGame
{
	public interface IGuideUI
	{
		void InitFrame();

		void SetVisible(bool b);

		void SetPointAt(int x, int y);

		void SetParent(string name);

		void ClearGuide();

		void SetGuideDesc(string desc);

		void AdjustChilds(Variant atts);

		void AdjustTarget(Rect rect, Variant targetConf, string ori);

		void StopGuide();

		void StartGuide();
	}
}
