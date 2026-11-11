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

			Byte[] zstd = Decomp(buffer[start..end]);

			start = end;
			end = BitConverter.ToInt32(buffer, 32);

			mThumbnail = buffer[start..end];
			mSystemConfig = buffer[end..];

			return zstd;
		}

		public void Save(string filename, byte[] buffer)
		{
			Byte[] zstd = Comp(buffer);

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

			System.IO.File.WriteAllBytes(filename, buffer);
		}

		private Byte[] Comp(Byte[] data)
		{
			Byte[] result = [];
			using (var input = new System.IO.MemoryStream(data))
			{
				using (var output = new System.IO.MemoryStream())
				{
					using (var zlib = new System.IO.Compression.ZstandardStream(output, System.IO.Compression.CompressionLevel.Fastest))
					{
						input.CopyTo(zlib);
						zlib.Flush();
					}
					result = output.ToArray();
				}
			}
			return result;
		}

		private Byte[] Decomp(Byte[] data)
		{
			Byte[] result = [];
			using (var input = new System.IO.MemoryStream(data))
			{
				using (var zlib = new System.IO.Compression.ZstandardStream(input, System.IO.Compression.CompressionMode.Decompress))
				{
					using (var output = new System.IO.MemoryStream())
					{
						zlib.CopyTo(output);
						output.Flush();
						result = output.ToArray();
					}
				}
			}
			return result;
		}
	}
}
