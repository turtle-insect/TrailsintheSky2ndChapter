namespace TrailsintheSky2ndChapter.FileFormat
{
	internal interface IFileFormat
	{
		Byte[] Load(String filename);
		void Save(String filename, Byte[] buffer);
	}
}
