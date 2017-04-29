using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameFramework
{
	public class JSONClass : JSONNode, IEnumerable
	{
		private Dictionary<string, JSONNode> m_Dict = new Dictionary<string, JSONNode>();

		public override JSONNode this[string aKey]
		{
			get
			{
				bool flag = this.m_Dict.ContainsKey(aKey);
				JSONNode result;
				if (flag)
				{
					result = this.m_Dict[aKey];
				}
				else
				{
					result = new JSONLazyCreator(this, aKey);
				}
				return result;
			}
			set
			{
				bool flag = this.m_Dict.ContainsKey(aKey);
				if (flag)
				{
					this.m_Dict[aKey] = value;
				}
				else
				{
					this.m_Dict.Add(aKey, value);
				}
			}
		}

		public override JSONNode this[int aIndex]
		{
			get
			{
				bool flag = aIndex < 0 || aIndex >= this.m_Dict.Count;
				JSONNode result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.m_Dict.ElementAt(aIndex).Value;
				}
				return result;
			}
			set
			{
				bool flag = aIndex < 0 || aIndex >= this.m_Dict.Count;
				if (!flag)
				{
					string key = this.m_Dict.ElementAt(aIndex).Key;
					this.m_Dict[key] = value;
				}
			}
		}

		public override int Count
		{
			get
			{
				return this.m_Dict.Count;
			}
		}

		public override IEnumerable<JSONNode> Childs
		{
			get
			{
				foreach (KeyValuePair<string, JSONNode> keyValuePair in this.m_Dict)
				{
					yield return keyValuePair.Value;
					keyValuePair = default(KeyValuePair<string, JSONNode>);
				}
				Dictionary<string, JSONNode>.Enumerator enumerator = default(Dictionary<string, JSONNode>.Enumerator);
				yield break;
				yield break;
			}
		}

		public override void Add(string aKey, JSONNode aItem)
		{
			bool flag = !string.IsNullOrEmpty(aKey);
			if (flag)
			{
				bool flag2 = this.m_Dict.ContainsKey(aKey);
				if (flag2)
				{
					this.m_Dict[aKey] = aItem;
				}
				else
				{
					this.m_Dict.Add(aKey, aItem);
				}
			}
			else
			{
				this.m_Dict.Add(Guid.NewGuid().ToString(), aItem);
			}
		}

		public override JSONNode Remove(string aKey)
		{
			bool flag = !this.m_Dict.ContainsKey(aKey);
			JSONNode result;
			if (flag)
			{
				result = null;
			}
			else
			{
				JSONNode jSONNode = this.m_Dict[aKey];
				this.m_Dict.Remove(aKey);
				result = jSONNode;
			}
			return result;
		}

		public override JSONNode Remove(int aIndex)
		{
			bool flag = aIndex < 0 || aIndex >= this.m_Dict.Count;
			JSONNode result;
			if (flag)
			{
				result = null;
			}
			else
			{
				KeyValuePair<string, JSONNode> keyValuePair = this.m_Dict.ElementAt(aIndex);
				this.m_Dict.Remove(keyValuePair.Key);
				result = keyValuePair.Value;
			}
			return result;
		}

		public override JSONNode Remove(JSONNode aNode)
		{
			JSONNode result;
			try
			{
				KeyValuePair<string, JSONNode> keyValuePair = (from k in this.m_Dict
				where k.Value == aNode
				select k).First<KeyValuePair<string, JSONNode>>();
				this.m_Dict.Remove(keyValuePair.Key);
				result = aNode;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public IEnumerator GetEnumerator()
		{
			foreach (KeyValuePair<string, JSONNode> keyValuePair in this.m_Dict)
			{
				yield return keyValuePair;
				keyValuePair = default(KeyValuePair<string, JSONNode>);
			}
			Dictionary<string, JSONNode>.Enumerator enumerator = default(Dictionary<string, JSONNode>.Enumerator);
			yield break;
			yield break;
		}

		public override string ToString()
		{
			string text = "{";
			foreach (KeyValuePair<string, JSONNode> current in this.m_Dict)
			{
				bool flag = text.Length > 2;
				if (flag)
				{
					text += ", ";
				}
				text = string.Concat(new string[]
				{
					text,
					"\"",
					JSONNode.Escape(current.Key),
					"\":",
					current.Value.ToString()
				});
			}
			text += "}";
			return text;
		}

		public override string ToString(string aPrefix)
		{
			string text = "{ ";
			foreach (KeyValuePair<string, JSONNode> current in this.m_Dict)
			{
				bool flag = text.Length > 3;
				if (flag)
				{
					text += ", ";
				}
				text = text + "\n" + aPrefix + "   ";
				text = string.Concat(new string[]
				{
					text,
					"\"",
					JSONNode.Escape(current.Key),
					"\" : ",
					current.Value.ToString(aPrefix + "   ")
				});
			}
			text = text + "\n" + aPrefix + "}";
			return text;
		}

		public override void Serialize(BinaryWriter aWriter)
		{
			aWriter.Write(2);
			aWriter.Write(this.m_Dict.Count);
			foreach (string current in this.m_Dict.Keys)
			{
				aWriter.Write(current);
				this.m_Dict[current].Serialize(aWriter);
			}
		}
	}
}
