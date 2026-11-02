namespace TrailsintheSky2ndChapter
{
	internal class Util
	{
		public static void WriteNumber(uint min, uint max, uint address, uint size, uint value)
		{
			if(value < min) value = min;
			if(value > max) value = max;
			SaveData.Instance().WriteNumber(address, size, value);
		}
	}
}
