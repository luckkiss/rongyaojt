using Cross;
using System;
using System.Collections.Generic;

public static class OutAssetsMgr
{
	private static Dictionary<string, OneAB_Info> m_mapAssetBundle = new Dictionary<string, OneAB_Info>();

	public static void LoadRes(string path, Action<OneAB_Info> fin, Action<OneAB_Info, float> prog, Action<OneAB_Info, string> fail)
	{
		OneAB_Info ab_info = null;
		bool flag = !OutAssetsMgr.m_mapAssetBundle.ContainsKey(path);
		if (flag)
		{
			ab_info = new OneAB_Info();
			OutAssetsMgr.m_mapAssetBundle.Add(path, ab_info);
		}
		else
		{
			ab_info = OutAssetsMgr.m_mapAssetBundle[path];
		}
		bool flag2 = ab_info.m_eStep == OUTASSETS_LOAD_STEP.OALS_LOADED;
		if (flag2)
		{
			fin(ab_info);
		}
		else
		{
			bool flag3 = ab_info.m_eStep == OUTASSETS_LOAD_STEP.OALS_FAILED;
			if (flag3)
			{
				fail(ab_info, "");
			}
			else
			{
				ab_info.m_onFins.Add(fin);
				ab_info.m_onProgs.Add(prog);
				ab_info.m_onFails.Add(fail);
				bool flag4 = ab_info.m_eStep == OUTASSETS_LOAD_STEP.OALS_NONE;
				if (flag4)
				{
					ab_info.m_eStep = OUTASSETS_LOAD_STEP.OALS_LOADING;
					new URLReqImpl
					{
						dataFormat = "assetbundle",
						url = path
					}.load(delegate(IURLReq r, object data)
					{
						ab_info.CallFin(data);
					}, delegate(IURLReq r, float progress)
					{
						ab_info.CallProg(progress);
					}, delegate(IURLReq r, string err)
					{
						ab_info.CallFail(err);
					});
				}
			}
		}
	}
}
