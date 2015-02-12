
1 Overview

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

   > The window should load a test page generated via
     SchemeHandler class as explained here:
     http://thechriskent.com/tag/ischemehandler/
   
   > The 2 buttons can be used to jump between 2 pages.

1.1 LocalZipSchemeHandler

A LocalZipSchemeHandler is a Cef SchemeHandler class that associates a scheme eg: 'local://'
in an URL like 'local://web.zip/index.html' with a local address like: 'C:\TEMP\web.zip\index.html'
where the 'index.html' file and all its resources (style, png etc.) are stored inside a zip file.

Please see
- http://thechriskent.com/2014/04/21/use-local-files-in-cefsharp/
- http://thechriskent.com/2014/08/18/embedded-chromium-in-winforms/

for more details.

2 Using the Code

- The constructor in App.xaml.cs contains

  CefSettings settings = new CefSettings();
  
  settings.RegisterScheme(new CefCustomScheme()
  {
    SchemeName = SchemeHandlerFactory.SchemeName,
    SchemeHandlerFactory = new SchemeHandlerFactory()
  });
  
  Cef.Initialize(settings);

statements to initialize the Cef sub-system. The CefSettings class is used to register
the custom scheme which is later on (see ViewModels/AppViewModel) used to display the
content as explained here: http://thechriskent.com/2014/04/21/use-local-files-in-cefsharp/.

- The MainWindow.xaml is constructed and displayed via the StartupUri="MainWindow.xaml"
  statement in the App.xaml
  
- The constructor of the MainWindow.xaml contains a constructor to create the
  AppViewModel and attach it to the DataContext of the MainWindow
  
- The binding statements in the MainWindow.xaml, eg:
   <cefSharp:ChromiumWebBrowser Address="{Binding BrowserAddress}"
                                Title="{Binding BrowserTitle, Mode=OneWay}" />

  bind into the AppViewModel and comunicate with it via binding.  

- The AppViewModel implements 2 commands:
  TestUrlCommand, TestUrl1Command

  to test browsing forth and back between 2 urls. You can also use the links
  provided in the html pages to test browsing via links in those pages.

2.1 The Model

2.1.1 The Zip Model

The Zip model is very simple and consists only of static extraction methods
that return a MemoryStream or string (or null if file was not found). The
memory stream method is used to extract, for example, the file 'index.html'
inside the web.zip file based on the URL: 'local://web.zip/index.html'.

2.2.1 The Cef Model

The Cef model consists of a 'LocalZipSchemeHandler' and a 'SchemeHandlerFactory'
class which are used to tell Cef what to do when it encounters a scheme/URL like 'local://'.

See previous examples for more details.

2.2 Code behind in MainWindow.xaml.cs

The constructor of the MainWindow.cs class creates the
AppViewModel instance and attaches it to the MainWindow's DataContext (for binding)
and the events: StatusMessage and NavStateChanged are registered with their corresponding
methods.
