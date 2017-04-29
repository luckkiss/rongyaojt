using System;
using UnityEngine.UI;

namespace MuGame
{
	internal class TaskOptElement
	{
		public int taskId;

		public bool isTaskMonsterAlive;

		public bool isKeepingKillMon;

		private Text _liteMinimapTaskTimer;

		public Text liteMinimapTaskTimer
		{
			get
			{
				bool flag = this._liteMinimapTaskTimer == null;
				if (flag)
				{
					this._liteMinimapTaskTimer = a3_liteMinimap.instance.GetTaskPage(this.taskId).transform.FindChild("name/timer").GetComponent<Text>();
				}
				return this._liteMinimapTaskTimer;
			}
			set
			{
				this._liteMinimapTaskTimer = value;
			}
		}

		public TaskOptElement()
		{
		}

		public TaskOptElement(int taskId, bool? isTaskMonsterAlive = null, bool? isKeepingKillMon = null)
		{
			this.taskId = taskId;
			this.isTaskMonsterAlive = isTaskMonsterAlive.GetValueOrDefault(this.isTaskMonsterAlive);
			this.isKeepingKillMon = isKeepingKillMon.GetValueOrDefault(this.isKeepingKillMon);
		}

		public void InitUi(Text liteMinimapTaskTimer = null)
		{
			this.liteMinimapTaskTimer = liteMinimapTaskTimer;
		}

		public void Set(bool? isTaskMonsterAlive = null, bool? isKeepingKillMon = null, long? timeKillTerminal = null)
		{
			this.isTaskMonsterAlive = isTaskMonsterAlive.GetValueOrDefault(this.isTaskMonsterAlive);
			this.isKeepingKillMon = isKeepingKillMon.GetValueOrDefault(this.isKeepingKillMon);
		}
	}
}
