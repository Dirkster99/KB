namespace KnowledgeBase.Models
{
	using CefSharp;
	using System.IO;
	using System;

	/// <summary>
	/// Source:
	/// http://thechriskent.com/2014/04/21/use-local-files-in-cefsharp/
	/// http://thechriskent.com/2014/08/18/embedded-chromium-in-winforms/
	/// 
	/// </summary>
	public class LocalSchemeHandler : ISchemeHandler
	{
		public bool ProcessRequestAsync(IRequest request, ISchemeHandlerResponse response, OnRequestCompletedHandler requestCompletedCallback)
		{
			Uri u = new Uri(request.Url);
			String file = u.Authority + u.AbsolutePath;

			if (File.Exists(file))
			{
				Byte[] bytes = File.ReadAllBytes(file);
				response.ResponseStream = new MemoryStream(bytes);
				switch (Path.GetExtension(file))
				{
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
