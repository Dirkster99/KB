namespace KnowledgeBase.Models
{
	using System;
	using System.IO;
	using System.Reflection;
	using CefSharp;

	/// <summary>
	/// Source: http://thechriskent.com/tag/ischemehandler/
	/// </summary>
	public class ResourceSchemeHandler : ISchemeHandler
	{
		public bool ProcessRequestAsync(IRequest request,
		                                ISchemeHandlerResponse response,
																		OnRequestCompletedHandler requestCompletedCallback)
		{
			Uri u = new Uri(request.Url);
			String file = u.Authority + u.AbsolutePath;

			Assembly ass = Assembly.GetExecutingAssembly();
			String resourcePath = ass.GetName().Name + "." + file.Replace("/", ".");

			if (ass.GetManifestResourceInfo(resourcePath) != null)
			{
				response.ResponseStream = ass.GetManifestResourceStream(resourcePath);

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
