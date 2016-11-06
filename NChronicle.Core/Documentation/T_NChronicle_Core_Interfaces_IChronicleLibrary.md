# IChronicleLibrary Interface
 

An NChronicle Library, i.e. a destination for <a href="T_NChronicle_Core_Model_ChronicleRecord.md">ChronicleRecord</a>s.

**Namespace:**&nbsp;<a href="N_NChronicle_Core_Interfaces.md">NChronicle.Core.Interfaces</a><br />**Assembly:**&nbsp;NChronicle.Core (in NChronicle.Core.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public interface IChronicleLibrary : IXmlSerializable
```

**VB**<br />
``` VB
Public Interface IChronicleLibrary
	Inherits IXmlSerializable
```

**F#**<br />
``` F#
type IChronicleLibrary =  
    interface
        interface IXmlSerializable
    end
```

The IChronicleLibrary type exposes the following members.


## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/6f7z1347" target="_blank">GetSchema</a></td><td>
This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <a href="http://msdn2.microsoft.com/en-us/library/f7th40y8" target="_blank">XmlSchemaProviderAttribute</a> to the class.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/fhd7bk0a" target="_blank">IXmlSerializable</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/w6txf8t9" target="_blank">ReadXml</a></td><td>
Generates an object from its XML representation.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/fhd7bk0a" target="_blank">IXmlSerializable</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="M_NChronicle_Core_Interfaces_IChronicleLibrary_Store.md">Store</a></td><td>
Store the specified <a href="T_NChronicle_Core_Model_ChronicleRecord.md">ChronicleRecord</a> in this library.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/9yt8e1yw" target="_blank">WriteXml</a></td><td>
Converts an object into its XML representation.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/fhd7bk0a" target="_blank">IXmlSerializable</a>.)</td></tr></table>&nbsp;
<a href="#ichroniclelibrary-interface">Back to Top</a>

## See Also


#### Reference
<a href="N_NChronicle_Core_Interfaces.md">NChronicle.Core.Interfaces Namespace</a><br />
