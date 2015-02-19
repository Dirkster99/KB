namespace KnowledgeBase.Models.Cef
{
	using System;
	using System.IO;
	using System.Text;
	using System.Windows;
	using CefSharp;
	using KnowledgeBase.Models.Zip;

	/// <summary>
	/// 
	/// Source based on:
	/// http://thechriskent.com/2014/04/21/use-local-files-in-cefsharp/
	/// http://thechriskent.com/2014/08/18/embedded-chromium-in-winforms/
	/// 
	/// </summary>
	public class LocalZipSchemeHandler : ISchemeHandler
	{
		public static string EncodeHTML(string input)
		{
			if (string.IsNullOrEmpty(input))
				return input;

			input = input.Replace("\n", "<br/>");

			return string.Format("<HTML>\n  <BODY>{0}\n  <BODY/>\n<HTML/>", input);
		}

		public bool ProcessRequestAsync(IRequest request,
		                                ISchemeHandlerResponse response,
		                                OnRequestCompletedHandler requestCompletedCallback)
		{
			Uri u = new Uri(request.Url);

			string inZipUri = "index.html";
			string zipPath = u.AbsolutePath;

			int idx = zipPath.IndexOf("::");

			if (idx > 0)
			{
				inZipUri = zipPath.Substring(idx + 3);

				zipPath = zipPath.Substring(0, idx);
				zipPath = zipPath.Replace("%20", " ");
			}

			// String file = u.Authority; // + u.AbsolutePath;

			if (File.Exists(zipPath))
			{
				try
				{
					response.ResponseStream = ZipExtractor.ExtractToMemoryStream(zipPath, inZipUri);
				}
				catch (Exception exp)
				{
					byte[] byteArray = Encoding.ASCII.GetBytes(EncodeHTML(
					                     string.Format("Error reading:\n File:'{0}'\n Url:'{1}'\n Message:{2}",
															 zipPath, inZipUri, exp.Message)));

					MemoryStream stream = new MemoryStream(byteArray);

					response.ResponseStream = stream;
				}

				switch (Path.GetExtension(inZipUri))
				{
					case ".htm":
					case ".html":
						response.MimeType = "text/html";
						break;

					case ".js":
						response.MimeType = "text/javascript";
						break;

					case ".png":
						response.MimeType = "image/png";
						break;

					case ".appcache":
					case ".manifest":
						response.MimeType = "text/cache-manifest";
						break;

					case ".css":
						response.MimeType = "text/css";
						break;

					default:
						response.MimeType = "application/octet-stream";
						break;
				}

				requestCompletedCallback();
				return true;
			}

			return false;
		}
	}
}
