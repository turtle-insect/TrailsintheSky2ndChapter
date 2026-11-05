namespace TrailsintheSky2ndChapter.ViewModel
{
	internal class GeneralViewModel
	{
		// Sepith
		public uint Earth
		{
			get => SaveData.Instance().ReadNumber(0x20B620, 4);
			set => Util.WriteNumber(0x20B620, 4, value, 0, 99999);
		}

		public uint Water
		{
			get => SaveData.Instance().ReadNumber(0x20B624, 4);
			set => Util.WriteNumber(0x20B624, 4, value, 0, 99999);
		}

		public uint Fire
		{
			get => SaveData.Instance().ReadNumber(0x20B628, 4);
			set => Util.WriteNumber(0x20B628, 4, value, 0, 99999);
		}

		public uint Wind
		{
			get => SaveData.Instance().ReadNumber(0x20B62C, 4);
			set => Util.WriteNumber(0x20B62C, 4, value, 0, 99999);
		}

		public uint Time
		{
			get => SaveData.Instance().ReadNumber(0x20B630, 4);
			set => Util.WriteNumber(0x20B630, 4, value, 0, 99999);
		}

		public uint Space
		{
			get => SaveData.Instance().ReadNumber(0x20B634, 4);
			set => Util.WriteNumber(0x20B634, 4, value, 0, 99999);
		}

		public uint Mirage
		{
			get => SaveData.Instance().ReadNumber(0x20B638, 4);
			set => Util.WriteNumber(0x20B638, 4, value, 0, 99999);
		}

		public uint Mass
		{
			get => SaveData.Instance().ReadNumber(0x20B63C, 4);
			set => Util.WriteNumber(0x20B63C, 4, value, 0, 99999);
		}

		public uint Money
		{
			get => SaveData.Instance().ReadNumber(0x20B640, 4);
			set => Util.WriteNumber(0x20B640, 4, value, 0, 99999999);
		}
	}
}
