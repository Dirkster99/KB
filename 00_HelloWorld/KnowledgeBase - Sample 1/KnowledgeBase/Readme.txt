   Tutorial: http://www.philjeffes.co.uk/wordpress/?p=203
GitHub Site: https://github.com/cefsharp/CefSharp
 Nuget Site: http://www.nuget.org/packages/CefSharp.Wpf/

-----------------------------
- Start VS 2012 Express
- File > New Project > WPF Application (Name: KnowledgeBase)

- Solution Explorer > Solution (Context Menu) > Configuration Manager
  > Add x86 and x64 Solution and Remove 'Any CPU' Solution Platform
  > 

- Solution Explorer > Solution (Context Menu) > Nuget Manager
  > Select Online and Type CefSharp to install 'CefSharp.Wpf' for solution
- Close Visual Studio and Re-open (Re-start not just close solution)

- Check references section to make sure it contains a reference to:
  CefSharp.dll
  CefSharp.Core.dll
  CefSharp.Wpf.dll

- Go into MainWindow.xaml and add the following namespace:
  xmlns:cefSharp="clr-namespace:CefSharp.Wpf;assembly=CefSharp.Wpf"

  Add a line in the Grid section:
  <cefSharp:ChromiumWebBrowser Grid.Row="0" Address="http://edi.codeplex.com/documentation" />

- Build project and watch site poping up in the window
