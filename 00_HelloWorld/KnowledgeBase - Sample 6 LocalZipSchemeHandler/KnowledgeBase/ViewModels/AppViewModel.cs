namespace KnowledgeBase.ViewModel
{
	using System.Reflection;
	using System.Windows.Input;
	using CefSharp.Wpf;
	using KnowledgeBase.ViewModels.Commands;

	/// <summary>
	/// ApplicationViewModel manages the appplications state and its main objects.
	/// </summary>
	public class AppViewModel : Base.ViewModelBase
	{
		#region fields
		// This string holds a URL to the web.zip file in the local file system
		public string LocalZipUrl = "{0}/SampleData/web.zip";
		public string LocalZipUrl1 = "{0}/SampleData/CP_Articles.zip";

		// These 2 Demo URLs are based on the local zip file path
		// but point to individual html pages inside the package
		public const string TestResourceUrl = "local://{0}::/index.html";

		private ICommand mTestUrlCommand = null;
		private ICommand mTestUrl1Command = null;
		private ICommand mShowDevToolsCommand = null;

		private string mBrowserAddress;
		private string mAssemblyTitle;
		private string mAssemblyPath;
		#endregion fields

		#region constructors
		/// <summary>
		/// Class Constructor
		/// </summary>
		public AppViewModel()
		{
			// Set title of assembly in viewmodel and reset first browser address
			this.mAssemblyTitle = Assembly.GetEntryAssembly().GetName().Name;

			this.mAssemblyPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
			this.mAssemblyPath = this.mAssemblyPath.Replace(" ", "%20");
			this.mAssemblyPath = this.mAssemblyPath.Replace("\\", "/");

			this.LocalZipUrl = string.Format(LocalZipUrl, this.mAssemblyPath);
			this.LocalZipUrl1 = string.Format(LocalZipUrl1, this.mAssemblyPath);

			this.BrowserAddress = string.Format(TestResourceUrl, this.LocalZipUrl1);
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
						this.BrowserAddress = string.Format(AppViewModel.TestResourceUrl, this.LocalZipUrl);
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
						this.BrowserAddress = string.Format(AppViewModel.TestResourceUrl, this.LocalZipUrl1);
					});
				}

				return this.mTestUrl1Command;
			}
		}

		/// <summary>
		/// Get ShowDevToolsCommand Command to display chromiums web browser.
		/// </summary>
		public ICommand ShowDevToolsCommand
		{
			get
			{
				if (this.mShowDevToolsCommand == null)
				{
					this.mShowDevToolsCommand = new RelayCommand<object>((p) =>
					{
						var chromBrowser = p as ChromiumWebBrowser;

						if (chromBrowser != null)
							chromBrowser.ShowDevTools();
					});
				}

				return this.mShowDevToolsCommand;
			}
		}
		#endregion properties

		#region methods
		#endregion methods
	}
}
