namespace TrailsintheSky2ndChapter
{
	internal class Util
	{
		public static uint ValidationValue(uint min, uint max, uint value)
		{
			if(value < min) value = min;
			if(value > max) value = max;
			return value;
		}
	}
}
