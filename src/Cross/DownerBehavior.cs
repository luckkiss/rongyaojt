using System;
using System.Collections;
using System.Text;
using UnityEngine;

namespace Cross
{
	public class DownerBehavior : MonoBehaviour
	{
		public void load(string url, string method, string dataFmt, string paras, Action<object> onFin, Action<float> onProg = null, Action<string> onFail = null)
		{
			base.StartCoroutine(this._load(url, method, dataFmt, paras, onFin, onProg, onFail));
		}

		protected WWW _evalWWW(WWW w, Action<float> onProg)
		{
			bool flag = onProg != null;
			if (flag)
			{
				onProg(w.progress * 0.3f);
			}
			return w;
		}

		protected IEnumerator _load(string url, string method, string dataFmt, string paras, Action<object> onFin, Action<float> onProg = null, Action<string> onFail = null)
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
			yield return this._evalWWW(wWW, onProg);
			bool flag5 = wWW.error != null;
			if (flag5)
			{
				Debug.Log("httpfail: " + url + " : " + wWW.error);
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
							byte[] array3 = wWW.bytes;
							bool flag10 = array3 != null && array3.Length > 3 && array3[0] == 239 && array3[1] == 187 && array3[2] == 191;
							string obj;
							if (flag10)
							{
								obj = Encoding.UTF8.GetString(array3, 3, array3.Length - 3);
							}
							else
							{
								obj = wWW.text;
							}
							onFin(obj);
							array3 = null;
							obj = null;
						}
					}
				}
			}
			wWW.Dispose();
			yield break;
		}
	}
}
