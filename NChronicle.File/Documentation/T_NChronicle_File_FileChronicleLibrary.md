# FileChronicleLibrary Class
 

A IChronicleLibrary writing ChronicleRecords to a file.


## Inheritance Hierarchy
<a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">System.Object</a><br />&nbsp;&nbsp;NChronicle.File.FileChronicleLibrary<br />
**Namespace:**&nbsp;<a href="N_NChronicle_File.md">NChronicle.File</a><br />**Assembly:**&nbsp;NChronicle.File (in NChronicle.File.dll) Version: 1.0.1.0 (1.0.1.0)

## Syntax

**C#**<br />
``` C#
public class FileChronicleLibrary : IChronicleLibrary, 
	IXmlSerializable, IDisposable
```

**VB**<br />
``` VB
Public Class FileChronicleLibrary
	Implements IChronicleLibrary, IXmlSerializable, IDisposable
```

**F#**<br />
``` F#
type FileChronicleLibrary =  
    class
        interface IChronicleLibrary
        interface IXmlSerializable
        interface IDisposable
    end
```

<br />
The FileChronicleLibrary type exposes the following members.


## Constructors
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_FileChronicleLibrary__ctor.md">FileChronicleLibrary</a></td><td>
Create a new FileChronicleLibrary instance with the default configuration.</td></tr></table>&nbsp;
<a href="#filechroniclelibrary-class">Back to Top</a>

## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_FileChronicleLibrary_Configure.md">Configure</a></td><td>
Configure this FileChronicleLibrary with the specified options.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_FileChronicleLibrary_Dispose.md">Dispose</a></td><td>
Conclude and close this FileChronicleLibrary.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/bsc2ak47" target="_blank">Equals</a></td><td>
Determines whether the specified <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a> is equal to the current <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="M_NChronicle_File_FileChronicleLibrary_Finalize.md">Finalize</a></td><td>
The destructor for this FileChronicleLibrary. Calls <a href="M_NChronicle_File_FileChronicleLibrary_Dispose.md">Dispose()</a>.
 (Overrides <a href="http://msdn2.microsoft.com/en-us/library/4k87zsw7" target="_blank">Object.Finalize()</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/zdee4b3y" target="_blank">GetHashCode</a></td><td>
Serves as a hash function for a particular type.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_FileChronicleLibrary_GetSchema.md">GetSchema</a></td><td>
Required for XML serialization, this method offers no functionality.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/dfwy45w9" target="_blank">GetType</a></td><td>
Gets the <a href="http://msdn2.microsoft.com/en-us/library/42892f65" target="_blank">Type</a> of the current instance.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/57ctke0a" target="_blank">MemberwiseClone</a></td><td>
Creates a shallow copy of the current <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_FileChronicleLibrary_ReadXml.md">ReadXml</a></td><td>
Populate configuration from XML via the specified <a href="http://msdn2.microsoft.com/en-us/library/b8a5e1s5" target="_blank">XmlReader</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_FileChronicleLibrary_Store.md">Store</a></td><td>
Render the record to the file (if not filtered by ChronicleLevel or tag ignorance).</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/7bxwbwt2" target="_blank">ToString</a></td><td>
Returns a string that represents the current object.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_File_FileChronicleLibrary_WriteXml.md">WriteXml</a></td><td>
Write configuration to XML via the specified <a href="http://msdn2.microsoft.com/en-us/library/5y8188ze" target="_blank">XmlWriter</a>.</td></tr></table>&nbsp;
<a href="#filechroniclelibrary-class">Back to Top</a>

## See Also


#### Reference
<a href="N_NChronicle_File.md">NChronicle.File Namespace</a><br />
