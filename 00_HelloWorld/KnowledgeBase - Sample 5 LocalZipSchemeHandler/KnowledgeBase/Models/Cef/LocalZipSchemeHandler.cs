namespace KnowledgeBase.Models.Cef
{
	using System;
	using System.IO;
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
		public bool ProcessRequestAsync(IRequest request,
		                                ISchemeHandlerResponse response,
		                                OnRequestCompletedHandler requestCompletedCallback)
		{
			Uri u = new Uri(request.Url);
			String file = u.Authority; // + u.AbsolutePath;

			if (File.Exists(file))
			{
				string inZipUri = "index.html";

				if (u.AbsolutePath != null)
				{
					if (u.AbsolutePath.Substring(0,1) == "/")
						inZipUri = u.AbsolutePath.Substring(1);
				}

				response.ResponseStream = ZipExtractor.ExtractToMemoryStream(u.Authority, inZipUri);

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
