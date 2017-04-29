using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MuGame
{
	internal class debug : LoadingUI
	{
		public static int count = 0;

		private static float milliSecond = 0f;

		public static float fps = 0f;

		private static float deltaTime = 0f;

		public bool monstersPos = false;

		public static debug instance;

		private Text txt;

		public AsyncOperation async;

		public static void Log(string msg)
		{
			Debug.Log(msg);
		}

		public override void init()
		{
			base.alain();
			this.txt = base.getComponentByPath<Text>("Text");
			this.txt.text = "";
		}

		public void changetxt()
		{
			bool activeSelf = this.txt.gameObject.activeSelf;
			if (activeSelf)
			{
				this.txt.gameObject.SetActive(false);
			}
			else
			{
				this.txt.gameObject.SetActive(true);
			}
		}

		public override void onShowed()
		{
			debug.instance = this;
			base.onShowed();
			this.txt.gameObject.SetActive(false);
		}

		public override void onClosed()
		{
			debug.instance = null;
			base.onClosed();
		}

		private void Update()
		{
			debug.deltaTime += (Time.deltaTime - debug.deltaTime) * 0.1f;
			bool flag = ++debug.count > 10;
			if (flag)
			{
				debug.count = 0;
				debug.milliSecond = debug.deltaTime * 1000f;
				debug.fps = 1f / debug.deltaTime;
			}
			string text = string.Format(" 当前每帧渲染间隔：{0:0.0} ms ({1:0.} 帧每秒)", debug.milliSecond, debug.fps);
			bool flag2 = SelfRole._inst != null && SelfRole._inst.m_curModel != null;
			if (flag2)
			{
				int num = (int)((double)SelfRole._inst.m_curModel.position.x * 53.3);
				int num2 = (int)((double)SelfRole._inst.m_curModel.position.z * 53.3);
				int num3 = num / 32;
				int num4 = num2 / 32;
				text = string.Concat(new object[]
				{
					text,
					"\n pos:",
					SelfRole._inst.m_curModel.position.x,
					"(",
					num,
					"  ",
					num3,
					")",
					SelfRole._inst.m_curModel.position.z,
					"(",
					num2,
					"  ",
					num4,
					")"
				});
				bool flag3 = this.monstersPos;
				if (flag3)
				{
					Dictionary<uint, LGAvatarMonster> mons = LGMonsters.instacne.getMons();
					foreach (LGAvatarMonster current in mons.Values)
					{
						text = string.Concat(new object[]
						{
							text,
							"\n monsterpos iid:",
							current.iid,
							" x:",
							current.x,
							" y:",
							current.y
						});
					}
				}
			}
			this.txt.text = text;
			bool flag4 = this.async != null;
			if (flag4)
			{
				debug.Log("进度" + this.async.progress);
				bool flag5 = this.async.progress == 1f;
				if (flag5)
				{
					this.async = null;
				}
			}
		}

		public void LoadScene(string name)
		{
			base.StartCoroutine(this.LoadSceneAsync(name));
		}

		private IEnumerator LoadSceneAsync(string name)
		{
			this.async = SceneManager.LoadSceneAsync(name);
			yield return this.async;
			yield break;
		}
	}
}
