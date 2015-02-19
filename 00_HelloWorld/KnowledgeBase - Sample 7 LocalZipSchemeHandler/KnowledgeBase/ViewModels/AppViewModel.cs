namespace KnowledgeBase.ViewModels
{
	using System.Reflection;
	using System.Windows.Input;
	using CefSharp.Wpf;
	using KnowledgeBase.ViewModels.Commands;

	/// <summary>
	/// Define an application specific interface for viewmodels that contain a
	/// <seealso cref="IWpfWebBrowser"/> interface.
	/// </summary>
	public interface IWebBrowser
	{
		IWpfWebBrowser WebBrowser { get; }
	}

	/// <summary>
	/// ApplicationViewModel manages the appplications state and its main objects.
	/// </summary>
	public class AppViewModel : Base.ViewModelBase, IWebBrowser
	{
		#region fields
		// This string holds a URL to the web.zip file in the local file system
		public string LocalZipUrl = "{0}/00_SampleData/web.zip";
		public string LocalZipUrl1 = "{0}/00_SampleData/CP_Articles.zip";
		public string LocalZipUrl2 = "{0}/00_SampleData/cef_binary_3.2171.1979_docs.zip";

		// These 2 Demo URLs are based on the local zip file path
		// but point to individual html pages inside the package
		public const string TestResourceUrl = "local://{0}::/index.html";

		public const string TestCefResourceUrl = "local://{0}::/projects/(default)/CefBrowser.html";

		private ICommand mTestUrlCommand = null;
		private ICommand mTestUrl1Command = null;
		private ICommand mTestUrl2Command = null;
		private ICommand mShowDevToolsCommand = null;

		private string mBrowserAddress;
		private IWpfWebBrowser mWebBrowser;
		private string mAssemblyTitle;
		private string mAssemblyPath;
		private string mSampleDataPath;
		private FindTextViewModel mFind;
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

			this.mSampleDataPath = System.IO.Directory.GetParent(this.mAssemblyPath).FullName;

			for (int i = 0; i < 4; i++)
				this.mSampleDataPath = System.IO.Directory.GetParent(this.mSampleDataPath).FullName;

			this.mSampleDataPath = this.mSampleDataPath.Replace(" ", "%20");
			this.mSampleDataPath = this.mSampleDataPath.Replace("\\", "/");

			this.LocalZipUrl = string.Format(LocalZipUrl, this.mSampleDataPath);
			this.LocalZipUrl1 = string.Format(LocalZipUrl1, this.mSampleDataPath);
			this.LocalZipUrl2 = string.Format(LocalZipUrl2, this.mSampleDataPath);

			this.BrowserAddress = string.Format(AppViewModel.TestCefResourceUrl, this.LocalZipUrl2);
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
		/// Gets/sets the web browser object bound from the ChromiumWebBrowser control.
		/// </summary>
		public IWpfWebBrowser WebBrowser
		{
			get
			{
				return this.mWebBrowser;
			}

			set
			{
				if (mWebBrowser != value)
				{
					this.mWebBrowser = value;
					this.RaisePropertyChanged(() => this.WebBrowser);
				}
			}
		}

		/// <summary>
		/// Gets a property that exposes all viewmodel related functions
		/// for finding and highlighting text.
		/// </summary>
		public FindTextViewModel Find
		{
			get
			{
				if (this.mFind == null)
					this.mFind = new FindTextViewModel(this);

				return this.mFind;
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
		/// Get test Command to browse to a test URL 2 ...
		/// </summary>
		public ICommand TestUrl2Command
		{
			get
			{
				if (this.mTestUrl2Command == null)
				{
					this.mTestUrl2Command = new RelayCommand(() =>
					{
						// Setting this address sets the current address of the browser
						// control via bound BrowserAddress property
						this.BrowserAddress = string.Format(AppViewModel.TestCefResourceUrl, this.LocalZipUrl2);
					});
				}

				return this.mTestUrl2Command;
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
