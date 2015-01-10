# space-engineers-doc-collab
This is a collaborative Space Engineers in-game API documentation project.
Our goal is to accurately document the in-game API's features to make development easier for everyone.
This repository contains a decompiled, edited copy of the Sandbox.Common.dll.

**The latest documentation build is available from this thread:** http://forums.keenswh.com/post/file-programmable-block-api-documentation-generated-7224725

## Editing the project
First of all, you'll need to fork or download this repository.
You can help by removing elements which cannot be accessed from the programmable block and by adding XML summaries above fields, properties, methods, interfaces, etc. which explain usage.
Example summary:
```c#
/// <summary>
/// Transfers items from this inventory to the target inventory.
/// </summary>
/// <param name="dst">Target inventory</param>
/// <param name="sourceItemIndex">The target item's index in this inventory</param>
/// <param name="targetItemIndex">The index at which the item will be placed in the target inventory</param>
/// <param name="stackIfPossible">Add the items to an existing stack of items instead of creating a new stack</param>
/// <param name="amount">Amount of items to transfer</param>
/// <returns>A boolean value depicting whether the transfer was successful*</returns>
bool TransferItemTo(IMyInventory dst, int sourceItemIndex, int? targetItemIndex = null, bool? stackIfPossible = null, MyFixedPoint? amount = null);
```
<sup>*Does not actually depict the success of the transfer - text only included for example</sup>

Once you've done your edits, create a pull request if you've forked the project, or send a PM to Cuber to have the edits reviewed. Thank you!
## Utilizing the project
The latest documentation build can always be found from this KeenSWH forum thread. The documentation is a compilation of HTML files, viewable on any device with a web browser and local file access.

**[Preview of the documentation [imgur.com]](http://i.imgur.com/2TZxrJq.png)**

This project can also be compiled to produce the XML file, which will allow your IDE to show the documentation with its autocomplete feature.
For example, the summary above will show like this in Visual Studio (click to view full size):

![](http://i.imgur.com/hWPSJcB.png)

In Visual Studio, the XML documentation generation can be enabled like so: Project tab -> Sandbox.Common Properties -> Build tab -> Output section -> Tick "XML documentation file"


Space Engineers® is trademark of KEEN SWH LTD.
