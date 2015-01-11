# space-engineers-doc-collab
This is a collaborative Space Engineers in-game API documentation project.
Our goal is to accurately document the in-game API's features to make development easier for everyone.
This repository contains a decompiled, edited copy of the Sandbox.Common.dll.

# Editing the project
First of all, you'll need to fork or download this repository.
You can help by adding XML summaries above fields, properties, methods, interfaces, etc. which explain usage.
Example summary:
```c#
// ModAPI\Interfaces\IMyInventory.cs

/// <summary>
/// Transfers items from this inventory to the target inventory.
/// </summary>
/// <include file='/IMyInventory.xml' path='Documentation/Member[@name="TransferItemsTo"]/*'/>
bool TransferItemTo(IMyInventory dst, int sourceItemIndex, int? targetItemIndex = null, bool? stackIfPossible = null, MyFixedPoint? amount = null);
```
I am utilizing the [`<include>` tag](http://msdn.microsoft.com/en-us/library/9h8dy30z.aspx) to allow for a less cluttered source file. This specific include will grab the contents of the `<Member>` tag with the name "TransferItemsTo".
```xml
<!-- ModAPI\Interfaces\IMyInventory.xml -->
<?xml version="1.0" encoding="utf-8" ?>
<Documentation>
	<Member name="TransferItemsTo">
		<param name="dst">Target inventory</param>
		<param name="sourceItemIndex">The target item's index in this inventory</param>
		<param name="targetItemIndex">The index at which the item will be placed in the target inventory</param>
		<param name="stackIfPossible"></param>
		<param name="amount">Amount of items to transfer</param>
		<returns></returns>
		<example>
			The following example demonstrates the TransferItemTo method.
			<code source="Examples\Interfaces.IMyInventory.TransferItemsTo.cs" lang="cs"></code>
		</example>
	</Member>
</Documentation>
```
The example code snippet can also be placed inside the` <code>` tags, in the case of which the source attribute would be left out.
When documenting a member without previous documentation, please add a new `<Member>` section with the method's name to the .xml file and include it in the class' .cs file. The above will also include a code example from the file `Interfaces.IMyInventory.TransferItemsTo.cs`. If using a relative path, the Sandcastle Help File Builder will read them relative to the `.shfbproj` project file's location, hence the `Examples` folder can be found from the `Sandcastle Files` folder.

A list of documentation comment tags can be found [here](http://msdn.microsoft.com/en-us/library/5ast78ax.aspx).

If something cannot be accessed from the programmable block, please add the following summary above it:
```c#
/// <summary>
/// Inaccessible
/// </summary>
```

Once you've done your edits, create a pull request if you've forked the project, or [send a PM to Cuber](http://forums.keenswh.com/pm?userid=3302466) to have the edits reviewed. Thank you!
# Utilizing the project
The latest documentation build can always be found from this KeenSWH forum thread. The documentation is a compilation of HTML files, viewable on any device with a web browser and local file access.

**[Preview of the documentation <sup>[imgur.com]</sup>](http://i.imgur.com/2TZxrJq.png)**

This project can also be compiled to produce the XML file, which will allow your IDE to show the documentation with its autocomplete feature.
For example, the summary above will show like this in Visual Studio (click to view full size):

![](http://i.imgur.com/hWPSJcB.png)

In Visual Studio, the XML documentation generation can be enabled like so: Project tab -> Sandbox.Common Properties -> Build tab -> Output section -> Tick "XML documentation file"

# Links
* [**Download** a local copy here!](https://github.com/jCuber/space-engineers-doc-collab/tree/gh-pages)
* [Related KeenSWH forum thread](http://forums.keenswh.com/post?id=7224725)

*Space Engineers® is trademark of KEEN SWH LTD.*
