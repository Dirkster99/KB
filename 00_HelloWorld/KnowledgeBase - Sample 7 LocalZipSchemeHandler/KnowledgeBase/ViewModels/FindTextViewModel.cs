namespace KnowledgeBase.ViewModels
{
	using System;
	using System.Windows.Input;
	using KnowledgeBase.ViewModels.Commands;

	/// <summary>
	/// Implements a viewmodel to find and highlight string in the currently viewed content.
	/// </summary>
	public class FindTextViewModel : Base.ViewModelBase
	{
		#region fields
		private bool mMatchCase = false;
		private string mText2Find;

		private IWebBrowser mBrowser;
		private ICommand mFindNextCommand;
		private ICommand mFindPreviousCommand;
		private ICommand mFindTextChangedCommand;
		#endregion fields

		#region constructor
		/// <summary>
		/// Class constructor
		/// </summary>
		public FindTextViewModel(IWebBrowser browser)
		{
			this.mBrowser = browser;
		}
		#endregion constructor

		#region properties
		/// <summary>
		/// Get/set a string to find in the currently viewed content.
		/// </summary>
		public string Text2Find
		{
			get
			{
				return this.mText2Find;
			}

			private set
			{
				if (this.mText2Find != value)
				{
					this.mText2Find = value;
					this.RaisePropertyChanged(() => this.Text2Find);
				}
			}
		}

		/// <summary>
		/// Gets a command that implements a Find Previous string function for the currently viewed content.
		/// </summary>
		public ICommand FindPreviousCommand
		{
			get
			{
				if (this.mFindPreviousCommand == null)
				{
					this.mFindPreviousCommand = new RelayCommand(() =>
					{
						this.Find(false);
					});
				}

				return this.mFindPreviousCommand;
			}
		}

		/// <summary>
		/// Gets a command that implements a Find Next string function for the currently viewed content.
		/// </summary>
		public ICommand FindNextCommand
		{
			get
			{
				if (this.mFindNextCommand == null)
				{
					this.mFindNextCommand = new RelayCommand(() =>
					{
							this.Find(true);
					});
				}

				return this.mFindNextCommand;
			}
		}

		/// <summary>
		/// Gets a command that executes code when the user has changed text in the search box.
		/// (The current search is extended by the newly typed text).
		/// </summary>
		public ICommand FindTextChangedCommand
		{
			get
			{
				if (this.mFindTextChangedCommand == null)
				{
					this.mFindTextChangedCommand = new RelayCommand<object>((p) =>
					{
						if (p != null)
						{
							var param = p as object[];

							// stop searching if we have no text to find
							if (param == null)
							{
								this.StopSearch();
								return;
							}

							//// bool isEditByUser = (bool)param[2];

							if (param[1] is KeyEventArgs)
							{
								switch ((param[1] as KeyEventArgs).Key)
								{
									case Key.Return:    // Highlight next occurrence in text if user hits enter key
									case Key.Right:
										this.Find(true);
									break;

									case Key.Left:    // Highlight previous occurrence in text if user hits CNTRL + key left
									this.Find(false);
									break;

									default:
										return;
								}
							}

							ExtendFindString(param[3] as string);
						}
					});
				}

				return this.mFindTextChangedCommand;
			}
		}
		#endregion properties

		#region methods
		private void Find(bool next)
		{
			if (this.mBrowser.WebBrowser != null)
			{
				if (!string.IsNullOrEmpty(this.Text2Find))
				{
					this.mBrowser.WebBrowser.Find(0, this.Text2Find, next, false, false);
				}
			}
		}

		/// <summary>
		/// Extends the currently searched string to find/highlight the indicated string.
		/// </summary>
		/// <param name="findText"></param>
		private void ExtendFindString(string findText)
		{
			// Don't search the same string twice
			if (string.Compare(this.Text2Find, findText, this.mMatchCase) == 0)
				return;

			this.Text2Find = findText;

			// stop searching if we have no text to find
			if (string.IsNullOrEmpty(findText) == true)
			{
				this.StopSearch();
				return;
			}

			if (this.mBrowser != null)
			{
				// Console.WriteLine("Finding Text: '{0}'", Text2Find);
				this.Find(true);
			}
		}

		/// <summary>
		/// Clear current search arguments and reset web browser find status.
		/// </summary>
		private void StopSearch()
		{
			this.Text2Find = string.Empty;

			if (this.mBrowser != null)
				this.mBrowser.WebBrowser.StopFinding(true);
		}
		#endregion methods
	}
}
