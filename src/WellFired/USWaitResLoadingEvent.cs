using MuGame;
using System;
using UnityEngine;

namespace WellFired
{
	[USequencerEvent("Qsmy/WaitResLoading"), USequencerFriendlyName("Wait Res Loading")]
	public class USWaitResLoadingEvent : USEventBase
	{
		public USSequencer sequence;

		public override void FireEvent()
		{
			if (PlotMain._inst && PlotMain._inst.m_bUntestPlot)
			{
				Debug.Log("��Դ�ļ���������ȫ���ͣ���ֹͣ����");
				if (!this.sequence)
				{
					Debug.LogWarning("No sequence for USPauseSequenceEvent : " + base.name, this);
				}
				if (this.sequence)
				{
					this.sequence.Pause();
				}
				PlotMain._inst.m_ePlotStep = PlotMain.ENUM_POLTSTEP.PLSP_Wait_Res;
				PlotMain._inst.m_curSequence = this.sequence;
				gameST.REV_RES_LIST_OK();
			}
			else if (this.sequence != null)
			{
				this.sequence.Pause();
			}
		}

		public override void ProcessEvent(float deltaTime)
		{
		}
	}
}
