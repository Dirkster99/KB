namespace KnowledgeBase.Models
{
	using CefSharp;

	/// <summary>
	/// Source: http://thechriskent.com/tag/ischemehandler/
	/// </summary>
	public class ResourceSchemeHandlerFactory : ISchemeHandlerFactory
	{
		public ISchemeHandler Create()
		{
			return new ResourceSchemeHandler();
		}

		public static string SchemeName { get { return "resource"; } }
	}
}
