using MuGame;
using System;
using UnityEngine;
using WellFired;

public class PlotMain : MonoBehaviour
{
	public enum ENUM_POLTSTEP
	{
		PLSP_None,
		PLSP_Req_Res,
		PLSP_Wait_Res,
		PLSP_Playing_Plot
	}

	public PlotMain.ENUM_POLTSTEP m_ePlotStep;

	public static PlotMain _inst;

	public USSequencer m_curSequence;

	public bool m_bUntestPlot = true;

	private GameObject m_GamePlotSeq;

	private void Start()
	{
		gameST._bUntestPlot = this.m_bUntestPlot;
		PlotMain._inst = this;
		gameST.REQ_PLOT_RES = new Action<int>(this.Start_Rev_Plot_Res);
		gameST.REQ_PLAY_PLOT = new Action(this.Start_Rev_Play_Plot);
		gameST.REQ_STOP_PLOT = new Action(PlotMain._inst.GamePoltPlayOver);
		UtilsOut.init();
		Debug.Log("剧情模块接入成功");
		if (!this.m_bUntestPlot)
		{
			Debug.Log("剧情编辑模式");
			InterfaceMgr.getInstance().open(InterfaceMgr.PLOT_LINKUI, null, false);
		}
	}

	private void Start_Rev_Plot_Res(int id)
	{
		Debug.Log("请求准备需要加载的剧情资源，剧情ID为" + id);
		this.m_curSequence = null;
		Application.LoadLevelAdditive("p" + id.ToString());
		this.m_ePlotStep = PlotMain.ENUM_POLTSTEP.PLSP_Req_Res;
	}

	private void Start_Rev_Play_Plot()
	{
		if (this.m_ePlotStep == PlotMain.ENUM_POLTSTEP.PLSP_Wait_Res)
		{
			this.m_ePlotStep = PlotMain.ENUM_POLTSTEP.PLSP_Playing_Plot;
			Debug.Log("开启剧情了.................");
			this.m_curSequence.Play();
			this.m_curSequence = null;
			this.m_GamePlotSeq = GameObject.Find("Sequence");
			if (this.m_GamePlotSeq == null)
			{
				Debug.Log("剧情资源错误！！！之后会无法释放..............");
				Application.Quit();
			}
			else
			{
				Debug.Log("成功连接到了资源");
			}
		}
	}

	public void GamePoltPlayOver()
	{
		Debug.Log("剧情结束删除不用的Object ");
		if (this.m_GamePlotSeq == null)
		{
			Debug.Log("资源有错误");
		}
		else
		{
			Debug.Log("成功删除了剧情");
		}
		gameST.REV_ZIMU_TEXT(null);
		UnityEngine.Object.Destroy(this.m_GamePlotSeq);
		this.m_GamePlotSeq = null;
		gameST.REV_PLOT_PLAY_OVER();
	}
}
