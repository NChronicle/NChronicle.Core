# ConsoleChronicleLibrary Class
 

A IChronicleLibrary writing ChronicleRecords to the Console.


## Inheritance Hierarchy
<a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">System.Object</a><br />&nbsp;&nbsp;NChronicle.Console.ConsoleChronicleLibrary<br />
**Namespace:**&nbsp;<a href="N_NChronicle_Console.md">NChronicle.Console</a><br />**Assembly:**&nbsp;NChronicle.Console (in NChronicle.Console.dll) Version: 1.0.2.0 (1.0.2.0)

## Syntax

**C#**<br />
``` C#
public class ConsoleChronicleLibrary : IChronicleLibrary, 
	IXmlSerializable
```

**VB**<br />
``` VB
Public Class ConsoleChronicleLibrary
	Implements IChronicleLibrary, IXmlSerializable
```

**F#**<br />
``` F#
type ConsoleChronicleLibrary =  
    class
        interface IChronicleLibrary
        interface IXmlSerializable
    end
```

The ConsoleChronicleLibrary type exposes the following members.


## Constructors
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_ConsoleChronicleLibrary__ctor.md">ConsoleChronicleLibrary</a></td><td>
Create a new ConsoleChronicleLibrary instance with the default configuration.</td></tr></table>&nbsp;
<a href="#consolechroniclelibrary-class">Back to Top</a>

## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_ConsoleChronicleLibrary_Configure.md">Configure</a></td><td>
Configure this ConsoleChronicleLibrary with the specified options.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/bsc2ak47" target="_blank">Equals</a></td><td>
Determines whether the specified <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a> is equal to the current <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/4k87zsw7" target="_blank">Finalize</a></td><td>
Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/zdee4b3y" target="_blank">GetHashCode</a></td><td>
Serves as a hash function for a particular type.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_ConsoleChronicleLibrary_GetSchema.md">GetSchema</a></td><td>
Required for XML serialization, this method offers no functionality.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/dfwy45w9" target="_blank">GetType</a></td><td>
Gets the <a href="http://msdn2.microsoft.com/en-us/library/42892f65" target="_blank">Type</a> of the current instance.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/57ctke0a" target="_blank">MemberwiseClone</a></td><td>
Creates a shallow copy of the current <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_ConsoleChronicleLibrary_ReadXml.md">ReadXml</a></td><td>
Populate configuration from XML via the specified <a href="http://msdn2.microsoft.com/en-us/library/b8a5e1s5" target="_blank">XmlReader</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_ConsoleChronicleLibrary_Store.md">Store</a></td><td>
Render the record to the console (if not filtered by ChronicleLevel or tag ignorance).</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/7bxwbwt2" target="_blank">ToString</a></td><td>
Returns a string that represents the current object.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Console_ConsoleChronicleLibrary_WriteXml.md">WriteXml</a></td><td>
Write configuration to XML via the specified <a href="http://msdn2.microsoft.com/en-us/library/5y8188ze" target="_blank">XmlWriter</a>.</td></tr></table>&nbsp;
<a href="#consolechroniclelibrary-class">Back to Top</a>

## See Also


#### Reference
<a href="N_NChronicle_Console.md">NChronicle.Console Namespace</a><br />
