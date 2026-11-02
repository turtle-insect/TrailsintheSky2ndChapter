namespace TrailsintheSky2ndChapter.FileFormat
{
	internal class FileFormatSteam : IFileFormat
	{
		private Byte[] mThumbnail = [];
		private Byte[] mSystemConfig = [];

		public byte[] Load(string filename)
		{
			Byte[] buffer = System.IO.File.ReadAllBytes(filename);
			int start = BitConverter.ToInt32(buffer);
			int end = BitConverter.ToInt32(buffer, 16);

			Byte[] zstd = buffer[start..end];
			// TBD
			// .NET 11
			// zstanderd

			start = end;
			end = BitConverter.ToInt32(buffer, 32);

			mThumbnail = buffer[start..end];
			mSystemConfig = buffer[end..];

			// TBD
			return [];
		}

		public void Save(string filename, byte[] buffer)
		{
			Byte[] zstd = [];

			buffer = [
				.. BitConverter.GetBytes(40),
				.. BitConverter.GetBytes(0),
				.. BitConverter.GetBytes(zstd.LongLength),
				.. BitConverter.GetBytes(40 + zstd.LongLength),
				.. BitConverter.GetBytes(mThumbnail.LongLength),
				.. BitConverter.GetBytes(40 + zstd.LongLength + mThumbnail.LongLength),
				.. zstd,
				.. mThumbnail,
				.. mSystemConfig,
			];
		}
	}
}
