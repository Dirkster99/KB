# KB GitHub Repository
The KnowledgeBase project.

## 00_HelloWorld Sub-Folder

The solutions in the 00_HelloWorld Sub-Folder show step by step introductory
WPF sample applications that should help understanding and using CefSharp
within WPF. The samples are very simple on a 101 level to make sure everyone
can get most out of it. Please be sure to read the **Readme.txt** files in each
sub-folder to understand more details about each sample.

### KnowledgeBase - Sample 1
Description: This is a minimal bare bones XAML+Nuget demo of the CefSharp project.

### KnowledgeBase - Sample 2 ResourceHandler
Description: This is a sample ResourceHandler implementation of the CefSharp WPF control based on the Nuget libraries.

### KnowledgeBase - Sample 3 ResourceSchemeHandler

Description: This is a sample SchemeHandler implementation that loads its content from an embeded resource. It applies the CefSharp WPF control based on the Nuget libraries.

### KnowledgeBase - Sample 4 LocalSchemeHandler

Description: This is a sample SchemeHandler implementation that loads its content from the local file system. It applies the CefSharp WPF control based on the Nuget libraries.

### KnowledgeBase - Sample 5 LocalZipSchemeHandler

Description: This is a sample SchemeHandler implementation that loads its content from a zip file stored in the local file system. It applies the CefSharp WPF control based on the Nuget libraries.

### KnowledgeBase - Sample 6 LocalZipSchemeHandler

Description: This is a sample SchemeHandler implementation that loads its content from MULTIPLE zip files stored in the local file system. It applies the CefSharp WPF control based on the Nuget libraries.

### KnowledgeBase - Sample 7 LocalZipSchemeHandler

Description: This is a sample SchemeHandler implementation that loads its content from MULTIPLE zip files stored in the local file system. It applies the CefSharp WPF control based on the Nuget libraries. This sample implementation supports advanced features, such as:
- Go back in browser history
- Select All, Copy
- Zoom In/Zoom Out/Reset Zoom
- Stop/Reload
- Find Next, Find Previous, highlight text based on a search string

- Find Highlight text as you type
- Download files (eg.: sample projects) stored inside a zip file

## KnowledgeBase - Sample 8 ExtractText

Description: This sample illustrates how the OffScreenBrowser can be used to extract text from page that is available at a given URL.

## References

### CefSharp on GitHub

- [https://github.com/cefsharp/CefSharp](https://github.com/cefsharp/CefSharp "https://github.com/cefsharp/CefSharp")
- [https://github.com/cefsharp/CefSharp.MinimalExample](https://github.com/cefsharp/CefSharp.MinimalExample "https://github.com/cefsharp/CefSharp.MinimalExample")

### Helpful Blog Posts by Chris Kent

- [http://thechriskent.com/tag/ischemehandler/](http://thechriskent.com/tag/ischemehandler/ "http://thechriskent.com/tag/ischemehandler/")
- [http://thechriskent.com/2014/04/21/use-local-files-in-cefsharp/](http://thechriskent.com/2014/04/21/use-local-files-in-cefsharp/ "http://thechriskent.com/2014/04/21/use-local-files-in-cefsharp/")
- [http://thechriskent.com/2014/08/18/embedded-chromium-in-winforms/](http://thechriskent.com/2014/08/18/embedded-chromium-in-winforms/ "http://thechriskent.com/2014/08/18/embedded-chromium-in-winforms/")

### Tip

- Add an alternative (preview nuget) package source via
  VS 2013>Tools>Nuget Package Manager>Package Manager Settings

  Nuget Package > Package Manager Sources (click Plus icon on top right)
  Name: CefSharp Preview MyGet.Org
  Source: https://www.myget.org/F/cefsharp/

  Select the:
              'Include Prerelease' drop down option in the
              'Manage Nuget Packages' window when downloading package in Online section