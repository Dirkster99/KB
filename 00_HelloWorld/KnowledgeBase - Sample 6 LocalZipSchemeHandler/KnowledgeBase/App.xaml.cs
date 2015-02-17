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
				SchemeHandlerFactory = new SchemeHandlerFactory(),
				
				// Set this to false to make sure Cef does not re-write the URL
				// 
				// See also:
				// http://code.google.com/p/chromiumembedded/
				// http://magpcss.org/ceforum/apidocs3/
				// http://magpcss.org/ceforum/apidocs3/projects/%28default%29/CefSchemeRegistrar.html#AddCustomScheme%28constCefString&,bool,bool,bool%29
				IsStandard = false
			});

			Cef.Initialize(settings);
		}
	}
}
