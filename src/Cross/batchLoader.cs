using System;
using System.Collections.Generic;

namespace Cross
{
	public class batchLoader<LoadFuncT>
	{
		public LoadFuncT loadFunc;

		public List<string> assetsFileVec;

		public List<string> assetsLoaded;

		public List<string> assetsFailed;

		public batchLoader(List<string> afs, LoadFuncT lder)
		{
			this.assetsFileVec = afs;
			this.loadFunc = lder;
			this.assetsLoaded = new List<string>();
			this.assetsFailed = new List<string>();
		}

		public void loadNext(Action<List<string>, List<string>> onFin)
		{
			bool flag = this.assetsFileVec.Count <= 0;
			if (flag)
			{
				onFin(this.assetsLoaded, this.assetsFailed);
			}
			else
			{
				string curFile = ArrayUtil.arrayPop<string>(this.assetsFileVec);
				(this.loadFunc as Action<string, Action>)(curFile, delegate
				{
					this.assetsLoaded.Add(curFile);
					this.loadNext(onFin);
				});
			}
		}

		public void loadNextWithOnFail(Action<List<string>, List<string>> onFin)
		{
			bool flag = this.assetsFileVec.Count <= 0;
			if (flag)
			{
				onFin(this.assetsLoaded, this.assetsFailed);
			}
			else
			{
				string curFile = ArrayUtil.arrayPop<string>(this.assetsFileVec);
				(this.loadFunc as Action<string, Action, Action>)(curFile, delegate
				{
					this.assetsLoaded.Add(curFile);
					this.loadNextWithOnFail(onFin);
				}, delegate
				{
					this.assetsFailed.Add(curFile);
					this.loadNextWithOnFail(onFin);
				});
			}
		}
	}
}
