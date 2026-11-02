namespace TrailsintheSky2ndChapter
{
	internal class FileFormatSwitch : IFileFormat
	{
		private Byte[] mHeader = [];
		public byte[] Load(string filename)
		{
			Byte[] buffer = System.IO.File.ReadAllBytes(filename);
			var size = BitConverter.ToInt32(buffer);
			mHeader = buffer[..size];
			return buffer[size..];
		}

		public void Save(string filename, byte[] buffer)
		{
			buffer = [
				.. mHeader,
				.. buffer,
			];
			System.IO.File.WriteAllBytes(filename, buffer);
		}
	}
}
