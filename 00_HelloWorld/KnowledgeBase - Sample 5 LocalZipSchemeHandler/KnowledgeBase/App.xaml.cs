namespace KnowledgeBase
{
	using System.Windows;
	using CefSharp;
	using KnowledgeBase.Models.Cef;

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			CefSettings settings = new CefSettings();
			settings.RegisterScheme(new CefCustomScheme()
			{
				SchemeName = SchemeHandlerFactory.SchemeName,
				SchemeHandlerFactory = new SchemeHandlerFactory()
			});

			Cef.Initialize(settings);
		}
	}
}
