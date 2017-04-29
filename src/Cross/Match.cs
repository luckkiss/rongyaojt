using System;

namespace Cross
{
	internal class Match
	{
		public int startIndex;

		public int endIndex;

		public int length;

		public string content;

		public string replacement;

		public string before;

		public string toString()
		{
			return string.Concat(new object[]
			{
				"Match [",
				this.startIndex,
				" - ",
				this.endIndex,
				"] (",
				this.length,
				") ",
				this.content,
				", replacement:",
				this.replacement,
				";"
			});
		}
	}
}
