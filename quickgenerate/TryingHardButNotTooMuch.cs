using System;

namespace QuickGenerate
{
	public static class TryingHardButNotTooMuch
	{
		public static AttemptImplementation Attempts(this int numberOfTimes)
		{
			return new AttemptImplementation(numberOfTimes);
		}

		public class AttemptImplementation
		{
			private readonly int max;
			private int current;
			public AttemptImplementation(int max)
			{
				this.max = max;
			}

			public bool Next()
			{
				if (current >= max)
					return false;
				current++;
				return true;
			}

			public void Reset()
			{
				current = 0;
			}
		}
	}
}