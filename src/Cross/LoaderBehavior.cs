using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Cross
{
	public class LoaderBehavior : MonoBehaviour
	{
		protected static string UTF8_BOM = Encoding.UTF8.GetString(new byte[]
		{
			239,
			187,
			191
		});

		public static int MAX_LOADJOBS = 3;

		public static Action ms_AllLoadedFin;

		public static bool ms_HasAllLoaded = true;

		private static int ALL_LOADING_CALLBACK_W = 2;

		private static int ms_LoadingProCount = 0;

		private static int ms_NoLoadingCount = LoaderBehavior.ALL_LOADING_CALLBACK_W;

		protected int m_loadingJobs = 0;

		protected LinkedList<LoaderJob> m_jobs = new LinkedList<LoaderJob>();

		protected HashSet<string> m_uniJobs = new HashSet<string>();

		public static string DATA_PATH = "";

		private void Start()
		{
		}

		public void load(string url, string method, string dataFmt, string paras, Action<object> onFin, Action<float> onProg = null, Action<string> onFail = null)
		{
			dataFmt = dataFmt.ToLower();
			bool flag = url.IndexOf("http:") >= 0;
			if (flag)
			{
				base.StartCoroutine(this._httpReq(url, method, dataFmt, paras, onFin, onProg, onFail));
			}
			else
			{
				bool flag2 = dataFmt == "assetbundle";
				if (flag2)
				{
					bool unique = false;
					bool flag3 = url.IndexOf(".anim") >= 0;
					if (flag3)
					{
						string item = url.Substring(url.LastIndexOf('/') + 1);
						bool flag4 = this.m_uniJobs.Contains(item);
						if (flag4)
						{
							this.m_jobs.AddLast(new LoaderJob(url, method, dataFmt, paras, onFin, onProg, onFail));
							return;
						}
						unique = true;
					}
					bool flag5 = this.m_loadingJobs < LoaderBehavior.MAX_LOADJOBS;
					if (flag5)
					{
						base.StartCoroutine(this._loadRes(url, method, dataFmt, paras, unique, onFin, onProg, onFail));
					}
					else
					{
						this.m_jobs.AddLast(new LoaderJob(url, method, dataFmt, paras, onFin, onProg, onFail));
					}
				}
				else
				{
					base.StartCoroutine(this._loadRes(url, method, dataFmt, paras, false, onFin, onProg, onFail));
				}
			}
		}

		public void syncLoad(string url, string method, string dataFmt, string paras, Action<object> onFin, Action<float> onProg = null, Action<string> onFail = null)
		{
		}

		private void Update()
		{
			bool flag = this.m_loadingJobs < LoaderBehavior.MAX_LOADJOBS && this.m_jobs.Count > 0;
			if (flag)
			{
				LoaderJob value = this.m_jobs.First.Value;
				this.m_jobs.RemoveFirst();
				this.load(value.url, value.method, value.dataFmt, value.paras, value.onFin, value.onProg, value.onFail);
			}
			bool flag2 = LoaderBehavior.ms_LoadingProCount > 0;
			if (flag2)
			{
				LoaderBehavior.ms_NoLoadingCount = LoaderBehavior.ALL_LOADING_CALLBACK_W;
			}
			else
			{
				bool flag3 = LoaderBehavior.ms_NoLoadingCount > 0;
				if (flag3)
				{
					LoaderBehavior.ms_NoLoadingCount--;
				}
				else
				{
					bool flag4 = LoaderBehavior.ms_AllLoadedFin != null;
					if (flag4)
					{
						LoaderBehavior.ms_AllLoadedFin();
						LoaderBehavior.ms_AllLoadedFin = null;
						LoaderBehavior.ms_NoLoadingCount = LoaderBehavior.ALL_LOADING_CALLBACK_W;
					}
					LoaderBehavior.ms_HasAllLoaded = true;
				}
			}
		}

		protected WWW _evalWWW(WWW w, Action<float> onProg, float offset, float scale)
		{
			bool flag = onProg != null;
			if (flag)
			{
				onProg(offset + w.progress * scale);
			}
			return w;
		}

		protected AssetBundleRequest _evalAssetBundleReq(AssetBundleRequest abReq, Action<float> onProg, float offset, float scale)
		{
			bool flag = onProg != null;
			if (flag)
			{
				onProg(offset + abReq.progress * scale);
			}
			return abReq;
		}

		protected IEnumerator _httpReq(string url, string method, string dataFmt, string paras, Action<object> onFin, Action<float> onProg = null, Action<string> onFail = null)
		{
			WWW wWW = null;
			bool flag = method.ToLower() == "get";
			if (flag)
			{
				bool flag2 = paras != null && paras.Length > 0;
				if (flag2)
				{
					url = url + "?" + paras;
				}
				wWW = new WWW(url);
			}
			else
			{
				WWWForm wWWForm = new WWWForm();
				bool flag3 = paras != null && paras.Length > 0;
				if (flag3)
				{
					string[] array = paras.Split(new char[]
					{
						'&'
					});
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string text = array2[i];
						int num = text.IndexOf('=');
						bool flag4 = num != text.Length - 1;
						if (flag4)
						{
							wWWForm.AddField(text.Substring(0, num), text.Substring(num + 1));
						}
						else
						{
							wWWForm.AddField(text.Substring(0, num), "");
						}
						text = null;
					}
					array2 = null;
					array = null;
				}
				wWW = new WWW(url, wWWForm);
				wWWForm = null;
			}
			yield return this._evalWWW(wWW, onProg, 0f, 1f);
			bool flag5 = wWW.error != null;
			if (flag5)
			{
				Debug.Log("fail: " + url + " : " + wWW.error);
				bool flag6 = onFail != null;
				if (flag6)
				{
					onFail(wWW.error);
				}
			}
			else
			{
				bool flag7 = onFin != null;
				if (flag7)
				{
					bool flag8 = dataFmt.ToLower() == "binary";
					if (flag8)
					{
						onFin(wWW.bytes);
					}
					else
					{
						bool flag9 = dataFmt.ToLower() == "text";
						if (flag9)
						{
							onFin(wWW.text);
						}
					}
				}
			}
			wWW.Dispose();
			yield break;
		}

		protected IEnumerator _loadRes(string url, string method, string dataFmt, string paras, bool unique, Action<object> onFin, Action<float> onProg = null, Action<string> onFail = null)
		{
			bool flag = dataFmt == "assetbundle";
			if (flag)
			{
				this.m_loadingJobs++;
			}
			if (unique)
			{
				string item = url.Substring(url.LastIndexOf('/') + 1);
				this.m_uniJobs.Add(item);
				item = null;
			}
			string message = LoaderBehavior._getStreamingFilePath(url, dataFmt);
			url = "file:///" + LoaderBehavior.DATA_PATH + "OutAssets/" + url;
			Debug.Log(message);
			message = null;
			bool flag2 = paras != null && paras.Length > 0;
			if (flag2)
			{
				url = url + "?" + paras;
			}
			WWW wWW = new WWW(url);
			LoaderBehavior.ms_LoadingProCount++;
			LoaderBehavior.ms_HasAllLoaded = false;
			yield return this._evalWWW(wWW, onProg, 0f, 0.3f);
			bool flag3 = wWW.error != null;
			if (flag3)
			{
				Debug.Log("fail: " + url + " : " + wWW.error);
				bool flag4 = onFail != null;
				if (flag4)
				{
					onFail(wWW.error);
				}
			}
			else
			{
				bool flag5 = onFin != null;
				if (flag5)
				{
					bool flag6 = dataFmt == "binary";
					if (flag6)
					{
						onFin(wWW.bytes);
					}
					else
					{
						bool flag7 = dataFmt == "text";
						if (flag7)
						{
							onFin(wWW.text);
						}
						else
						{
							bool flag8 = dataFmt == "assetbundle";
							if (flag8)
							{
								AssetBundle assetBundle = null;
								while (assetBundle == null)
								{
									try
									{
										assetBundle = wWW.assetBundle;
									}
									catch (Exception ex2)
									{
										Exception ex = ex2;
										Debug.Log("URL Error " + ex.Message);
									}
									bool flag9 = assetBundle == null;
									if (flag9)
									{
										yield return new WaitForSeconds(1f);
									}
								}
								AssetBundleRequest assetBundleRequest = null;
								bool flag10 = true;
								bool flag11 = url.IndexOf(".pic") >= 0;
								if (flag11)
								{
									UnityEngine.Object @object = assetBundle.Load(LoaderBehavior._getAssetBundleName(url), typeof(Sprite));
									bool flag12 = @object != null;
									if (flag12)
									{
										onFin(@object);
									}
									else
									{
										bool flag13 = onFail != null;
										if (flag13)
										{
											onFail("AssebBundle.Load Sprite return null!");
										}
									}
									@object = null;
								}
								else
								{
									bool flag14 = url.IndexOf(".anipic") >= 0;
									if (flag14)
									{
										UnityEngine.Object[] array = assetBundle.LoadAll(typeof(Sprite));
										bool flag15 = array != null;
										if (flag15)
										{
											onFin(array);
										}
										else
										{
											bool flag16 = onFail != null;
											if (flag16)
											{
												onFail("AssebBundle.LoadAll Sprite return null!");
											}
										}
										array = null;
									}
									else
									{
										bool flag17 = url.IndexOf(".unity") >= 0;
										if (flag17)
										{
											onFin(assetBundle);
											flag10 = false;
										}
										else
										{
											bool flag18 = url.IndexOf(".anim") >= 0;
											if (flag18)
											{
												assetBundleRequest = assetBundle.LoadAsync(LoaderBehavior._getAssetBundleName(url), typeof(AnimationClip));
											}
											else
											{
												bool flag19 = url.IndexOf(".lmp") >= 0;
												if (flag19)
												{
													assetBundleRequest = assetBundle.LoadAsync(LoaderBehavior._getAssetBundleName(url), typeof(Texture));
												}
												else
												{
													bool flag20 = url.IndexOf(".lpb") >= 0;
													if (flag20)
													{
														assetBundleRequest = assetBundle.LoadAsync(LoaderBehavior._getAssetBundleName(url), typeof(LightProbes));
													}
													else
													{
														bool flag21 = url.IndexOf(".snd") >= 0;
														if (flag21)
														{
															assetBundleRequest = assetBundle.LoadAsync(LoaderBehavior._getAssetBundleName(url), typeof(AudioClip));
														}
														else
														{
															assetBundleRequest = assetBundle.LoadAsync(LoaderBehavior._getAssetBundleName(url), typeof(GameObject));
														}
													}
												}
											}
											yield return this._evalAssetBundleReq(assetBundleRequest, onProg, 0.3f, 0.7f);
											onFin(assetBundleRequest.asset);
										}
									}
								}
								bool flag22 = flag10;
								if (flag22)
								{
									assetBundle.Unload(false);
								}
								assetBundle = null;
								assetBundleRequest = null;
							}
						}
					}
				}
			}
			wWW.Dispose();
			bool flag23 = dataFmt == "assetbundle";
			if (flag23)
			{
				this.m_loadingJobs--;
			}
			if (unique)
			{
				string item2 = url.Substring(url.LastIndexOf('/') + 1);
				this.m_uniJobs.Remove(item2);
				item2 = null;
			}
			LoaderBehavior.ms_LoadingProCount--;
			yield break;
		}

		protected static string _getAssetBundleName(string url)
		{
			int num = url.LastIndexOf('/') + 1;
			int num2 = url.LastIndexOf('.');
			return url.Substring(num, num2 - num);
		}

		protected static string _getDownLoadFilePath(string filename, string dataFmt)
		{
			bool flag = dataFmt == "assetbundle";
			if (flag)
			{
				bool flag2 = Application.platform == RuntimePlatform.IPhonePlayer;
				if (flag2)
				{
					filename = "ios/" + filename;
				}
				else
				{
					bool flag3 = Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer;
					if (flag3)
					{
						filename = "win/" + filename;
					}
					else
					{
						filename = "android/" + filename;
					}
				}
			}
			return AssetManagerImpl.UPDATE_DOWN_PATH + filename;
		}

		protected static string _getStreamingFilePath(string filename, string dataFmt)
		{
			bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer;
			string result;
			if (flag)
			{
				result = "file:///" + LoaderBehavior.DATA_PATH + "OutAssets/" + filename;
			}
			else
			{
				bool flag2 = Application.platform == RuntimePlatform.IPhonePlayer;
				if (flag2)
				{
					result = "file://" + Application.dataPath + "/Raw/OutAssets/" + filename;
				}
				else
				{
					bool flag3 = Application.platform == RuntimePlatform.Android;
					if (flag3)
					{
						result = "jar:file://" + Application.dataPath + "!/assets/OutAssets/" + filename;
					}
					else
					{
						result = Application.dataPath + "/config/" + filename;
					}
				}
			}
			return result;
		}
	}
}
