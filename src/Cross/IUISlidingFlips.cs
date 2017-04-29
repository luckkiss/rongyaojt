using System;

namespace Cross
{
	public interface IUISlidingFlips : IUIContainer, IUIBaseControl
	{
		int page
		{
			set;
		}

		bool isRep
		{
			set;
		}

		bool canTween
		{
			set;
		}

		int speedDecay
		{
			set;
		}

		Action onFin
		{
			set;
		}

		int currentPage
		{
			get;
		}

		void closeFlips();
	}
}
