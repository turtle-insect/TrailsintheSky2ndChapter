namespace TrailsintheSky2ndChapter.ViewModel
{
	internal class GeneralViewModel
	{
		public uint Money
		{
			get => SaveData.Instance().ReadNumber(0x20B640, 4);
			set => Util.WriteNumber(0, 99999999, 0x20B640, 4, value);
		}
	}
}
