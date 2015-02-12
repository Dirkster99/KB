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
		public const string TestResourceUrl = "local://web.zip/index.html";
		public const string TestUnicodeResourceUrl = "local://web.zip/home.html";

		private ICommand mTestUrlCommand = null;
		private ICommand mTestUrl1Command = null;
		private ICommand mShowDevToolsCommand = null;

		private string mBrowserAddress;
		private string mAssemblyTitle;
		#endregion fields

		#region constructors
		/// <summary>
		/// Class Constructor
		/// </summary>
		public AppViewModel()
		{
			// Set title of assembly in viewmodel and reset first browser address
			this.mAssemblyTitle = Assembly.GetEntryAssembly().GetName().Name;

			this.BrowserAddress = AppViewModel.TestResourceUrl;
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
