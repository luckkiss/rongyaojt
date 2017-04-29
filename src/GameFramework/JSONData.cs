using System;
using System.IO;

namespace GameFramework
{
	public class JSONData : JSONNode
	{
		private string m_Data;

		public override string Value
		{
			get
			{
				return this.m_Data;
			}
			set
			{
				this.m_Data = value;
			}
		}

		public JSONData(string aData)
		{
			this.m_Data = aData;
		}

		public JSONData(float aData)
		{
			this.AsFloat = aData;
		}

		public JSONData(double aData)
		{
			this.AsDouble = aData;
		}

		public JSONData(bool aData)
		{
			this.AsBool = aData;
		}

		public JSONData(int aData)
		{
			this.AsInt = aData;
		}

		public override string ToString()
		{
			return this.ToString("");
		}

		public override string ToString(string aPrefix)
		{
			return aPrefix + JSONNode.Escape(this.m_Data) + aPrefix;
		}

		public override void Serialize(BinaryWriter aWriter)
		{
			JSONData jSONData = new JSONData("");
			jSONData.AsInt = this.AsInt;
			bool flag = jSONData.m_Data == this.m_Data;
			if (flag)
			{
				aWriter.Write(4);
				aWriter.Write(this.AsInt);
			}
			else
			{
				jSONData.AsFloat = this.AsFloat;
				bool flag2 = jSONData.m_Data == this.m_Data;
				if (flag2)
				{
					aWriter.Write(7);
					aWriter.Write(this.AsFloat);
				}
				else
				{
					jSONData.AsDouble = this.AsDouble;
					bool flag3 = jSONData.m_Data == this.m_Data;
					if (flag3)
					{
						aWriter.Write(5);
						aWriter.Write(this.AsDouble);
					}
					else
					{
						jSONData.AsBool = this.AsBool;
						bool flag4 = jSONData.m_Data == this.m_Data;
						if (flag4)
						{
							aWriter.Write(6);
							aWriter.Write(this.AsBool);
						}
						else
						{
							aWriter.Write(3);
							aWriter.Write(this.m_Data);
						}
					}
				}
			}
		}
	}
}
