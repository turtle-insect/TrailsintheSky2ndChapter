using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;
using TrailsintheSky2ndChapter.Model;

namespace TrailsintheSky2ndChapter
{
	internal class SaveData
	{
		private string _filename = string.Empty;
		private byte[] _header = [];
		private byte[] _body = [];

		public GeneralModel Load(string filename)
		{
			var buffer = System.IO.File.ReadAllBytes(filename);

			var size = BitConverter.ToInt32(buffer);
			_header = buffer[0..size];
			buffer = buffer[size..];

			GeneralModel general = new();

			var crc = BitConverter.ToUInt32(buffer, 0x08);
			if (crc != CalcCRC(ref buffer)) return general;

			_filename = filename;
			_body = buffer;

			general.Money = BitConverter.ToUInt32(buffer, 0x20B640);

			return general;
		}

		public void Save(GeneralModel general)
		{
			BinaryPrimitives.WriteUInt32LittleEndian(_body.AsSpan(0x20B640, 4), general.Money);

			var crc = CalcCRC(ref _body);
			BinaryPrimitives.WriteUInt32LittleEndian(_body.AsSpan(0x08, 4), crc);
			System.IO.File.WriteAllBytes(_filename, [.._header, .._body]);
		}

		private uint CalcCRC(ref byte[] buffer)
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
