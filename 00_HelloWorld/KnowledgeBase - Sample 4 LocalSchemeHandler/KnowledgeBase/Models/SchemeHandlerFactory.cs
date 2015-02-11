namespace KnowledgeBase.Models
{
	using CefSharp;

	/// <summary>
	/// Source:
	/// http://thechriskent.com/2014/04/21/use-local-files-in-cefsharp/
	/// http://thechriskent.com/2014/08/18/embedded-chromium-in-winforms/
	/// 
	/// </summary>
	public class SchemeHandlerFactory : ISchemeHandlerFactory
	{
		public ISchemeHandler Create()
		{
			return new LocalSchemeHandler();
		}

		public static string SchemeName { get { return "local"; } }
	}
}
