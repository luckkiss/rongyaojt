using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class ReportMessagePage
	{
		private float ReportMoveDisY;

		public GameObject owner;

		private static ReportMessagePage instance;

		private Queue<GameObject> qReport;

		private readonly int DisplayNum = 11;

		public Dictionary<uint, EliteMonsterInfo> dicMessageInfo;

		public GameObject prefabReportMessage;

		public GameObject goMessageScroll;

		public static ReportMessagePage Instance
		{
			get
			{
				ReportMessagePage arg_15_0;
				if ((arg_15_0 = ReportMessagePage.instance) == null)
				{
					arg_15_0 = (ReportMessagePage.instance = new ReportMessagePage());
				}
				return arg_15_0;
			}
			set
			{
				ReportMessagePage.instance = value;
			}
		}

		private ReportMessagePage()
		{
			ReportMessagePage.Instance = this;
			this.qReport = new Queue<GameObject>();
			this.dicMessageInfo = new Dictionary<uint, EliteMonsterInfo>();
			this.owner = A3_EliteMonster.Instance.transform.FindChild("con_page/container/report/").gameObject;
			this.prefabReportMessage = this.owner.transform.FindChild("Template/reportMessage").gameObject;
			this.goMessageScroll = this.owner.transform.FindChild("scroll/scrollview").gameObject;
			this.ReportMoveDisY = this.prefabReportMessage.GetComponent<RectTransform>().sizeDelta.y;
		}

		public void AddReportMessage(string date, string playerName, string monsterName, Func<bool> infoSyncHandler = null, bool desc = false)
		{
			bool flag = date == null || (infoSyncHandler != null && !infoSyncHandler());
			if (!flag)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabReportMessage);
				gameObject.transform.FindChild("textLayout/DateText").GetComponent<Text>().text = date;
				gameObject.transform.FindChild("textLayout/PlayerName").GetComponent<Text>().text = playerName;
				gameObject.transform.FindChild("textLayout/BossName").GetComponent<Text>().text = monsterName;
				gameObject.transform.SetParent(this.goMessageScroll.transform, false);
				this.qReport.Enqueue(gameObject);
				bool flag2 = this.qReport.Count >= this.DisplayNum;
				if (flag2)
				{
					UnityEngine.Object.Destroy(this.qReport.Dequeue());
				}
				if (desc)
				{
					gameObject.transform.SetAsFirstSibling();
				}
			}
		}

		public bool AddReportInfo(uint monId, EliteMonsterInfo message)
		{
			bool flag = this.dicMessageInfo.ContainsKey(monId);
			bool result;
			if (flag)
			{
				bool flag2 = this.dicMessageInfo[monId].lastKilledTime == message.lastKilledTime;
				if (flag2)
				{
					result = false;
					return result;
				}
				this.dicMessageInfo[monId] = message;
			}
			else
			{
				this.dicMessageInfo.Add(monId, message);
			}
			result = true;
			return result;
		}
	}
}
