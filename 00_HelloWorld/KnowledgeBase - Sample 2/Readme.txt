This is a Hello World! Type CefSharp sample that should work with
NuGet CefSharp Version 37.0.0.

The following steps are necessary to get this to work
(Tested with Visual Studio 2013 Express):

1> Load Solution in Visual Studio
2> Enable Nuget by clicking on the Solution root via
   ContextMenu>Enable NuGet Package Restore

3> Rebuild Solution
   
4> Close and Re>Open Visual Studio (not just the solution)
   You will otherwise not see the References to CefSharp and get corresponding errors

5> Clean Solution
   Rebuild and Execute solution (should go without error)

   > The window should load a test page generated via ResourceHandler
   
   > The 2 buttons can be used to jump between 2 pages.
   
   > Problem:
     Each Url can be used at most once.
	 The Output (TW) in VS Studio shows an exception in mscorelib
	 
	 > Switch catch exception on in debugger and the êxception reads something like:
	 DataStream was already closed...???