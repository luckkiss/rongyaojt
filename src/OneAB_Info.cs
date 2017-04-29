using System;
using System.Collections.Generic;

public class OneAB_Info
{
	public OUTASSETS_LOAD_STEP m_eStep = OUTASSETS_LOAD_STEP.OALS_NONE;

	public object m_LoadedObj = null;

	public List<Action<OneAB_Info>> m_onFins = new List<Action<OneAB_Info>>();

	public List<Action<OneAB_Info, float>> m_onProgs = new List<Action<OneAB_Info, float>>();

	public List<Action<OneAB_Info, string>> m_onFails = new List<Action<OneAB_Info, string>>();

	private void ClearCallBack()
	{
		this.m_onFins.Clear();
		this.m_onFins = null;
		this.m_onProgs.Clear();
		this.m_onProgs = null;
		this.m_onFails.Clear();
		this.m_onFails = null;
	}

	public void CallFin(object data)
	{
		this.m_LoadedObj = data;
		this.m_eStep = OUTASSETS_LOAD_STEP.OALS_LOADED;
		bool flag = this.m_onFins != null;
		if (flag)
		{
			foreach (Action<OneAB_Info> current in this.m_onFins)
			{
				bool flag2 = current != null;
				if (flag2)
				{
					current(this);
				}
			}
		}
		this.ClearCallBack();
	}

	public void CallProg(float per)
	{
		bool flag = this.m_onProgs != null;
		if (flag)
		{
			foreach (Action<OneAB_Info, float> current in this.m_onProgs)
			{
				bool flag2 = current != null;
				if (flag2)
				{
					current(this, per);
				}
			}
		}
	}

	public void CallFail(string err)
	{
		bool flag = this.m_onProgs != null;
		if (flag)
		{
			foreach (Action<OneAB_Info, string> current in this.m_onFails)
			{
				bool flag2 = current != null;
				if (flag2)
				{
					current(this, err);
				}
			}
		}
		this.ClearCallBack();
	}
}
