using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class A3_TaskProxy : BaseProxy<A3_TaskProxy>
	{
		private uint GET_TASK_INFO = 1u;

		private uint GET_TASK_AWARD = 2u;

		private uint TASK_DIALOG = 3u;

		private uint ACCEPT_TASK = 4u;

		public uint COLLECT_TARGET = 5u;

		private uint ONEKEY_FINISH = 6u;

		private uint UPGRADE_STAR = 7u;

		private uint SUBMIT_ITEM = 8u;

		private uint WAIT_START = 9u;

		private uint WAIT_END = 10u;

		private uint CALL_MONSTER = 11u;

		public const uint ON_SYNC_TASK = 0u;

		public const uint ON_GET_TASK_AWARD = 1u;

		public const uint ON_GET_NEW_TASK = 2u;

		public const uint ON_TASK_REFRESH = 3u;

		public const uint ON_TASK_STAR_CHANGE = 4u;

		public A3_TaskProxy()
		{
			this.addProxyListener(110u, new Action<Variant>(this.OnTask));
		}

		public void SendGetTask()
		{
			Variant variant = new Variant();
			variant["mis_cmd"] = this.GET_TASK_INFO;
			this.sendRPC(110u, variant);
		}

		public void SendGetAward(int rate = 0)
		{
			int taskId = ModelBase<A3_TaskModel>.getInstance().curTask.taskId;
			debug.Log("完成任务ID::" + taskId);
			Variant variant = new Variant();
			variant["mis_cmd"] = this.GET_TASK_AWARD;
			variant["id"] = taskId;
			bool flag = rate > 1;
			if (flag)
			{
				variant["double_exp"] = true;
			}
			this.sendRPC(110u, variant);
		}

		public void SendTalkWithNpc(int npcId)
		{
			debug.Log("发送对话ID::" + npcId);
			Variant variant = new Variant();
			variant["mis_cmd"] = this.TASK_DIALOG;
			variant["npcid"] = npcId;
			this.sendRPC(110u, variant);
		}

		public void SendAcceptTask()
		{
			Variant variant = new Variant();
			variant["mis_cmd"] = this.ACCEPT_TASK;
			this.sendRPC(110u, variant);
		}

		public void OneKeyFinishTask()
		{
			Variant variant = new Variant();
			variant["mis_cmd"] = this.ONEKEY_FINISH;
			this.sendRPC(110u, variant);
		}

		public void SendUpgradeStar()
		{
			Variant variant = new Variant();
			variant["mis_cmd"] = this.UPGRADE_STAR;
			this.sendRPC(110u, variant);
		}

		public void SendCallMonster(uint id)
		{
			Variant variant = new Variant();
			variant["mis_cmd"] = this.CALL_MONSTER;
			variant["id"] = id;
			this.sendRPC(110u, variant);
		}

		public void SendWaitStart(int id)
		{
			Variant variant = new Variant();
			variant["mis_cmd"] = this.WAIT_START;
			variant["id"] = (uint)id;
			this.sendRPC(110u, variant);
		}

		public void SendWaitEnd(int id)
		{
			Variant variant = new Variant();
			variant["mis_cmd"] = this.WAIT_END;
			variant["id"] = (uint)id;
			this.sendRPC(110u, variant);
		}

		public void SendSubmit(int taskId, uint itemIId)
		{
			Variant variant = new Variant();
			variant["mis_cmd"] = this.SUBMIT_ITEM;
			variant["id"] = taskId;
			variant["item_id"] = itemIId;
			this.sendRPC(110u, variant);
		}

		private void OnTask(Variant data)
		{
			debug.Log("任务::" + data.dump());
			bool flag = SelfRole._inst != null;
			if (flag)
			{
				SelfRole._inst.m_LockRole = null;
			}
			bool flag2 = data.ContainsKey("res");
			if (flag2)
			{
				int res = data["res"];
				switch (res)
				{
				case 1:
					this.OnSyncTaskInfo(data);
					FunctionOpenMgr.instance.onFinshedMainTask(ModelBase<A3_TaskModel>.getInstance().main_task_id, false, false);
					base.dispatchEvent(GameEvent.alloc(0u, this, data, false, true));
					goto IL_10F;
				case 2:
					this.OnGetTaskAward(data);
					base.dispatchEvent(GameEvent.alloc(1u, this, data, false, true));
					goto IL_10F;
				case 5:
					goto IL_10F;
				case 7:
					this.OnAddTaskInfo(data);
					base.dispatchEvent(GameEvent.alloc(2u, this, data, false, true));
					goto IL_10F;
				case 8:
					this.OnRefreshTaskCount(data);
					base.dispatchEvent(GameEvent.alloc(3u, this, data, false, true));
					goto IL_10F;
				}
				Globle.err_output(res);
				IL_10F:;
			}
			bool flag3 = data.ContainsKey("mlmis");
			if (flag3)
			{
				ModelBase<A3_TaskModel>.getInstance().main_task_id = data["mlmis"]["id"];
			}
		}

		private void OnSyncTaskInfo(Variant data)
		{
			ModelBase<A3_TaskModel>.getInstance().OnSyncTask(data);
			a3_liteMinimap expr_12 = a3_liteMinimap.instance;
			bool flag = expr_12 != null && expr_12.transTask.FindChild("skin/view/con").childCount == 0;
			if (flag)
			{
				a3_liteMinimap.instance.InitTaskInfo();
			}
		}

		private void OnGetTaskAward(Variant data)
		{
			ModelBase<A3_TaskModel>.getInstance().OnSubmitTask(data);
		}

		private void OnAddTaskInfo(Variant data)
		{
			ModelBase<A3_TaskModel>.getInstance().OnAddTask(data);
		}

		private void OnRefreshTaskCount(Variant data)
		{
			ModelBase<A3_TaskModel>.getInstance().RefreshTask(data);
		}
	}
}
