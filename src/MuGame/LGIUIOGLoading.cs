using System;

namespace MuGame
{
	public interface LGIUIOGLoading
	{
		void show();

		void clearAll();

		void setProgressBar(uint bytesLoaded, uint bytesTotal);

		string getProgressPercent();

		void setTipInfo(string info);
	}
}
