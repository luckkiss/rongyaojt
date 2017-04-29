using System;

namespace GameFramework
{
	public interface IProcess
	{
		bool destroy
		{
			get;
			set;
		}

		bool pause
		{
			get;
			set;
		}

		string processName
		{
			get;
			set;
		}

		void updateProcess(float tmSlice);
	}
}
