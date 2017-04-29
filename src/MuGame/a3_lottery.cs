using Cross;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_lottery : Window
	{
		private enum DrawType
		{
			Non,
			FreeOnce,
			IceOnce,
			IceTenth
		}

		private class award
		{
			public Transform transform;

			public uint rootType;

			public uint type;

			public uint id;

			public uint itemType;

			public uint itemId;

			public uint num;

			public string itemName;

			public uint cost;

			public uint stage;

			public uint intensify;
		}

		public class lotteryAwardController : MonoBehaviour
		{
			private float t = 8f;

			private float _awardCount = 11f;

			public a3_lottery.lotteryAwardController mInstance;

			private List<a3_lottery.itemLotteryAward> allLotteryAwardsDic;

			public List<a3_lottery.itemLotteryAward> movingLotteryAwards;

			private a3_lottery.movePathData _mpd;

			private List<a3_lottery.itemLotteryAward> quickMovingLotteryAwards;

			public List<a3_lottery.itemLotteryAward> mTempVisualLotteryAwards;

			private a3_lottery.itemLotteryAward middleItemLotteryAward;

			private float waitMovesecond;

			private List<a3_lottery.itemLotteryAward> allLotteryAwardsDicTemp;

			private List<a3_lottery.itemLotteryAward> quickMovingLotteryAwardsTemp;

			private float _dureationQuickMoveTime = 0.1f;

			private float _repeatRateTime = 0.1f;

			private int movingCount = 0;

			public int mMoveCompleteCount = 0;

			public void init(a3_lottery.movePathData mpd)
			{
				this.mInstance = this;
				this._mpd = mpd;
				this.movingLotteryAwards = new List<a3_lottery.itemLotteryAward>();
				this.allLotteryAwardsDic = new List<a3_lottery.itemLotteryAward>();
				this.quickMovingLotteryAwards = new List<a3_lottery.itemLotteryAward>();
				this.mTempVisualLotteryAwards = new List<a3_lottery.itemLotteryAward>();
				List<itemLotteryAwardData> lotteryAwardItems = ModelBase<LotteryModel>.getInstance().lotteryAwardItems;
				for (int i = 0; i < lotteryAwardItems.Count; i++)
				{
					a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(lotteryAwardItems[i].itemId);
					int awardNumById = ModelBase<LotteryModel>.getInstance().getAwardNumById(lotteryAwardItems[i].id);
					GameObject gameObject = IconImageMgr.getInstance().createItemIcon4Lottery(itemDataById, false, awardNumById, false, 1f, false);
					gameObject.name = lotteryAwardItems[i].id.ToString();
					gameObject.transform.SetParent(this._mpd.parent);
					gameObject.transform.localScale = Vector3.zero;
					gameObject.transform.position = this._mpd.arrayV3[0];
					a3_lottery.itemLotteryAward item = new a3_lottery.itemLotteryAward(gameObject.transform);
					this.allLotteryAwardsDic.Add(item);
				}
			}

			public void playNormal()
			{
				bool flag = this.mInstance.IsInvoking("invokeQuickMove");
				if (flag)
				{
					this.mInstance.CancelInvoke("invokeQuickMove");
				}
				bool flag2 = this.mInstance.IsInvoking("invokeQuickMove4Ten");
				if (flag2)
				{
					this.mInstance.CancelInvoke("invokeQuickMove4Ten");
				}
				for (int i = 0; i < a3_lottery.mInstance.mV3List.Count; i++)
				{
					a3_lottery.mInstance.mV3List[i].gameObject.SetActive(false);
				}
				this.mTempVisualLotteryAwards.Clear();
				this.movingLotteryAwards.Clear();
				this.allLotteryAwardsDicTemp = new List<a3_lottery.itemLotteryAward>();
				this.allLotteryAwardsDicTemp.Clear();
				this.allLotteryAwardsDicTemp.AddRange(this.allLotteryAwardsDic);
				this._mpd.duration = this.t;
				this._mpd.pt = PathType.Linear;
				this._mpd.pm = PathMode.Full3D;
				this._mpd.isTempObj = false;
				float arcLength = this.getArcLength(this._mpd.arrayV3);
				float num = arcLength / this.t;
				this.waitMovesecond = arcLength / this._awardCount / num;
				this.mInstance.InvokeRepeating("invokeWait4oneSecond", this.waitMovesecond, this.waitMovesecond);
			}

			public void playOneAward(int id)
			{
				this.movingCount = 0;
				this.mInstance.CancelInvoke("invokeWait4oneSecond");
				this.quickMovingLotteryAwards.Clear();
				this.mTempVisualLotteryAwards.Clear();
				this.quickMovingLotteryAwards.AddRange(this.movingLotteryAwards);
				for (int i = 0; i < this.quickMovingLotteryAwards.Count; i++)
				{
					this.quickMovingLotteryAwards[i].doKill();
				}
				int count = this.quickMovingLotteryAwards.Count;
				for (int j = count; j < 5; j++)
				{
					a3_lottery.itemLotteryAward item = this.allLotteryAwardsDicTemp[j];
					this.quickMovingLotteryAwards.Add(item);
				}
				uint awardItemIdById = ModelBase<LotteryModel>.getInstance().getAwardItemIdById((uint)id);
				a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(awardItemIdById);
				int awardNumById = ModelBase<LotteryModel>.getInstance().getAwardNumById((uint)id);
				GameObject gameObject = IconImageMgr.getInstance().createItemIcon4Lottery(itemDataById, false, awardNumById, false, 1f, false);
				gameObject.name = "itemLotteryAward";
				gameObject.transform.SetParent(this._mpd.parent);
				gameObject.transform.localScale = Vector3.zero;
				gameObject.transform.position = this._mpd.arrayV3[0];
				a3_lottery.itemLotteryAward item2 = new a3_lottery.itemLotteryAward(gameObject.transform);
				this.quickMovingLotteryAwards.Add(item2);
				this.middleItemLotteryAward = item2;
				bool flag = this.movingLotteryAwards.Count < 6;
				if (flag)
				{
					for (int k = 6; k < 11; k++)
					{
						a3_lottery.itemLotteryAward item3 = this.allLotteryAwardsDicTemp[k];
						this.quickMovingLotteryAwards.Add(item3);
					}
				}
				else
				{
					for (int l = this.movingLotteryAwards.Count; l < this.movingLotteryAwards.Count + 5; l++)
					{
						a3_lottery.itemLotteryAward item4 = this.allLotteryAwardsDicTemp[l];
						this.quickMovingLotteryAwards.Add(item4);
					}
				}
				bool flag2 = this.quickMovingLotteryAwardsTemp == null;
				if (flag2)
				{
					this.quickMovingLotteryAwardsTemp = new List<a3_lottery.itemLotteryAward>();
				}
				else
				{
					this.quickMovingLotteryAwardsTemp.Clear();
				}
				this.quickMovingLotteryAwardsTemp.AddRange(this.quickMovingLotteryAwards);
				this.movingCount = this.movingLotteryAwards.Count;
				this.mInstance.InvokeRepeating("invokeQuickMove", this._dureationQuickMoveTime, this._repeatRateTime);
			}

			public void playTenAward(List<int> ids)
			{
				this.mMoveCompleteCount = 0;
				this.mInstance.CancelInvoke("invokeWait4oneSecond");
				this.mInstance.CancelInvoke("invokeQuickMove4Ten");
				this.quickMovingLotteryAwards.Clear();
				this.quickMovingLotteryAwards.AddRange(this.movingLotteryAwards);
				for (int i = 0; i < ids.Count; i++)
				{
					int id = ids[i];
					uint awardItemIdById = ModelBase<LotteryModel>.getInstance().getAwardItemIdById((uint)id);
					a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(awardItemIdById);
					int awardNumById = ModelBase<LotteryModel>.getInstance().getAwardNumById((uint)id);
					GameObject gameObject = IconImageMgr.getInstance().createItemIcon4Lottery(itemDataById, false, awardNumById, false, 1f, false);
					gameObject.name = "itemLotteryAward";
					gameObject.transform.SetParent(this._mpd.parent);
					gameObject.transform.localScale = Vector3.zero;
					gameObject.transform.position = this._mpd.arrayV3[0];
					a3_lottery.itemLotteryAward item = new a3_lottery.itemLotteryAward(gameObject.transform);
					this.quickMovingLotteryAwards.Add(item);
				}
				this.movingCount = this.movingLotteryAwards.Count;
				bool flag = this.quickMovingLotteryAwardsTemp == null;
				if (flag)
				{
					this.quickMovingLotteryAwardsTemp = new List<a3_lottery.itemLotteryAward>();
				}
				else
				{
					this.quickMovingLotteryAwardsTemp.Clear();
				}
				this.quickMovingLotteryAwardsTemp.AddRange(this.quickMovingLotteryAwards);
				Debug.LogWarning(this._dureationQuickMoveTime + ":_dureationQuickMoveTime|_repeatRateTime:" + this._repeatRateTime);
				this.mInstance.InvokeRepeating("invokeQuickMove4Ten", this._dureationQuickMoveTime, this._repeatRateTime);
			}

			private void invokeWait4oneSecond()
			{
				bool flag = this.allLotteryAwardsDicTemp.Count > 0;
				if (flag)
				{
					this.allLotteryAwardsDicTemp[0].root.localScale = Vector3.one;
					this.allLotteryAwardsDicTemp[0].doPath(this._mpd);
					a3_lottery.itemLotteryAward item = this.allLotteryAwardsDicTemp[0];
					this.allLotteryAwardsDicTemp.RemoveAt(0);
					this.allLotteryAwardsDicTemp.Add(item);
				}
				else
				{
					this.mInstance.CancelInvoke("invokeWait4oneSecond");
				}
			}

			private void invokeQuickMove()
			{
				bool flag = this.quickMovingLotteryAwardsTemp.Count > 0;
				if (flag)
				{
					int num = this.quickMovingLotteryAwards.IndexOf(this.quickMovingLotteryAwardsTemp[0]);
					a3_lottery.movePathData mpd = (a3_lottery.movePathData)this._mpd.Clone();
					mpd.arrayV3 = this.getPathByIndex(num);
					mpd.duration = 2f;
					bool flag2 = this.quickMovingLotteryAwards.Count >= 17;
					if (flag2)
					{
						mpd.hide = (num < 6);
					}
					else
					{
						mpd.hide = false;
					}
					this.quickMovingLotteryAwardsTemp[0].root.localScale = Vector3.one;
					bool flag3 = this.middleItemLotteryAward == this.quickMovingLotteryAwardsTemp[0];
					if (flag3)
					{
						this.quickMovingLotteryAwardsTemp[0].doQuickPath(mpd, true);
					}
					else
					{
						this.quickMovingLotteryAwardsTemp[0].doQuickPath(mpd, false);
						this.mTempVisualLotteryAwards.Add(this.quickMovingLotteryAwardsTemp[0]);
					}
					a3_lottery.itemLotteryAward itemLotteryAward = this.quickMovingLotteryAwardsTemp[0];
					this.quickMovingLotteryAwardsTemp.RemoveAt(0);
				}
				else
				{
					this.mInstance.CancelInvoke("invokeQuickMove");
				}
			}

			private void invokeQuickMove4Ten()
			{
				bool flag = this.mInstance.IsInvoking("invokeWait4oneSecond");
				if (flag)
				{
					this.mInstance.CancelInvoke("invokeWait4oneSecond");
				}
				bool flag2 = this.mInstance.IsInvoking("invokeQuickMove");
				if (flag2)
				{
					this.mInstance.CancelInvoke("invokeQuickMove");
				}
				bool flag3 = this.quickMovingLotteryAwardsTemp.Count > 0;
				if (flag3)
				{
					int num = this.quickMovingLotteryAwards.IndexOf(this.quickMovingLotteryAwardsTemp[0]);
					a3_lottery.movePathData mpd = (a3_lottery.movePathData)this._mpd.Clone();
					mpd.arrayV3 = this.getPathByIndex4Ten(num);
					mpd.isTempObj = (num >= this.movingCount);
					mpd.duration = 2f;
					mpd.hide = (num < this.movingCount);
					this.quickMovingLotteryAwardsTemp[0].root.localScale = Vector3.one;
					this.quickMovingLotteryAwardsTemp[0].doQuickPathTen(mpd);
					this.quickMovingLotteryAwardsTemp.RemoveAt(0);
				}
				else
				{
					this.mInstance.CancelInvoke("invokeQuickMove4Ten");
				}
			}

			private Vector3[] getPathByIndex(int index)
			{
				bool flag = this.quickMovingLotteryAwards.Count < 17;
				Vector3[] result;
				if (flag)
				{
					bool flag2 = this.quickMovingLotteryAwards.Count > 11;
					if (flag2)
					{
						Vector3[] array = new Vector3[this._mpd.arrayV3.Length];
						this._mpd.arrayV3.CopyTo(array, 0);
						List<Vector3> list = array.ToList<Vector3>();
						bool flag3 = index < this.movingCount;
						if (flag3)
						{
							for (int i = 0; i < this.movingCount - index; i++)
							{
								try
								{
									list.RemoveAt(0);
								}
								catch (Exception var_7_81)
								{
									Debug.LogError(string.Concat(new object[]
									{
										"i:",
										i,
										"index:",
										index,
										"movingCount:",
										this.movingCount
									}));
								}
							}
							int num = this.quickMovingLotteryAwards.Count - 11;
							bool flag4 = num < index;
							if (flag4)
							{
								for (int j = 0; j < index - num; j++)
								{
									int count = list.Count;
									list.RemoveAt(count - 1);
								}
							}
							result = list.ToArray();
						}
						else
						{
							int num2 = list.Count - (this.quickMovingLotteryAwards.Count - index);
							for (int k = 0; k < num2; k++)
							{
								int count2 = list.Count;
								list.RemoveAt(count2 - 1);
							}
							result = list.ToArray();
						}
					}
					else
					{
						Vector3[] array2 = new Vector3[this._mpd.arrayV3.Length];
						this._mpd.arrayV3.CopyTo(array2, 0);
						List<Vector3> list2 = array2.ToList<Vector3>();
						bool flag5 = index < this.movingCount;
						if (flag5)
						{
							for (int l = 0; l < this.movingCount; l++)
							{
								list2.RemoveAt(l);
							}
						}
						for (int m = 0; m < index; m++)
						{
							int count3 = list2.Count;
							list2.RemoveAt(count3 - 1);
						}
						result = list2.ToArray();
					}
				}
				else
				{
					Vector3[] array3 = new Vector3[this._mpd.arrayV3.Length];
					this._mpd.arrayV3.CopyTo(array3, 0);
					bool flag6 = index <= this._mpd.arrayV3.Length;
					if (flag6)
					{
						List<Vector3> list3 = array3.ToList<Vector3>();
						for (int n = this._mpd.arrayV3.Length; n > index; n--)
						{
							list3.RemoveAt(0);
						}
						bool flag7 = index >= 6;
						if (flag7)
						{
							int num3 = index - 6;
							for (int num4 = 0; num4 < num3; num4++)
							{
								list3.RemoveAt(list3.Count - 1);
							}
						}
						this._dureationQuickMoveTime = 0f;
						this._repeatRateTime = 0.1f;
						result = list3.ToArray();
					}
					else
					{
						List<Vector3> list4 = array3.ToList<Vector3>();
						int num5 = list4.Count - (this.quickMovingLotteryAwards.Count - index);
						for (int num6 = 0; num6 < num5; num6++)
						{
							list4.RemoveAt(list4.Count - 1);
						}
						this._dureationQuickMoveTime = 0f;
						this._repeatRateTime = this.waitMovesecond;
						result = list4.ToArray();
					}
				}
				return result;
			}

			private Vector3[] getPathByIndex4Ten(int index)
			{
				List<Vector3> list = new List<Vector3>();
				Vector3[] array = new Vector3[this._mpd.arrayV3.Length];
				this._mpd.arrayV3.CopyTo(array, 0);
				bool flag = index < this.movingCount;
				if (flag)
				{
					List<Vector3> list2 = array.ToList<Vector3>();
					for (int i = 0; i < this.movingCount - index; i++)
					{
						list2.RemoveAt(0);
					}
					this._dureationQuickMoveTime = 0f;
					this._repeatRateTime = 0.1f;
					this._mpd.isTempObj = false;
					list = list2;
				}
				else
				{
					List<Vector3> list3 = array.ToList<Vector3>();
					int num = index - this.movingCount;
					for (int j = 0; j < num; j++)
					{
						int index2 = list3.Count - 1;
						list3.RemoveAt(index2);
					}
					this._dureationQuickMoveTime = 0f;
					this._repeatRateTime = 0.1f;
					this._mpd.isTempObj = true;
					list = list3;
				}
				return list.ToArray();
			}

			private float getArcLength(Vector3[] v3Array)
			{
				float num = 0f;
				for (int i = 0; i < v3Array.Length - 1; i++)
				{
					num += Vector3.Distance(v3Array[i + 1], v3Array[i]);
				}
				return num;
			}

			public void close()
			{
				for (int i = 0; i < this.movingLotteryAwards.Count; i++)
				{
					this.movingLotteryAwards[i].resetPos();
				}
				this.mInstance.CancelInvoke("invokeWait4oneSecond");
				this.mInstance.CancelInvoke("invokeQuickMove");
				this.mInstance.CancelInvoke("invokeQuickMove4Ten");
			}
		}

		public class itemLotteryAward : Skin
		{
			public Transform root;

			private a3_lottery.movePathData _mpd;

			private bool isMoving = false;

			private Vector3 startPostion;

			public itemLotteryAward(Transform trans) : base(trans)
			{
				this.root = trans;
				UIClient.instance.addEventListener(1902u, new Action<GameEvent>(this.onStopLotteryAward));
			}

			public void doPath(a3_lottery.movePathData mpd)
			{
				this._mpd = mpd;
				this.startPostion = this._mpd.arrayV3[0];
			}

			public void doQuickPath(a3_lottery.movePathData mpd, bool middleTarget = false)
			{
				this._mpd = mpd;
				if (middleTarget)
				{
					base.transform.DOPath(mpd.arrayV3, mpd.duration, mpd.pt, mpd.pm, 10, null).SetEase(Ease.Linear).OnUpdate(delegate
					{
						Vector3 b = a3_lottery.mInstance._arrayV3[a3_lottery.mInstance._arrayV3.Length / 2 + 1];
						bool flag = base.transform.position - b == Vector3.zero;
						if (flag)
						{
							UIClient.instance.dispatchEvent(GameEvent.Create(1902u, this, null, false));
							base.transform.DOPause();
						}
						this.isMoving = true;
					}).OnComplete(delegate
					{
						this.moveToBag(this._mpd);
						a3_lottery.mInstance.mV3List[a3_lottery.mInstance._arrayV3.Length / 2].gameObject.SetActive(true);
					});
				}
			}

			public void doQuickPathTen(a3_lottery.movePathData mpd)
			{
				this._mpd = mpd;
			}

			private void onStopLotteryAward(GameEvent e)
			{
				bool flag = this.isMoving;
				if (flag)
				{
					base.transform.DOPause();
				}
			}

			private void onAllMoveComplete(a3_lottery.movePathData mpd)
			{
				this.moveToBag4Ten(this._mpd);
			}

			public void play()
			{
				base.transform.DOPlay();
			}

			public void resetPos()
			{
				base.transform.position = this._mpd.arrayV3[0];
				base.transform.localScale = Vector3.zero;
				base.transform.DOKill(false);
			}

			public void doKill()
			{
				base.transform.DOKill(false);
			}

			public void moveToBag(a3_lottery.movePathData mpd)
			{
				base.transform.DOMove(mpd.endPos, mpd.duration, false).SetEase(Ease.InQuint).OnComplete(delegate
				{
					base.transform.localScale = Vector3.zero;
				});
			}

			public void moveToBag4Ten(a3_lottery.movePathData mpd)
			{
				base.transform.DOMove(mpd.endPos, mpd.duration, false).SetEase(Ease.InQuint).OnComplete(delegate
				{
					this.transform.localScale = Vector3.zero;
					bool isTempObj = this._mpd.isTempObj;
					if (isTempObj)
					{
						UnityEngine.Object.Destroy(this.transform.gameObject);
					}
					else
					{
						this.transform.localScale = Vector3.zero;
						this.transform.position = mpd.startPos;
					}
				});
			}
		}

		public class lotteryInfoPanel : Skin
		{
			public Transform root;

			private Transform _content;

			private Queue<a3_lottery.itemLotteryInfo> iliQueue;

			public lotteryInfoPanel(Transform trans) : base(trans)
			{
				this.root = trans;
				this.iliQueue = new Queue<a3_lottery.itemLotteryInfo>();
				BaseButton baseButton = new BaseButton(base.getTransformByPath("title/btnClose"), 1, 1);
				baseButton.onClick = new Action<GameObject>(this.onBtnCloseClick);
				this._content = base.transform.FindChild("scroll/contents");
			}

			private void onBtnCloseClick(GameObject go)
			{
				this.root.gameObject.SetActive(false);
			}

			public void onShow(Variant dt)
			{
				float num = -3.40282347E+38f;
				bool flag = dt.ContainsKey("lotlog");
				if (flag)
				{
					List<Variant> arr = dt["lotlog"]._arr;
					Debug.Log(dt["lotlog"].dump());
					for (int i = 0; i < arr.Count; i++)
					{
						itemLotteryAwardInfoData itemLotteryAwardInfoData = new itemLotteryAwardInfoData();
						itemLotteryAwardInfoData.name = arr[i]["name"]._str;
						itemLotteryAwardInfoData.tpid = arr[i]["tpid"]._uint;
						itemLotteryAwardInfoData.cnt = arr[i]["cnt"]._uint;
						itemLotteryAwardInfoData.tm = (arr[i].ContainsKey("intensify") ? arr[i]["intensify"]._uint : 0u);
						itemLotteryAwardInfoData.stage = (arr[i].ContainsKey("stage") ? arr[i]["stage"]._uint : 0u);
						GameObject gameObject = IconImageMgr.getInstance().createLotteryInfo(itemLotteryAwardInfoData, false, -1, 1f);
						a3_lottery.itemLotteryInfo item = new a3_lottery.itemLotteryInfo(gameObject.transform, this._content);
						bool flag2 = !this.iliQueue.Contains(item);
						if (flag2)
						{
							this.iliQueue.Enqueue(item);
						}
						bool flag3 = num == -3.40282347E+38f;
						if (flag3)
						{
							num = gameObject.GetComponent<LayoutElement>().preferredHeight;
						}
						bool flag4 = this.iliQueue.Count > 10;
						if (flag4)
						{
							for (int j = this.iliQueue.Count - 1; j > 0; j--)
							{
								bool flag5 = this.iliQueue.Count > 10;
								if (!flag5)
								{
									break;
								}
								UnityEngine.Object.Destroy(this.iliQueue.Dequeue().gameObject);
							}
						}
					}
					RectTransform component = this._content.GetComponent<RectTransform>();
					float size = (float)arr.Count * num;
					component.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
				}
			}

			public void onClosed()
			{
				for (int i = this.iliQueue.Count; i > 0; i--)
				{
					UnityEngine.Object.Destroy(this.iliQueue.Dequeue().gameObject);
				}
			}
		}

		private class itemLotteryInfo : Skin
		{
			public Transform root;

			public itemLotteryInfo(Transform trans, Transform parent) : base(trans)
			{
				GameObject gameObject = trans.gameObject;
				this.root = gameObject.transform;
				gameObject.name = "lotteryItemAwardInfo";
				gameObject.transform.localScale = Vector3.one;
				this.root.SetParent(parent, false);
			}
		}

		public struct movePathData : ICloneable
		{
			public Vector3[] arrayV3;

			public float duration;

			public PathType pt;

			public PathMode pm;

			public Vector3 startPos;

			public Vector3 endPos;

			public Transform parent;

			public bool hide;

			public bool isTempObj;

			public object Clone()
			{
				a3_lottery.movePathData movePathData = default(a3_lottery.movePathData);
				movePathData.arrayV3 = new Vector3[this.arrayV3.Length];
				this.arrayV3.CopyTo(movePathData.arrayV3, 0);
				movePathData.duration = this.duration;
				movePathData.hide = this.hide;
				movePathData.endPos = this.endPos;
				movePathData.startPos = this.startPos;
				movePathData.isTempObj = this.isTempObj;
				return movePathData;
			}
		}

		public class DelayToInvoke : MonoBehaviour
		{
			public static IEnumerator DelayToInvokeDo(Action action, float delaySeconds)
			{
				yield return new WaitForSeconds(delaySeconds);
				action();
				yield break;
			}
		}

		public static a3_lottery mInstance;

		private int[] LootterCoolingTime = new int[]
		{
			0,
			600,
			1800,
			3600,
			7200
		};

		public bool is_open;

		private a3_lottery.DrawType cureentDrawType = a3_lottery.DrawType.Non;

		private BaseButton btn_close;

		private Text txtNotic;

		private Text txtGold;

		private Text txtDiamond;

		private Text txtPersonalDiamond;

		private BaseButton btn_freeOnce;

		private BaseButton btn_iceOnce;

		private Text txtIceOnce;

		private Text txtLeftTimes;

		private BaseButton btn_IceTenth;

		private BaseButton btn_openBag;

		private BaseButton btn_newbie_ice;

		private Transform awardsAchieved;

		private BaseButton bg_hintFree;

		private Text hintFreeText;

		private BaseButton bg_hintIce;

		private Text hintIceText;

		private BaseButton btn_buyIce;

		private BaseButton btn_cancel;

		private float timer = 0f;

		private int timesDraw = -2147483648;

		public Vector3[] _arrayV3;

		public List<Transform> mV3List;

		private int _awardCount;

		private Transform _awardContents;

		private Transform _lotteryInfoPanelTf;

		private a3_lottery.lotteryInfoPanel _lotteryInfoPanel;

		public TickItem tickItemTime;

		private BaseButton btn_OkOne;

		private BaseButton btn_OkTen;

		private Transform TipOne;

		private Transform TipTen;

		private Transform TipOneP;

		private Transform TipTenP;

		private List<GameObject> DrawLstOne = new List<GameObject>();

		private List<GameObject> DrawLstTen = new List<GameObject>();

		private List<Vector3> DrawLstOneV3 = new List<Vector3>();

		private List<Vector3> DrawLstTenV3 = new List<Vector3>();

		private BaseButton Btn_TenSkip;

		private Vector3 PosOne;

		private Text NameOne;

		private Vector3[] PosTen;

		private Vector3 PosQuality;

		private float updateTime = 0f;

		private float textTime = 5.6f;

		private int outTime = 0;

		private Animator ani_Add;

		private Animator ani_Text;

		private Image textBg;

		private Transform _imgTip;

		private int click = 0;

		private List<Animator> animAwardItemList;

		private int animAwardItemListIndex;

		private int iconAwardItemListIndex;

		private float timeSpan = 0.5f;

		private Transform tfLeftTimeParent;

		public float times = 0f;

		public int i;

		public override void init()
		{
			a3_lottery.mInstance = this;
			this.btn_close = new BaseButton(base.transform.FindChild("btn_close"), 1, 1);
			this.btn_close.onClick = new Action<GameObject>(this.onBtnCloseClick);
			this.btn_openBag = new BaseButton(base.transform.FindChild("bottom/btn_openbag"), 1, 1);
			this.btn_openBag.onClick = new Action<GameObject>(this.onBtnOpenBagClick);
			this.txtNotic = base.transform.FindChild("notice/Text").GetComponent<Text>();
			this.awardsAchieved = base.transform.FindChild("body/left/bg/hlg_award");
			this.btn_freeOnce = new BaseButton(base.transform.FindChild("bottom/btn_freeOnce"), 1, 1);
			this.btn_iceOnce = new BaseButton(base.transform.FindChild("bottom/btn_iceOnce"), 1, 1);
			this.txtIceOnce = this.btn_iceOnce.transform.FindChild("free/txtInfo").GetComponent<Text>();
			this.btn_IceTenth = new BaseButton(base.transform.FindChild("bottom/btn_iceTenth"), 1, 1);
			this.btn_freeOnce.onClick = new Action<GameObject>(this.onBtnFreeOnceClick);
			this.btn_iceOnce.onClick = new Action<GameObject>(this.onBtnIceOnceClick);
			this.btn_IceTenth.onClick = new Action<GameObject>(this.onBtnIceTenthClick);
			this.txtLeftTimes = this.btn_freeOnce.transform.FindChild("LeftTime/Times").GetComponent<Text>();
			this._lotteryInfoPanelTf = base.transform.FindChild("Hint/lotteryInfoPanel");
			this.bg_hintFree = new BaseButton(base.transform.FindChild("Hint/forFree"), 1, 1);
			this.bg_hintFree.onClick = new Action<GameObject>(this.onHintClick);
			this.hintFreeText = this.bg_hintFree.transform.FindChild("Text").GetComponent<Text>();
			this.bg_hintIce = new BaseButton(base.transform.FindChild("Hint/forIce"), 1, 1);
			this.bg_hintIce.onClick = new Action<GameObject>(this.onHiIceClick);
			this.hintIceText = this.bg_hintIce.transform.FindChild("Text").GetComponent<Text>();
			this.btn_buyIce = new BaseButton(this.bg_hintIce.transform.FindChild("btn_ok"), 1, 1);
			this.btn_buyIce.onClick = new Action<GameObject>(this.onHitIceBuyIce);
			this.btn_cancel = new BaseButton(this.bg_hintIce.transform.FindChild("btn_cancel"), 1, 1);
			this.btn_cancel.onClick = new Action<GameObject>(this.onHintIceCancle);
			BaseButton baseButton = new BaseButton(base.transform.FindChild("bottom/btn_notice"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onBtnNoticeClick);
			this._lotteryInfoPanel = new a3_lottery.lotteryInfoPanel(this._lotteryInfoPanelTf);
			this._imgTip = base.transform.FindChild("imgTip");
			this._imgTip.gameObject.SetActive(false);
			this.TipOne = base.transform.FindChild("TipOne");
			this.TipOneP = base.transform.FindChild("TipOne/Node");
			this.btn_OkOne = new BaseButton(base.transform.FindChild("TipOne/BtnOne"), 1, 1);
			this.btn_OkOne.onClick = new Action<GameObject>(this.onBtnOkOneClick);
			this.btn_newbie_ice = new BaseButton(base.transform.FindChild("bottom/btn_NewBieOnce"), 1, 1);
			this.btn_newbie_ice.onClick = new Action<GameObject>(this.onNewBieIceOnce);
			this.btn_newbie_ice.gameObject.SetActive(false);
			this.TipTen = base.transform.FindChild("TipTen");
			this.TipTenP = base.transform.FindChild("TipTen/Node");
			this.btn_OkTen = new BaseButton(base.transform.FindChild("TipTen/BtnTen"), 1, 1);
			this.btn_OkTen.onClick = new Action<GameObject>(this.onBtnOkTenClick);
			this.Btn_TenSkip = new BaseButton(base.transform.FindChild("TipTen/BtnSkip"), 1, 1);
			this.Btn_TenSkip.onClick = new Action<GameObject>(this.onBtnTenSkipClick);
			this.PosOne = base.transform.FindChild("TipOne/icon").position;
			this.NameOne = base.transform.FindChild("TipOne/name").GetComponent<Text>();
			this.PosQuality = base.transform.FindChild("TipTen/ImageTop").position;
			this.PosTen = new Vector3[10];
			for (int i = 0; i < 10; i++)
			{
				this.PosTen[i] = base.transform.FindChild("TipTen/GardIcon/Image" + i).position;
			}
			this.ani_Add = base.transform.FindChild("TipTen/Add").GetComponent<Animator>();
			this.ani_Text = base.transform.FindChild("notice").GetComponent<Animator>();
			this.textBg = base.transform.FindChild("notice/bg").GetComponent<Image>();
			this.txtGold = base.transform.FindChild("money/gold/image/num").GetComponent<Text>();
			this.txtDiamond = base.transform.FindChild("money/diamond/image/num").GetComponent<Text>();
			this.txtPersonalDiamond = base.transform.FindChild("money/personalDiamond/image/num").GetComponent<Text>();
			new BaseButton(base.transform.FindChild("money/gold/btnAdd"), 1, 1).onClick = new Action<GameObject>(this.OnGetGold);
			new BaseButton(base.transform.FindChild("money/diamond/btnAdd"), 1, 1).onClick = new Action<GameObject>(this.OnGetDiamond);
		}

		private a3_lottery.movePathData initMovingPath()
		{
			Transform transform = base.transform.FindChild("bg/leftRoad");
			Transform transform2 = base.transform.FindChild("bg/rightRoad");
			this._awardCount = transform.childCount + transform2.childCount;
			this._arrayV3 = new Vector3[this._awardCount];
			this.mV3List = new List<Transform>();
			for (int i = 0; i < transform.childCount; i++)
			{
				this._arrayV3[i] = transform.GetChild(i).position;
				this.mV3List.Add(transform.GetChild(i));
			}
			for (int j = 0; j < transform2.childCount; j++)
			{
				this._arrayV3[transform.childCount + j] = transform2.GetChild(j).position;
				this.mV3List.Add(transform2.GetChild(j));
			}
			this._awardContents = base.transform.FindChild("body/awardContents");
			Transform transform3 = base.transform.FindChild("bottom/btn_openbag");
			return new a3_lottery.movePathData
			{
				arrayV3 = this._arrayV3,
				parent = this._awardContents,
				endPos = transform3.position,
				startPos = transform.GetChild(0).position
			};
		}

		public override void onShowed()
		{
			this.setNewBie_btn();
			this.btn_close.addEvent();
			this.btn_openBag.addEvent();
			this.btn_freeOnce.addEvent();
			this.btn_iceOnce.addEvent();
			this.btn_IceTenth.addEvent();
			this.bg_hintFree.addEvent();
			this.bg_hintIce.addEvent();
			this.btn_buyIce.addEvent();
			this.btn_cancel.addEvent();
			this.btn_OkTen.addEvent();
			this.btn_OkOne.addEvent();
			this.Btn_TenSkip.addEvent();
			BaseProxy<LotteryProxy>.getInstance().sendlottery(1);
			BaseProxy<LotteryProxy>.getInstance().addEventListener(LotteryProxy.LOADLOTTERY, new Action<GameEvent>(this.onLoadLotterys));
			BaseProxy<LotteryProxy>.getInstance().addEventListener(LotteryProxy.LOTTERYOK_FREEDRAW, new Action<GameEvent>(this.onLotteryFreeDraw));
			BaseProxy<LotteryProxy>.getInstance().addEventListener(LotteryProxy.LOTTERYOK_ICEDRAWONCE, new Action<GameEvent>(this.onLotteryIceOnce));
			BaseProxy<LotteryProxy>.getInstance().addEventListener(LotteryProxy.LOTTERYOK_ICEDRAWTENTH, new Action<GameEvent>(this.onLotteryIceTenth));
			BaseProxy<LotteryProxy>.getInstance().addEventListener(LotteryProxy.LOTTERYOK_ICED_NEWBIE, new Action<GameEvent>(this.onLotteryIceTenth_newbie));
			BaseProxy<BagProxy>.getInstance().addEventListener(9005u, new Action<GameEvent>(this.OnShowMoney));
			UIClient.instance.addEventListener(9005u, new Action<GameEvent>(this.onMoneyChange));
			this.refreshMoney();
			this.refreshGold();
			this.refreshGift();
			this.updateTime = 0f;
			this.textTime = 5.6f;
			GRMap.GAME_CAMERA.SetActive(false);
			this.txtGold.text = ModelBase<PlayerModel>.getInstance().money.ToString();
			this.txtDiamond.text = ModelBase<PlayerModel>.getInstance().gold.ToString();
			this.txtPersonalDiamond.text = ModelBase<PlayerModel>.getInstance().gift.ToString();
		}

		public override void onClosed()
		{
			BaseProxy<LotteryProxy>.getInstance().removeEventListener(LotteryProxy.LOADLOTTERY, new Action<GameEvent>(this.onLoadLotterys));
			BaseProxy<LotteryProxy>.getInstance().removeEventListener(LotteryProxy.LOTTERYOK_FREEDRAW, new Action<GameEvent>(this.onLotteryFreeDraw));
			BaseProxy<LotteryProxy>.getInstance().removeEventListener(LotteryProxy.LOTTERYOK_ICED_NEWBIE, new Action<GameEvent>(this.onLotteryIceTenth_newbie));
			BaseProxy<LotteryProxy>.getInstance().removeEventListener(LotteryProxy.LOTTERYOK_ICEDRAWONCE, new Action<GameEvent>(this.onLotteryIceOnce));
			BaseProxy<LotteryProxy>.getInstance().removeEventListener(LotteryProxy.LOTTERYOK_ICEDRAWTENTH, new Action<GameEvent>(this.onLotteryIceTenth));
			BaseProxy<BagProxy>.getInstance().removeEventListener(9005u, new Action<GameEvent>(this.OnShowMoney));
			UIClient.instance.removeEventListener(19001u, new Action<GameEvent>(this.onGetAcquisitionAwardInfo));
			UIClient.instance.removeEventListener(9005u, new Action<GameEvent>(this.onMoneyChange));
			this.btn_close.removeAllListener();
			this.btn_openBag.removeAllListener();
			this.btn_freeOnce.removeAllListener();
			this.btn_iceOnce.removeAllListener();
			this.btn_IceTenth.removeAllListener();
			this.bg_hintFree.removeAllListener();
			this.bg_hintIce.removeAllListener();
			this.btn_buyIce.removeAllListener();
			this.btn_cancel.removeAllListener();
			GRMap.GAME_CAMERA.SetActive(true);
			this.btn_OkTen.removeAllListener();
			this.btn_OkOne.removeAllListener();
			this.Btn_TenSkip.removeAllListener();
			this.ani_Text.Play("drawtext", -1, 1f);
			bool isNewBie = ModelBase<LotteryModel>.getInstance().isNewBie;
			if (isNewBie)
			{
				ModelBase<LotteryModel>.getInstance().isNewBie = false;
				this.txtIceOnce.gameObject.SetActive(true);
				this.btn_freeOnce.transform.FindChild("LeftTime").gameObject.SetActive(true);
				this.btn_newbie_ice.gameObject.SetActive(false);
			}
			this._lotteryInfoPanel.onClosed();
			BaseProxy<LotteryProxy>.getInstance().sendlottery(1);
		}

		private void onMoneyChange(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data.ContainsKey("money");
			if (flag)
			{
				this.refreshMoney();
			}
			bool flag2 = data.ContainsKey("yb");
			if (flag2)
			{
				this.refreshGold();
			}
			bool flag3 = data.ContainsKey("bndyb");
			if (flag3)
			{
				this.refreshGift();
			}
		}

		public void refreshMoney()
		{
			Text component = base.transform.FindChild("money/gold/image/num").GetComponent<Text>();
			component.text = Globle.getBigText(ModelBase<PlayerModel>.getInstance().money);
		}

		public void refreshGold()
		{
			Text component = base.transform.FindChild("money/diamond/image/num").GetComponent<Text>();
			component.text = ModelBase<PlayerModel>.getInstance().gold.ToString();
		}

		public void refreshGift()
		{
			Text component = base.transform.FindChild("money/personalDiamond/image/num").GetComponent<Text>();
			component.text = ModelBase<PlayerModel>.getInstance().gift.ToString();
		}

		private void OnShowMoney(GameEvent e)
		{
			this.txtGold.text = ModelBase<PlayerModel>.getInstance().money.ToString();
			this.txtDiamond.text = ModelBase<PlayerModel>.getInstance().gold.ToString();
			this.txtPersonalDiamond.text = ModelBase<PlayerModel>.getInstance().gift.ToString();
		}

		private void onBtnCloseClick(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_LOTTERY);
			this.is_open = false;
		}

		private void setNewBie_btn()
		{
			bool isNewBie = ModelBase<LotteryModel>.getInstance().isNewBie;
			if (isNewBie)
			{
				this.btn_newbie_ice.gameObject.SetActive(true);
				this.txtIceOnce.gameObject.SetActive(false);
				this.btn_freeOnce.transform.FindChild("LeftTime").gameObject.SetActive(false);
			}
		}

		private void onBtnOpenBagClick(GameObject go)
		{
			this.is_open = true;
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_LOTTERY);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_BAG, null, false);
			bool flag = a3_bag.Instance != null;
			if (flag)
			{
				a3_bag.Instance.transform.SetAsLastSibling();
			}
		}

		private void onBtnFreeOnceClick(GameObject go)
		{
			bool flag = this.timer > 0.3f && this.timesDraw > 0;
			if (flag)
			{
				this.setHintFreeInfo("现在还不能使用");
			}
			else
			{
				bool flag2 = this.timesDraw == 0;
				if (!flag2)
				{
					bool flag3 = this.timer <= 0f && this.timesDraw > 0;
					if (flag3)
					{
						this.setDrawButtonUnEnable(a3_lottery.DrawType.FreeOnce);
						BaseProxy<LotteryProxy>.getInstance().sendlottery(2);
					}
				}
			}
		}

		private void onBtnIceOnceClick(GameObject go)
		{
			bool flag = (ulong)ModelBase<PlayerModel>.getInstance().gold < (ulong)((long)LotteryModel.iceOnceCost);
			if (flag)
			{
				this.setHitIceInfo();
			}
			else
			{
				this.setDrawButtonUnEnable(a3_lottery.DrawType.IceOnce);
				BaseProxy<LotteryProxy>.getInstance().sendlottery(3);
			}
		}

		private void onNewBieIceOnce(GameObject go)
		{
			BaseProxy<LotteryProxy>.getInstance().sendlottery(5);
		}

		private void onBtnIceTenthClick(GameObject go)
		{
			bool flag = ModelBase<PlayerModel>.getInstance().gold < 500u;
			if (flag)
			{
				this.setHitIceInfo();
			}
			else
			{
				this.btn_IceTenth.interactable = false;
				this.setDrawButtonUnEnable(a3_lottery.DrawType.IceTenth);
				BaseProxy<LotteryProxy>.getInstance().sendlottery(4);
			}
			base.StartCoroutine(this.wait());
		}

		private IEnumerator wait()
		{
			yield return new WaitForSeconds(0.2f);
			yield break;
		}

		private void onHintClick(GameObject go)
		{
			bool activeSelf = this.bg_hintFree.gameObject.activeSelf;
			if (activeSelf)
			{
				this.bg_hintFree.gameObject.SetActive(false);
			}
		}

		private void onHiIceClick(GameObject go)
		{
			bool activeSelf = this.bg_hintIce.gameObject.activeSelf;
			if (activeSelf)
			{
				this.bg_hintIce.gameObject.SetActive(false);
			}
		}

		private void onHitIceBuyIce(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.SHOP_A3, null, false);
		}

		private void onHintIceCancle(GameObject go)
		{
			bool activeSelf = this.bg_hintIce.gameObject.activeSelf;
			if (activeSelf)
			{
				this.bg_hintIce.gameObject.SetActive(false);
			}
		}

		private void onBtnNoticeClick(GameObject go)
		{
			this._lotteryInfoPanelTf.gameObject.SetActive(true);
		}

		private void onBtnOkOneClick(GameObject go)
		{
			for (int i = 0; i < this.TipOneP.childCount; i++)
			{
				bool flag = this.TipOneP.GetChild(i).name == "itemLotteryAward";
				if (flag)
				{
					UnityEngine.Object.Destroy(this.TipOneP.GetChild(i).transform.gameObject);
				}
			}
			this.TipOne.gameObject.SetActive(false);
			this._imgTip.gameObject.SetActive(false);
			this.btn_iceOnce.interactable = true;
			this.btn_IceTenth.interactable = true;
		}

		private void onBtnOkTenClick(GameObject go)
		{
			for (int i = 0; i < this.TipTenP.childCount; i++)
			{
				bool flag = this.TipTenP.GetChild(i).name == "itemLotteryAward";
				if (flag)
				{
					UnityEngine.Object.Destroy(this.TipTenP.GetChild(i).transform.gameObject);
				}
			}
			this._imgTip.gameObject.SetActive(false);
			this.TipTen.gameObject.SetActive(false);
			this.btn_iceOnce.interactable = true;
			this.btn_IceTenth.interactable = true;
		}

		private void onBtnTenSkipClick(GameObject go)
		{
			this.ani_Add.Play("drawadd", -1, 1f);
			this.DrawLstOne[0].transform.FindChild("qicon").GetComponent<Image>().CrossFadeAlpha(1f, 0f, true);
			this.DrawLstOne[0].transform.FindChild("bicon").GetComponent<Image>().CrossFadeAlpha(1f, 0f, true);
			this.DrawLstOne[0].transform.FindChild("icon").GetComponent<Image>().CrossFadeAlpha(1f, 0f, true);
			base.transform.FindChild("TipTen/Text").GetComponent<Text>().CrossFadeAlpha(1f, 0f, true);
			for (int i = 0; i < 10; i++)
			{
				this.DrawLstTen[i].transform.FindChild("qicon").GetComponent<Image>().CrossFadeAlpha(1f, 0f, true);
				this.DrawLstTen[i].transform.FindChild("bicon").GetComponent<Image>().CrossFadeAlpha(1f, 0f, true);
				this.DrawLstTen[i].transform.FindChild("icon").GetComponent<Image>().CrossFadeAlpha(1f, 0f, true);
				base.transform.FindChild("TipTen/GardText").GetChild(i).GetComponent<Text>().CrossFadeAlpha(1f, 0f, true);
			}
		}

		private void onLoadLotterys(GameEvent e)
		{
			bool flag = true;
			Variant data = e.data;
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = data.ContainsKey("left_times");
				if (flag3)
				{
					this.timesDraw = data["left_times"]._int;
				}
				bool flag4 = data.ContainsKey("left_tm");
				if (flag4)
				{
					this.timer = data["left_tm"]._float;
					bool flag5 = this.timer > 0f;
					if (flag5)
					{
						int num = (int)this.timer;
						string text = ((num / 60 / 60).ToString().Length > 1) ? (num / 60 / 60).ToString() : ("0" + num / 60 / 60);
						string text2 = ((num / 60 % 60).ToString().Length > 1) ? (num / 60 % 60).ToString() : ("0" + num / 60 % 60);
						string text3 = ((num % 60).ToString().Length > 1) ? (num % 60).ToString() : ("0" + num % 60);
						string text4 = string.Concat(new string[]
						{
							text,
							":",
							text2,
							":",
							text3,
							"后免费"
						});
						this.txtIceOnce.text = text4;
					}
				}
				this._lotteryInfoPanel.onShow(e.data);
				Variant variant = new Variant();
			}
			this.setNoticeInfo(data);
		}

		private void setNoticeInfo(Variant dt)
		{
			bool flag = dt.ContainsKey("lotlog");
			if (flag)
			{
				List<Variant> arr = dt["lotlog"]._arr;
				itemLotteryAwardInfoData itemLotteryAwardInfoData = new itemLotteryAwardInfoData();
				itemLotteryAwardInfoData.name = arr[0]["name"]._str;
				itemLotteryAwardInfoData.tpid = arr[0]["tpid"]._uint;
				itemLotteryAwardInfoData.cnt = arr[0]["cnt"]._uint;
				itemLotteryAwardInfoData.tm = (arr[0].ContainsKey("intensify") ? arr[0]["intensify"]._uint : 0u);
				itemLotteryAwardInfoData.stage = (arr[0].ContainsKey("stage") ? arr[0]["stage"]._uint : 0u);
				this.txtNotic.text = "恭喜 <color=#FF0000FF>" + itemLotteryAwardInfoData.name + "</color> 获得了 " + this.setQualityColor(ModelBase<a3_BagModel>.getInstance().getItemDataById(itemLotteryAwardInfoData.tpid).item_name, ModelBase<a3_BagModel>.getInstance().getItemDataById(itemLotteryAwardInfoData.tpid).quality);
				AnimatorStateInfo currentAnimatorStateInfo = this.ani_Text.GetCurrentAnimatorStateInfo(0);
				bool flag2 = currentAnimatorStateInfo.normalizedTime >= 1f && currentAnimatorStateInfo.IsName("drawtext");
				if (flag2)
				{
					this.ani_Text.Play("drawtext", -1, 0f);
				}
			}
		}

		private void setFreeOnceDrawText(int leftTm = -1, bool freeBtnShow = true)
		{
			this.txtLeftTimes.gameObject.SetActive(false);
			bool flag = leftTm <= 0;
			if (flag)
			{
				this.txtIceOnce.gameObject.SetActive(false);
			}
			else
			{
				this.txtIceOnce.gameObject.SetActive(true);
				string text = string.Empty;
				string text2 = ((leftTm / 60 / 60).ToString().Length > 1) ? (leftTm / 60 / 60).ToString() : ("0" + leftTm / 60 / 60);
				string text3 = ((leftTm / 60 % 60).ToString().Length > 1) ? (leftTm / 60 % 60).ToString() : ("0" + leftTm / 60 / 60);
				string text4 = ((leftTm % 60).ToString().Length > 1) ? (leftTm % 60).ToString() : ("0" + leftTm % 60);
				text = string.Concat(new string[]
				{
					text2,
					":",
					text3,
					":",
					text4,
					"后免费"
				});
				this.txtIceOnce.text = text;
			}
			this.btn_iceOnce.gameObject.SetActive(!freeBtnShow);
			this.btn_freeOnce.gameObject.SetActive(freeBtnShow);
		}

		private void onLotteryFreeDraw(GameEvent e)
		{
			this.btn_iceOnce.interactable = false;
			this.btn_IceTenth.interactable = false;
			Variant data = e.data;
			List<Variant> arr = data["ids"]._arr;
			uint @uint = arr[0]._uint;
			uint awardItemIdById = ModelBase<LotteryModel>.getInstance().getAwardItemIdById(@uint);
			a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(awardItemIdById);
			int awardNumById = ModelBase<LotteryModel>.getInstance().getAwardNumById(@uint);
			GameObject gameObject = IconImageMgr.getInstance().createItemIcon4Lottery(itemDataById, false, awardNumById, true, 1f, false);
			gameObject.name = "itemLotteryAward";
			gameObject.transform.SetParent(this.TipOneP, false);
			gameObject.transform.position = this.PosOne;
			this.NameOne.text = this.setQualityColor(itemDataById.item_name.ToString(), itemDataById.quality);
			this._imgTip.gameObject.SetActive(true);
			this.TipOne.gameObject.SetActive(true);
			this.TipOne.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			Image qicon = gameObject.transform.FindChild("qicon").GetComponent<Image>();
			Image bicon = gameObject.transform.FindChild("bicon").GetComponent<Image>();
			Image sicon = gameObject.transform.FindChild("icon").GetComponent<Image>();
			qicon.CrossFadeAlpha(0f, 0f, true);
			bicon.CrossFadeAlpha(0f, 0f, true);
			sicon.CrossFadeAlpha(0f, 0f, true);
			this.NameOne.CrossFadeAlpha(0f, 0f, true);
			Sequence s = DOTween.Sequence();
			Tweener t = this.TipOne.DOScale(1f, 0.5f);
			s.Append(t);
			t.OnComplete(delegate
			{
				qicon.CrossFadeAlpha(1f, 0.2f, true);
				bicon.CrossFadeAlpha(1f, 0.2f, true);
				sicon.CrossFadeAlpha(1f, 0.2f, true);
				this.NameOne.CrossFadeAlpha(1f, 0.2f, true);
			});
			bool flag = this.timesDraw > 1;
			if (flag)
			{
				this.timer = (float)this.LootterCoolingTime[5 - this.timesDraw + 1];
			}
			else
			{
				this.timer = 0f;
			}
			bool flag2 = this.timesDraw != 0;
			if (flag2)
			{
				this.timesDraw--;
			}
		}

		private void onLotteryIceOnce(GameEvent e)
		{
			this.btn_iceOnce.interactable = false;
			this.btn_IceTenth.interactable = false;
			Variant data = e.data;
			bool flag = data["res"] < 0;
			if (!flag)
			{
				List<Variant> arr = data["ids"]._arr;
				uint @uint = arr[0]._uint;
				uint awardItemIdById = ModelBase<LotteryModel>.getInstance().getAwardItemIdById(@uint);
				a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(awardItemIdById);
				int awardNumById = ModelBase<LotteryModel>.getInstance().getAwardNumById(@uint);
				GameObject gameObject = IconImageMgr.getInstance().createItemIcon4Lottery(itemDataById, false, awardNumById, true, 1f, false);
				gameObject.name = "itemLotteryAward";
				gameObject.transform.SetParent(this.TipOneP, false);
				gameObject.transform.position = this.PosOne;
				BaseButton baseButton = new BaseButton(gameObject.transform, 1, 1);
				this.NameOne.text = this.setQualityColor(itemDataById.item_name.ToString(), itemDataById.quality);
				this._imgTip.gameObject.SetActive(true);
				this.TipOne.gameObject.SetActive(true);
				this.TipOne.localScale = new Vector3(0.1f, 0.1f, 0.1f);
				Image qicon = gameObject.transform.FindChild("qicon").GetComponent<Image>();
				Image bicon = gameObject.transform.FindChild("bicon").GetComponent<Image>();
				Image sicon = gameObject.transform.FindChild("icon").GetComponent<Image>();
				qicon.CrossFadeAlpha(0f, 0f, true);
				bicon.CrossFadeAlpha(0f, 0f, true);
				sicon.CrossFadeAlpha(0f, 0f, true);
				this.NameOne.CrossFadeAlpha(0f, 0f, true);
				Sequence s = DOTween.Sequence();
				Tweener t = this.TipOne.DOScale(1f, 0.5f);
				s.Append(t);
				t.OnComplete(delegate
				{
					qicon.CrossFadeAlpha(1f, 0.2f, true);
					bicon.CrossFadeAlpha(1f, 0.2f, true);
					sicon.CrossFadeAlpha(1f, 0.2f, true);
					this.NameOne.CrossFadeAlpha(1f, 0.2f, true);
				});
			}
		}

		private void onLotteryIceTenth_newbie(GameEvent e)
		{
			this.btn_iceOnce.interactable = false;
			this.btn_IceTenth.interactable = false;
			Variant data = e.data;
			uint tpid = data["item_id"];
			a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(tpid);
			GameObject gameObject = IconImageMgr.getInstance().createItemIcon4Lottery(itemDataById, false, 1, true, 1f, false);
			gameObject.name = "itemLotteryAward";
			gameObject.transform.SetParent(this.TipOneP, false);
			gameObject.transform.position = this.PosOne;
			this.NameOne.text = this.setQualityColor(itemDataById.item_name.ToString(), itemDataById.quality);
			this._imgTip.gameObject.SetActive(true);
			this.TipOne.gameObject.SetActive(true);
			this.TipOne.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			Image qicon = gameObject.transform.FindChild("qicon").GetComponent<Image>();
			Image bicon = gameObject.transform.FindChild("bicon").GetComponent<Image>();
			Image sicon = gameObject.transform.FindChild("icon").GetComponent<Image>();
			qicon.CrossFadeAlpha(0f, 0f, true);
			bicon.CrossFadeAlpha(0f, 0f, true);
			sicon.CrossFadeAlpha(0f, 0f, true);
			this.NameOne.CrossFadeAlpha(0f, 0f, true);
			Sequence s = DOTween.Sequence();
			Tweener t = this.TipOne.DOScale(1f, 0.5f);
			s.Append(t);
			t.OnComplete(delegate
			{
				qicon.CrossFadeAlpha(1f, 0.2f, true);
				bicon.CrossFadeAlpha(1f, 0.2f, true);
				sicon.CrossFadeAlpha(1f, 0.2f, true);
				this.NameOne.CrossFadeAlpha(1f, 0.2f, true);
			});
		}

		private void OnGetGold(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_EXCHANGE, null, false);
			a3_exchange expr_18 = a3_exchange.Instance;
			if (expr_18 != null)
			{
				expr_18.transform.SetSiblingIndex(base.transform.GetSiblingIndex() + 1);
			}
		}

		private void OnGetDiamond(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_RECHARGE, null, false);
			a3_Recharge expr_18 = a3_Recharge.Instance;
			if (expr_18 != null)
			{
				expr_18.transform.SetSiblingIndex(base.transform.GetSiblingIndex() + 1);
			}
		}

		private void onLotteryIceTenth(GameEvent e)
		{
			Debug.Log("抽奖信息:::" + e.data["ids"].dump());
			this.btn_iceOnce.interactable = false;
			this.btn_IceTenth.interactable = false;
			Variant data = e.data;
			List<Variant> arr = data["ids"]._arr;
			List<int> list = new List<int>();
			list.Clear();
			this.DrawLstOne.Clear();
			this.DrawLstTen.Clear();
			this.DrawLstOneV3.Clear();
			this.DrawLstTenV3.Clear();
			uint num = 0u;
			uint num2 = 0u;
			uint num3 = 0u;
			bool flag = true;
			bool flag2 = true;
			List<GameObject> lst = new List<GameObject>();
			List<GameObject> lstTop = new List<GameObject>();
			List<Text> lsx = new List<Text>();
			lst.Clear();
			lstTop.Clear();
			lsx.Clear();
			for (int i = 0; i < arr.Count; i++)
			{
				uint @uint = arr[i]._uint;
				uint awardItemIdById = ModelBase<LotteryModel>.getInstance().getAwardItemIdById(@uint);
				a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(awardItemIdById);
				num3 += 1u;
				bool flag3 = itemDataById.quality == 5;
				if (flag3)
				{
					num = @uint;
					num2 = num3 - 1u;
					break;
				}
				bool flag4 = itemDataById.quality == 4 & flag2;
				if (flag4)
				{
					flag2 = false;
					num = @uint;
					num2 = num3 - 1u;
				}
			}
			bool flag5 = flag2 && num == 0u;
			if (flag5)
			{
				uint @uint = arr[0]._uint;
				uint awardItemIdById = ModelBase<LotteryModel>.getInstance().getAwardItemIdById(@uint);
				a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(awardItemIdById);
				num = @uint;
				num2 = 0u;
			}
			for (int j = 0; j < arr.Count; j++)
			{
				uint @uint = arr[j]._uint;
				uint awardItemIdById = ModelBase<LotteryModel>.getInstance().getAwardItemIdById(@uint);
				a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(awardItemIdById);
				int awardNumById = ModelBase<LotteryModel>.getInstance().getAwardNumById(@uint);
				GameObject gameObject = IconImageMgr.getInstance().createItemIcon4Lottery(itemDataById, false, awardNumById, true, 1f, false);
				gameObject.name = "itemLotteryAward";
				gameObject.transform.SetParent(this.TipTenP, false);
				bool flag6 = @uint == num & flag;
				if (flag6)
				{
					flag = false;
					gameObject.transform.position = this.PosQuality;
					base.transform.FindChild("TipTen/Text").GetComponent<Text>().text = this.setQualityColor(itemDataById.item_name.ToString(), itemDataById.quality);
					lstTop.Add(gameObject);
					this.DrawLstOne.Add(gameObject);
					this.DrawLstOneV3.Add(this.PosQuality);
					gameObject.transform.FindChild("qicon").GetComponent<Image>().CrossFadeAlpha(0f, 0f, true);
					gameObject.transform.FindChild("bicon").GetComponent<Image>().CrossFadeAlpha(0f, 0f, true);
					gameObject.transform.FindChild("icon").GetComponent<Image>().CrossFadeAlpha(0f, 0f, true);
					base.transform.FindChild("TipTen/Text").GetComponent<Text>().CrossFadeAlpha(0f, 0f, true);
				}
				else
				{
					bool flag7 = (long)j <= (long)((ulong)num2);
					if (flag7)
					{
						gameObject.transform.position = this.PosTen[j];
						base.transform.FindChild("TipTen/GardText").GetChild(j).GetComponent<Text>().text = this.setQualityColor(itemDataById.item_name.ToString(), itemDataById.quality);
						lst.Add(gameObject);
						this.DrawLstTen.Add(gameObject);
						this.DrawLstTenV3.Add(this.PosTen[j]);
						lsx.Add(base.transform.FindChild("TipTen/GardText").GetChild(j).GetComponent<Text>());
						gameObject.transform.FindChild("qicon").GetComponent<Image>().CrossFadeAlpha(0f, 0f, true);
						gameObject.transform.FindChild("bicon").GetComponent<Image>().CrossFadeAlpha(0f, 0f, true);
						gameObject.transform.FindChild("icon").GetComponent<Image>().CrossFadeAlpha(0f, 0f, true);
						base.transform.FindChild("TipTen/GardText").GetChild(j).GetComponent<Text>().CrossFadeAlpha(0f, 0f, true);
					}
					else
					{
						gameObject.transform.position = this.PosTen[j - 1];
						base.transform.FindChild("TipTen/GardText").GetChild(j - 1).GetComponent<Text>().text = this.setQualityColor(itemDataById.item_name.ToString(), itemDataById.quality);
						lst.Add(gameObject);
						this.DrawLstTen.Add(gameObject);
						this.DrawLstTenV3.Add(this.PosTen[j - 1]);
						lsx.Add(base.transform.FindChild("TipTen/GardText").GetChild(j - 1).GetComponent<Text>());
						gameObject.transform.FindChild("qicon").GetComponent<Image>().CrossFadeAlpha(0f, 0f, true);
						gameObject.transform.FindChild("bicon").GetComponent<Image>().CrossFadeAlpha(0f, 0f, true);
						gameObject.transform.FindChild("icon").GetComponent<Image>().CrossFadeAlpha(0f, 0f, true);
						base.transform.FindChild("TipTen/GardText").GetChild(j - 1).GetComponent<Text>().CrossFadeAlpha(0f, 0f, true);
					}
				}
			}
			this._imgTip.gameObject.SetActive(true);
			this.TipTen.gameObject.SetActive(true);
			this.TipTen.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			Sequence s = DOTween.Sequence();
			Tweener t = this.TipTen.DOScale(1f, 0.5f);
			s.Append(t);
			t.OnComplete(delegate
			{
				this.ani_Add.Play("drawadd");
				this.StartCoroutine(a3_lottery.DelayToInvoke.DelayToInvokeDo(delegate
				{
					this.setTween(lst[0], lsx[0]);
				}, 0f));
				this.StartCoroutine(a3_lottery.DelayToInvoke.DelayToInvokeDo(delegate
				{
					this.setTween(lst[1], lsx[1]);
				}, 0.2f));
				this.StartCoroutine(a3_lottery.DelayToInvoke.DelayToInvokeDo(delegate
				{
					this.setTween(lst[2], lsx[2]);
				}, 0.4f));
				this.StartCoroutine(a3_lottery.DelayToInvoke.DelayToInvokeDo(delegate
				{
					this.setTween(lst[3], lsx[3]);
				}, 0.6f));
				this.StartCoroutine(a3_lottery.DelayToInvoke.DelayToInvokeDo(delegate
				{
					this.setTween(lst[4], lsx[4]);
				}, 0.8f));
				this.StartCoroutine(a3_lottery.DelayToInvoke.DelayToInvokeDo(delegate
				{
					this.setTween(lst[5], lsx[5]);
				}, 1f));
				this.StartCoroutine(a3_lottery.DelayToInvoke.DelayToInvokeDo(delegate
				{
					this.setTween(lst[6], lsx[6]);
				}, 1.2f));
				this.StartCoroutine(a3_lottery.DelayToInvoke.DelayToInvokeDo(delegate
				{
					this.setTween(lst[7], lsx[7]);
				}, 1.4f));
				this.StartCoroutine(a3_lottery.DelayToInvoke.DelayToInvokeDo(delegate
				{
					this.setTween(lst[8], lsx[8]);
				}, 1.6f));
				this.StartCoroutine(a3_lottery.DelayToInvoke.DelayToInvokeDo(delegate
				{
					this.setTween(lst[9], lsx[9]);
				}, 1.8f));
				this.StartCoroutine(a3_lottery.DelayToInvoke.DelayToInvokeDo(delegate
				{
					this.setTween(lstTop[0], null);
				}, 2f));
			});
		}

		public string GetLotteryItemNameColor(string name, int quality)
		{
			return this.setQualityColor(name, quality);
		}

		private string setQualityColor(string name, int quality)
		{
			bool flag = quality == 1;
			string result;
			if (flag)
			{
				result = "<color=#ffffff>" + name + "</color>";
			}
			else
			{
				bool flag2 = quality == 2;
				if (flag2)
				{
					result = "<color=#00FF00>" + name + "</color>";
				}
				else
				{
					bool flag3 = quality == 3;
					if (flag3)
					{
						result = "<color=#66FFFF>" + name + "</color>";
					}
					else
					{
						bool flag4 = quality == 4;
						if (flag4)
						{
							result = "<color=#FF00FF>" + name + "</color>";
						}
						else
						{
							bool flag5 = quality == 5;
							if (flag5)
							{
								result = "<color=#f7790a>" + name + "</color>";
							}
							else
							{
								bool flag6 = quality == 6;
								if (flag6)
								{
									result = "<color=#f90e0e>" + name + "</color>";
								}
								else
								{
									bool flag7 = quality == 7;
									if (flag7)
									{
										result = "<color=#f90e0e>" + name + "</color>";
									}
									else
									{
										result = "<color=#ffffff>" + name + "</color>";
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		private void setTween(GameObject go1, Text go2)
		{
			bool flag = go1;
			if (flag)
			{
				go1.transform.FindChild("qicon").GetComponent<Image>().CrossFadeAlpha(1f, 0.2f, true);
				go1.transform.FindChild("bicon").GetComponent<Image>().CrossFadeAlpha(1f, 0.2f, true);
				go1.transform.FindChild("icon").GetComponent<Image>().CrossFadeAlpha(1f, 0.2f, true);
				bool flag2 = go2;
				if (flag2)
				{
					go2.CrossFadeAlpha(1f, 0.2f, true);
				}
				else
				{
					base.transform.FindChild("TipTen/Text").GetComponent<Text>().CrossFadeAlpha(1f, 0.2f, true);
				}
			}
		}

		private void onEquipClick(GameObject gb, uint id)
		{
			debug.Log("点击icon打开装备预览界面");
			ArrayList arrayList = new ArrayList();
			arrayList.Add(new a3_BagItemData
			{
				id = id,
				num = 1,
				ismark = false,
				confdata = ModelBase<a3_BagModel>.getInstance().getItemDataById(id)
			});
			arrayList.Add(equip_tip_type.BagPick_tip);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIPTIP, arrayList, false);
		}

		private void setDrawButtonUnEnable(a3_lottery.DrawType dt)
		{
			this.cureentDrawType = dt;
		}

		private void Update()
		{
			this.updateTime -= Time.deltaTime;
			this.tfLeftTimeParent = this.txtLeftTimes.transform.parent;
			bool flag = this.timesDraw <= 0;
			if (flag)
			{
				this.btn_freeOnce.gameObject.SetActive(false);
				this.txtLeftTimes.gameObject.SetActive(true);
				for (int i = 0; i < this.tfLeftTimeParent.childCount; i++)
				{
					bool flag2 = this.tfLeftTimeParent.GetChild(i).name != "Times";
					if (flag2)
					{
						this.tfLeftTimeParent.GetChild(i).gameObject.SetActive(false);
					}
				}
				this.txtIceOnce.text = "今日免费占卜次数已用完";
				this.btn_iceOnce.gameObject.SetActive(true);
				this.txtIceOnce.gameObject.SetActive(true);
			}
			else
			{
				bool flag3 = this.timer <= 0f;
				if (flag3)
				{
					this.btn_freeOnce.gameObject.SetActive(true);
					this.txtLeftTimes.gameObject.SetActive(true);
					for (int j = 0; j < this.tfLeftTimeParent.childCount; j++)
					{
						this.tfLeftTimeParent.GetChild(j).gameObject.SetActive(true);
					}
					this.txtLeftTimes.text = this.timesDraw.ToString();
					this.btn_iceOnce.gameObject.SetActive(false);
					this.txtIceOnce.gameObject.SetActive(false);
				}
				else
				{
					this.btn_freeOnce.gameObject.SetActive(false);
					this.txtLeftTimes.gameObject.SetActive(false);
					this.btn_iceOnce.gameObject.SetActive(true);
					this.txtIceOnce.gameObject.SetActive(true);
					bool flag4 = this.updateTime <= 0f;
					if (flag4)
					{
						this.updateTime = 1f;
						this.timer -= 1f;
						this.outTime = (int)this.timer;
						string text = ((this.outTime / 60 / 60).ToString().Length > 1) ? (this.outTime / 60 / 60).ToString() : ("0" + this.outTime / 60 / 60);
						string text2 = ((this.outTime / 60 % 60).ToString().Length > 1) ? (this.outTime / 60 % 60).ToString() : ("0" + this.outTime / 60 % 60);
						string text3 = ((this.outTime % 60).ToString().Length > 1) ? (this.outTime % 60).ToString() : ("0" + this.outTime % 60);
						string text4 = string.Concat(new string[]
						{
							text,
							":",
							text2,
							":",
							text3,
							"后免费"
						});
						this.txtIceOnce.text = text4;
					}
				}
			}
			this.textTime -= Time.deltaTime;
			bool flag5 = this.textTime <= 0f;
			if (flag5)
			{
				this.textTime = 5.6f;
				BaseProxy<LotteryProxy>.getInstance().sendlottery(1);
			}
		}

		private void onUpdateLottery(float s)
		{
			this.times += s;
			bool flag = this.times >= 1f;
			if (flag)
			{
				this.i--;
				bool flag2 = this.i <= 0;
				if (flag2)
				{
					this.i = 0;
					this.setFreeOnceDrawText(0, true);
					TickMgr.instance.removeTick(this.tickItemTime);
					this.tickItemTime = null;
				}
				else
				{
					this.setFreeOnceDrawText(this.i, false);
				}
				this.times = 0f;
			}
		}

		private void setHintFreeInfo(string info)
		{
			bool flag = !this.bg_hintFree.gameObject.activeSelf;
			if (flag)
			{
				this.bg_hintFree.gameObject.SetActive(true);
			}
			this.hintFreeText.text = info;
		}

		private void setHitIceInfo()
		{
			MsgBoxMgr.getInstance().showConfirm("钻石不足,是否前往充值?", new UnityAction(this.backDepositHandle), null, 0);
		}

		private void backDepositHandle()
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_RECHARGE, null, false);
		}

		public void onGetAcquisitionAwardInfo(GameEvent e)
		{
			itemLotteryAwardInfoData itemLotteryAwardInfoData = new itemLotteryAwardInfoData();
			itemLotteryAwardInfoData.name = e.data["name"]._str;
			itemLotteryAwardInfoData.tpid = uint.Parse(e.data["itemId"]);
			itemLotteryAwardInfoData.cnt = uint.Parse(e.data["cnt"]);
			itemLotteryAwardInfoData.stage = uint.Parse(e.data["stage"]);
			GameObject gameObject = IconImageMgr.getInstance().createLotteryInfo(itemLotteryAwardInfoData, false, -1, 1f);
			gameObject.name = "lotteryItemAwardInfo";
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.SetSiblingIndex(0);
		}
	}
}
