using System.Buffers.Binary;
using TrailsintheSky2ndChapter.Model;

namespace TrailsintheSky2ndChapter
{
	internal class SaveData
	{
		public byte[] Body { get; set; } = [];
		private string _filename = string.Empty;
		private byte[] _header = [];

		public GeneralModel Load(string filename)
		{
			var buffer = System.IO.File.ReadAllBytes(filename);

			var size = BitConverter.ToInt32(buffer);
			_header = buffer[0..size];
			buffer = buffer[size..];

			GeneralModel general = new();

			var crc = BitConverter.ToUInt32(buffer, 0x08);
			if (crc != CalcCRC(buffer)) return general;

			_filename = filename;
			Body = buffer;

			general.Money = BitConverter.ToUInt32(buffer, 0x20B640);

			return general;
		}

		public void Save()
		{
			System.IO.File.WriteAllBytes(_filename, [.._header, .. Body]);
		}

		public void Reflection(GeneralModel general)
		{
			BinaryPrimitives.WriteUInt32LittleEndian(Body.AsSpan(0x20B640, 4), general.Money);

			var crc = CalcCRC(Body);
			BinaryPrimitives.WriteUInt32LittleEndian(Body.AsSpan(0x08, 4), crc);
		}

		private uint CalcCRC(byte[] buffer)
		{
			var tables = new uint[256];
			for (uint i = 0; i < tables.Length; i++)
			{
				var x = i;
				for (var j = 0; j < 8; j++)
				{
					x = (x & 1) == 0L ? x >> 1 : 0xEDB88320 ^ x >> 1;
				}
				tables[i] = x;
			}

			uint crc = 0x0020BD44;
			int offset = 0x0C;
			int count = 0x82F51 * 4;

			if (buffer.Length <= offset + count) return 0;

			for (int i = 0; i < count; i++)
			{
				crc = (crc >> 8) ^ tables[(crc ^ buffer[offset + i]) & 0xFF];
			}

			return crc;
		}
	}
}
