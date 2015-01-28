namespace KnowledgeBase
{
	using System;
	using System.Windows;
	using CefSharp;

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			Cef.Initialize();

			this.InitializeComponent();

			this.Initialized += MainWindow_Initialized;
		}

		void MainWindow_Initialized(object sender, EventArgs e)
		{
			this.browser.Load("dummy:");
			this.browser.LoadHtml("<HTML><BODY><H1>Test</H1></BODY></HTML>", "dummy");
		}
	}
}
