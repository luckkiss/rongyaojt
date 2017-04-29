using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace GameFramework
{
	public class JSONArray : JSONNode, IEnumerable
	{
		private List<JSONNode> m_List = new List<JSONNode>();

		public override JSONNode this[int aIndex]
		{
			get
			{
				bool flag = aIndex < 0 || aIndex >= this.m_List.Count;
				JSONNode result;
				if (flag)
				{
					result = new JSONLazyCreator(this);
				}
				else
				{
					result = this.m_List[aIndex];
				}
				return result;
			}
			set
			{
				bool flag = aIndex < 0 || aIndex >= this.m_List.Count;
				if (flag)
				{
					this.m_List.Add(value);
				}
				else
				{
					this.m_List[aIndex] = value;
				}
			}
		}

		public override JSONNode this[string aKey]
		{
			get
			{
				return new JSONLazyCreator(this);
			}
			set
			{
				this.m_List.Add(value);
			}
		}

		public override int Count
		{
			get
			{
				return this.m_List.Count;
			}
		}

		public override IEnumerable<JSONNode> Childs
		{
			get
			{
				foreach (JSONNode jSONNode in this.m_List)
				{
					yield return jSONNode;
					jSONNode = null;
				}
				List<JSONNode>.Enumerator enumerator = default(List<JSONNode>.Enumerator);
				yield break;
				yield break;
			}
		}

		public override void Add(string aKey, JSONNode aItem)
		{
			this.m_List.Add(aItem);
		}

		public override JSONNode Remove(int aIndex)
		{
			bool flag = aIndex < 0 || aIndex >= this.m_List.Count;
			JSONNode result;
			if (flag)
			{
				result = null;
			}
			else
			{
				JSONNode jSONNode = this.m_List[aIndex];
				this.m_List.RemoveAt(aIndex);
				result = jSONNode;
			}
			return result;
		}

		public override JSONNode Remove(JSONNode aNode)
		{
			this.m_List.Remove(aNode);
			return aNode;
		}

		public IEnumerator GetEnumerator()
		{
			foreach (JSONNode jSONNode in this.m_List)
			{
				yield return jSONNode;
				jSONNode = null;
			}
			List<JSONNode>.Enumerator enumerator = default(List<JSONNode>.Enumerator);
			yield break;
			yield break;
		}

		public override string ToString()
		{
			string text = "[ ";
			foreach (JSONNode current in this.m_List)
			{
				bool flag = text.Length > 2;
				if (flag)
				{
					text += ", ";
				}
				text += current.ToString();
			}
			text += " ]";
			return text;
		}

		public override string ToString(string aPrefix)
		{
			string text = "[ ";
			foreach (JSONNode current in this.m_List)
			{
				bool flag = text.Length > 3;
				if (flag)
				{
					text += ", ";
				}
				text = text + "\n" + aPrefix + "   ";
				text += current.ToString(aPrefix + "   ");
			}
			text = text + "\n" + aPrefix + "]";
			return text;
		}

		public override void Serialize(BinaryWriter aWriter)
		{
			aWriter.Write(1);
			aWriter.Write(this.m_List.Count);
			for (int i = 0; i < this.m_List.Count; i++)
			{
				this.m_List[i].Serialize(aWriter);
			}
		}
	}
}
