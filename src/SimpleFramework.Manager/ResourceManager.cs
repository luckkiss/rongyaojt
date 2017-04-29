using LuaInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace SimpleFramework.Manager
{
	public class ResourceManager : View
	{
		private string[] m_Variants = new string[0];

		private AssetBundleManifest manifest;

		private AssetBundle shared;

		private AssetBundle assetbundle;

		private Dictionary<string, AssetBundle> bundles;

		private void Awake()
		{
		}

		public void Initialize()
		{
			string path = string.Empty;
			this.bundles = new Dictionary<string, AssetBundle>();
			path = Util.DataPath + "StreamingAssets";
			if (!File.Exists(path))
			{
				return;
			}
			byte[] binary = File.ReadAllBytes(path);
			this.assetbundle = AssetBundle.LoadFromMemory(binary);
			this.manifest = this.assetbundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
		}

		public GameObject LoadAsset(string abname, string assetname)
		{
			abname = abname.ToLower();
			AssetBundle assetBundle = this.LoadAssetBundle(abname);
			return assetBundle.LoadAsset<GameObject>(assetname);
		}

		public Sprite LoadSpriteAsset(string abname, string assetname)
		{
			abname = abname.ToLower();
			AssetBundle assetBundle = this.LoadAssetBundle(abname);
			return assetBundle.LoadAsset<Sprite>(assetname);
		}

		public T LoadAsset<T>(string abname, string assetname) where T : Component
		{
			abname = abname.ToLower();
			AssetBundle assetBundle = this.LoadAssetBundle(abname);
			return assetBundle.LoadAsset<T>(assetname);
		}

		public void LoadAsset(string abname, string assetname, LuaFunction func)
		{
			abname = abname.ToLower();
			base.StartCoroutine(this.OnLoadAsset(abname, assetname, func));
		}

		[DebuggerHidden]
		private IEnumerator OnLoadAsset(string abname, string assetName, LuaFunction func)
		{
			ResourceManager.<OnLoadAsset>c__Iterator3 <OnLoadAsset>c__Iterator = new ResourceManager.<OnLoadAsset>c__Iterator3();
			<OnLoadAsset>c__Iterator.abname = abname;
			<OnLoadAsset>c__Iterator.assetName = assetName;
			<OnLoadAsset>c__Iterator.func = func;
			<OnLoadAsset>c__Iterator.<$>abname = abname;
			<OnLoadAsset>c__Iterator.<$>assetName = assetName;
			<OnLoadAsset>c__Iterator.<$>func = func;
			<OnLoadAsset>c__Iterator.<>f__this = this;
			return <OnLoadAsset>c__Iterator;
		}

		private AssetBundle LoadAssetBundle(string abname)
		{
			if (!abname.EndsWith(".assetbundle"))
			{
				abname += ".assetbundle";
			}
			AssetBundle assetBundle = null;
			if (!this.bundles.ContainsKey(abname))
			{
				string text = Util.DataPath + abname;
				UnityEngine.Debug.LogWarning("LoadFile::>> " + text);
				this.LoadDependencies(abname);
				byte[] binary = File.ReadAllBytes(text);
				assetBundle = AssetBundle.LoadFromMemory(binary);
				this.bundles.Add(abname, assetBundle);
			}
			else
			{
				this.bundles.TryGetValue(abname, out assetBundle);
			}
			return assetBundle;
		}

		private void LoadDependencies(string name)
		{
			if (this.manifest == null)
			{
				UnityEngine.Debug.LogError("Please initialize AssetBundleManifest by calling AssetBundleManager.Initialize()");
				return;
			}
			string[] allDependencies = this.manifest.GetAllDependencies(name);
			if (allDependencies.Length == 0)
			{
				return;
			}
			for (int i = 0; i < allDependencies.Length; i++)
			{
				allDependencies[i] = this.RemapVariantName(allDependencies[i]);
			}
			for (int j = 0; j < allDependencies.Length; j++)
			{
				this.LoadAssetBundle(allDependencies[j]);
			}
		}

		private string RemapVariantName(string assetBundleName)
		{
			string[] allAssetBundlesWithVariant = this.manifest.GetAllAssetBundlesWithVariant();
			if (Array.IndexOf<string>(allAssetBundlesWithVariant, assetBundleName) < 0)
			{
				return assetBundleName;
			}
			string[] array = assetBundleName.Split(new char[]
			{
				'.'
			});
			int num = 2147483647;
			int num2 = -1;
			for (int i = 0; i < allAssetBundlesWithVariant.Length; i++)
			{
				string[] array2 = allAssetBundlesWithVariant[i].Split(new char[]
				{
					'.'
				});
				if (!(array2[0] != array[0]))
				{
					int num3 = Array.IndexOf<string>(this.m_Variants, array2[1]);
					if (num3 != -1 && num3 < num)
					{
						num = num3;
						num2 = i;
					}
				}
			}
			if (num2 != -1)
			{
				return allAssetBundlesWithVariant[num2];
			}
			return assetBundleName;
		}

		private void OnDestroy()
		{
			if (this.shared != null)
			{
				this.shared.Unload(true);
			}
			if (this.manifest != null)
			{
				this.manifest = null;
			}
			UnityEngine.Debug.Log("~ResourceManager was destroy!");
		}
	}
}
