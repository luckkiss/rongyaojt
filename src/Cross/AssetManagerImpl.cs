using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace Cross
{
	public class AssetManagerImpl : IAssetManager
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly AssetManagerImpl.<>c <>9 = new AssetManagerImpl.<>c();

			public static Action<IURLReq, string> <>9__32_2;

			public static Action<IURLReq, string> <>9__32_3;

			public static Action<IURLReq, string> <>9__33_2;

			public static Action<IURLReq, string> <>9__33_3;

			public static Action<IURLReq, float> <>9__43_1;

			internal void <checkUpdateAssets>b__32_2(IURLReq server_r, string err)
			{
				AssetManagerImpl.m_updateOnError(201, AssetManagerImpl.m_strVerName + ".ver");
				AssetManagerImpl.m_updateOnEnd(false);
			}

			internal void <checkUpdateAssets>b__32_3(IURLReq local_r, string err)
			{
				AssetManagerImpl.m_updateOnError(101, AssetManagerImpl.m_strVerName + ".ver");
				AssetManagerImpl.m_updateOnEnd(false);
			}

			internal void <startUpdateFileList>b__33_2(IURLReq server_r, string err)
			{
				AssetManagerImpl.m_updateOnError(401, AssetManagerImpl.m_strVerName);
				AssetManagerImpl.m_updateOnEnd(false);
			}

			internal void <startUpdateFileList>b__33_3(IURLReq local_r, string err)
			{
				AssetManagerImpl.m_updateOnError(301, AssetManagerImpl.m_strVerName);
				AssetManagerImpl.m_updateOnEnd(false);
			}

			internal void <onProcess>b__43_1(IURLReq r, float progress)
			{
				float num = (float)AssetManagerImpl.m_curDownedSize + (float)AssetManagerImpl.m_curDowningFileSize * progress;
				AssetManagerImpl.m_updateOnProg(num / (float)AssetManagerImpl.m_curNeedDownSize);
			}
		}

		protected Dictionary<string, AssetImpl> m_assetMap = new Dictionary<string, AssetImpl>();

		protected static List<AssetImpl> m_readyAssets = new List<AssetImpl>();

		protected static int m_autoDisposeIdx = 0;

		protected static ulong m_autoDisposeCounter = 0uL;

		protected static bool m_bTryAsync = true;

		public static string UPDATE_DOWN_PATH;

		public static bool START_UPDATEGAME;

		protected static string m_localVer;

		protected static string m_serverVer;

		protected static string m_localFileList;

		protected static string m_serverFileList;

		protected static string m_strUpdateServerUrl;

		protected static string m_strVerName;

		protected static ArrayList m_needUpdateFiles = null;

		protected static List<int> m_needUpdateFileSize = null;

		protected static int m_curUpdateFileIndex = 0;

		protected static int m_curDowningFileSize = 0;

		protected static int m_curDownedSize = 0;

		protected static int m_curNeedDownSize = 1;

		protected static bool m_bUpdateEnd_Success = false;

		protected static Action<int> m_updateOnBegin;

		protected static Action<float> m_updateOnProg;

		protected static Action<bool> m_updateOnEnd;

		protected static Action<int, string> m_updateOnError;

		protected Variant m_conf;

		public bool async
		{
			get
			{
				return AssetManagerImpl.m_bTryAsync;
			}
			set
			{
				AssetManagerImpl.m_bTryAsync = value;
			}
		}

		public int maxLoadingNum
		{
			get
			{
				return LoaderBehavior.MAX_LOADJOBS;
			}
			set
			{
				LoaderBehavior.MAX_LOADJOBS = value;
			}
		}

		public string rootPath
		{
			get
			{
				return Application.dataPath + "/Resources";
			}
		}

		public void checkUpdateAssets(string url, Action<int> onBegin, Action<float> onProg, Action<bool> onEnd, Action<int, string> onError)
		{
			AssetManagerImpl.START_UPDATEGAME = false;
			AssetManagerImpl.m_strUpdateServerUrl = url;
			AssetManagerImpl.m_bUpdateEnd_Success = true;
			AssetManagerImpl.m_curDownedSize = 0;
			AssetManagerImpl.m_curNeedDownSize = 1;
			bool flag = Application.platform == RuntimePlatform.IPhonePlayer;
			if (flag)
			{
				AssetManagerImpl.m_strVerName = "ios_version.ver";
			}
			else
			{
				bool flag2 = Application.platform == RuntimePlatform.Android;
				if (flag2)
				{
					AssetManagerImpl.m_strVerName = "android_version.ver";
				}
				else
				{
					AssetManagerImpl.m_strVerName = "win_version.ver";
				}
			}
			AssetManagerImpl.m_updateOnBegin = onBegin;
			AssetManagerImpl.m_updateOnProg = onProg;
			AssetManagerImpl.m_updateOnEnd = onEnd;
			AssetManagerImpl.m_updateOnError = onError;
			AssetManagerImpl.m_localVer = "????????";
			URLReqImpl arg_EC_0 = new URLReqImpl
			{
				dataFormat = "text",
				url = AssetManagerImpl.m_strVerName + ".ver"
			};
			Action<IURLReq, object> <>9__1;
			Action<IURLReq, object> arg_EC_1 = delegate(IURLReq local_r, object local_ret)
			{
				AssetManagerImpl.m_localVer = (local_ret as string);
				Debug.Log("local_ver = " + AssetManagerImpl.m_localVer);
				DownLoadURLImpl arg_8E_0 = new DownLoadURLImpl
				{
					dataFormat = "text",
					url = AssetManagerImpl.m_strUpdateServerUrl + AssetManagerImpl.m_strVerName + ".ver"
				};
				Action<IURLReq, object> arg_8E_1;
				if ((arg_8E_1 = <>9__1) == null)
				{
					arg_8E_1 = (<>9__1 = delegate(IURLReq server_r, object server_ret)
					{
						AssetManagerImpl.m_serverVer = (server_ret as string);
						Debug.Log("server_ver = " + AssetManagerImpl.m_serverVer);
						bool flag3 = AssetManagerImpl.m_serverVer != AssetManagerImpl.m_localVer;
						if (flag3)
						{
							this.startUpdateFileList();
						}
						else
						{
							onBegin(0);
						}
					});
				}
				Action<IURLReq, float> arg_8E_2 = null;
				Action<IURLReq, string> arg_8E_3;
				if ((arg_8E_3 = AssetManagerImpl.<>c.<>9__32_2) == null)
				{
					arg_8E_3 = (AssetManagerImpl.<>c.<>9__32_2 = new Action<IURLReq, string>(AssetManagerImpl.<>c.<>9.<checkUpdateAssets>b__32_2));
				}
				arg_8E_0.load(arg_8E_1, arg_8E_2, arg_8E_3);
			};
			Action<IURLReq, float> arg_EC_2 = null;
			Action<IURLReq, string> arg_EC_3;
			if ((arg_EC_3 = AssetManagerImpl.<>c.<>9__32_3) == null)
			{
				arg_EC_3 = (AssetManagerImpl.<>c.<>9__32_3 = new Action<IURLReq, string>(AssetManagerImpl.<>c.<>9.<checkUpdateAssets>b__32_3));
			}
			arg_EC_0.load(arg_EC_1, arg_EC_2, arg_EC_3);
		}

		private void startUpdateFileList()
		{
			URLReqImpl arg_4C_0 = new URLReqImpl
			{
				dataFormat = "text",
				url = AssetManagerImpl.m_strVerName
			};
			Action<IURLReq, object> arg_4C_1 = delegate(IURLReq local_r, object local_ret)
			{
				AssetManagerImpl.m_localFileList = (local_ret as string);
				Debug.Log("m_localFileList = " + AssetManagerImpl.m_localFileList);
				DownLoadURLImpl arg_76_0 = new DownLoadURLImpl
				{
					dataFormat = "text",
					url = AssetManagerImpl.m_strUpdateServerUrl + AssetManagerImpl.m_strVerName
				};
				Action<IURLReq, object> arg_76_1 = delegate(IURLReq server_r, object server_ret)
				{
					AssetManagerImpl.m_serverFileList = (server_ret as string);
					Debug.Log("m_serverFileList = " + AssetManagerImpl.m_serverFileList);
					this.progUpdateAllFiles();
				};
				Action<IURLReq, float> arg_76_2 = null;
				Action<IURLReq, string> arg_76_3;
				if ((arg_76_3 = AssetManagerImpl.<>c.<>9__33_2) == null)
				{
					arg_76_3 = (AssetManagerImpl.<>c.<>9__33_2 = new Action<IURLReq, string>(AssetManagerImpl.<>c.<>9.<startUpdateFileList>b__33_2));
				}
				arg_76_0.load(arg_76_1, arg_76_2, arg_76_3);
			};
			Action<IURLReq, float> arg_4C_2 = null;
			Action<IURLReq, string> arg_4C_3;
			if ((arg_4C_3 = AssetManagerImpl.<>c.<>9__33_3) == null)
			{
				arg_4C_3 = (AssetManagerImpl.<>c.<>9__33_3 = new Action<IURLReq, string>(AssetManagerImpl.<>c.<>9.<startUpdateFileList>b__33_3));
			}
			arg_4C_0.load(arg_4C_1, arg_4C_2, arg_4C_3);
		}

		private void progUpdateAllFiles()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string[] array = AssetManagerImpl.m_localFileList.Split(new char[]
			{
				'\n'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					','
				});
				bool flag = array2.Length == 3;
				if (flag)
				{
					dictionary.Add(array2[0], array2[1]);
				}
			}
			int num = 0;
			array = AssetManagerImpl.m_serverFileList.Split(new char[]
			{
				'\n'
			});
			AssetManagerImpl.m_needUpdateFiles = new ArrayList();
			AssetManagerImpl.m_needUpdateFileSize = new List<int>();
			for (int j = 0; j < array.Length; j++)
			{
				string[] array3 = array[j].Split(new char[]
				{
					','
				});
				bool flag2 = array3.Length == 3;
				if (flag2)
				{
					bool flag3 = dictionary.ContainsKey(array3[0]);
					if (flag3)
					{
						bool flag4 = dictionary[array3[0]] != array3[1];
						if (flag4)
						{
							AssetManagerImpl.m_needUpdateFiles.Add(array3[0]);
							int num2 = int.Parse(array3[2]);
							num += num2;
							AssetManagerImpl.m_needUpdateFileSize.Add(num2);
						}
					}
					else
					{
						AssetManagerImpl.m_needUpdateFiles.Add(array3[0]);
						int num3 = int.Parse(array3[2]);
						num += num3;
						AssetManagerImpl.m_needUpdateFileSize.Add(num3);
					}
				}
			}
			AssetManagerImpl.m_curDownedSize = 0;
			AssetManagerImpl.m_curNeedDownSize = num;
			AssetManagerImpl.m_updateOnBegin(num);
			Debug.Log("need_update_files count = " + AssetManagerImpl.m_needUpdateFiles.Count);
		}

		public void preloadShaders()
		{
			AssetShaderImpl assetShaderImpl = this.getAsset<IAssetShader>("mono/shaders/img_base") as AssetShaderImpl;
			assetShaderImpl.autoDisposeTime = -1f;
			assetShaderImpl.loadImpl(true);
			AssetShaderImpl assetShaderImpl2 = this.getAsset<IAssetShader>("mono/shaders/text_base") as AssetShaderImpl;
			assetShaderImpl2.autoDisposeTime = -1f;
			assetShaderImpl2.loadImpl(true);
		}

		public T getAsset<T>(string assetPath) where T : class, IAsset
		{
			return this.getAsset<T>(assetPath, null, null, null);
		}

		public T getAsset<T>(string assetPath, Action<IAsset> onFin, Action<IAsset, float> onProg, Action<IAsset, string> onFail) where T : class, IAsset
		{
			bool flag = assetPath == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				string str = typeof(T).ToString();
				bool flag2 = this.m_assetMap.ContainsKey(str + "$" + assetPath);
				if (flag2)
				{
					AssetImpl assetImpl = this.m_assetMap[str + "$" + assetPath];
					bool flag3 = assetImpl.isReady && onFin != null;
					if (flag3)
					{
						onFin(assetImpl);
					}
					else
					{
						assetImpl.addCallbacks(onFin, onProg, onFail);
					}
					result = (assetImpl as T);
				}
				else
				{
					AssetImpl assetImpl2 = null;
					Type typeFromHandle = typeof(T);
					bool flag4 = typeFromHandle.Equals(typeof(IAssetMesh));
					if (flag4)
					{
						assetImpl2 = new AssetMeshImpl();
					}
					else
					{
						bool flag5 = typeFromHandle.Equals(typeof(IAssetBitmap));
						if (flag5)
						{
							assetImpl2 = new AssetBitmapImpl();
						}
						else
						{
							bool flag6 = typeFromHandle.Equals(typeof(IAssetSkAniMesh));
							if (flag6)
							{
								assetImpl2 = new AssetSkAniMeshImpl();
							}
							else
							{
								bool flag7 = typeFromHandle.Equals(typeof(IAssetSkAnimation));
								if (flag7)
								{
									assetImpl2 = new AssetSkAnimationImpl();
								}
								else
								{
									bool flag8 = typeFromHandle.Equals(typeof(IAssetAniBitmap));
									if (flag8)
									{
										assetImpl2 = new AssetAniBitmapImpl();
									}
									else
									{
										bool flag9 = typeFromHandle.Equals(typeof(IAssetParticles));
										if (flag9)
										{
											assetImpl2 = new AssetParticlesImpl();
										}
										else
										{
											bool flag10 = typeFromHandle.Equals(typeof(IAssetMaterial));
											if (flag10)
											{
												assetImpl2 = new AssetMaterialImpl();
											}
											else
											{
												bool flag11 = typeFromHandle.Equals(typeof(IAssetHeightMap));
												if (flag11)
												{
													assetImpl2 = new AssetHeightMapImpl();
												}
												else
												{
													bool flag12 = typeFromHandle.Equals(typeof(IAssetAudio));
													if (flag12)
													{
														assetImpl2 = new AssetAudioImpl();
													}
													else
													{
														bool flag13 = typeFromHandle.Equals(typeof(IAssetTexture));
														if (flag13)
														{
															assetImpl2 = new AssetTextureImpl();
														}
														else
														{
															bool flag14 = typeFromHandle.Equals(typeof(IAssetShader));
															if (flag14)
															{
																assetImpl2 = new AssetShaderImpl();
															}
															else
															{
																bool flag15 = typeFromHandle.Equals(typeof(IAssetRenderTexture));
																if (flag15)
																{
																	assetImpl2 = new AssetRenderTextureImpl();
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					bool flag16 = assetImpl2 != null;
					if (flag16)
					{
						this.m_assetMap[str + "$" + assetPath] = assetImpl2;
						assetImpl2.path = assetPath;
						assetImpl2.addCallbacks(onFin, onProg, onFail);
						assetImpl2.load();
						result = (assetImpl2 as T);
					}
					else
					{
						result = default(T);
					}
				}
			}
			return result;
		}

		public void readyAsset(AssetImpl ass)
		{
			AssetManagerImpl.m_readyAssets.Add(ass);
		}

		public static void preparePath(string path)
		{
			bool flag = File.Exists(path);
			if (!flag)
			{
				path = path.Substring(0, path.LastIndexOf('/'));
				bool flag2 = File.Exists(path);
				if (!flag2)
				{
					string[] array = path.Split(new char[]
					{
						'/'
					});
					for (int i = 2; i <= array.Length; i++)
					{
						string text = "";
						for (int j = 0; j < i; j++)
						{
							text += array[j];
							text += "/";
						}
						bool flag3 = !Directory.Exists(text);
						if (flag3)
						{
							Directory.CreateDirectory(text);
						}
					}
				}
			}
		}

		private void saveLocalVer()
		{
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(AssetManagerImpl.m_serverFileList);
				string text = AssetManagerImpl.UPDATE_DOWN_PATH + AssetManagerImpl.m_strVerName;
				AssetManagerImpl.preparePath(text);
				FileStream fileStream = new FileStream(text, FileMode.Create);
				fileStream.Write(bytes, 0, bytes.Length);
				fileStream.Flush();
				fileStream.Close();
				Debug.Log("写入文件列表成功 " + text);
			}
			catch (Exception ex)
			{
				AssetManagerImpl.m_curDowningFileSize = 0;
				AssetManagerImpl.m_curUpdateFileIndex = AssetManagerImpl.m_needUpdateFiles.Count;
				AssetManagerImpl.m_bUpdateEnd_Success = false;
				AssetManagerImpl.m_updateOnError(601, AssetManagerImpl.m_strVerName + "  Exception " + ex.Message);
			}
			try
			{
				byte[] bytes2 = Encoding.UTF8.GetBytes(AssetManagerImpl.m_serverVer);
				string text2 = AssetManagerImpl.UPDATE_DOWN_PATH + AssetManagerImpl.m_strVerName + ".ver";
				AssetManagerImpl.preparePath(text2);
				FileStream fileStream2 = new FileStream(text2, FileMode.Create);
				fileStream2.Write(bytes2, 0, bytes2.Length);
				fileStream2.Flush();
				fileStream2.Close();
				Debug.Log("写入文件标示成功 " + text2);
			}
			catch (Exception ex2)
			{
				AssetManagerImpl.m_curDowningFileSize = 0;
				AssetManagerImpl.m_curUpdateFileIndex = AssetManagerImpl.m_needUpdateFiles.Count;
				AssetManagerImpl.m_bUpdateEnd_Success = false;
				AssetManagerImpl.m_updateOnError(701, AssetManagerImpl.m_strVerName + ".ver  Exception " + ex2.Message);
			}
		}

		public void onProcess(float tmSlice)
		{
			bool flag = AssetManagerImpl.m_needUpdateFiles != null && AssetManagerImpl.START_UPDATEGAME;
			if (flag)
			{
				bool flag2 = AssetManagerImpl.m_curUpdateFileIndex < AssetManagerImpl.m_needUpdateFiles.Count;
				if (flag2)
				{
					bool flag3 = AssetManagerImpl.m_curDowningFileSize == 0;
					if (flag3)
					{
						AssetManagerImpl.m_curDowningFileSize = AssetManagerImpl.m_needUpdateFileSize[AssetManagerImpl.m_curUpdateFileIndex];
						bool flag4 = AssetManagerImpl.m_curDowningFileSize <= 0;
						if (flag4)
						{
							AssetManagerImpl.m_curDowningFileSize = 1;
						}
						DownLoadURLImpl urlreqimpl = new DownLoadURLImpl();
						urlreqimpl.dataFormat = "binary";
						urlreqimpl.url = AssetManagerImpl.m_strUpdateServerUrl + AssetManagerImpl.m_needUpdateFiles[AssetManagerImpl.m_curUpdateFileIndex];
						DownLoadURLImpl arg_F1_0 = urlreqimpl;
						Action<IURLReq, object> arg_F1_1 = delegate(IURLReq server_r, object file_ret)
						{
							try
							{
								byte[] array = file_ret as byte[];
								string path = AssetManagerImpl.UPDATE_DOWN_PATH + AssetManagerImpl.m_needUpdateFiles[AssetManagerImpl.m_curUpdateFileIndex];
								AssetManagerImpl.preparePath(path);
								FileStream fileStream = new FileStream(path, FileMode.Create);
								fileStream.Write(array, 0, array.Length);
								fileStream.Flush();
								fileStream.Close();
								AssetManagerImpl.m_curDownedSize += AssetManagerImpl.m_curDowningFileSize;
								AssetManagerImpl.m_updateOnProg((float)AssetManagerImpl.m_curDownedSize / (float)AssetManagerImpl.m_curNeedDownSize);
							}
							catch (Exception ex)
							{
								AssetManagerImpl.m_curDowningFileSize = 0;
								AssetManagerImpl.m_curUpdateFileIndex = AssetManagerImpl.m_needUpdateFiles.Count;
								AssetManagerImpl.m_bUpdateEnd_Success = false;
								AssetManagerImpl.m_updateOnError(501, urlreqimpl.url + "  Exception " + ex.Message);
							}
							AssetManagerImpl.m_curDowningFileSize = 0;
							AssetManagerImpl.m_curUpdateFileIndex++;
						};
						Action<IURLReq, float> arg_F1_2;
						if ((arg_F1_2 = AssetManagerImpl.<>c.<>9__43_1) == null)
						{
							arg_F1_2 = (AssetManagerImpl.<>c.<>9__43_1 = new Action<IURLReq, float>(AssetManagerImpl.<>c.<>9.<onProcess>b__43_1));
						}
						arg_F1_0.load(arg_F1_1, arg_F1_2, delegate(IURLReq server_r, string err)
						{
							AssetManagerImpl.m_curDowningFileSize = 0;
							AssetManagerImpl.m_curUpdateFileIndex = AssetManagerImpl.m_needUpdateFiles.Count;
							AssetManagerImpl.m_bUpdateEnd_Success = false;
							AssetManagerImpl.m_updateOnError(501, urlreqimpl.url);
						});
					}
				}
				else
				{
					AssetManagerImpl.m_needUpdateFiles.Clear();
					AssetManagerImpl.m_needUpdateFileSize.Clear();
					bool bUpdateEnd_Success = AssetManagerImpl.m_bUpdateEnd_Success;
					if (bUpdateEnd_Success)
					{
						this.saveLocalVer();
					}
					AssetManagerImpl.m_updateOnEnd(AssetManagerImpl.m_bUpdateEnd_Success);
					AssetManagerImpl.m_localVer = null;
					AssetManagerImpl.m_serverVer = null;
					AssetManagerImpl.m_localFileList = null;
					AssetManagerImpl.m_serverFileList = null;
					AssetManagerImpl.m_needUpdateFiles = null;
					AssetManagerImpl.m_needUpdateFileSize = null;
					AssetManagerImpl.m_curUpdateFileIndex = 0;
					AssetManagerImpl.m_curDowningFileSize = 0;
					AssetManagerImpl.m_updateOnBegin = null;
					AssetManagerImpl.m_updateOnProg = null;
					AssetManagerImpl.m_updateOnEnd = null;
					AssetManagerImpl.m_updateOnError = null;
					AssetManagerImpl.m_strUpdateServerUrl = null;
					AssetManagerImpl.m_strVerName = null;
					AssetManagerImpl.START_UPDATEGAME = false;
					AssetManagerImpl.m_curDownedSize = 0;
					AssetManagerImpl.m_curNeedDownSize = 1;
				}
			}
			bool flag5 = AssetManagerImpl.m_readyAssets.Count > 0;
			if (flag5)
			{
				int num = AssetManagerImpl.m_autoDisposeIdx;
				int i = 0;
				while (i < 30)
				{
					bool flag6 = num >= AssetManagerImpl.m_readyAssets.Count;
					if (flag6)
					{
						num = 0;
					}
					AssetImpl assetImpl = AssetManagerImpl.m_readyAssets[num];
					bool flag7 = assetImpl.autoDisposeCounter == AssetManagerImpl.m_autoDisposeCounter;
					if (flag7)
					{
						break;
					}
					assetImpl.autoDisposeCounter = AssetManagerImpl.m_autoDisposeCounter;
					bool flag8 = !assetImpl.isReady;
					if (flag8)
					{
						AssetManagerImpl.m_readyAssets.Remove(assetImpl);
					}
					else
					{
						assetImpl.onProcess(tmSlice);
						num++;
					}
					i++;
					num++;
				}
				AssetManagerImpl.m_autoDisposeIdx = num;
				AssetManagerImpl.m_autoDisposeCounter += 1uL;
			}
		}
	}
}
