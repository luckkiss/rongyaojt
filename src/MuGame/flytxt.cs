using DG.Tweening;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class flytxt : LoadingUI
	{
		public static int COMMON_TYPE = 0;

		public static flytxt instance;

		protected int[] m_num = new int[9];

		protected float[] m_time = new float[]
		{
			0.2f,
			0.2f,
			0.2f,
			0.2f,
			0.4f,
			0.2f,
			0.2f,
			0.6f,
			0.2f
		};

		protected Dictionary<int, List<string>> m_txtmap;

		protected Dictionary<int, List<GameObject>> m_gameobject;

		private Queue<Transform> qRewardFlyTxt = new Queue<Transform>();

		private string str;

		private Queue<string> qFlytxt = new Queue<string>();

		private GameObject curFlyTxt;

		public override void init()
		{
			flytxt.instance = this;
			base.transform.SetAsFirstSibling();
			this.m_txtmap = new Dictionary<int, List<string>>();
			this.m_gameobject = new Dictionary<int, List<GameObject>>();
			for (int i = 0; i < this.m_num.Length; i++)
			{
				List<string> value = new List<string>();
				this.m_txtmap.Add(i, value);
			}
			for (int j = 0; j < this.m_num.Length; j++)
			{
				List<GameObject> value2 = new List<GameObject>();
				this.m_gameobject.Add(j, value2);
			}
		}

		public void fly(string txt, int tag = 0, Color m_color = default(Color), GameObject showIcon = null)
		{
			bool flag = this.m_num[tag] > 0 && this.m_time[tag] > 0f;
			if (flag)
			{
				this.m_txtmap[tag].Add(txt);
				this.m_gameobject[tag].Add(showIcon);
			}
			this.m_num[tag]++;
			switch (tag)
			{
			case 0:
				this.fly0(txt);
				break;
			case 1:
				this.fly1(txt);
				break;
			case 2:
				this.fly2(txt);
				break;
			case 3:
				this.fly3(txt);
				break;
			case 4:
				this.fly4(txt, m_color, 0);
				break;
			case 5:
				this.fly5(txt);
				break;
			case 6:
				this.fly6(showIcon);
				break;
			case 7:
				this.fly7(txt);
				break;
			case 8:
				this.flyTaskNotice(txt);
				break;
			}
		}

		public void FlyQueue(List<KeyValuePair<string, string>> showPrefab)
		{
			Queue<Transform> queue = new Queue<Transform>();
			for (int i = 0; i < showPrefab.Count; i++)
			{
				Transform transform = UnityEngine.Object.Instantiate<Transform>(base.transform.FindChild(showPrefab[i].Key));
				transform.GetComponentInChildren<Text>().text = showPrefab[i].Value;
				this.qRewardFlyTxt.Enqueue(transform);
			}
			bool flag = !base.IsInvoking("FlyNext");
			if (flag)
			{
				base.InvokeRepeating("FlyNext", 0f, 0.2f);
			}
		}

		public void FlyQueue(Transform tfPrefab)
		{
			this.qRewardFlyTxt.Enqueue(tfPrefab);
			bool flag = base.IsInvoking("FlyNext");
			if (flag)
			{
				base.CancelInvoke("FlyNext");
			}
			base.InvokeRepeating("FlyNext", 0f, 0.2f);
		}

		public void FlyNext()
		{
			bool flag = this.qRewardFlyTxt.Count == 0;
			if (flag)
			{
				base.CancelInvoke("FlyNext");
			}
			else
			{
				Transform transform = this.qRewardFlyTxt.Dequeue();
				bool flag2 = transform == null;
				if (!flag2)
				{
					GameObject expr_41 = transform.gameObject;
					if (expr_41 != null)
					{
						expr_41.SetActive(true);
					}
					transform.SetParent(base.transform, false);
					UnityEngine.Object.Destroy(transform.gameObject, 0.8f);
				}
			}
		}

		public static void flyUseContId(string id, List<string> contPram = null, int tag = 0)
		{
			flytxt.instance.fly(ContMgr.getCont(id, contPram), tag, default(Color), null);
		}

		public override void onShowed()
		{
		}

		public override void onClosed()
		{
		}

		private void timeGo_0()
		{
			bool flag = this.m_txtmap[0].Count > 0;
			if (flag)
			{
				this.fly0(this.m_txtmap[0][0]);
				this.m_txtmap[0].RemoveAt(0);
				this.m_num[0]--;
			}
		}

		private void timeGo_1()
		{
			bool flag = this.m_txtmap[1].Count > 0;
			if (flag)
			{
				this.fly1(this.m_txtmap[1][0]);
				this.m_txtmap[1].RemoveAt(0);
				this.m_num[1]--;
			}
		}

		private void timeGo_2()
		{
			bool flag = this.m_txtmap[2].Count > 0;
			if (flag)
			{
				this.fly2(this.m_txtmap[2][0]);
				this.m_txtmap[2].RemoveAt(0);
				this.m_num[2]--;
			}
		}

		private void timeGo_3()
		{
			bool flag = this.m_txtmap[3].Count > 0;
			if (flag)
			{
				this.fly3(this.m_txtmap[3][0]);
				this.m_txtmap[3].RemoveAt(0);
				this.m_num[3]--;
			}
		}

		private void timeGo_7()
		{
			bool flag = this.m_txtmap[7].Count > 0;
			if (flag)
			{
				this.fly3(this.m_txtmap[7][0]);
				this.m_txtmap[7].RemoveAt(0);
				this.m_num[7]--;
			}
		}

		public void fly0Delay()
		{
			this.fly0(this.str);
			this.str = "";
		}

		public GameObject fly0(string txt)
		{
			GameObject gameObject = base.transform.FindChild("txt_1").gameObject;
			GameObject txtclone = UnityEngine.Object.Instantiate<GameObject>(gameObject);
			txtclone.transform.SetParent(base.transform, false);
			Image component = txtclone.transform.GetComponent<Image>();
			Text component2 = txtclone.transform.FindChild("txt_1").GetComponent<Text>();
			component2.text = txt;
			Tweener t = component.transform.DOLocalMoveY(-50f, 1.5f, false).SetDelay((float)Mathf.Max(0, this.m_num[0] - 1) * this.m_time[0]).OnStart(delegate
			{
				bool flag = this.m_txtmap[0].Count > 0;
				if (flag)
				{
					this.m_txtmap[0].RemoveAt(0);
					this.m_num[0]--;
				}
				txtclone.gameObject.SetActive(true);
			});
			t.OnComplete(delegate
			{
				this.onEnd(txtclone);
			});
			return txtclone;
		}

		public void fly1(string txt)
		{
			GameObject gameObject = base.transform.FindChild("txt_1").gameObject;
			GameObject txtclone = UnityEngine.Object.Instantiate<GameObject>(gameObject);
			txtclone.transform.SetParent(base.transform, false);
			Image component = txtclone.transform.GetComponent<Image>();
			Text component2 = txtclone.transform.FindChild("txt_1").GetComponent<Text>();
			component2.text = txt;
			Tweener t = component.transform.DOLocalMoveY(0f, 1f, false).SetDelay((float)Mathf.Max(0, this.m_num[1] - 1) * this.m_time[1]).OnStart(delegate
			{
				bool flag = this.m_txtmap[1].Count > 0;
				if (flag)
				{
					this.m_txtmap[1].RemoveAt(0);
					this.m_num[1]--;
				}
				txtclone.gameObject.SetActive(true);
			});
			t.OnComplete(delegate
			{
				this.onEnd(txtclone);
			});
		}

		private void onEnd(GameObject go)
		{
			UnityEngine.Object.Destroy(go);
		}

		public void fly2(string txt)
		{
			GameObject gameObject = base.transform.FindChild("txt_1").gameObject;
			GameObject txtclone = UnityEngine.Object.Instantiate<GameObject>(gameObject);
			txtclone.transform.SetParent(base.transform, false);
			Image component = txtclone.transform.GetComponent<Image>();
			Text component2 = txtclone.transform.FindChild("txt_1").GetComponent<Text>();
			component2.text = txt;
			component2.fontSize = 30;
			component2.color = Globle.COLOR_GREEN;
			txtclone.transform.FindChild("bg").gameObject.SetActive(false);
			Tweener t = component.transform.DOLocalMoveY(0f, 1f, false).SetDelay((float)Mathf.Max(0, this.m_num[2] - 1) * this.m_time[2]).OnStart(delegate
			{
				bool flag = this.m_txtmap[2].Count > 0;
				if (flag)
				{
					this.m_txtmap[2].RemoveAt(0);
					this.m_num[2]--;
				}
				txtclone.gameObject.SetActive(true);
			});
			t.OnComplete(delegate
			{
				this.onEnd(txtclone);
			});
		}

		public void fly3(string txt)
		{
			GameObject gameObject = base.transform.FindChild("txt_2").gameObject;
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
			gameObject2.transform.FindChild("txt").GetComponent<Text>().text = txt;
			gameObject2.transform.SetParent(base.transform, false);
			gameObject2.gameObject.SetActive(true);
			UnityEngine.Object.Destroy(gameObject2.gameObject, 1f);
		}

		public void fly4(string txt, Color m_color, int index = 0)
		{
			GameObject gameObject = base.transform.FindChild("txt_3").gameObject;
			bool flag = gameObject == null;
			if (!flag)
			{
				GameObject txtclone = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				txtclone.transform.FindChild("txt").GetComponent<Text>().text = txt;
				txtclone.transform.SetParent(base.transform, false);
				txtclone.transform.FindChild("txt").GetComponent<Text>().color = m_color;
				Tweener t = txtclone.transform.FindChild("txt").DOLocalMoveY(100f, 2.5f, false).SetDelay((float)Mathf.Max(0, this.m_num[4] - 1) * this.m_time[4]).OnStart(delegate
				{
					bool flag2 = this.m_txtmap[4].Count > 0;
					if (flag2)
					{
						this.m_txtmap[4].RemoveAt(0);
						this.m_num[4]--;
					}
					txtclone.gameObject.SetActive(true);
				});
				t.OnComplete(delegate
				{
					this.onEnd(txtclone);
				});
			}
		}

		public void fly5(string txt)
		{
			GameObject gameObject = base.transform.FindChild("txt_4").gameObject;
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
			gameObject2.transform.SetParent(base.transform, false);
			Image component = gameObject2.transform.GetComponent<Image>();
			Text component2 = gameObject2.transform.FindChild("txt_1").GetComponent<Text>();
			component2.text = txt;
			this.FlyQueue(gameObject2.transform);
			UnityEngine.Object.Destroy(gameObject2, 1f);
		}

		public void fly6(GameObject go)
		{
			GameObject item = base.transform.FindChild("txt_5").gameObject;
			Transform itemicon = go.transform;
			itemicon.GetComponent<RectTransform>().sizeDelta = item.GetComponent<RectTransform>().sizeDelta;
			GameObject txtclone = UnityEngine.Object.Instantiate<GameObject>(item);
			txtclone.transform.SetParent(base.transform, false);
			itemicon.SetParent(txtclone.transform, false);
			Tweener t = txtclone.transform.DOLocalMoveY(10f, 0.2f, false).SetDelay((float)Mathf.Max(0, this.m_num[6] - 1) * this.m_time[6]).OnStart(delegate
			{
				txtclone.gameObject.SetActive(true);
				bool flag = this.m_gameobject[6].Count > 0;
				if (flag)
				{
					this.m_gameobject[6].RemoveAt(0);
					this.m_num[6]--;
				}
			});
			t.OnComplete(delegate
			{
				Tweener t2 = txtclone.transform.DOLocalMoveY(-30f, 0.3f, false).SetDelay((float)Mathf.Max(0, this.m_num[6] - 1) * this.m_time[6]).OnStart(delegate
				{
					itemicon.GetComponent<RectTransform>().sizeDelta = item.GetComponent<RectTransform>().sizeDelta * 0.5f;
				});
				t2.OnComplete(delegate
				{
					this.onEnd(txtclone);
				});
			});
		}

		public void fly7(string txt)
		{
			GameObject gameObject = base.transform.FindChild("txt_7").gameObject;
			GameObject txtclone = UnityEngine.Object.Instantiate<GameObject>(gameObject);
			Text component = txtclone.transform.FindChild("txt_1").GetComponent<Text>();
			component.text = txt;
			Image component2 = txtclone.transform.GetComponent<Image>();
			txtclone.transform.SetParent(base.transform, false);
			Tweener t = component2.transform.DOLocalMoveY(200f, 1f, false).SetDelay((float)Mathf.Max(0, this.m_num[7] - 1) * this.m_time[7]).OnStart(delegate
			{
				bool flag = this.m_txtmap[7].Count > 0;
				if (flag)
				{
					this.m_txtmap[7].RemoveAt(0);
					this.m_num[7]--;
				}
				txtclone.gameObject.SetActive(true);
			});
			t.OnComplete(delegate
			{
				this.onEnd(txtclone);
			});
		}

		public void flyTaskNotice(string txt)
		{
			GameObject gameObject = base.transform.FindChild("txt_task").gameObject;
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
			Text component = gameObject2.transform.FindChild("txt").GetComponent<Text>();
			component.text = txt;
			gameObject2.transform.SetParent(base.transform, false);
			gameObject2.gameObject.SetActive(true);
			UnityEngine.Object.Destroy(gameObject2, 4f);
		}

		public void AddDelayFlytxt(string element)
		{
			this.qFlytxt.Enqueue(element);
		}

		public void AddDelayFlytxtList(List<string> element_list)
		{
			for (int i = 0; i < element_list.Count; i++)
			{
				this.AddDelayFlytxt(element_list[i]);
			}
		}

		public void StartDelayFly(float start = 0f, float timeSpan = 0.2f)
		{
			base.InvokeRepeating("DoDelayFly", start, timeSpan);
		}

		protected void DoDelayFly()
		{
			bool flag = this.qFlytxt.Count == 0;
			if (flag)
			{
				base.CancelInvoke("DoDelayFly");
			}
			else
			{
				this.curFlyTxt = this.fly0(this.qFlytxt.Dequeue());
			}
		}

		public void StopDelayFly()
		{
			bool flag = !this.curFlyTxt;
			if (!flag)
			{
				UnityEngine.Object.DestroyImmediate(this.curFlyTxt);
				base.CancelInvoke("DoDelayFly");
				this.qFlytxt.Clear();
			}
		}
	}
}
