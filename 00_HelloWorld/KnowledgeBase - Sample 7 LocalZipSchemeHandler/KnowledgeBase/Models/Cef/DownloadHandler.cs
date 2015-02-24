namespace KnowledgeBase.Models.Cef
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using CefSharp;

	public class DownloadHandler : IDownloadHandler
	{
		public bool OnBeforeDownload(DownloadItem downloadItem, out string downloadPath, out bool showDialog)
		{
			downloadPath = downloadItem.SuggestedFileName;
			showDialog = true;

			return true;
		}

		public bool OnDownloadUpdated(DownloadItem downloadItem)
		{
			return false;
		}
	}
}
