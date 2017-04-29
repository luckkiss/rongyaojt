using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MuGame
{
	internal class NpcMgr
	{
		public static NpcMgr instance = new NpcMgr();

		public bool can_touch = true;

		private Dictionary<int, NpcRole> dNpc = new Dictionary<int, NpcRole>();

		private Dictionary<int, Dictionary<int, int>> dicNpcTaskState = new Dictionary<int, Dictionary<int, int>>();

		public NpcMgr()
		{
			ModelBase<A3_TaskModel>.getInstance().addEventListener(A3_TaskModel.ON_NPC_TASK_STATE_CHANGE, new Action<GameEvent>(this.OnNpcTaskStateChange));
		}

		public void addRole(NpcRole npc)
		{
			bool flag = this.dNpc.ContainsKey(npc.id);
			if (!flag)
			{
				this.dNpc[npc.id] = npc;
				this.CheckNpcTaskState(npc);
			}
		}

		public NpcRole getRole(int id)
		{
			bool flag = !this.dNpc.ContainsKey(id);
			NpcRole result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.dNpc[id];
			}
			return result;
		}

		public void clear()
		{
			foreach (NpcRole current in this.dNpc.Values)
			{
				current.dispose();
			}
			this.dNpc.Clear();
		}

		public void SetNpcTaskState(NpcRole npc, NpcTaskState taskState)
		{
			bool flag = npc == null;
			if (!flag)
			{
				int id = npc.id;
				npc.refreshTaskIcon(taskState);
				bool flag2 = taskState > NpcTaskState.NONE;
				if (flag2)
				{
					npc.listTaskId = this.dicNpcTaskState[npc.id].Keys.ToList<int>();
				}
				else
				{
					npc.listTaskId = null;
				}
			}
		}

		private void OnNpcTaskStateChange(GameEvent e)
		{
			Variant data = e.data;
			int num = data["npcId"];
			int num2 = data["taskId"];
			int num3 = data["taskState"];
			bool flag = !this.dicNpcTaskState.ContainsKey(num);
			if (flag)
			{
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				dictionary[num2] = num3;
				this.dicNpcTaskState[num] = dictionary;
			}
			else
			{
				bool flag2 = num3 == 0;
				if (flag2)
				{
					this.dicNpcTaskState[num].Remove(num2);
				}
				else
				{
					this.dicNpcTaskState[num][num2] = num3;
				}
			}
			NpcRole role = this.getRole(num);
			List<int> list = this.dicNpcTaskState[num].Values.ToList<int>();
			int taskState = 0;
			bool flag3 = list.Count > 0;
			if (flag3)
			{
				taskState = list.Max<int>();
			}
			this.SetNpcTaskState(role, (NpcTaskState)taskState);
			TaskData taskDataById = ModelBase<A3_TaskModel>.getInstance().GetTaskDataById(num2);
			bool flag4 = taskDataById != null;
			if (flag4)
			{
				bool topShowOnLiteminimap = taskDataById.topShowOnLiteminimap;
				if (topShowOnLiteminimap)
				{
					a3_liteMinimap expr_11C = a3_liteMinimap.instance;
					if (expr_11C != null)
					{
						expr_11C.SetTopShow(num2);
					}
				}
				else
				{
					a3_liteMinimap expr_130 = a3_liteMinimap.instance;
					if (expr_130 != null)
					{
						expr_130.RefreshTaskPage(num2);
					}
				}
			}
		}

		private void CheckNpcTaskState(NpcRole npc)
		{
			int id = npc.id;
			bool flag = this.dicNpcTaskState.ContainsKey(id);
			if (flag)
			{
				Dictionary<int, int> dictionary = this.dicNpcTaskState[id];
				List<int> list = dictionary.Values.ToList<int>();
				int taskState = 0;
				bool flag2 = list.Count > 0;
				if (flag2)
				{
					taskState = list.Max<int>();
				}
				this.SetNpcTaskState(npc, (NpcTaskState)taskState);
			}
		}
	}
}
