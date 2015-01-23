# space-engineers-doc-collab
### Current version: 01.066.015 23.1.2015
This is a collaborative Space Engineers in-game API documentation project.
Our goal is to accurately document the in-game API's features to make development easier for everyone - online documentation is generated with Sandcastle Help File Builder.

# Contributing to the project
First of all, you'll need to fork, clone or download this repository.
You can help by adding XML summaries above fields, properties, methods, interfaces, etc. which explain usage.
Example summary:
```c#
// ModAPI\Interfaces\IMyInventory.cs

/// <summary>
/// Transfers items from the inventory to the target inventory.
/// </summary>
/// <param name="dst">Target inventory</param>
/// <param name="sourceItemIndex">Index of the item being transferred in the source inventory</param>
/// <param name="targetItemIndex">Index to which the item will be placed in the target inventory</param>
/// <param name="stackIfPossible"></param>
/// <param name="amount">Amount of items to transfer</param>
/// <returns></returns>
/// <remarks>
/// 	<note type="caution">When using this method in a loop, the item indexes will change as the inventory automatically fills the empty inventory spaces left by item transfers. It is thus recommended to set <paramref name="sourceItemIndex"/> to zero when iterating over every element in the inventory.</note>
/// </remarks>
/// <example>
/// 	The following example demonstrates the <c>TransferItemTo</c> method.
/// 	<code source="Examples\Interfaces.IMyInventory.TransferItemTo.cs" lang="cs"></code>
/// </example>
bool TransferItemTo(IMyInventory dst, int sourceItemIndex, int? targetItemIndex = null, bool? stackIfPossible = null, MyFixedPoint? amount = null);
```
The example code snippet can also be placed inside the` <code>` tags, in the case of which the source attribute would be left out. The above will also include a code example from the file `Interfaces.IMyInventory.TransferItemTo.cs`. If using a relative path, the Sandcastle Help File Builder will read them relative to the `.shfbproj` project file's location, hence the `Examples` folder can be found from the `Sandcastle Files` folder in this repository.

Please see the **XML Comments References** in the *Links*-section for a comprehensive list of supported documentation XML tags.
<hr>
If something cannot be accessed from the programmable block, please add the following summary above it:
```c#
/// <summary>
/// Inaccessible
/// </summary>
```

Once you've done your edits, create a pull request or [send a PM to Cuber](http://forums.keenswh.com/pm?userid=3302466) to have the edits reviewed. Thank you!
# Utilizing the project
The latest documentation build can always be found from this KeenSWH forum thread. The documentation is a compilation of HTML files, viewable on any device with a web browser and local file access.

**[Preview of the documentation <sup>[imgur.com]</sup>](http://i.imgur.com/2TZxrJq.png)**

This project can also be compiled to produce the XML file, which will allow your IDE to show the documentation with its autocomplete feature.
For example, the summary above will show like this in Visual Studio (click to view full size):

![](http://i.imgur.com/hWPSJcB.png)

In Visual Studio, the XML documentation generation can be enabled like so: Project tab -> Sandbox.Common Properties -> Build tab -> Output section -> Tick "XML documentation file"

# Links
* [**Download** a local copy here!](https://github.com/jCuber/space-engineers-doc-collab/tree/gh-pages)
* [**Sandcastle Help File Builder**](https://shfb.codeplex.com/)
* [Related KeenSWH forum thread](http://forums.keenswh.com/post?id=7224725)

##### XML Comments References
* [Sandcastle XML Comments Guide](http://www.ewoodruff.us/xmlcommentsguide/html/4268757F-CE8D-4E6D-8502-4F7F2E22DDA3.htm)
* [Recommended Tags for Documentation Comments (C# Programming Guide)](http://msdn.microsoft.com/en-us/library/5ast78ax.aspx)
* [A Reference to Sandcastle’s XML Documentation Comments](http://www.red-gate.com/assets/products/dotnet-development/ants-performance-profiler/entrypage/assets/pdfs/sandcastle-wallchart.pdf)

<hr>
*Space Engineers® is trademark of KEEN SWH LTD.*
