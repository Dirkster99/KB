namespace KnowledgeBase.ViewModel
{
	using System.Reflection;
	using System.Windows.Input;
	using CefSharp;
	using KnowledgeBase.ViewModels.Commands;

	/// <summary>
	/// ApplicationViewModel manages the appplications state and its main objects.
	/// </summary>
	public class AppViewModel : Base.ViewModelBase
	{
		#region fields
		public const string DefaultUrl = "custom://cefsharp/BindingTest.html";
		public const string TestResourceUrl = "http://test/resource/load";
		public const string TestUnicodeResourceUrl = "http://test/resource/loadUnicode";

		private ICommand mTestUrlCommand = null;
		private ICommand mTestUrl1Command = null;

		private string mBrowserAddress;
		private string mAssemblyTitle;
		#endregion fields

		#region constructors
		/// <summary>
		/// Class Constructor
		/// </summary>
		public AppViewModel()
		{
			this.mAssemblyTitle = Assembly.GetEntryAssembly().GetName().Name;

			this.BrowserAddress = AppViewModel.TestUnicodeResourceUrl;
		}
		#endregion constructors

		#region properties
		/// <summary>
		/// Get/set current address of web browser URI.
		/// </summary>
		public string BrowserAddress
		{
			get
			{
				return this.mBrowserAddress;
			}

			set
			{
				if (this.mBrowserAddress != value)
				{
					this.mBrowserAddress = value;
					this.RaisePropertyChanged(() => this.BrowserAddress);
					this.RaisePropertyChanged(() => this.BrowserTitle);
				}
			}
		}

		/// <summary>
		/// Get "title" - "Uri" string of current address of web browser URI
		/// for display in UI - to let the user know what his looking at.
		/// </summary>
		public string BrowserTitle
		{
			get
			{
				return string.Format("{0} - {1}", this.mAssemblyTitle, this.mBrowserAddress);
			}
		}

		/// <summary>
		/// Get test Command to browse to a test URL ...
		/// </summary>
		public ICommand TestUrlCommand
		{
			get
			{
				if (this.mTestUrlCommand == null)
				{
					this.mTestUrlCommand = new RelayCommand(() => 
					{
						// Setting this address sets the current address of the browser
						// control via bound BrowserAddress property
						this.BrowserAddress = AppViewModel.TestResourceUrl;
					});
				}

				return this.mTestUrlCommand;
			}
		}

		/// <summary>
		/// Get test Command to browse to a test URL 1 ...
		/// </summary>
		public ICommand TestUrl1Command
		{
			get
			{
				if (this.mTestUrl1Command == null)
				{
					this.mTestUrl1Command = new RelayCommand(() =>
					{
						// Setting this address sets the current address of the browser
						// control via bound BrowserAddress property
						this.BrowserAddress = AppViewModel.TestUnicodeResourceUrl;
					});
				}

				return this.mTestUrl1Command;
			}
		}
		#endregion properties

		#region methods
		/// <summary>
		/// Registers 2 Test URIs with HTML loaded from strings
		/// </summary>
		/// <param name="browser"></param>
		public static void RegisterTestResources(IWebBrowser browser)
		{
			var handler = browser.ResourceHandler;
			if (handler != null)
			{
				const string responseBody = "<html><body><h1>Success</h1><p>This document is loaded from a System.IO.Stream</p></body></html>";
				handler.RegisterHandler(TestResourceUrl, ResourceHandler.FromString(responseBody));

				const string unicodeResponseBody = "<html><body>整体满意度</body></html>";
				handler.RegisterHandler(TestUnicodeResourceUrl, ResourceHandler.FromString(unicodeResponseBody));
			}
		}
		#endregion methods
	}
}
