using System.IO;

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

			Byte[] zstd = Decompress(buffer[start..end]);

			start = end;
			end = BitConverter.ToInt32(buffer, 32);

			mThumbnail = buffer[start..end];
			mSystemConfig = buffer[end..];

			return zstd;
		}

		public void Save(string filename, byte[] buffer)
		{
			Byte[] zstd = Compression(buffer);

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

		public static Byte[] Compression(Byte[] buffer)
		{
			using System.IO.MemoryStream output = new();
			using (System.IO.Compression.ZstandardStream zstd = new(output, System.IO.Compression.CompressionLevel.Optimal))
			{
				zstd.Write(buffer);
			}

			return output.ToArray();
		}

		public static Byte[] Decompress(Byte[] buffer)
		{
			using System.IO.MemoryStream input = new(buffer);
			using System.IO.MemoryStream output = new();
			using (System.IO.Compression.ZstandardStream zstd = new(input, System.IO.Compression.CompressionMode.Decompress))
			{
				zstd.CopyTo(output);
			}

			return output.ToArray();
		}
	}
}
