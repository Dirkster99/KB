namespace KnowledgeBase.Models.Zip
{
	using System;
	using System.IO;
	using System.IO.Compression;  // requires a reference to System.IO.Compression.dll
	using System.Text;

	/// <summary>
	/// Helper class to wrap around zip archive API.
	/// </summary>
	class ZipExtractor
	{
		/// <summary>
		/// Extract a zip entry into an in-memory string.
		/// </summary>
		/// <param name="pathToZip"></param>
		/// <param name="fileToExtract"></param>
		/// <returns></returns>
		public static string ExtractToString(string pathToZip, string fileToExtract)
		{
			string value = null;

			using (var file = File.OpenRead(pathToZip))
			using (var zip = new ZipArchive(file, ZipArchiveMode.Read))
			{
				foreach (var entry in zip.Entries)
				{
					if (fileToExtract == entry.FullName)
					{
						using (var stream = entry.Open())
						{
							using (var reader = new StreamReader(stream, Encoding.UTF8))
							{
								value = reader.ReadToEnd();
								// Do something with the value
							}
						}
					}
				}
			}

			return value;
		}

		/// <summary>
		/// Extract a zip entry into an in-memory stream.
		/// </summary>
		/// <param name="pathToZip"></param>
		/// <param name="fileToExtract"></param>
		/// <returns></returns>
		public static MemoryStream ExtractToMemoryStream(string pathToZip, string fileToExtract)
		{
			using (var file = File.OpenRead(pathToZip))
			{
				using (var zip = new ZipArchive(file, ZipArchiveMode.Read))
				{
					foreach (var entry in zip.Entries)
					{
						if (fileToExtract == entry.FullName)
						{
							var data = new MemoryStream();

							var zipInputStream = entry.Open();
							zipInputStream.CopyTo(data);

							return data;
						}
					}
				}
			}

			return null;
		}
	}
}
